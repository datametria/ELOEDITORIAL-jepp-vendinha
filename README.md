# 🎮 ELOEDITORIAL - Jepp Vendinha (Protótipo Livro 1)

[![Unity](https://img.shields.io/badge/Unity-6000.2.8f1-black)](https://unity.com/)
[![Android](https://img.shields.io/badge/Android-API%2024+-green)](https://developer.android.com/)
[![Git LFS](https://img.shields.io/badge/Git%20LFS-Enabled-blue)](https://git-lfs.github.com/)
[![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)]()
[![Deadline](https://img.shields.io/badge/Deadline-21%2F11%2F2025-red)]()

> Protótipo de gamificação educacional 3D mobile para o Livro 1 do Projeto Jepp - Crianças 6-7 anos

**Cliente**: ELOEDITORIAL  
**Prazo**: 10/11/2025 - 21/11/2025 (11 dias)  
**Status**: 🟡 Em Desenvolvimento (4 dias restantes)

---

## 🎯 Sobre o Projeto

Experiência imersiva 3D de uma vendinha virtual com:
- ✅ Visual externo + interno da vendinha
- ✅ Navegação mobile FPS (joystick virtual)
- ✅ Personagem Pamela (do livro)
- ✅ Sistema de interação com objetos
- ✅ 2 placeholders para atividades educacionais
- ✅ Áudio imersivo (música + SFX)

**Target**: Android API 24+ (7.0+)  
**Performance**: 30+ FPS em dispositivos médios

---

## 📋 Pré-requisitos

- **Unity**: `6000.2.8f1` (OBRIGATÓRIO - mesma versão)
- **Git LFS**: Instalado e configurado
- **Android SDK**: API Level 24+
- **Dispositivo Android**: Para testes (recomendado)

## 🚀 Setup Inicial

```bash
# 1. Clonar repositório
git clone <repo-url>
cd ELOEDITORIAL-jepp-vendinha

# 2. Instalar Git LFS
git lfs install
git lfs pull

# 3. Abrir projeto no Unity
# Abrir pasta: Jepp/
# Unity Hub → Add → Selecionar pasta "Jepp"
```

## 🌿 Branching Strategy

- **`main`**: Produção (protegida)
- **`dev`**: Integração
- **`feature/*`**: Features individuais

Ver detalhes em [BRANCHING.md](BRANCHING.md)

## 🔒 File Locking

**SEMPRE fazer lock antes de editar:**
- `.unity` (cenas)
- `.prefab` (prefabs)
- `.asset` (ScriptableObjects)

```bash
git lfs lock Jepp/Assets/Scenes/MainScene.unity
# Editar no Unity
git add . && git commit -m "feat: atualiza cena"
git push
git lfs unlock Jepp/Assets/Scenes/MainScene.unity
```

## 📦 Estrutura Atual

```
Jepp/Assets/
├── JeppGame/              → Código e assets do jogo
│   ├── Scripts/           → 7 scripts C# (GameManager, FPS Controller, etc)
│   ├── Scenes/            → 2 cenas (External, JeppGame)
│   ├── Models/            → Modelos 3D (Vendinha Externa/Interna)
│   ├── Mat/               → Materiais e shaders (Highlight, Outline)
│   └── Sounds/            → Áudio (música ambiente + SFX)
├── Settings/              → URP Mobile configs
└── TextMesh Pro/          → Plugin UI
```

**Nota**: Estrutura será refatorada conforme [Refactor Plan](REFACTOR_PLAN.md)

## 🎮 Como Testar

### No Unity Editor
1. Abrir Unity Hub → Open → pasta `Jepp/`
2. Abrir cena `External.unity` ou `JeppGame.unity`
3. Play ▶️

### Build Android
```bash
# Build está em:
Build/Android/jeppgame.apk

# Instalar em dispositivo:
adb install Build/Android/jeppgame.apk
```

## 🛠️ Comandos Úteis

```bash
# Git LFS
git lfs locks              # Ver locks ativos
git lfs ls-files           # Listar arquivos LFS

# Git
git status                 # Status do repo
git pull origin dev        # Atualizar branch dev
```

## ⚠️ Importante

- ✅ Usar Unity `6000.2.8f1` (mesma versão)
- ✅ Fazer lock de `.unity` e `.prefab`
- ✅ Trabalhar em feature branches
- ❌ Nunca commitar `Library/`, `Temp/`, `Logs/`
- ❌ Nunca fazer commit direto em `main`

## 📚 Documentação

### 📄 Projeto
| Documento | Descrição |
|-----------|----------|
| [Project Conception](docs/project/project-conception.md) | Visão, objetivos, cronograma |
| [Technical Specification](docs/technical/technical-specification.md) | Especificação técnica completa |
| [Setup Guide](docs/project/setup-guide.md) | Workflow diário + troubleshooting |
| [Branching Strategy](docs/project/branching-strategy.md) | Estratégia Git |
| [Verification Checklist](docs/project/verification-checklist.md) | Checklist de conformidade |

### 🏗️ Arquitetura
| Documento | Descrição |
|-----------|----------|
| [ADR-001: Unity Structure](docs/architecture/adr-001-unity-structure.md) | Estrutura de pastas multiplataforma |

### 🧠 Memory Bank
| Documento | Descrição |
|-----------|----------|
| [idea.md](.amazonq/rules/memory/idea.md) | Visão do produto |
| [state.md](.amazonq/rules/memory/state.md) | Estado técnico atual |
| [vibe.md](.amazonq/rules/memory/vibe.md) | Cultura da equipe |
| [decisions.md](.amazonq/rules/memory/decisions.md) | 8 decisões arquiteturais (ADRs) |

### 📖 Standards
| Documento | Descrição |
|-----------|----------|
| [DATAMETRIA Unity Standards](.amazonq/rules/stacks/datametria_std_unity_ar_vr.md) | Padrões Unity AR/VR |
| [Refactor Plan](REFACTOR_PLAN.md) | Plano de reestruturação |

---

## 📊 Status do Projeto

### ✅ Implementado
- Ambientes 3D (externo + interno)
- Navegação mobile FPS
- Sistema de interação com highlight
- Áudio imersivo
- Transição entre cenas

### 🟡 Em Andamento
- Placeholder atividade 1
- Placeholder atividade 2

### 🔴 Pendente
- Testes em 3 dispositivos Android
- Build final otimizado
- Personagem Pamela (desejável)

### 📅 Cronograma
| Data | Marco | Status |
|------|-------|--------|
| 10/11 | Início | ✅ |
| 15/11 | Ambientes + Navegação | ✅ |
| 17/11 | Sistema interação | ✅ |
| 19/11 | Placeholders | 🟡 |
| 20/11 | Testes | 🟡 |
| **21/11** | **ENTREGA** | 🎯 |

---

## 🎯 Métricas de Performance

| Métrica | Meta | Atual | Status |
|---------|------|-------|--------|
| **FPS** | 30+ | ~45 | ✅ |
| **APK Size** | < 150MB | ~120MB | ✅ |
| **RAM Usage** | < 500MB | ~380MB | ✅ |
| **Load Time** | < 10s | ~7s | ✅ |

## 👥 Equipe

**Desenvolvimento**: Vander Loto - CTO DATAMETRIA  
**Cliente**: ELOEDITORIAL  
**Contato**: vander.loto@datametria.io

---

## 📝 Notas Técnicas

### Stack
- Unity 6000.2.8f1
- URP Mobile Renderer
- New Input System
- Git + Git LFS
- Android API 24+ (IL2CPP, ARM64)

### Otimizações
- Texturas ASTC 6x6
- LOD system
- Occlusion culling
- URP Mobile preset

### Assets
- 7 scripts C#
- 2 cenas Unity
- 2 modelos 3D
- 4 arquivos de áudio
- Shader Graph (outline)

---

**Última Atualização**: 17/11/2025  
**Versão**: 1.0.0  
**Status**: 🟡 Em Desenvolvimento (4 dias para entrega)
