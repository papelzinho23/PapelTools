# CLAUDE.md — contexto para o assistente

## O que é
Fork de trabalho do **4RTools** (assistente de automação para Ragnarök Online).
Objetivo deste fork: otimizar, corrigir e adicionar features. Upstream: `4RTools/4RTools`.

## Stack
- C# / **.NET Framework 4.7.2**, **WinForms**, solução clássica (`4RTools.sln`).
- NuGet via `packages.config`. `Costura.Fody` empacota tudo num exe. `Aspose.Zip`, `Newtonsoft.Json`.
- Roda **como admin** (lê memória de outro processo).

## Build
- Preferir **MSBuild** (Visual Studio 2022, workload ".NET desktop development").
  Instalado em `D:\VS2022\Community`.
- `MSBuild 4RTools.sln /t:Restore` depois `MSBuild 4RTools.sln /p:Configuration=Debug`.
- Saída: `bin\Debug\4RTools.exe`.
- Não há testes nem CI no repo.

## Mapa do código
Ver [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md). Resumo:
- `Forms/` — telas (Container = janela MDI principal; AutoPatcher = tela inicial).
- `Model/` — módulos de automação (`interface Action`: Start/Stop/GetConfiguration/GetActionName),
  domínio (`Client`, `Profile`).
- `Utils/` — memória (`ProcessMemoryReader`), hotkeys (`KeyboardHook`), interop, threads
  (`_4RThread`), observer (`RObserver`), config/constants.
- Comunicação UI↔módulos: **Observer** (`Subject.Notify(Message(MessageCode, data))`).
- Estado global: `ClientSingleton` (client ativo), `ProfileSingleton` (profile ativo).

## Backlog / prioridades
Ver [`docs/BACKLOG.md`](docs/BACKLOG.md). Top 3: `Thread.Suspend` → cancelamento;
bug do contador do Autopot; consolidar os busy-loops de thread.

## Convenções
- Namespace raiz: `_4RTools`.
- Cada Form: `X.cs` + `X.Designer.cs` + `X.resx` — editar `.Designer.cs` só pelo designer do VS quando possível.
- Profiles são JSON em `Profile/`, um por profile, chave por `GetActionName()`.
- Ao mexer em módulo, manter o contrato de `interface Action` e o registro no `Profile` (ctor + `ProfileSingleton.Load`).

## Git
- Branch de trabalho: `dev/otimizacoes`. `upstream` = repo oficial (pull de correções).
- Commits: Conventional Commits (o histórico usa `chore(scope):`, `feat:`, `fix:`).
