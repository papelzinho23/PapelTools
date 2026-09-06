# 4RTools — Arquitetura (mapa do código)

> Documento de referência para desenvolvimento. Baseado no estado do repositório em `dev/otimizacoes` (pós-v2.10.0).

## Visão geral

4RTools é um assistente de automação para clients de **Ragnarök Online** (WinForms, .NET Framework 4.7.2).
Ele roda em paralelo ao jogo e:

1. **Lê a memória** do processo do client (HP/SP/nome do personagem, tabela de status/buffs);
2. **Envia input** para a janela do jogo via `PostMessage` / `mouse_event` / `keybd_event`;
3. Reage a **hotkeys globais** (low-level keyboard hook) para ligar/desligar e disparar macros.

Empacotamento: `Costura.Fody` embute todas as DLLs num único `4RTools.exe`. Requer execução **como administrador** (`app.manifest`) para abrir o handle do processo do jogo.

## Fluxo de inicialização

```
Program.Main()
  └─ Forms/AutoPatcher  (tela inicial: aplica patches no client, baixa updates)
        └─ (usuário avança) ─> Forms/Container   ← janela MDI principal
```

`Forms/Container.cs` é o coração da UI:
- É um `Form` **e** um `IObserver`, e possui um `Subject subject` próprio.
- No construtor, instancia **um form por aba** (`SetAutopotWindow()`, `SetAHKWindow()`, …) e injeta o mesmo `subject` em cada um.
- `Container_Load` cria/carrega o profile `"Default"` e popula os combos de processo e de profile.

## Padrão Observer (`Utils/RObserver.cs`)

Toda a comunicação entre a UI e os módulos passa por aqui.

| Peça | Papel |
|---|---|
| `interface ISubject` / `class Subject` | mantém `List<IObserver>`, faz `Notify(Message)` |
| `interface IObserver` | `void Update(ISubject subject)` — lê `(subject as Subject).Message` |
| `class Message` | `{ MessageCode code; object data; }` |
| `enum MessageCode` | `PROCESS_CHANGED`, `PROFILE_CHANGED`, `PROFILE_INPUT_CHANGE`, `TURN_ON`, `TURN_OFF`, `SHUTDOWN_APPLICATION`, `CLICK_ICON_TRAY`, `SERVER_LIST_CHANGED` |

Fluxo típico: usuário troca o processo no combo → `Container.processCB_SelectedIndexChanged` cria um novo `Client`, seta `ClientSingleton`, e faz `subject.Notify(PROCESS_CHANGED)` → cada form-observer reage (ex.: relê o nome do personagem, revalida config).

## Camada de "Client" (`Model/Client.cs`)

- `Client` encapsula o `System.Diagnostics.Process` do jogo + um `ProcessMemoryReader`.
- `ClientSingleton` — instância "ativa" atualmente selecionada (`GetClient()`).
- `ClientListSingleton` — catálogo de clients suportados (carregado de `supported_servers.json`, remoto + local).
- `ClientDTO` — forma serializável (nome do processo, `hpAddress`, `nameAddress` em hex).

Leitura de memória (offsets a partir de `currentHPBaseAddress`):

| Dado | Endereço |
|---|---|
| HP atual | `base + 0` |
| HP máx | `base + 4` |
| SP atual | `base + 8` |
| SP máx | `base + 12` |
| Nome do personagem | `currentNameAddress` (string, até 40 bytes) |
| Tabela de status/buffs | `base + 0x474`, entradas de 4 bytes: `CurrentBuffStatusCode(i)` |

`IsHpBelow(percent)` / `IsSpBelow(percent)` comparam sem divisão (`cur*100 < percent*max`).

`Utils/ProcessMemoryReader.cs` — P/Invokes de `kernel32`: `OpenProcess`, `ReadProcessMemory`, `WriteProcessMemory`, `VirtualAllocEx/FreeEx`. Abre com `VM_OPERATION | VM_READ | VM_WRITE`.

## Módulos de automação (`Model/`, implementam `interface Action`)

`interface Action { Start(); Stop(); string GetConfiguration(); string GetActionName(); }`

| Classe | O que faz | Loop |
|---|---|---|
| `Autopot` | usa poção de HP/SP quando abaixo do % configurado | `_4RThread` + `Sleep(delay)` |
| `AutoBuff` | varre a tabela de status; reaplica buffs (item/skill) que sumiram; trata Quagmire/Overthrust Max | `_4RThread` + `Sleep(300)` |
| `StatusRecovery` / `DebuffsRecovery` | cura Poison/Silence/Blind/Curse/etc. com Panacéia/Green Pot/Royal Jelly | `_4RThread` |
| `AHK` | spam de skill/click enquanto a tecla está pressionada; modos `Compatibility` e `SpeedBoost`; `mouseFlick` | `_4RThread` + `while(Keyboard.IsKeyDown)` interno |
| `AutoRefreshSpammer` (x3) | macro de "refresh" (spam de tecla) | `_4RThread` |
| `Macro` (SongMacro, MacroSwitch) | sequências/lanes de macros (bardo, troca de macro) | `_4RThread` |
| `ATKDEFMode` | alterna dois conjuntos de teclas (ataque/defesa) | evento de hotkey |
| `UserPreferences` | guarda `toggleStateKey` (tecla ON/OFF, default `End`) | — |

Envio de input pro jogo:
- `Interop.PostMessage(hwnd, WM_KEYDOWN/WM_KEYUP, Keys, 0)` — teclado
- `Interop.mouse_event` / `AHK.mouse_event` / `AHK.keybd_event` — mouse/tecla a nível de sistema (SpeedBoost)
- Guarda comum: só age se **Alt não está pressionado** (`Keyboard.IsKeyDown(Key.LeftAlt/RightAlt)`), pra não atrapalhar Alt+Tab / atalhos do jogo.

## Threading (`Utils/_4RThread.cs`)

```csharp
new Thread(() => { while (true) { try { toRun(0); } catch {…} finally { Thread.Sleep(5); } } })
  .SetApartmentState(STA)
```

- `_4RThread.Start(t)` → `t.thread.Start()`
- `_4RThread.Stop(t)` → **`t.thread.Suspend()`** ⚠️ (API obsoleta, ver BACKLOG #1)
- Cada módulo cria/para o seu próprio thread em `Start()`/`Stop()`.

## Hotkeys globais (`Utils/KeyboardHook.cs`)

- `WH_KEYBOARD_LL` via `SetWindowsHookEx`. Estático.
- Rastreia modificadores (`Control/Shift/Alt/Win`).
- `AddKeyDown(Keys, KeyPressed)` / `AddKeyUp(...)` registram callbacks; o callback retorna `bool` (true = deixa passar, false = engole a tecla).
- `Enable()` / `Disable()` — ligado enquanto o `Container` está aberto.

## Profiles (`Model/Profile.cs`)

- `Profile` = agregado com uma instância de cada módulo (`Autopot`, `AHK`, `AutoBuff`, …).
- Persistência: **um `.json` por profile** em `Profile/` (`AppConfig.ProfileFolder`).
- Formato: objeto com uma chave por `GetActionName()`, cada valor é o JSON serializado (string) daquele módulo (`ProfileSingleton.SetConfiguration` / `GetByAction`).
- `ProfileSingleton.profile` — profile ativo. `Load/Create/Delete/Rename/Copy`.
- Serialização: `Newtonsoft.Json`. `Key` (de `System.Windows.Input`, WPF via `PresentationCore`) é serializado como enum.

## Servidores suportados (`Model/LocalServerManager.cs`, `supported_servers.json`)

- Lista remota: `AppConfig._4RClientsURL` (Google Storage). Lista local: `supported_servers.json` ao lado do exe.
- Cada entrada = `{ name (nome do processo), hpAddress, nameAddress }` em hex de 8 dígitos.
- `Forms/AddServerForm` + `ServersForm` deixam o usuário cadastrar offsets de um servidor novo.
- **É aqui que servidores de fã são adicionados** — o par de offsets `hpAddress`/`nameAddress` é específico do executável do client.

## Rede / telemetria

| Item | Endpoint |
|---|---|
| `Model/Tracker.cs` | `POST {_4RApiHost}/analytics/public/sendMetrics` (fire-and-forget) |
| `Model/Advertiser.cs` | `_4RAdvertiserUrl` (banners — já removido do fluxo em `0d592e4`) |
| Checagem de update | `_4RLatestVersionURL` (GitHub releases API) |
| Lista de servidores | `_4RClientsURL` |

## Pastas

```
Forms/     ~15 telas WinForms (cada uma: .cs + .Designer.cs + .resx)
Model/     módulos de automação + Client/Profile/domínio
Utils/     infra: memória, hook de teclado, interop, threads, observer, config, constants
Resources/ ícones e imagens
assets/    imagens do README
ILLink/    config de trimming
```

## Dependências (packages.config)

| Pacote | Uso |
|---|---|
| `Newtonsoft.Json` 13 | serialização de profiles/DTOs |
| `Costura.Fody` 5.7 + `Fody` 6.5 | merge de DLLs no exe |
| `Aspose.Zip` 22.10 | descompactar patches no AutoPatcher |
| `System.*` 4.3 (net472 shims) | polyfills |
