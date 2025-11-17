# DATAMETRIA Standards - Rules Structure

**Versão:** 4.2.0
**Data:** 07/11/2025
**Baseado em:** AmazonQ-Guidelines v2.0

---

## 📁 Estrutura de Diretórios

```
.amazonq/rules/
├── 00-master-context.md              # Índice central - ponto de entrada
├── 01-code-style.md                  # Rules de formatação e estilo
├── 02-architecture.md                # Rules de arquitetura
├── 03-security.md                    # Rules de segurança
├── 04-testing.md                     # Rules de testes
├── 05-performance.md                 # Rules de performance
├── 06-documentation.md               # Rules de documentação
├── frameworks/                       # Rules específicas por framework
│   ├── python.md                     # 5 rules Python
│   ├── flask.md                      # 5 rules Flask
│   ├── vuejs.md                      # 5 rules Vue.js 3
│   ├── fastapi.md                    # 5 rules FastAPI
│   ├── flutter.md                    # 5 rules Flutter
│   ├── react-native.md               # 5 rules React Native
│   ├── sqlalchemy.md                 # 5 rules SQLAlchemy
│   ├── docker.md                     # 5 rules Docker
│   ├── kubernetes.md                 # 5 rules Kubernetes
│   └── typescript.md                 # 5 rules TypeScript
├── stacks/                           # Standards completos por stack
│   ├── datametria_std_web_dev.md
│   ├── datametria_std_python_automation.md
│   ├── datametria_std_aws_development.md
│   └── [20 standards completos]
├── templates/                        # Templates de documentação
│   ├── template-readme.md
│   ├── template-adr.md
│   └── [42 templates]
└── memory/                           # Memory Bank (contexto persistente)
    ├── idea.md                       # Visão do produto, objetivos, KPIs
    ├── vibe.md                       # Cultura da equipe, valores
    ├── state.md                      # Snapshot técnico, stack, métricas
    ├── decisions.md                  # ADRs históricos (14 decisões)
    └── q-vibes-memory-banking.md     # Instruções para Amazon Q
```

---

## 🎯 Hierarquia de Aplicação

### Ordem de Prioridade (Amazon Q Developer)

1. **Rules Atômicas** (01-06-*.md) - Aplicadas SEMPRE
2. **Rules de Framework** (frameworks/*.md) - Quando framework detectado
3. **Standards Completos** (stacks/*.md) - Contexto amplo
4. **Templates** (templates/*.md) - Referência para geração
5. **Memory Bank** (memory/*.md) - Contexto persistente do projeto

---

## 📖 Rules Atômicas (Camada 1)

### 01-code-style.md

**5 Rules de Estilo de Código:**

- Rule 1.1: Naming Conventions por Linguagem
- Rule 1.2: Formatação Automática (Black, Prettier)
- Rule 1.3: Imports Organization
- Rule 1.4: Funções Máximo 50 Linhas
- Rule 1.5: Máximo 3 Parâmetros

### 02-architecture.md

**5 Rules de Arquitetura:**

- Rule 2.1: Clean Architecture em Camadas
- Rule 2.2: Dependency Injection Obrigatória
- Rule 2.3: Repository Pattern para Persistência
- Rule 2.4: Feature Folders (Vertical Slicing)
- Rule 2.5: API Versioning Obrigatório

### 03-security.md

**5 Rules de Segurança:**

- Rule 3.1: JWT com Refresh Tokens
- Rule 3.2: Input Validation com Pydantic/Zod
- Rule 3.3: Secrets em Variáveis de Ambiente
- Rule 3.4: Rate Limiting em APIs Públicas
- Rule 3.5: SQL Injection Prevention

### 04-testing.md

**5 Rules de Testes:**

- Rule 4.1: Cobertura Mínima 80%
- Rule 4.2: AAA Pattern (Arrange-Act-Assert)
- Rule 4.3: Naming Convention para Testes
- Rule 4.4: Fixtures e Factories
- Rule 4.5: Testes de Integração Separados

### 05-performance.md

**5 Rules de Performance:**

- Rule 5.1: Database Indexing Obrigatório
- Rule 5.2: N+1 Query Prevention
- Rule 5.3: Caching Strategy
- Rule 5.4: Async/Await para I/O
- Rule 5.5: Lazy Loading e Pagination

### 06-documentation.md

**5 Rules de Documentação:**

- Rule 2.1: README Obrigatório
- Rule 2.2: ADR para Decisões Arquiteturais
- Rule 2.3: Docstrings Google Style
- Rule 2.4: API Documentation OpenAPI 3.0
- Rule 2.5: Changelog Keep a Changelog

**Total: 30 Rules Atômicas**

---

## 🔧 Frameworks (Camada 2)

### Criados (✅ Completo)

| Framework | Arquivo | Status | Rules |
|-----------|---------|--------|-------|
| **Python** | [frameworks/python.md](frameworks/python.md) | ✅ Criado | 5 rules |
| **Flask** | [frameworks/flask.md](frameworks/flask.md) | ✅ Criado | 5 rules |
| **Vue.js 3** | [frameworks/vuejs.md](frameworks/vuejs.md) | ✅ Criado | 5 rules |
| **FastAPI** | [frameworks/fastapi.md](frameworks/fastapi.md) | ✅ Criado | 5 rules |
| **Flutter** | [frameworks/flutter.md](frameworks/flutter.md) | ✅ Criado | 5 rules |
| **React Native** | [frameworks/react-native.md](frameworks/react-native.md) | ✅ Criado | 5 rules |
| **SQLAlchemy** | [frameworks/sqlalchemy.md](frameworks/sqlalchemy.md) | ✅ Criado | 5 rules |
| **Docker** | [frameworks/docker.md](frameworks/docker.md) | ✅ Criado | 5 rules |
| **Kubernetes** | [frameworks/kubernetes.md](frameworks/kubernetes.md) | ✅ Criado | 5 rules |
| **TypeScript** | [frameworks/typescript.md](frameworks/typescript.md) | ✅ Criado | 5 rules |

**Total: 50 Framework Rules (10 frameworks × 5 rules)**

---

## 📊 Consumo de Tokens Amazon Q

### Budget e Configurações

**Amazon Q Token Budget**: 200,000 tokens

| Configuração | Arquivos | Tokens | % Budget | Disponível |
|--------------|----------|--------|----------|------------|
| **Mínimo** (9 arquivos) | 7 rules + 1 framework + 1 standard | ~51K | 25.6% | 149K (74.4%) |
| **Recomendado** (10 arquivos) | + memory/q-vibes | ~55K | 27.5% | 145K (72.5%) |
| **API Simples** (8 arquivos) | 7 rules + 1 framework | ~40K | 20% | 160K (80%) |
| **Full Stack** (12 arquivos) | + 2 standards + memory | ~70K | 35% | 130K (65%) |

### Análise de Impacto

**Por que 27.5% é ACEITÁVEL:**

- ✅ Deixa 72.5% disponível para código e conversação
- ✅ Fornece contexto completo e consistente
- ✅ Garante conformidade com todos os padrões
- ✅ Permite geração de código complexo
- ✅ Reduz necessidade de re-explicar regras

**Estimativa por Arquivo:**

- Master context (00-master-context.md): ~6K tokens
- Rules atômicas (01-06): ~3-9K tokens cada
- Framework rules: ~5-7K tokens
- Standards completos: ~11K tokens
- Memory bank: ~4K tokens

### Otimizações Possíveis

**Se precisar reduzir consumo:**

1. **Configuração Mínima** (25.6%): Apenas essencial
2. **Sem Memory Bank** (23%): Remove q-vibes-memory-banking.md
3. **API Simples** (20%): Remove standard completo
4. **Projeto Específico** (15%): Apenas 1-2 rules atômicas relevantes

**Recomendação**: Manter configuração de 27.5% para máxima qualidade e conformidade.

---

## 📚 Standards Completos (Camada 3)

### 20 Standards por Stack Tecnológico

| Standard | Tecnologias | Seções |
|----------|-------------|--------|
| **Web Development** | Flask + Vue.js 3 + SQLAlchemy | 14 |
| **Python Automation** | Python + Poetry + Pywinauto | 9 |
| **AWS Development** | Lambda + CDK + Step Functions | 9 |
| **GCP Firebase** | Cloud Functions + Firestore | 9 |
| **UX/UI Design** | Figma + Vue Material + WCAG | 12 |
| **Documentation** | Markdown + Templates | 11 |
| **Logging Enterprise** | Python Logging + LGPD/GDPR | 9 |
| **Security Development** | OWASP + DevSecOps | 10 |
| **Mobile Flutter** | Flutter + Clean Architecture | 14 |
| **Mobile React Native** | React Native + TypeScript | 15 |
| **Mobile Native** | iOS Swift 5.9+ + Android Kotlin 1.9+ | 6 |
| **Unity AR/VR** | Unity 2023.2+ + AR Foundation + XR Toolkit | 7 |
| **Reverse Engineering** | Obfuscation + Protection | 8 |
| **Data Architecture** | Spark + Kafka + Airflow | 9 |
| **AI/ML Development** | MLflow + PyTorch + LLMs | 9 |
| **Microservices** | Docker + Kubernetes | 12 |
| **Flow Designer** | Figma + Workflow Design | 8 |
| **Agents Development v2.0** | LangChain + OpenAI + LLMOrchestrator | 16 |
| **Interactive Avatars** | Ready Player Me + Inworld.ai + LangChain | 9 |
| **Interactive Avatars** | Ready Player Me + Inworld.ai + LangChain | 9 |

---

## 📋 Templates (Camada 4)

### 42 Templates Profissionais

**Categorias:**

- **Projeto** (9): README, Changelog, Release Notes, Conception, Kickoff, Setup, Developer Guide, Onboarding, Memory Bank Creation
- **Técnico** (9): ADR, API Docs, Class Reference, Docstring, DB Schema, Tech Spec, Architecture, Mermaid, Environment
- **Gestão** (8): Product Backlog, Feature Docs, Code Review, Status Report, MVP Planning, AI-First Estimation, Git Workflow, Flow Designer
- **Operações** (6): Deployment, Product Guide, Security Assessment, Cloud Cost, Production Readiness, Code Standards
- **Mobile** (4): Mobile Architecture, App Store, Performance, Release Checklist
- **Checklists** (6): Code Review, Security, Performance, Accessibility, Markdown Linting, Compliance Dashboard
- **Comercial** (1): Proposal (Proposta Comercial/Técnica)

---

## 🧠 Memory Bank (Camada 5)

### Contexto Persistente do Projeto (✅ Completo)

| Arquivo | Propósito | Quando Atualizar | Status |
|---------|-----------|------------------|--------|
| [memory/idea.md](memory/idea.md) | Visão do produto, objetivos, KPIs, roadmap | Mudanças de escopo | ✅ Criado |
| [memory/vibe.md](memory/vibe.md) | Cultura da equipe, valores, workflow | Mudanças de processo | ✅ Criado |
| [memory/state.md](memory/state.md) | Snapshot técnico, stack, métricas | Mudanças no stack | ✅ Criado |
| [memory/decisions.md](memory/decisions.md) | ADRs históricos (14 decisões) | Decisões arquiteturais | ✅ Criado |
| [memory/q-vibes-memory-banking.md](memory/q-vibes-memory-banking.md) | Instruções para Amazon Q | Melhorias no processo | ✅ Criado |

**Total: 5 Memory Bank Files (contexto persistente completo)**

---

## 📊 Resumo Executivo

### Estatísticas v4.2.0

| Componente | Quantidade | Status |
|------------|------------|--------|
| **Rules Atômicas** | 30 (6 × 5) | ✅ 100% |
| **Framework Rules** | 50 (10 × 5) | ✅ 100% |
| **Standards Completos** | 20 | ✅ 100% |
| **Templates** | 42 | ✅ 100% |
| **Memory Bank** | 5 arquivos | ✅ 100% |

### Cobertura Tecnológica

**Backend**: Python, Flask, FastAPI, SQLAlchemy
**Frontend**: Vue.js 3, TypeScript
**Mobile**: Flutter, React Native, Swift, Kotlin
**Cloud**: AWS, GCP, Docker, Kubernetes
**AI/ML**: LangChain, OpenAI, PyTorch, MLflow
**3D/AR/VR**: Unity, Ready Player Me, Inworld.ai

---

## 🚀 Como Usar

### 1. Leia o Master Context

Comece sempre por `00-master-context.md` - é o índice central que explica toda a estrutura.

### 2. Selecione Rules Relevantes

**Mínimo obrigatório** (9 arquivos):
- ✅ 00-master-context.md
- ✅ 01-06-*.md (6 rules atômicas)
- ✅ frameworks/[seu-framework].md
- ✅ stacks/[seu-stack].md

**Recomendado** (+1 arquivo):
- ✅ memory/q-vibes-memory-banking.md

### 3. Configure Amazon Q

Selecione explicitamente os arquivos no Amazon Q Developer - ele NÃO carrega automaticamente!

### 4. Desenvolva com IA

90% do código será gerado pelo Amazon Q seguindo todos os padrões automaticamente.

---

## 📞 Contato

**CTO**: Vander Loto - vander.loto@datametria.io
**GitHub**: [github.com/datametria](https://github.com/datametria)
**Discord**: [discord.gg/kKYGmCC3](https://discord.gg/kKYGmCC3)

---

**Versão**: 4.2.0 | **Data**: 07/11/2025 | **Mantido por**: Vander Loto - CTO DATAMETRIA |

**Total: 5 Memory Bank Files (contexto persistente completo)**

---

## 🚀 Como Usar

### ⚠️ Seleção Explícita de Rules

**IMPORTANTE**: Amazon Q **NÃO carrega arquivos automaticamente**!

Você deve selecionar explicitamente no Amazon Q Developer:

#### Mínimo Obrigatório (9 arquivos):

```
✅ 00-master-context.md              (sempre)
✅ 01-06-*.md                        (6 rules atômicas - sempre)
✅ frameworks/[seu-framework].md     (1 framework que usa)
✅ stacks/[seu-stack].md             (1 standard que usa)
```

#### Recomendado (+1 arquivo = 10 total):

```
✅ memory/q-vibes-memory-banking.md  (instruções)
```

#### Exemplo: Projeto FastAPI

```
Selecione no Amazon Q Rules:
✅ 00-master-context.md
✅ 01-code-style.md
✅ 02-architecture.md
✅ 03-security.md
✅ 04-testing.md
✅ 05-performance.md
✅ 06-documentation.md
✅ frameworks/fastapi.md
✅ stacks/datametria_std_web_dev.md
✅ memory/q-vibes-memory-banking.md

Total: 10 arquivos (~55K tokens = 27.5% do budget)
```

### Para Desenvolvedores

**Consultar Rules:**

```bash
# Ver todas as rules de código
cat 01-code-style.md

# Ver rules de segurança
cat 03-security.md

# Ver standard completo de Web Dev
cat stacks/datametria_std_web_dev.md
```

**Usar Templates:**

```bash
# Copiar template de README
cp templates/template-readme.md /seu-projeto/README.md

# Copiar template de ADR
cp templates/template-adr.md /seu-projeto/docs/adr/0001-decisao.md
```

---

## 📊 Estatísticas v4.1.0

| Componente | Quantidade | Status |
|------------|------------|--------|
| Rules Atômicas | 30 (6 × 5) | ✅ 100% |
| Framework Rules | 30 (6 × 5) | ✅ 100% |
| Standards | 20 | ✅ 100% |
| Templates | 42+ | ✅ 100% |
| Memory Bank | 5 | ✅ 100% |

**Total**: 126+ componentes completos

---

## 📊 Benefícios da Nova Estrutura

### Para Amazon Q

| Antes | Depois | Benefício |
|-------|--------|-----------|vedores

**Consultar Rules:**

```bash
# Ver todas as rules de código
cat 01-code-style.md

# Ver rules de segurança
cat 03-security.md

# Ver standard completo de Web Dev
cat stacks/datametria_std_web_dev.md
```

**Usar Templates:**

```bash
# Copiar template de README
cp templates/template-readme.md /seu-projeto/README.md

# Copiar template de ADR
cp templates/template-adr.md /seu-projeto/docs/adr/0001-decisao.md
```

---

## 📊 Benefícios da Nova Estrutura

### Para Amazon Q

| Antes | Depois | Benefício |
|-------|--------|-----------|
| 16 arquivos de 100+ seções | 6 rules atômicas + contexto | 90% menos tokens |
| Busca em docs longos | Acesso direto a rules | 5x mais rápido |
| Padrões inconsistentes | Rules específicas | 95% conformidade |

### Para Desenvolvedores

| Antes | Depois | Benefício |
|-------|--------|-----------|
| Lê 100+ seções | Lê 1 rule específica | 80% menos tempo |
| Padrões vagos | Rules com exemplos ✅❌ | Clareza absoluta |
| Sem ferramentas | Ferramentas listadas | Automação fácil |

---

## 🔄 Migração de Projetos Existentes

### Checklist

- [ ] Ler 00-master-context.md para visão geral
- [ ] Ler memory/q-vibes-memory-banking.md (instruções Amazon Q)
- [ ] Consultar memory/idea.md (visão do produto)
- [ ] Consultar memory/state.md (stack tecnológico)
- [ ] Revisar rules atômicas (01-06)
- [ ] Identificar frameworks usados
- [ ] Consultar framework rules se aplicável
- [ ] Consultar standard completo do stack
- [ ] Usar templates para documentação
- [ ] Configurar ferramentas de automação
- [ ] Validar conformidade com rules

---

## 📝 Próximos Passos

### Concluído ✅

- [x] Criar rules de frameworks (Flask, Vue.js, FastAPI, Flutter, React Native) - **COMPLETO**
- [x] Atualizar 00-master-context.md com nova estrutura - **COMPLETO**
- [x] Criar Memory Bank completo (5 arquivos) - **COMPLETO**

### Curto Prazo (2 semanas)

- [ ] Configurar CI/CD para validação de rules
- [ ] Criar dashboard de conformidade
- [ ] Treinar equipe na nova estrutura

### Médio Prazo (1 mês)

- [ ] Métricas automatizadas de conformidade
- [ ] Relatórios mensais de qualidade
- [ ] Refinamento contínuo de rules

### Longo Prazo (3 meses)

- [ ] Criar frameworks adicionais (SQLAlchemy, Docker, Kubernetes)
- [ ] Open source do framework
- [ ] Comunidade externa ativa

---

**Mantido por:** Vander Loto - CTO DATAMETRIA
**Última atualização:** 07/11/2025
