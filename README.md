<p align="center">
  <img src="assets/images/logo-papeltools.png" width="420">
</p>

# PapelTools

Assistente **tudo-em-um para Ragnarök Online** — Autopot, Autobuff, Skill Spammer,
Macros de música, Macro Switch e mais. Roda em paralelo ao jogo, lendo HP/SP/buffs
da memória do client e enviando input pra janela.

> Fork pessoal do [4RTools](https://github.com/4RTools/4RTools) (MIT), mantido em
> [papelzinho23/PapelTools](https://github.com/papelzinho23/PapelTools).
> O objetivo é otimizar, corrigir e adicionar recursos. O upstream continua como
> remote `upstream` para trazer correções.

## Rodando / compilando

Requer **Visual Studio 2022** (workload *.NET desktop development*) ou os
Build Tools + `nuget.exe`.

```bash
nuget restore 4RTools.sln
MSBuild 4RTools.sln -p:Configuration=Release
```

Saída: `bin/Release/PapelTools.exe` (exe único, via Costura.Fody). Precisa rodar
**como administrador** (lê a memória do processo do jogo). O `supported_servers.json`
é copiado junto do exe e já traz o client **`#Ethel Adventure`** configurado.

Detalhes de arquitetura em [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md);
backlog de melhorias em [`docs/BACKLOG.md`](docs/BACKLOG.md).

## Recursos

- [x] Botão ON/OFF (com hotkey global)
- [x] Autopot (HP/SP) + Autopot Yggdrasil
- [x] Autobuff (status, stuffs, skills)
- [x] Debuff / Status Recovery
- [x] Skill Spammer (AHK) — modos Compatibility e Speed Boost
- [x] Auto Refresh Spammer (x3)
- [x] Macro de músicas / Macro Switch (chain)
- [x] ATK x DEF Mode
- [x] Perfis (JSON, quantos quiser)
- [x] Cadastro de servidores por offset de memória

## Créditos

Baseado no trabalho da comunidade 4RTools.

<a href="https://github.com/4RTools/4RTools/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=4RTools/4RTools" />
</a>
