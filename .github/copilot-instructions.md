# 🎮 Jepp Vendinha - GitHub Copilot Instructions

<div align="center">

[![Unity](https://img.shields.io/badge/Unity-6000.2.8f1-black)](https://unity.com/)
[![Android](https://img.shields.io/badge/Android-API%2024+-green)](https://developer.android.com/)
[![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)]()
[![Deadline](https://img.shields.io/badge/Deadline-21%2F11%2F2025-red)]()
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/DATAMETRIA-standards)

</div>

Este arquivo contém instruções para GitHub Copilot trabalhar efetivamente no projeto **Jepp Vendinha**, protótipo de gamificação educacional 3D mobile para ELOEDITORIAL.

**Cliente**: ELOEDITORIAL
**Projeto**: Protótipo Livro 1 - Projeto Jepp
**Prazo**: 10/11/2025 - 21/11/2025 (11 dias)
**Status**: 🟡 Em Desenvolvimento (4 dias restantes)
**Desenvolvedor**: Vander Loto - CTO DATAMETRIA
**Data Atual**: 17/11/2025

---

## 📋 Índice

1. [Visão do Projeto](#-visão-do-projeto)
2. [Stack Tecnológico](#-stack-tecnológico)
3. [Estrutura do Projeto](#-estrutura-do-projeto)
4. [Memory Bank](#-memory-bank)
5. [Padrões de Código](#-padrões-de-código)
6. [Decisões Arquiteturais](#-decisões-arquiteturais)
7. [Workflow de Desenvolvimento](#-workflow-de-desenvolvimento)
8. [Performance e Otimizações](#-performance-e-otimizações)
9. [Testes e Validação](#-testes-e-validação)
10. [Como Usar com Copilot](#-como-usar-com-copilot)

---

## 🎯 Visão do Projeto

### Objetivo Principal

Protótipo de **gamificação educacional 3D mobile** para o Livro 1 do Projeto Jepp, focado em crianças de 6-7 anos. Experiência imersiva de vendinha virtual com personagem Pamela e atividades interativas.

### Escopo do Protótipo

**Ambientes:**
- ✅ Visual externo da vendinha
- ✅ Visual interno da vendinha

**Funcionalidades:**
- ✅ Navegação 3D mobile (FPS com joystick virtual)
- ✅ Sistema de interação com objetos (raycast + highlight)
- ✅ Transição entre cenas (externa/interna)
- ✅ 2 placeholders para atividades educacionais
- ✅ Áudio imersivo (música ambiente + SFX)

**Público-Alvo:**
- 👶 Crianças de 6-7 anos
- 📚 Alunos do Projeto Jepp - Livro 1

### Critérios de Sucesso (MVP)

| Critério | Meta | Status |
|----------|------|--------|
| **Build Android** | Funcional | ✅ |
| **FPS** | 30+ em dispositivos médios | ✅ ~45 FPS |
| **APK Size** | < 150MB | ✅ ~120MB |
| **RAM Usage** | < 500MB | ✅ ~380MB |
| **Load Time** | < 10s | ✅ ~7s |
| **Controles** | Intuitivos para crianças | ✅ |

### Cronograma

| Data | Marco | Status |
|------|-------|--------|
| 10/11 | Início | ✅ |
| 15/11 | Ambientes + Navegação | ✅ |
| 17/11 | Sistema interação | ✅ |
| 19/11 | Placeholders | 🟡 |
| 20/11 | Testes | 🟡 |
| **21/11** | **ENTREGA** | 🎯 |

---

## 🛠️ Stack Tecnológico

### Engine e Frameworks

| Tecnologia | Versão | Uso | Status |
|------------|--------|-----|--------|
| **Unity** | 6000.2.8f1 | Game engine | ✅ Produção |
| **URP** | 17.0+ | Render pipeline mobile | ✅ Produção |
| **New Input System** | 1.7+ | Controles mobile | ✅ Produção |
| **TextMesh Pro** | 3.0+ | UI text rendering | ✅ Produção |
| **Shader Graph** | 17.0+ | Outline shader | ✅ Produção |
| **Git LFS** | 3.4+ | Versionamento assets | ✅ Produção |

### Plataforma Target

| Plataforma | Versão | Prioridade | Observação |
|------------|--------|------------|------------|
| **Android** | API 24+ (7.0+) | Alta | Único build do protótipo |
| **iOS** | - | Fora do escopo | Fase futura |
| **WebGL** | - | Fora do escopo | Fase futura |

### Build Settings

```json
{
  "productName": "Jepp Vendinha",
  "companyName": "ELOEDITORIAL",
  "version": "1.0.0",
  "bundleIdentifier": "com.eloeditorial.jeppvendinha",
  "targetSdkVersion": 33,
  "minSdkVersion": 24,
  "scriptingBackend": "IL2CPP",
  "targetArchitectures": "ARM64"
}
```

---

## 📦 Estrutura do Projeto

### Hierarquia de Diretórios

```text
ELOEDITORIAL-jepp-vendinha/
├── .github/
│   └── copilot-instructions.md    → Este arquivo
├── .amazonq/rules/memory/          → Memory Bank
│   ├── idea.md                     → Visão produto
│   ├── state.md                    → Estado técnico
│   ├── vibe.md                     → Cultura equipe
│   └── decisions.md                → ADRs (8 decisões)
├── docs/                           → Documentação
│   ├── project/
│   │   ├── project-conception.md
│   │   ├── setup-guide.md
│   │   ├── branching-strategy.md
│   │   └── verification-checklist.md
│   ├── technical/
│   │   └── technical-specification.md
│   └── architecture/
│       └── adr-001-unity-structure.md
├── Jepp/                           → Projeto Unity
│   ├── Assets/
│   │   ├── JeppGame/               → Assets do jogo
│   │   │   ├── Scripts/            → 7 scripts C#
│   │   │   ├── Scenes/             → 2 cenas Unity
│   │   │   ├── Models/             → Modelos 3D
│   │   │   ├── Mat/                → Materiais/Shaders
│   │   │   └── Sounds/             → Áudio
│   │   ├── Settings/               → URP configs
│   │   └── TextMesh Pro/           → Plugin UI
│   ├── ProjectSettings/
│   └── Packages/
├── Build/
│   └── Android/
│       └── jeppgame.apk
└── README.md
```

### Assets do Protótipo

#### Scripts C# (7 classes)
- `GameManager.cs` - Gerenciamento global (Singleton)
- `MobileFPSController_InputSystem.cs` - Controle mobile FPS
- `InteractionController.cs` - Sistema de interação (raycast)
- `Interactable.cs` - Base para objetos interativos
- `ShelfStore.cs` - Prateleiras da loja
- `VirtualJoystick.cs` - Controle touch
- `CameraBobbing.cs` - Movimento de câmera

#### Cenas Unity (2)
- `External.unity` - Cena externa vendinha
- `JeppGame.unity` - Cena interna vendinha (principal)

#### Modelos 3D (2)
- `Vendinha_Externa.fbx`
- `Vendinha_Interna.fbx`

#### Áudio (4)
- `bell.mp3` - SFX sino
- `door.mp3` - SFX porta
- `nature.mp3` - Ambiente natureza
- Música ambiente (Animal Crossing style)

---

## 🧠 Memory Bank

**Localização**: `.amazonq/rules/memory/`

### Arquivos do Memory Bank

| Arquivo | Propósito | Última Atualização |
|---------|-----------|-------------------|
| **[idea.md](.amazonq/rules/memory/idea.md)** | Visão, objetivos, KPIs, personas | 17/11/2025 |
| **[state.md](.amazonq/rules/memory/state.md)** | Stack tecnológico, métricas, projetos ativos | 17/11/2025 |
| **[vibe.md](.amazonq/rules/memory/vibe.md)** | Cultura, workflow, valores da equipe | 17/11/2025 |
| **[decisions.md](.amazonq/rules/memory/decisions.md)** | 8 ADRs (decisões arquiteturais) | 17/11/2025 |

### Fluxo de Uso

**GitHub Copilot automaticamente consulta:**
1. ✅ Este arquivo (`.github/copilot-instructions.md`)
2. ✅ Memory Bank (contexto persistente)
3. ✅ Documentação do projeto (`docs/`)

**Para contexto adicional, consulte:**
- [Project Conception](docs/project/project-conception.md) - Visão completa
- [Technical Specification](docs/technical/technical-specification.md) - Spec técnica
- [Setup Guide](docs/project/setup-guide.md) - Workflow diário

## 🤖 Modelo AI-First

### Distribuição de Trabalho

- **90% Amazon Q Developer**: Implementação automatizada
- **10% Supervisão Humana**: Análise crítica e aprovações
- **Suporte Completo**: Enterprise Framework

### Benefícios Quantificados

| Benefício | Impacto | Métrica |
|-----------|---------|---------|
| **Velocidade** | 10x mais rápido | Redução de 80-90% no tempo |
| **Qualidade** | 95% redução em bugs | Testes automatizados |
| **Consistência** | 100% conformidade | Padrões obrigatórios |
| **Custo** | 80% redução | Automação completa |

---

## 📊 Estrutura Hierárquica

O framework segue uma estrutura hierárquica de aplicação em 5 camadas:

```text
00-master-context.md (Índice Central)
    ↓
Rules Atômicas (01-06) → 30 rules aplicadas SEMPRE
    ↓
Framework Rules (frameworks/) → 30 rules quando framework detectado
    ↓
Standards Completos (stacks/) → 20 standards contexto amplo
    ↓
Templates (templates/) → 42 templates referência
    ↓
Memory Bank (memory/) → 5 arquivos contexto persistente
```

### Componentes por Camada

| Camada | Quantidade | Aplicação | Automação IA |
|--------|------------|-----------|--------------|
| **Rules Atômicas** | 30 (6 × 5) | 100% projetos | 95% |
| **Framework Rules** | 30 (6 × 5) | Quando detectado | 90% |
| **Standards** | 20 | Contexto amplo | 85% |
| **Templates** | 42 | Referência | 90% |
| **Memory Bank** | 5 arquivos | Persistente | 80% |

---

## 🆕 Novidades v4.2.0 (08/11/2025)

### 🤖 Interactive Avatars Development

Novo standard completo para desenvolvimento de **avatares interativos 3D com IA**:

**Stack Tecnológico:**
- **Avatar 3D**: Ready Player Me (geração de avatares .glb)
- **Engine**: Unity 2023.2+ com WebGL
- **Animação/Voz**: Inworld.ai SDK (lipsync + emoções + TTS)
- **Cognição**: LangChain + OpenAI GPT-4
- **Backend**: FastAPI + Redis
- **Deploy**: Unity WebGL embedável em websites

**Casos de Uso:**
- 🎯 Atendimento virtual 24/7
- 🚀 Onboarding interativo
- 💬 FAQ inteligente contextual
- 🛍️ Vendas e demos de produtos
- 🎓 Treinamento e tutoriais
- 🔧 Suporte técnico guiado

**Automações Empresariais:**
- Event Bus para comunicação assíncrona
- Webhooks para CRM, FAQ, Onboarding
- Integração com sistemas externos

📄 **Documentação**: [datametria_std_interactive_avatars.md](docs/stacks/datametria_std_interactive_avatars.md)

### 📱 Mobile Native + 🥽 Unity AR/VR

**v4.1.0** também trouxe:
- **Mobile Native**: Swift (iOS) + Kotlin (Android) nativos
- **Unity AR/VR**: AR Foundation + Meta Quest + WebXR
- **Agents v2.0**: LLM Orchestrator + Prompt Curator + State Manager

---

## �🎯 Rules Atômicas

**Aplicação**: SEMPRE (100% dos projetos)

Localizadas em `docs/01-*.md` e `.amazonq/rules/01-*.md`

**Aplicação**: SEMPRE (100% dos projetos)

Localizadas em `docs/01-*.md` e `.amazonq/rules/01-*.md`

| Rule | Arquivo | Rules | Descrição |
|------|---------|-------|-----------|
| **01** | [01-code-style.md](docs/01-code-style.md) | 5 | Naming, formatação, imports |
| **02** | [02-architecture.md](docs/02-architecture.md) | 5 | Clean Architecture, DI, Repository |
| **03** | [03-security.md](docs/03-security.md) | 5 | JWT, validação, secrets |
| **04** | [04-testing.md](docs/04-testing.md) | 5 | Coverage 80%, AAA, fixtures |
| **05** | [05-performance.md](docs/05-performance.md) | 5 | Indexing, caching, async |
| **06** | [06-documentation.md](docs/06-documentation.md) | 5 | README, ADR, docstrings |

**Total: 30 Rules Atômicas**

---

## 🔧 Framework Rules

**Aplicação**: Quando framework específico é detectado

Localizadas em `docs/frameworks/` e `.amazonq/rules/frameworks/`

| Framework | Arquivo | Rules | Tecnologias |
|-----------|---------|-------|-------------|
| **Python** | [frameworks/python.md](docs/frameworks/python.md) | 5 | Poetry, Type Hints, Dataclasses |
| **Flask** | [frameworks/flask.md](docs/frameworks/flask.md) | 5 | Flask, SQLAlchemy, Blueprints |
| **Vue.js** | [frameworks/vuejs.md](docs/frameworks/vuejs.md) | 5 | Vue 3, Composition API, Pinia |
| **FastAPI** | [frameworks/fastapi.md](docs/frameworks/fastapi.md) | 5 | FastAPI, Pydantic, Async |
| **Flutter** | [frameworks/flutter.md](docs/frameworks/flutter.md) | 5 | Flutter, Dart, BLoC |
| **React Native** | [frameworks/react-native.md](docs/frameworks/react-native.md) | 5 | React Native, TypeScript, Zustand |

**Total: 30 Framework Rules**

---

## 📚 Standards Completos

**Aplicação**: Contexto amplo por stack tecnológica

Localizados em `docs/stacks/` e `.amazonq/rules/stacks/`

| Standard | Arquivo | Seções | Quando Usar |
|----------|---------|--------|-------------|
| **Web Development** | [datametria_std_web_dev.md](docs/stacks/datametria_std_web_dev.md) | 14 | Projetos web full-stack |
| **Python Automation** | [datametria_std_python_automation.md](docs/stacks/datametria_std_python_automation.md) | 9 | Automações Python |
| **AWS Development** | [datametria_std_aws_development.md](docs/stacks/datametria_std_aws_development.md) | 9 | Cloud AWS |
| **GCP Firebase** | [datametria_std_gcp_firebase.md](docs/stacks/datametria_std_gcp_firebase.md) | 9 | Cloud GCP/Firebase |
| **UX/UI Design** | [datametria_std_ux_ui.md](docs/stacks/datametria_std_ux_ui.md) | 12 | Design de interfaces |
| **Documentation** | [datametria_std_documentation.md](docs/stacks/datametria_std_documentation.md) | 11 | Documentação técnica |
| **Logging Enterprise** | [datametria_std_logging.md](docs/stacks/datametria_std_logging.md) | 9 | Logging estruturado |
| **Security** | [datametria_std_security.md](docs/stacks/datametria_std_security.md) | 10 | Segurança aplicação |
| **Mobile Flutter** | [datametria_std_mobile_flutter.md](docs/stacks/datametria_std_mobile_flutter.md) | 14 | Apps Flutter |
| **Mobile React Native** | [datametria_std_mobile_react_native.md](docs/stacks/datametria_std_mobile_react_native.md) | 15 | Apps React Native |
| **Reverse Engineering** | [datametria_std_reverse_engineering_prevention.md](docs/stacks/datametria_std_reverse_engineering_prevention.md) | 8 | Proteção código |
| **Data Architecture** | [datametria_std_data_architecture_engineering.md](docs/stacks/datametria_std_data_architecture_engineering.md) | 9 | Arquitetura dados |
| **AI/ML Development** | [datametria_std_ai_ml_development.md](docs/stacks/datametria_std_ai_ml_development.md) | 9 | Machine Learning |
| **Microservices** | [datametria_std_microservices_architecture.md](docs/stacks/datametria_std_microservices_architecture.md) | 12 | Microserviços |
| **Flow Designer** | [datametria_std_flow_designer.md](docs/stacks/datametria_std_flow_designer.md) | 8 | Design de fluxos |
| **Agents Development** | [datametria_std_agents_development.md](docs/stacks/datametria_std_agents_development.md) | 16 | Agentes IA |
| **Mobile Native** | [datametria_std_mobile_native.md](docs/stacks/datametria_std_mobile_native.md) | 6 | Apps iOS/Android nativos |
| **Unity AR/VR** | [datametria_std_unity_ar_vr.md](docs/stacks/datametria_std_unity_ar_vr.md) | 7 | Realidade Aumentada/Virtual |
| **Interactive Avatars** | [datametria_std_interactive_avatars.md](docs/stacks/datametria_std_interactive_avatars.md) | 9 | Avatares 3D Interativos |

**Total: 20 Standards Completos**

---

## 📋 Templates

**Aplicação**: Referência para geração de documentação

Localizados em `docs/templates/` e `.amazonq/rules/templates/`

### Categorias de Templates

#### 📄 Projeto (9 templates)
- README, Changelog, Release Notes
- Project Conception, Kickoff, Setup
- Developer Guide, Onboarding
- Memory Bank Creation

#### 🏗️ Técnico (13 templates)
- ADR, API Documentation, Class Reference
- Docstring Google Style, Database Schema
- Technical Specification, Architecture Diagram
- Environment Setup, Mermaid Guide
- Git Workflow, Code Standards
- Performance Review, Production Readiness

#### 📊 Gestão (8 templates)
- Product Backlog, Feature Documentation
- Code Review, Code Review Checklist
- Project Status Report, MVP Planning
- Flow Designer Conception
- Compliance Dashboard

#### 🚀 Operações (6 templates)
- Deployment Guide, Product Guide
- Security Assessment, Security Review
- Cloud Cost Estimation, Environment Setup

#### 📱 Mobile (4 templates)
- Mobile Architecture, App Store Submission
- Mobile Performance, Mobile Release

#### ✅ Checklists (2 templates)
- Accessibility Review
- Markdown Linting Guide

**Total: 42 Templates**

---

## 🧠 Memory Bank

**Aplicação**: Contexto persistente do projeto

Localizados em `docs/memory/` e `.amazonq/rules/memory/`

| Arquivo | Propósito | Quando Atualizar |
|---------|-----------|------------------|
| **[idea.md](docs/memory/idea.md)** | Visão, objetivos, KPIs | Mudanças de escopo |
| **[vibe.md](docs/memory/vibe.md)** | Cultura, valores, workflow | Mudanças de processo |
| **[state.md](docs/memory/state.md)** | Stack, métricas, projetos | Mudanças no stack |
| **[decisions.md](docs/memory/decisions.md)** | ADRs (14 decisões) | Decisões arquiteturais |
| **[q-vibes-memory-banking.md](docs/memory/q-vibes-memory-banking.md)** | Instruções Amazon Q | Melhorias no processo |

### Fluxo de Uso do Memory Bank

**Início de toda sessão:**

1. ✅ Ler `q-vibes-memory-banking.md` (instruções)
2. ✅ Ler `idea.md` (contexto produto)
3. ✅ Ler `state.md` (contexto técnico)
4. 📖 Consultar `decisions.md` (se necessário)
5. 📖 Consultar `vibe.md` (estilo e cultura)

**Total: 5 Memory Bank Files**

---

## 🔄 Fluxos de Trabalho

### Estrutura de Diretórios

```text
DATAMETRIA-standards/
├── .amazonq/rules/          # Regras Amazon Q (sincronizado)
│   ├── 00-master-context.md
│   ├── 01-06-*.md          # 6 rules atômicas
│   ├── frameworks/         # 6 framework rules
│   ├── stacks/             # 17 standards
│   ├── templates/          # 42 templates
│   └── memory/             # 5 memory bank
├── docs/                    # Documentação pública (espelho)
│   └── [mesma estrutura]
├── tools/                   # Scripts automação
│   ├── sync_docs.py        # Sincronização docs ↔ .amazonq
│   └── validate_markdown.py
└── README.md
```

## Development Workflows

### Template Usage

When creating new documentation files:

1. Identify the appropriate template from `.amazonq/rules/template-*.md`
2. Follow standard naming conventions in the template
3. Ensure synchronization between `docs/` and `.amazonq/rules/`

### Documentation Patterns

- Use standardized headers and emoji prefixes (🎯, 📋, 🔧, etc.)
- Include required sections: Overview, Requirements, Setup, Usage
- Follow markdown linting rules defined in `.markdownlint.json`

### Integration Points

- Amazon Q integration is managed through `.amazonq/rules/`
- Templates are synced between `docs/` and `.amazonq/rules/` using `tools/cli/sync_amazonq.py`

### Common Commands

```bash
# Sync Amazon Q rules
python tools/cli/sync_amazonq.py

# Validate markdown
python tools/validate_markdown.py

# Get template count
ls .amazonq/rules/template-*.md | wc -l  # Should be 30
```

## Project Conventions

### Documentation Structure

Standards documentation follows a consistent pattern:

- 🎯 Objective/Overview
- 📋 Requirements/Prerequisites
- 🔧 Implementation Details
- 📚 References/Resources

### Content Organization

- All standards files use `.md` extension
- Template files prefixed with `template-`
- Domain standards prefixed with `datametria_std_`

## Integration Guidelines

When making changes:

1. Update both `docs/` and `.amazonq/rules/` directories
2. Maintain template structure and emoji conventions
3. Follow markdown linting standards
4. Verify AWS/GCP guidelines for cloud components

---

## 💻 Padrões de Código

### Convenções Unity C#

#### Naming Conventions

```csharp
// Classes: PascalCase
public class GameManager : MonoBehaviour

// Methods: PascalCase
public void LoadScene(string sceneName)

// Variables privadas: camelCase
private float moveSpeed = 3f;

// Variables públicas/serializadas: PascalCase
[SerializeField] private float MoveSpeed = 3f;

// Constantes: UPPER_CASE
private const int MAX_LIVES = 3;
```

#### Code Organization

```csharp
public class ExampleClass : MonoBehaviour
{
    // 1. Serialized Fields
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    // 2. Private Fields
    private CharacterController controller;
    private bool isGrounded;

    // 3. Unity Methods
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        // Inicialização
    }

    void Update()
    {
        // Lógica por frame
    }

    // 4. Public Methods
    public void CustomMethod()
    {
        // Implementação
    }

    // 5. Private Methods
    private void HelperMethod()
    {
        // Implementação
    }
}
```

#### Comentários

- Use comentários XML para métodos públicos
- Código deve ser autoexplicativo
- Comente apenas lógica complexa

```csharp
/// <summary>
/// Carrega uma nova cena do jogo.
/// </summary>
/// <param name="sceneName">Nome da cena a ser carregada</param>
public void LoadScene(string sceneName)
{
    SceneManager.LoadScene(sceneName);
}
```

### Git Workflow

#### Conventional Commits

```bash
feat: adiciona sistema de interação com objetos
fix: corrige bug de highlight permanecendo ativo
docs: atualiza README com instruções de build
refactor: reorganiza estrutura de pastas Unity
perf: otimiza texturas para ASTC 6x6
test: adiciona testes manuais para navegação mobile
```

#### Branching

- `main` - Produção (protegida)
- `dev` - Integração
- `feature/nome-feature` - Features individuais

#### File Locking (Git LFS)

**SEMPRE fazer lock antes de editar:**

```bash
# Lock arquivo
git lfs lock Jepp/Assets/Scenes/MainScene.unity

# Editar no Unity
# ...

# Commit e push
git add .
git commit -m "feat: atualiza cena principal"
git push

# Unlock arquivo
git lfs unlock Jepp/Assets/Scenes/MainScene.unity
```

**Arquivos que PRECISAM de lock:**
- `.unity` (cenas)
- `.prefab` (prefabs)
- `.asset` (ScriptableObjects)

---

## 🏛️ Decisões Arquiteturais

**Localização**: `.amazonq/rules/memory/decisions.md`

### ADRs Principais

#### ADR-001: Unity 6000.2.8f1 como Engine

**Decisão**: Usar Unity 6000.2.8f1 com URP para mobile

**Motivo**:
- Ecosystem maduro (Asset Store, documentação)
- URP otimizado para mobile
- New Input System para controles touch
- Build Android nativo

**Alternativas rejeitadas**: Unreal Engine (overkill), Godot (menos recursos mobile)

#### ADR-002: Android Único como Target

**Decisão**: Build apenas para Android (API 24+)

**Motivo**:
- Prazo curto (11 dias)
- Foco total em otimização Android
- Testes simplificados

**Alternativas rejeitadas**: Android + iOS (dobra tempo), WebGL (performance inferior)

#### ADR-003: URP Mobile Renderer

**Decisão**: URP com preset Mobile otimizado

**Configurações**:
- Render Scale: 0.8
- MSAA: Disabled
- HDR: Disabled
- Shadow Resolution: 512
- Max Lights: 4

**Motivo**: Performance 2x melhor que Built-in, suporta dispositivos antigos

#### ADR-004: New Input System

**Decisão**: New Input System com Virtual Joystick + Touch

**Motivo**:
- Controles modernos e responsivos
- Suporte a múltiplos dispositivos
- Futuro-proof (Input System legado será depreciado)

#### ADR-005: Git LFS para Assets

**Decisão**: Git LFS para binários (modelos, texturas, áudio)

**Motivo**:
- Otimização de clones (LFS Partial Clone)
- File locking para evitar conflitos
- Melhor performance do repositório

#### ADR-006: Estrutura de Pastas Unificada

**Decisão**: Todo código/assets em `Assets/JeppGame/`

**Motivo**:
- Namespace claro
- Fácil de portar para outros projetos
- Evita conflitos com assets externos

#### ADR-007: Singleton Pattern para GameManager

**Decisão**: GameManager como Singleton (Instance)

**Motivo**:
- Acesso global simples
- Gerenciamento centralizado de estado
- Padrão comum em Unity

#### ADR-008: Sistema de Interação com Raycast

**Decisão**: Raycast da câmera + Interface Interactable

**Motivo**:
- Performance superior a Trigger Colliders
- Feedback visual claro (highlight)
- Extensível para novos tipos de interação

---

## 🔄 Workflow de Desenvolvimento

### Fluxo Diário

#### 1. Início do Dia

```bash
# Atualizar repositório
git pull origin dev

# Verificar locks ativos
git lfs locks

# Instalar Git LFS (se necessário)
git lfs pull
```

#### 2. Desenvolvimento

1. **Criar feature branch**
```bash
git checkout -b feature/nome-feature
```

2. **Fazer lock de arquivos Unity**
```bash
git lfs lock Jepp/Assets/Scenes/MinhaScene.unity
```

3. **Desenvolver no Unity**
   - Implementar feature
   - Testar no Editor
   - Build Android para validação

4. **Commit incremental**
```bash
git add .
git commit -m "feat: implementa [feature]"
git push origin feature/nome-feature
```

5. **Unlock arquivos**
```bash
git lfs unlock Jepp/Assets/Scenes/MinhaScene.unity
```

#### 3. Merge para Dev

```bash
# Após PR aprovado
git checkout dev
git pull origin dev
git merge feature/nome-feature
git push origin dev
```

### Checklist de Build Android

- [ ] URP Mobile Renderer ativo
- [ ] Texturas comprimidas (ASTC 6x6)
- [ ] IL2CPP configurado (ARM64)
- [ ] Stripping Level: Medium
- [ ] Build and Run em dispositivo real
- [ ] Validar FPS (30+)
- [ ] Validar RAM (< 500MB)
- [ ] Validar APK size (< 150MB)

### Troubleshooting Comum

#### Unity não abre projeto

```bash
# Solução: Verificar versão
# Unity Hub → Installs → Verificar se 6000.2.8f1 está instalado
```

#### Git LFS não baixa arquivos

```bash
# Solução:
git lfs install
git lfs pull
```

#### Build Android falha

```bash
# Solução: Verificar Android SDK
# Unity → Preferences → External Tools → Verificar Android SDK path
```

#### Performance baixa no dispositivo

- Verificar URP Mobile preset ativo
- Verificar compressão de texturas (ASTC)
- Verificar LOD system em modelos 3D
- Usar Unity Profiler para identificar gargalos

---

## ⚡ Performance e Otimizações

### Configurações URP Mobile

```csharp
// Mobile_RPAsset configurações atuais:
renderScale: 0.8
msaaQuality: Disabled
hdr: false
shadowResolution: 512
maxAdditionalLightsCount: 4
depthTexture: false
opaqueTexture: false
```

### Otimização de Texturas

| Tipo | Formato | Max Size | Mipmaps |
|------|---------|----------|---------|
| **Albedo** | ASTC 6x6 | 1024 | ✅ |
| **Normal** | ASTC 6x6 | 1024 | ✅ |
| **UI** | ASTC 6x6 | 512 | ❌ |
| **Icons** | ASTC 6x6 | 256 | ❌ |

### Otimização de Modelos 3D

- **Poly count**: < 10k por modelo
- **LOD levels**: 3 (100%, 50%, 25%)
- **Mesh Compression**: Medium
- **Read/Write**: Disabled
- **Optimize Mesh**: Enabled

### Otimização de Áudio

- **Formato**: Vorbis (comprimido)
- **Quality**: 70% (música), 90% (SFX)
- **Load Type**: Compressed in Memory
- **Preload Audio Data**: Disabled

### Métricas Atuais

| Métrica | Meta | Atual | Tendência |
|---------|------|-------|-----------|
| **FPS** | 30+ | ~45 | ✅ |
| **Draw Calls** | < 100 | ~75 | ✅ |
| **APK Size** | < 150MB | ~120MB | ✅ |
| **RAM Usage** | < 500MB | ~380MB | ✅ |
| **Load Time** | < 10s | ~7s | ✅ |
| **Battery Drain** | Baixo | Médio | ⚠️ |

### Performance Budget

```text
Target Device: Samsung Galaxy A52 (mid-range)
- CPU: Qualcomm Snapdragon 720G
- GPU: Adreno 618
- RAM: 6GB
- Android: 11+

FPS Budget: 30-60 FPS
Draw Calls: < 100
Triangles: < 100k total na cena
Texture Memory: < 200MB
```

---

## 🧪 Testes e Validação

### Estratégia de Testes

**Foco**: Testes manuais (protótipo tem prazo curto)

#### Testes Obrigatórios

| ID | Cenário | Resultado Esperado | Status |
|----|---------|-------------------|--------|
| **T-001** | Navegação joystick | Player se move suavemente 360° | ✅ |
| **T-002** | Rotação câmera touch | Câmera rotaciona sem lag | ✅ |
| **T-003** | Transição cenas | Porta carrega outra cena < 3s | ✅ |
| **T-004** | Interação objetos | Highlight aparece ao mirar | ✅ |
| **T-005** | Performance 5min | FPS > 30 constante | ✅ |
| **T-006** | Áudio | Música + SFX funcionam | ✅ |
| **T-007** | Placeholder 1 | Área interativa responde | 🟡 |
| **T-008** | Placeholder 2 | Área interativa responde | 🟡 |

#### Testes de Dispositivos

| Dispositivo | Android | RAM | GPU | Status |
|-------------|---------|-----|-----|--------|
| Samsung Galaxy A52 | 11 | 6GB | Adreno 618 | ✅ Testado |
| Xiaomi Redmi Note 10 | 11 | 4GB | Adreno 610 | 🟡 Pendente |
| Motorola Moto G8 | 10 | 4GB | Adreno 610 | 🟡 Pendente |

### Checklist de Entrega (21/11)

#### Funcionalidades
- [ ] Build Android instalável
- [ ] Cena externa navegável
- [ ] Cena interna navegável
- [ ] Transição porta funcional
- [ ] Sistema interação ativo
- [ ] Placeholder 1 preparado
- [ ] Placeholder 2 preparado
- [ ] Áudio funcionando

#### Performance
- [ ] FPS 30+ em Galaxy A52
- [ ] FPS 30+ em Redmi Note 10
- [ ] FPS 30+ em Moto G8
- [ ] APK < 150MB
- [ ] RAM < 500MB
- [ ] Load time < 10s

#### Documentação
- [ ] README.md atualizado
- [ ] Setup Guide completo
- [ ] Technical Specification revisada
- [ ] Memory Bank atualizado
- [ ] ADRs documentados

---

## 🚀 Como Usar com Copilot

### Inicialização Automática

GitHub Copilot automaticamente lê este arquivo quando você abre o projeto. Não é necessário configuração adicional.

### Contexto Sempre Disponível

**Copilot tem acesso a:**
1. ✅ Este arquivo (`.github/copilot-instructions.md`)
2. ✅ Memory Bank (`.amazonq/rules/memory/`)
3. ✅ Documentação (`docs/`)
4. ✅ Código-fonte (`Jepp/Assets/JeppGame/`)

### Prompts Recomendados

#### Para Implementação

```
"Crie um script Unity C# para [funcionalidade] seguindo os padrões do projeto Jepp Vendinha.
Use PascalCase para classes/métodos, inclua comentários XML, e siga a estrutura do GameManager."
```

#### Para Debugging

```
"Analise o script [nome] e identifique possíveis problemas de performance ou bugs.
Considere que o target é Android mobile com URP otimizado."
```

#### Para Documentação

```
"Atualize a documentação [arquivo.md] com as mudanças feitas em [feature].
Siga o formato Markdown com emojis e tabelas como no projeto Jepp Vendinha."
```

#### Para Otimização

```
"Otimize o script [nome] para performance mobile Android.
Meta: 30+ FPS em dispositivos médios. Considere draw calls e alocações de memória."
```

### Exemplo Prático

**Prompt:**
```
"Crie um novo Interactable para um objeto 'Balcão' que, ao ser tocado,
mostra um texto 'Bem-vindo à vendinha!'. Siga os padrões do Interactable.cs existente."
```

**Copilot gera:**
```csharp
using UnityEngine;
using TMPro;

/// <summary>
/// Interactable para o balcão da vendinha.
/// Mostra mensagem de boas-vindas ao interagir.
/// </summary>
public class CounterInteractable : Interactable
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private float messageDuration = 3f;

    private Coroutine messageCoroutine;

    public override void Interact()
    {
        base.Interact();
        ShowWelcomeMessage();
    }

    private void ShowWelcomeMessage()
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(DisplayMessage());
    }

    private System.Collections.IEnumerator DisplayMessage()
    {
        welcomeText.text = "Bem-vindo à vendinha!";
        welcomeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        welcomeText.gameObject.SetActive(false);
    }
}
```

### Boas Práticas com Copilot

#### DO ✅

- Sempre mencionar "projeto Jepp Vendinha" em prompts
- Referenciar classes existentes (GameManager, Interactable)
- Pedir validação contra padrões Unity C#
- Solicitar comentários XML em métodos públicos
- Pedir performance checks para Android mobile

#### DON'T ❌

- Não pedir código para outras plataformas (iOS, WebGL)
- Não solicitar features fora do escopo do protótipo
- Não ignorar constraints de performance (30+ FPS)
- Não criar código que não siga naming conventions
- Não gerar código sem considerar URP mobile

### Troubleshooting com Copilot

#### Copilot não entende contexto

**Solução:**
```
"Leia o Memory Bank (.amazonq/rules/memory/) e a documentação (docs/)
do projeto Jepp Vendinha antes de responder."
```

#### Código gerado não segue padrões

**Solução:**
```
"Refatore este código seguindo:
1. Naming conventions (PascalCase classes, camelCase private vars)
2. Estrutura Unity (Awake, Start, Update)
3. Comentários XML em métodos públicos
4. Otimizações para mobile Android"
```

#### Precisa de mais contexto

**Solução:**
```
"Consulte:
- .amazonq/rules/memory/state.md (stack técnico)
- docs/technical/technical-specification.md (spec completa)
- Jepp/Assets/JeppGame/Scripts/GameManager.cs (exemplo de código)"
```

---

## 📚 Referências Rápidas

### Documentação do Projeto

| Documento | Descrição | Atualização |
|-----------|-----------|-------------|
| [README.md](../README.md) | Visão geral e quickstart | 17/11/2025 |
| [Project Conception](../docs/project/project-conception.md) | Visão, escopo, cronograma | 17/11/2025 |
| [Technical Specification](../docs/technical/technical-specification.md) | Spec técnica completa | 17/11/2025 |
| [Setup Guide](../docs/project/setup-guide.md) | Workflow diário | 17/11/2025 |
| [Branching Strategy](../docs/project/branching-strategy.md) | Estratégia Git | 10/11/2025 |
| [Verification Checklist](../docs/project/verification-checklist.md) | Checklist entrega | 15/11/2025 |

### Memory Bank

| Arquivo | Propósito | Atualização |
|---------|-----------|-------------|
| [idea.md](../.amazonq/rules/memory/idea.md) | Visão produto | 17/11/2025 |
| [state.md](../.amazonq/rules/memory/state.md) | Estado técnico | 17/11/2025 |
| [vibe.md](../.amazonq/rules/memory/vibe.md) | Cultura equipe | 17/11/2025 |
| [decisions.md](../.amazonq/rules/memory/decisions.md) | 8 ADRs | 17/11/2025 |

### Recursos Externos

- [Unity URP Documentation](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
- [New Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@latest)
- [Android Optimization Guide](https://docs.unity3d.com/Manual/android-optimization.html)
- [Git LFS Documentation](https://git-lfs.github.com/)
- [DATAMETRIA Standards](https://github.com/datametria/DATAMETRIA-standards)

---

## ⚠️ Importante - Lembrar Sempre

### Deadline

**21/11/2025** - 4 dias restantes (a partir de 17/11)

### Escopo MVP (Inegociável)

- ✅ Build Android funcional
- ✅ Navegação mobile FPS
- ✅ Ambientes externo + interno
- ✅ Sistema de interação
- ✅ 2 placeholders preparados
- ✅ Performance 30+ FPS

### Restrições Técnicas

- Unity **6000.2.8f1** (mesma versão obrigatória)
- Android **API 24+** (único target)
- URP **Mobile Renderer** (obrigatório)
- Git LFS **File Locking** (sempre fazer lock)

### Contatos

**Desenvolvedor**: Vander Loto - CTO DATAMETRIA
**Cliente**: ELOEDITORIAL
**Email**: vander.loto@datametria.io
**Discord**: [discord.gg/kKYGmCC3](https://discord.gg/kKYGmCC3)

---

<div align="center">

**Jepp Vendinha v1.0.0**

Protótipo de Gamificação Educacional 3D Mobile

Desenvolvido com ❤️ por Vander Loto para ELOEDITORIAL

[⭐ GitHub](https://github.com/vanderloto/jepp-vendinha) • [📖 Documentação](../README.md) • [🎯 DATAMETRIA Standards](https://github.com/datametria/DATAMETRIA-standards)

**Última Atualização**: 17/11/2025 | **Status**: 🟡 Em Desenvolvimento | **Deadline**: 21/11/2025

</div>
