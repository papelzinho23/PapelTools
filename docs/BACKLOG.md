# Backlog — otimizações e correções

Prioridade: 🔴 alta · 🟡 média · 🟢 baixa/nice-to-have
Tipo: 🐛 bug · ⚡ performance · 🧹 dívida técnica · ✨ feature

---

## 🔴 🐛 1. `_4RThread.Stop()` usa `Thread.Suspend()`

`Utils/_4RThread.cs:46`. `Thread.Suspend/Resume` são obsoletos e perigosos: se a thread for
suspensa segurando um lock (ex.: dentro do `ReadProcessMemory` ou de um `PostMessage`),
o processo pode travar. Além disso o thread **nunca termina** — `while(true)` sem saída;
"parar" só congela. Trocar por:

- loop controlado por `CancellationToken` (`while(!token.IsCancellationRequested)`);
- `Stop()` sinaliza o cancelamento e dá `Join(timeout)`;
- descartar a instância e criar uma nova no próximo `Start()`.

Impacto: estabilidade. Toca todos os módulos (todos chamam `_4RThread.Stop`).

## 🔴 🐛 2. `Autopot` — contador de poção não acumula

`Model/Autopot.cs:53-60`. `hpPotCount` é uma variável local capturada **por valor** no
lambda `_ => AutopotThreadExecution(roClient, hpPotCount)`; a cada iteração entra como `0`,
o `hpPotCount++` interno é descartado. A regra "a cada 3 poções de HP, checa SP" nunca
dispara. Corrigir promovendo o contador a campo da classe (ou a um closure real).

## 🔴 ⚡ 3. N threads em busy-loop com `Sleep(5)`

`Utils/_4RThread.cs:28` + cada módulo cria o seu. Com Autopot + AutoBuff + 3 spammers +
macros ativos são ~7 threads acordando a cada 5 ms. Custa CPU e causa jitter na leitura de
memória. Opções:
- consolidar num **scheduler único** (uma thread, lista de tarefas com período próprio);
- ou no mínimo respeitar o `delay` de cada módulo e remover o `Sleep(5)` fixo do wrapper.

## 🟡 🐛 4. Sem tratamento quando o processo do jogo morre

`Model/Client.cs` / `ProcessMemoryReader.cs`. Se o client fecha durante a operação,
`ReadProcessMemory` passa a ler lixo/zero e o handle fica órfão (`CloseHandle` nunca é
chamado no fluxo normal — não há `Dispose`). Adicionar:
- `Client : IDisposable`, fechar handle;
- detectar `process.HasExited` no loop e emitir `PROCESS_CHANGED`/`TURN_OFF`;
- `ProcessMemoryReader` checar o retorno de `ReadProcessMemory` (hoje ignorado).

## 🟡 🧹 5. `Tracker` cria um `HttpClient` por instância

`Model/Tracker.cs:45`. É singleton, então na prática é 1 — mas o padrão correto é um
`static readonly HttpClient`. Também: `async void` engole exceções; trocar por `Task` e
`fire-and-forget` explícito com log.

## 🟡 🧹 6. `catch { }` silenciosos

Vários pontos (`Profile.ListAll`, `LocalServerManager.RemoveClient/Copy`,
`KeyboardHook.Enable/Disable`, `Client` ctor). Engolem erro sem log. Padronizar em
`catch (Exception ex) { Log.Warn(...) }` com um logger simples (ver #9).

## 🟡 ⚡ 7. `AutoBuff` clona o dicionário a cada iteração

`Model/Autobuff.cs:37` — `new Dictionary<>(this.buffMapping)` 3x/segundo. Barato hoje
(mapa pequeno), mas dá pra manter um `HashSet` de "buffs presentes" reaproveitado e só
diffar. Fazer junto com #3.

## 🟡 🧹 8. `Enum.Parse(typeof(Keys), key.ToString())` em loop quente

`Autopot.pot`, `AHK`, `AutoBuff.useAutobuff` convertem `System.Windows.Input.Key` →
`System.Windows.Forms.Keys` via string toda hora. Fazer um mapa `static` calculado uma
vez, ou padronizar o app num único tipo de tecla.

## 🟢 🧹 9. Logging

Não há logger — só `Console.WriteLine` (invisível num WinExe). Adicionar um logger leve
(arquivo rotativo em `%APPDATA%/4RTools/logs`), útil pra suporte no Discord.

## 🟢 🧹 10. `AppConfig` com campos `static` mutáveis públicos

`Utils/AppConfig.cs` — deveria ser `const`/`static readonly`. `Version` deveria vir de
`Assembly` (hoje duplica com `Properties/AssemblyInfo` e o `.csproj`).

## 🟢 🧹 11. Modernizar o projeto

- Migrar `4RTools.csproj` para **SDK-style** (`<Project Sdk="Microsoft.NET.Sdk">`),
  `packages.config` → `<PackageReference>`.
- Avaliar alvo `net48` (targeting pack menor) ou `net8.0-windows` (WinForms moderno,
  build só com `dotnet`, `PublishSingleFile` substitui Costura).
  Riscos a validar: `Aspose.Zip` em .NET 8, `System.Windows.Input.Key` (WPF) —
  provavelmente trocar por `System.Windows.Forms.Keys` (ligado a #8).
- Ativar nullable reference types gradualmente.

---

## ✨ Ideias de feature (a priorizar depois do build verde)

- **Multi-cliente real**: hoje `ClientSingleton` é único. Permitir N clients ativos com
  profile por janela (útil pra quem faz multi-box).
- **Painel de status ao vivo**: HP/SP/buff atuais na própria UI (já lê tudo, só faltou
  exibir).
- **Hotkey global configurável por ação** (start/stop de cada módulo).
- **Auto-update in-app** via GitHub releases (`_4RLatestVersionURL` já existe).
- **Dark theme** / tema consistente nos ~15 forms.
- **Editor visual de offsets** com scan assistido (ajuda a cadastrar servidor de fã).
- **Import/export de profile** (compartilhar setups no Discord).
- **Perfil por personagem** (auto-troca lendo `ReadCharacterName`).
