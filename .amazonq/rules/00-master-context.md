# DATAMETRIA Standards - Master Context

> **Rule Master**: Índice central e referência principal para Amazon Q Developer

**Versão:** 4.3.0
**Data:** 17/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Baseado em:** AmazonQ-Guidelines v2.0

---

## 🎯 Visão Geral

Este documento é o **ponto de entrada único** para o Amazon Q Developer e supervisores humanos. Define a estrutura completa de rules, standards, templates e memory bank do DATAMETRIA Standards.

### Modelo AI-First Development

- **90% Amazon Q Developer**: Implementação automática seguindo rules
- **10% Supervisão Humana**: Análise crítica e decisões estratégicas

### Estrutura do Framework

```
.amazonq/rules/
├── 00-master-context.md          # Este arquivo - Rule Master
├── 01-06-*.md                    # Rules Atômicas (30 rules)
├── frameworks/                   # Rules por Framework
├── stacks/                       # Standards Completos (17 standards)
├── templates/                    # Templates (40+ templates)
└── memory/                       # Memory Bank (contexto persistente)
```

---

## 📖 Rules Atômicas (Camada 1)

### Aplicação: SEMPRE (100% dos projetos)

| Rule | Arquivo | Rules | Descrição |
|------|---------|-------|-----------|
| **01** | [01-code-style.md](01-code-style.md) | 5 rules | Naming, formatação, imports, funções, parâmetros |
| **02** | [02-architecture.md](02-architecture.md) | 5 rules | Clean Architecture, DI, Repository, Feature Folders, Versioning |
| **03** | [03-security.md](03-security.md) | 5 rules | JWT, validação, secrets, rate limiting, SQL injection |
| **04** | [04-testing.md](04-testing.md) | 5 rules | Coverage, AAA, naming, fixtures, separação |
| **05** | [05-performance.md](05-performance.md) | 5 rules | Indexing, N+1, caching, async, pagination |
| **06** | [06-documentation.md](06-documentation.md) | 5 rules | README, ADR, docstrings, OpenAPI, changelog |

**Total: 30 Rules Atômicas**

### Detalhamento das Rules

#### 01-code-style.md

- **Rule 1.1**: Naming Conventions (snake_case, PascalCase, UPPER_SNAKE_CASE)
- **Rule 1.2**: Formatação Automática (Black, Prettier, pre-commit)
- **Rule 1.3**: Imports Organization (Standard → Third-party → Local)
- **Rule 1.4**: Funções Máximo 50 Linhas
- **Rule 1.5**: Máximo 3 Parâmetros

#### 02-architecture.md

- **Rule 2.1**: Clean Architecture em Camadas
- **Rule 2.2**: Dependency Injection Obrigatória
- **Rule 2.3**: Repository Pattern para Persistência
- **Rule 2.4**: Feature Folders (Vertical Slicing)
- **Rule 2.5**: API Versioning Obrigatório

#### 03-security.md

- **Rule 3.1**: JWT com Refresh Tokens (Access 15min, Refresh 7 dias)
- **Rule 3.2**: Input Validation (Pydantic/Zod obrigatório)
- **Rule 3.3**: Secrets em Variáveis de Ambiente
- **Rule 3.4**: Rate Limiting (10 req/min anônimos, 100 autenticados)
- **Rule 3.5**: SQL Injection Prevention (ORM obrigatório)

#### 04-testing.md

- **Rule 4.1**: Cobertura Mínima 80% (branches ≥ 75%)
- **Rule 4.2**: AAA Pattern (Arrange, Act, Assert)
- **Rule 4.3**: Naming Convention (test_<método>_<cenário>_<resultado>)
- **Rule 4.4**: Fixtures e Factories (Setup reutilizável)
- **Rule 4.5**: Testes Separados (Unit, Integration, E2E)

#### 05-performance.md

- **Rule 5.1**: Database Indexing (FK, WHERE, ORDER BY)
- **Rule 5.2**: N+1 Query Prevention (Eager loading)
- **Rule 5.3**: Caching Strategy (Redis com TTL)
- **Rule 5.4**: Async/Await para I/O
- **Rule 5.5**: Lazy Loading e Pagination (Max 100 itens)

#### 06-documentation.md

- **Rule 2.1**: README Obrigatório (Badges, comandos, pré-requisitos)
- **Rule 2.2**: ADR para Decisões Arquiteturais
- **Rule 2.3**: Docstrings Google Style
- **Rule 2.4**: API Documentation OpenAPI 3.0
- **Rule 2.5**: Changelog Keep a Changelog

---

## 🔧 Rules por Framework (Camada 2)

### Aplicação: Quando framework detectado

| Framework | Arquivo | Status | Tecnologias | Rules |
|-----------|---------|--------|-------------|-------|
| **Python** | [frameworks/python.md](frameworks/python.md) | ✅ Criado | Poetry + Type Hints + Dataclasses + Async | 5 rules |
| **Flask** | [frameworks/flask.md](frameworks/flask.md) | ✅ Criado | Flask + SQLAlchemy + Blueprints + Alembic | 5 rules |
| **Vue.js** | [frameworks/vuejs.md](frameworks/vuejs.md) | ✅ Criado | Vue 3 + Composition API + Pinia + Vite | 5 rules |
| **FastAPI** | [frameworks/fastapi.md](frameworks/fastapi.md) | ✅ Criado | FastAPI + Pydantic + Async + Celery | 5 rules |
| **Flutter** | [frameworks/flutter.md](frameworks/flutter.md) | ✅ Criado | Flutter + Dart + BLoC + Clean Architecture | 5 rules |
| **React Native** | [frameworks/react-native.md](frameworks/react-native.md) | ✅ Criado | React Native + TypeScript + Zustand + Expo | 5 rules |
| **SQLAlchemy** | [frameworks/sqlalchemy.md](frameworks/sqlalchemy.md) | ✅ Criado | SQLAlchemy 2.0 + Alembic + Async | 5 rules |
| **Docker** | [frameworks/docker.md](frameworks/docker.md) | ✅ Criado | Docker + Compose + Multi-stage + Security | 5 rules |
| **Kubernetes** | [frameworks/kubernetes.md](frameworks/kubernetes.md) | ✅ Criado | K8s + Helm + Ingress + Monitoring | 5 rules |
| **TypeScript** | [frameworks/typescript.md](frameworks/typescript.md) | ✅ Criado | TypeScript + ESLint + Prettier + Strict Mode | 5 rules |
| **Unity** | [frameworks/unity.md](frameworks/unity.md) | ✅ Criado | Unity 6000+ + URP + New Input System + Mobile | 6 rules |

**Total: 56 Framework Rules (10 frameworks × 5 rules + Unity 6 rules)**

---

## 📚 Standards Completos (Camada 3)

### Aplicação: Contexto amplo por stack

| Standard | Arquivo | Tecnologias | Seções |
|----------|---------|-------------|--------|
| **🌐 Web Development** | [stacks/datametria_std_web_dev.md](stacks/datametria_std_web_dev.md) | Flask + Vue.js 3 + SQLAlchemy | 14 |
| **🐍 Python Automation** | [stacks/datametria_std_python_automation.md](stacks/datametria_std_python_automation.md) | Python + Poetry + Pywinauto | 9 |
| **☁️ AWS Development** | [stacks/datametria_std_aws_development.md](stacks/datametria_std_aws_development.md) | Lambda + CDK + Step Functions | 9 |
| **☁️ GCP Firebase** | [stacks/datametria_std_gcp_firebase.md](stacks/datametria_std_gcp_firebase.md) | Cloud Functions + Firestore | 9 |
| **🎨 UX/UI Design** | [stacks/datametria_std_ux_ui.md](stacks/datametria_std_ux_ui.md) | Figma + Vue Material + WCAG | 12 |
| **📚 Documentation** | [stacks/datametria_std_documentation.md](stacks/datametria_std_documentation.md) | Markdown + Templates | 11 |
| **📊 Logging Enterprise** | [stacks/datametria_std_logging.md](stacks/datametria_std_logging.md) | Python Logging + LGPD/GDPR | 9 |
| **🔒 Security** | [stacks/datametria_std_security.md](stacks/datametria_std_security.md) | OWASP + DevSecOps | 10 |
| **📱 Mobile Flutter** | [stacks/datametria_std_mobile_flutter.md](stacks/datametria_std_mobile_flutter.md) | Flutter + Clean Architecture | 14 |
| **📱 Mobile React Native** | [stacks/datametria_std_mobile_react_native.md](stacks/datametria_std_mobile_react_native.md) | React Native + TypeScript | 15 |
| **🛡️ Reverse Engineering** | [stacks/datametria_std_reverse_engineering_prevention.md](stacks/datametria_std_reverse_engineering_prevention.md) | Obfuscation + Protection | 8 |
| **📊 Data Architecture** | [stacks/datametria_std_data_architecture_engineering.md](stacks/datametria_std_data_architecture_engineering.md) | Spark + Kafka + Airflow | 9 |
| **🤖 AI/ML Development** | [stacks/datametria_std_ai_ml_development.md](stacks/datametria_std_ai_ml_development.md) | MLflow + PyTorch + LLMs | 9 |
| **🏢 Microservices** | [stacks/datametria_std_microservices_architecture.md](stacks/datametria_std_microservices_architecture.md) | Docker + Kubernetes | 12 |
| **🎨 Flow Designer** | [stacks/datametria_std_flow_designer.md](stacks/datametria_std_flow_designer.md) | Figma + Workflow Design | 8 |
| **🤖 Agents Development** | [stacks/datametria_std_agents_development.md](stacks/datametria_std_agents_development.md) | LangChain + OpenAI + Redis | 16 |
| **📱 Mobile Native** | [stacks/datametria_std_mobile_native.md](stacks/datametria_std_mobile_native.md) | Swift + Kotlin + Native APIs | 6 |
| **🎮 Unity AR/VR** | [stacks/datametria_std_unity_ar_vr.md](stacks/datametria_std_unity_ar_vr.md) | Unity + URP + XR Toolkit | 7 |
| **🎮 Unity Mobile 3D** | [stacks/datametria_std_unity_mobile_3d.md](stacks/datametria_std_unity_mobile_3d.md) | Unity 6000+ + URP Mobile + Android/iOS | 6 |
| **🎭 Interactive Avatars** | [stacks/datametria_std_interactive_avatars.md](stacks/datametria_std_interactive_avatars.md) | Ready Player Me + Inworld.ai + LangChain | 9 |

**Total: 21 Standards Completos**

---

## 📋 Templates (Camada 4)

### Aplicação: Referência para geração

#### Projeto (9 templates)

- [template-readme.md](templates/template-readme.md) - Documentação principal
- [template-changelog.md](templates/template-changelog.md) - Histórico de mudanças
- [template-release-notes.md](templates/template-release-notes.md) - Notas de lançamento
- [template-project-conception.md](templates/template-project-conception.md) - Concepção
- [template-project-kickoff.md](templates/template-project-kickoff.md) - Início de projeto
- [template-project-setup.md](templates/template-project-setup.md) - Setup inicial
- [template-developer-guide.md](templates/template-developer-guide.md) - Guia do dev
- [template-developer-onboarding.md](templates/template-developer-onboarding.md) - Onboarding
- [template-memory-bank-creation.md](templates/template-memory-bank-creation.md) - Memory Bank

#### Técnico (9 templates)

- [template-adr.md](templates/template-adr.md) - Decisões arquiteturais
- [template-api-documentation.md](templates/template-api-documentation.md) - API docs
- [template-class-reference.md](templates/template-class-reference.md) - Referência de classes
- [template-docstring-google-style.md](templates/template-docstring-google-style.md) - Docstrings
- [template-database-schema-documentation.md](templates/template-database-schema-documentation.md) - Schema BD
- [template-technical-specification.md](templates/template-technical-specification.md) - Spec técnica
- [template-technical-architecture-diagram.md](templates/template-technical-architecture-diagram.md) - Diagramas
- [template-mermaid-guide.md](templates/template-mermaid-guide.md) - Guia Mermaid
- [template-environment-setup.md](templates/template-environment-setup.md) - Setup ambiente

#### Gestão (8 templates)

- [template-product-backlog.md](templates/template-product-backlog.md) - Backlog
- [template-feature-documentation.md](templates/template-feature-documentation.md) - Features
- [template-code-review.md](templates/template-code-review.md) - Code review
- [template-project-status-report.md](templates/template-project-status-report.md) - Status
- [template-product-guide.md](templates/template-product-guide.md) - Guia do produto
- [template-mvp-planning.md](templates/template-mvp-planning.md) - MVP Planning
- [template-ai-first-time-estimation.md](templates/template-ai-first-time-estimation.md) - Estimativas AI
- [template-flow-designer-conception.md](templates/template-flow-designer-conception.md) - Flow Design

#### Operações (6 templates)

- [template-deployment-guide.md](templates/template-deployment-guide.md) - Deploy
- [template-security-assessment.md](templates/template-security-assessment.md) - Security
- [template-git-workflow.md](templates/template-git-workflow.md) - Git workflow
- [template-cloud-infrastructure-cost-estimation.md](templates/template-cloud-infrastructure-cost-estimation.md) - Cloud Cost
- [template-production-readiness-checklist.md](templates/template-production-readiness-checklist.md) - Production Readiness
- [template-code-standards.md](templates/template-code-standards.md) - Code Standards

#### Mobile (4 templates)

- [template-mobile-app-architecture.md](templates/template-mobile-app-architecture.md) - Arquitetura
- [template-app-store-submission.md](templates/template-app-store-submission.md) - App Store
- [template-mobile-performance-guide.md](templates/template-mobile-performance-guide.md) - Performance
- [template-mobile-release-checklist.md](templates/template-mobile-release-checklist.md) - Release Checklist

#### Checklists (6 templates)

- [template-accessibility-review-checklist.md](templates/template-accessibility-review-checklist.md)
- [template-code-review-checklist.md](templates/template-code-review-checklist.md)
- [template-performance-review-checklist.md](templates/template-performance-review-checklist.md)
- [template-security-review-checklist.md](templates/template-security-review-checklist.md)
- [template-markdown-linting-guide.md](templates/template-markdown-linting-guide.md)

#### Comercial (1 template)

- [template-proposal.md](templates/template-proposal.md) - Proposta Comercial/Técnica

**Total: 42 Templates**

---

## 🧠 Memory Bank (Camada 5)

### Aplicação: Contexto persistente do projeto

| Arquivo | Propósito | Quando Atualizar | Status |
|---------|-----------|------------------|--------|
| [memory/idea.md](memory/idea.md) | Visão do produto, objetivos, KPIs, roadmap | Mudanças de escopo | ✅ Criado |
| [memory/vibe.md](memory/vibe.md) | Cultura da equipe, valores, workflow | Mudanças de processo | ✅ Criado |
| [memory/state.md](memory/state.md) | Snapshot técnico, stack, métricas | Mudanças no stack | ✅ Criado |
| [memory/decisions.md](memory/decisions.md) | ADRs históricos (14 decisões) | Decisões arquiteturais | ✅ Criado |
| [memory/q-vibes-memory-banking.md](memory/q-vibes-memory-banking.md) | Instruções para Amazon Q | Melhorias no processo | ✅ Criado |

**Total: 5 Memory Bank Files (contexto persistente completo)**

---

## 🎯 Hierarquia de Aplicação

### Ordem de Prioridade (Amazon Q Developer)

```
1. Rules Atômicas (01-06)        → Aplicadas SEMPRE
   ↓
2. Rules de Framework             → Quando framework detectado
   ↓
3. Standards Completos            → Contexto amplo
   ↓
4. Templates                      → Referência para geração
   ↓
5. Memory Bank                    → Contexto persistente
```

### Exemplo Prático: API FastAPI

**Amazon Q carrega automaticamente:**

1. ✅ **01-code-style.md** → Naming conventions Python, formatação Black
2. ✅ **02-architecture.md** → Clean Architecture, DI, Repository Pattern
3. ✅ **03-security.md** → JWT, Pydantic validation, secrets em env
4. ✅ **04-testing.md** → Pytest, coverage 80%, AAA pattern
5. ✅ **05-performance.md** → Async/await, caching Redis, indexing
6. ✅ **06-documentation.md** → Docstrings, OpenAPI automático
7. ✅ **frameworks/fastapi.md** → Pydantic models, async endpoints, DI
8. 📚 **stacks/datametria_std_web_dev.md** → Contexto amplo web
9. 📋 **templates/template-api-documentation.md** → Referência para docs
10. 🧠 **memory/state.md** → Stack tecnológico e configurações
11. 🧠 **memory/decisions.md** → Decisões sobre FastAPI, PostgreSQL, Redis

---

## 🚀 Fluxos de Trabalho por Stack

### Web Development (Flask + Vue.js)

**Rules aplicadas:**

- 01-code-style.md (Python + JavaScript)
- 02-architecture.md (Clean Architecture)
- 03-security.md (JWT, CORS, CSRF)
- 04-testing.md (Pytest + Jest)
- 05-performance.md (Caching, async)
- 06-documentation.md (OpenAPI)
- frameworks/flask.md (Blueprints, Application Factory, SQLAlchemy)
- frameworks/vuejs.md (Composition API, Pinia, Vue Router)

**Standard:** stacks/datametria_std_web_dev.md

**Memory Bank:**

- memory/state.md (Stack: Flask 3.0+, Vue.js 3.3+, PostgreSQL 16+)
- memory/decisions.md (ADRs: Poetry, Vue.js 3, PostgreSQL)

**Templates:**

- template-readme.md
- template-api-documentation.md
- template-deployment-guide.md
- template-technical-specification.md

### Mobile Flutter

**Rules aplicadas:**

- 01-code-style.md (Dart)
- 02-architecture.md (Clean Architecture)
- 03-security.md (Secure storage, biometrics)
- 04-testing.md (Widget tests, integration)
- 05-performance.md (Lazy loading, caching)
- 06-documentation.md (Dartdoc)
- frameworks/flutter.md (BLoC Pattern, Clean Architecture, Widget Composition)

**Standard:** stacks/datametria_std_mobile_flutter.md

**Memory Bank:**

- memory/state.md (Stack: Flutter 3.16+, Dart 3.2+)
- memory/decisions.md (ADRs: BLoC Pattern, Clean Architecture)

**Templates:**

- template-mobile-app-architecture.md
- template-app-store-submission.md
- template-mobile-performance-guide.md

### Mobile React Native

**Rules aplicadas:**

- 01-code-style.md (TypeScript)
- 02-architecture.md (Clean Architecture)
- 03-security.md (Secure storage, JWT)
- 04-testing.md (Jest, React Native Testing Library)
- 05-performance.md (Hermes, optimization)
- 06-documentation.md (TSDoc)
- frameworks/react-native.md (TypeScript, Zustand, React Navigation)

**Standard:** stacks/datametria_std_mobile_react_native.md

**Memory Bank:**

- memory/state.md (Stack: React Native 0.73+, TypeScript 5.3+, Expo 50+)
- memory/decisions.md (ADRs: Zustand, Hermes Engine)

**Templates:**

- template-mobile-app-architecture.md
- template-app-store-submission.md
- template-mobile-performance-guide.md

### AI/ML Development

**Rules aplicadas:**

- 01-code-style.md (Python)
- 02-architecture.md (MLOps pipeline)
- 03-security.md (Model security, data privacy)
- 04-testing.md (Model testing, data validation)
- 05-performance.md (Model optimization)
- 06-documentation.md (Model cards, datasets)

**Standard:** stacks/datametria_std_ai_ml_development.md

**Memory Bank:**

- memory/state.md (Stack: PyTorch 2.1+, MLflow 2.9+, OpenAI)
- memory/decisions.md (ADRs: AI-First Development)

**Templates:**

- template-technical-specification.md
- template-api-documentation.md

---

## 📊 Métricas de Conformidade

### Objetivos de Qualidade

| Categoria | Meta | Validação |
|-----------|------|-----------|
| **Code Style** | 100% | Black, Prettier, ESLint |
| **Architecture** | 95% | Import linter, architecture tests |
| **Security** | 100% | Bandit, detect-secrets, OWASP |
| **Testing** | 80% coverage | pytest-cov, Jest coverage |
| **Performance** | 95% | EXPLAIN ANALYZE, Lighthouse |
| **Documentation** | 100% | markdownlint, pydocstyle |

### Dashboard de Conformidade

```bash
# Verificar conformidade geral
python scripts/check_compliance.py

# Resultado esperado:
# ✅ Code Style: 98%
# ✅ Architecture: 95%
# ✅ Security: 100%
# ✅ Testing: 87%
# ✅ Performance: 92%
# ✅ Documentation: 100%
#
# Score Geral: 95% ✅
```

---

## 🔄 Uso com Amazon Q Developer

### Comandos Essenciais

```bash
# Incluir contexto específico
@file 01-code-style.md
@file 02-architecture.md

# Incluir standard completo
@file stacks/datametria_std_web_dev.md

# Incluir template
@file templates/template-readme.md

# Contexto automático (recomendado)
@workspace
```

### Prompts Eficientes

#### Criar novo projeto

```
Contexto: @file 00-master-context.md
Tarefa: Criar API REST com FastAPI
Restrições: Seguir rules 01-06, usar Clean Architecture
Formato: Estrutura completa de pastas + código base
```

#### Refatorar código existente

```
Contexto: @file src/services/user_service.py
Tarefa: Refatorar seguindo rules de arquitetura
Restrições: Aplicar Rule 2.2 (DI) e Rule 2.3 (Repository)
Formato: Código refatorado + testes
```

#### Gerar documentação

```
Contexto: @workspace
Tarefa: Gerar README completo
Restrições: Seguir Rule 2.1 (template-readme.md)
Formato: README.md com badges, comandos, pré-requisitos
```

---

## 📚 Referências

### Documentos Principais

- [AmazonQ-Guidelines.md](AmazonQ-Guidelines.md) - Princípios de design de rules
- [ESTRUTURA_RULES_IDEAL.md](ESTRUTURA_RULES_IDEAL.md) - Proposta de estrutura
- [REORGANIZACAO_COMPLETA.md](REORGANIZACAO_COMPLETA.md) - Sumário de entregas
- [README.md](README.md) - Guia de uso da estrutura

### Links Externos

- [Amazon Q Developer Documentation](https://docs.aws.amazon.com/amazonq/)
- [Keep a Changelog](https://keepachangelog.com/)
- [Semantic Versioning](https://semver.org/)
- [OWASP Top 10](https://owasp.org/www-project-top-ten/)

---

## ✅ Checklist de Uso

### Para Novos Projetos

- [ ] Ler 00-master-context.md (este arquivo)
- [ ] Ler memory/q-vibes-memory-banking.md (instruções Amazon Q)
- [ ] Consultar memory/idea.md (visão do produto)
- [ ] Consultar memory/state.md (stack tecnológico)
- [ ] Identificar stack tecnológico do projeto
- [ ] Consultar rules atômicas (01-06)
- [ ] Consultar framework rules se aplicável
- [ ] Consultar standard completo do stack
- [ ] Usar templates apropriados
- [ ] Configurar ferramentas de automação
- [ ] Validar conformidade com rules

### Para Projetos Existentes

- [ ] Auditar conformidade com rules atômicas
- [ ] Consultar memory/decisions.md (decisões anteriores)
- [ ] Identificar gaps e inconsistências
- [ ] Criar plano de migração gradual
- [ ] Aplicar rules atômicas primeiro
- [ ] Aplicar framework rules depois
- [ ] Atualizar documentação com templates
- [ ] Configurar CI/CD com validação
- [ ] Documentar decisões em memory/decisions.md

### Para Manutenção Contínua

- [ ] Revisar rules mensalmente
- [ ] Atualizar memory/state.md (métricas, stack)
- [ ] Atualizar memory/decisions.md (novas decisões)
- [ ] Atualizar memory/idea.md (roadmap, KPIs)
- [ ] Monitorar métricas de conformidade
- [ ] Refinar rules baseado em feedback
- [ ] Treinar equipe em novas rules
- [ ] Revisar Memory Bank trimestralmente

---

**Versão:** 4.3.0
**Data:** 17/11/2025
**Próxima revisão:** 17/02/2026
**Mantido por:** Vander Loto - CTO DATAMETRIA
