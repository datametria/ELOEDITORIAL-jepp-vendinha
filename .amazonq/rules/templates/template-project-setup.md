# Template de Setup de Projeto

<div align="center">

## Guia Estruturado para Configuração de Novos Projetos - Framework Enterprise

[![Setup](https://img.shields.io/badge/setup-ready-green)](https://datametria.io)
[![Project](https://img.shields.io/badge/project-template-orange)](https://datametria.io)
[![Automation](https://img.shields.io/badge/automation-enabled-blue)](https://datametria.io)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)
[![Amazon Q](https://img.shields.io/badge/Amazon%20Q-Ready-yellow)](https://aws.amazon.com/q/)

[🔗 Template Original](link) • [🔗 Diretrizes](link) • [🔗 Exemplos](link)

[🎯 Informações](#-informações-do-projeto) • [🏗️ Arquitetura](#️-arquitetura-do-projeto) • [🛠️ Setup](#️-setup-técnico) •
[🚀 Deploy](#-configuração-de-deploy) • [🧪 Testes](#-configuração-de-testes)

</div>

---

## 📋 Índice

- [🎯 Visão Geral](#-visão-geral)
- [📋 Informações do Projeto](#-informações-do-projeto)
- [🎯 Objetivos do Projeto](#-objetivos-do-projeto)
- [🏗️ Arquitetura do Projeto](#️-arquitetura-do-projeto)
- [🛠️ Setup Técnico](#️-setup-técnico)
- [🔧 Configurações de Desenvolvimento](#-configurações-de-desenvolvimento)
- [🧪 Configuração de Testes](#-configuração-de-testes)
- [🚀 Configuração de Deploy](#-configuração-de-deploy)
- [📊 Monitoramento e Observabilidade](#-monitoramento-e-observabilidade)
- [🔒 Segurança](#-segurança)
- [📚 Documentação](#-documentação)
- [📈 Exemplos Práticos](#-exemplos-práticos)
- [🔗 Referências](#-referências)

---

## 🎯 Visão Geral

### Framework de Setup de Projetos

Este template fornece um guia estruturado para configuração de novos projetos seguindo os padrões DATAMETRIA, garantindo consistência, qualidade e melhores práticas desde o início.

#### Fluxo de Setup

```mermaid
flowchart TD
    A[Planejamento do Projeto] --> B[Definição da Arquitetura]
    B --> C[Setup do Repositório]
    C --> D[Configuração do Ambiente]
    D --> E[Setup de Desenvolvimento]
    E --> F[Configuração de Testes]
    F --> G[Setup de CI/CD]
    G --> H[Deploy e Monitoramento]
    H --> I[Documentação Final]

    J[Validação] --> E
    K[Code Review] --> G
    L[Security Check] --> H
```

---

## 📋 Informações do Projeto

| Campo | Descrição | Exemplo |
|-------|-----------|---------|
| **Nome do Projeto** | [Nome do projeto] | Sistema de Gestão de Inventário |
| **Código do Projeto** | [PROJ-YYYY-NNN] | PROJ-2025-004 |
| **Tipo de Projeto** | [Web App / Mobile App / API / Microservice] | Web App + API |
| **Stack Tecnológico** | [React/Vue + Node.js / Flutter / Python + Flask] | Vue.js 3 + Flask + PostgreSQL |
| **Responsável Técnico** | [Nome do tech lead] | Dalila Rodrigues |
| **Product Owner** | [Nome do PO] | João Silva |
| **Data de Início** | [DD/MM/AAAA] | 25/01/2025 |
| **Prazo Estimado** | [DD/MM/AAAA] | 25/07/2025 |
| **Orçamento** | [R$ X.XXX,XX] | R$ 320.000,00 |
| **Prioridade** | [Alta/Média/Baixa] | Alta |
| **Metodologia** | [Scrum/Kanban/Híbrida] | Scrum |

---

## 🎯 Objetivos do Projeto

### Problema a Resolver

**Contexto**: A empresa atualmente gerencia inventário através de planilhas Excel desconectadas, causando inconsistências, perda de dados e falta de rastreabilidade em tempo real.

**Impacto**:

- Perda de R$ 50.000/mês em produtos não localizados
- 4 horas/dia gastas em reconciliação manual
- 25% de erro em contagens de estoque
- Falta de auditoria e compliance

### Objetivos Principais

- [ ] **Centralização do Inventário**: Sistema único para gestão de todos os produtos
- [ ] **Rastreabilidade Completa**: Histórico completo de movimentações
- [ ] **Automação de Processos**: Redução de 80% em tarefas manuais
- [ ] **Dashboards em Tempo Real**: Visibilidade instantânea do estoque
- [ ] **Integração com ERP**: Sincronização automática com sistemas existentes

### Critérios de Sucesso

| Métrica | Valor Atual | Meta | Prazo | Método de Medição |
|---------|-------------|------|-------|-------------------|
| **Precisão do Inventário** | 75% | 98% | 3 meses | Auditoria mensal |
| **Tempo de Reconciliação** | 4h/dia | 30min/dia | 2 meses | Métricas do sistema |
| **Redução de Perdas** | R$ 50k/mês | R$ 5k/mês | 4 meses | Relatório financeiro |
| **Adoção do Sistema** | 0% | 95% | 1 mês | Analytics de uso |
| **Satisfação dos Usuários** | N/A | NPS > 70 | 2 meses | Pesquisa trimestral |

---

## 🏗️ Arquitetura do Projeto

### Stack Tecnológico DATAMETRIA

#### Frontend

- **Framework**: Vue.js 3 + TypeScript
- **UI Library**: Vue Material + Tailwind CSS
- **State Management**: Pinia
- **Build Tool**: Vite
- **Testing**: Vitest + Vue Test Utils
- **Linting**: ESLint + Prettier

#### Backend

- **Framework**: Flask + Python 3.11
- **Database**: PostgreSQL 15
- **ORM**: SQLAlchemy 2.0
- **Cache**: Redis 7
- **Message Queue**: Celery + Redis
- **API Documentation**: Flask-RESTX (Swagger)

#### Infrastructure

- **Cloud Provider**: AWS
- **Containerization**: Docker + ECS
- **Database**: RDS PostgreSQL
- **Cache**: ElastiCache Redis
- **CI/CD**: GitHub Actions
- **Monitoring**: CloudWatch + Grafana
- **CDN**: CloudFront

### Diagrama de Arquitetura

```mermaid
graph TB
    subgraph "Client Layer"
        WEB[Vue.js Web App]
        MOBILE[Mobile PWA]
        ADMIN[Admin Dashboard]
    end

    subgraph "API Gateway"
        GATEWAY[AWS API Gateway]
        AUTH[Authentication]
        RATE[Rate Limiting]
    end

    subgraph "Application Layer"
        API[Flask API Server]
        WORKER[Celery Workers]
        SCHEDULER[Task Scheduler]
    end

    subgraph "Data Layer"
        DB[(PostgreSQL RDS)]
        CACHE[(Redis ElastiCache)]
        S3[(S3 Storage)]
    end

    subgraph "External Services"
        ERP[ERP System]
        EMAIL[Email Service]
        SMS[SMS Gateway]
    end

    subgraph "Monitoring"
        LOGS[CloudWatch Logs]
        METRICS[CloudWatch Metrics]
        GRAFANA[Grafana Dashboard]
    end

    WEB --> GATEWAY
    MOBILE --> GATEWAY
    ADMIN --> GATEWAY

    GATEWAY --> AUTH
    AUTH --> API
    GATEWAY --> RATE
    RATE --> API

    API --> DB
    API --> CACHE
    API --> S3
    API --> WORKER

    WORKER --> ERP
    WORKER --> EMAIL
    WORKER --> SMS

    API --> LOGS
    API --> METRICS
    LOGS --> GRAFANA
    METRICS --> GRAFANA
```

### Integrações Necessárias

| Sistema | Tipo | Protocolo | Frequência | Criticidade |
|---------|------|-----------|------------|-------------|
| **ERP Corporativo** | REST API | HTTPS/OAuth2 | Tempo real | Alta |
| **Sistema Financeiro** | REST API | HTTPS/JWT | Diária | Alta |
| **Email Service** | SMTP/API | HTTPS | Conforme demanda | Média |
| **SMS Gateway** | REST API | HTTPS | Conforme demanda | Baixa |
| **Backup System** | S3 API | HTTPS | Diária | Alta |

---

## 🛠️ Setup Técnico

### Pré-requisitos

#### Ferramentas Necessárias

```bash
# Node.js para frontend
node --version  # >= 18.0.0
npm --version   # >= 9.0.0

# Python para backend
python --version  # >= 3.11.0
pip --version

# Docker para containerização
docker --version
docker-compose --version

# Git para versionamento
git --version

# AWS CLI para deploy
aws --version
```

#### Contas e Acessos

- [ ] **GitHub Enterprise** - Repositório criado com permissões
- [ ] **AWS Account** - Conta configurada com IAM roles
- [ ] **PostgreSQL** - Instância RDS provisionada
- [ ] **Redis** - ElastiCache configurado
- [ ] **Domain** - Domínio registrado (inventario.datametria.io)
- [ ] **SSL Certificate** - Certificado ACM configurado
- [ ] **Monitoring** - CloudWatch e Grafana setup

### Estrutura de Pastas

#### Projeto Full-Stack Enterprise

```
inventario-datametria/
├── frontend/                   # Vue.js 3 Application
│   ├── src/
│   │   ├── components/        # Componentes reutilizáveis
│   │   │   ├── common/       # Componentes comuns
│   │   │   ├── forms/        # Componentes de formulário
│   │   │   └── charts/       # Componentes de gráficos
│   │   ├── views/            # Páginas da aplicação
│   │   │   ├── inventory/    # Páginas de inventário
│   │   │   ├── reports/      # Páginas de relatórios
│   │   │   └── admin/        # Páginas administrativas
│   │   ├── services/         # Serviços e APIs
│   │   │   ├── api.js        # Cliente HTTP
│   │   │   ├── auth.js       # Serviços de autenticação
│   │   │   └── inventory.js  # Serviços de inventário
│   │   ├── stores/           # Pinia stores
│   │   │   ├── auth.js       # Store de autenticação
│   │   │   ├── inventory.js  # Store de inventário
│   │   │   └── ui.js         # Store de UI
│   │   ├── utils/            # Utilitários
│   │   │   ├── helpers.js    # Funções auxiliares
│   │   │   ├── constants.js  # Constantes
│   │   │   └── validators.js # Validadores
│   │   ├── assets/           # Assets estáticos
│   │   │   ├── images/       # Imagens
│   │   │   ├── icons/        # Ícones
│   │   │   └── styles/       # Estilos globais
│   │   └── router/           # Configuração de rotas
│   ├── public/               # Arquivos públicos
│   ├── tests/                # Testes do frontend
│   │   ├── unit/            # Testes unitários
│   │   ├── integration/     # Testes de integração
│   │   └── e2e/             # Testes end-to-end
│   ├── package.json
│   ├── vite.config.js
│   ├── tailwind.config.js
│   └── tsconfig.json
├── backend/                   # Flask API Backend
│   ├── src/
│   │   ├── controllers/      # Controladores da API
│   │   │   ├── auth.py       # Autenticação
│   │   │   ├── inventory.py  # Inventário
│   │   │   ├── reports.py    # Relatórios
│   │   │   └── admin.py      # Administração
│   │   ├── models/           # Modelos SQLAlchemy
│   │   │   ├── user.py       # Modelo de usuário
│   │   │   ├── product.py    # Modelo de produto
│   │   │   ├── movement.py   # Modelo de movimentação
│   │   │   └── audit.py      # Modelo de auditoria
│   │   ├── services/         # Lógica de negócio
│   │   │   ├── auth_service.py
│   │   │   ├── inventory_service.py
│   │   │   ├── report_service.py
│   │   │   └── integration_service.py
│   │   ├── middleware/       # Middlewares
│   │   │   ├── auth.py       # Middleware de autenticação
│   │   │   ├── logging.py    # Middleware de logging
│   │   │   └── cors.py       # Middleware CORS
│   │   ├── utils/            # Utilitários
│   │   │   ├── helpers.py    # Funções auxiliares
│   │   │   ├── validators.py # Validadores
│   │   │   └── exceptions.py # Exceções customizadas
│   │   ├── tasks/            # Tarefas Celery
│   │   │   ├── sync_erp.py   # Sincronização ERP
│   │   │   ├── reports.py    # Geração de relatórios
│   │   │   └── notifications.py # Notificações
│   │   └── config/           # Configurações
│   │       ├── settings.py   # Configurações gerais
│   │       ├── database.py   # Configuração do banco
│   │       └── celery.py     # Configuração Celery
│   ├── tests/                # Testes do backend
│   │   ├── unit/            # Testes unitários
│   │   ├── integration/     # Testes de integração
│   │   └── fixtures/        # Fixtures de teste
│   ├── migrations/          # Migrações do banco
│   ├── requirements.txt     # Dependências Python
│   ├── requirements-dev.txt # Dependências de desenvolvimento
│   └── app.py              # Aplicação principal
├── database/                 # Scripts de banco
│   ├── migrations/          # Migrações SQL
│   ├── seeds/               # Dados iniciais
│   │   ├── users.sql        # Usuários padrão
│   │   ├── products.sql     # Produtos de exemplo
│   │   └── categories.sql   # Categorias padrão
│   ├── schema.sql           # Schema inicial
│   └── backup/              # Scripts de backup
├── infrastructure/           # Infraestrutura como código
│   ├── docker/              # Dockerfiles
│   │   ├── Dockerfile.frontend
│   │   ├── Dockerfile.backend
│   │   └── Dockerfile.worker
│   ├── kubernetes/          # Manifests K8s (futuro)
│   │   ├── namespace.yaml
│   │   ├── deployment.yaml
│   │   └── service.yaml
│   ├── terraform/           # Scripts Terraform
│   │   ├── main.tf
│   │   ├── variables.tf
│   │   ├── outputs.tf
│   │   └── modules/
│   └── cloudformation/      # Templates CloudFormation
├── docs/                    # Documentação
│   ├── api/                 # Documentação da API
│   │   ├── swagger.yaml     # Especificação OpenAPI
│   │   └── endpoints.md     # Documentação dos endpoints
│   ├── architecture/        # Diagramas e ADRs
│   │   ├── adr/            # Architectural Decision Records
│   │   ├── diagrams/       # Diagramas de arquitetura
│   │   └── database/       # Documentação do banco
│   ├── deployment/          # Guias de deploy
│   │   ├── local.md        # Deploy local
│   │   ├── staging.md      # Deploy staging
│   │   └── production.md   # Deploy produção
│   └── user-guide/          # Guia do usuário
│       ├── admin.md        # Guia do administrador
│       └── user.md         # Guia do usuário final
├── .github/                 # GitHub Actions
│   └── workflows/
│       ├── ci.yml          # Integração contínua
│       ├── cd-staging.yml  # Deploy staging
│       ├── cd-production.yml # Deploy produção
│       └── security.yml    # Verificações de segurança
├── monitoring/              # Configurações de monitoramento
│   ├── grafana/            # Dashboards Grafana
│   ├── prometheus/         # Configurações Prometheus
│   └── alerts/             # Configurações de alertas
├── scripts/                 # Scripts utilitários
│   ├── setup.sh            # Script de setup inicial
│   ├── deploy.sh           # Script de deploy
│   ├── backup.sh           # Script de backup
│   └── migrate.py          # Script de migração
├── docker-compose.yml       # Ambiente local
├── docker-compose.prod.yml  # Ambiente produção
├── README.md               # Documentação principal
├── CHANGELOG.md            # Histórico de mudanças
├── .env.example            # Variáveis de ambiente exemplo
├── .gitignore              # Arquivos ignorados pelo Git
└── .editorconfig           # Configuração do editor
```

### Configuração Inicial

#### 1. Repositório Git

```bash
# Criar repositório
git init
git remote add origin https://github.com/datametria/inventario-datametria.git

# Configurar .gitignore
cat > .gitignore << EOF
# Dependencies
node_modules/
__pycache__/
*.pyc
venv/
.env

# Build outputs
dist/
build/
*.egg-info/

# IDE
.vscode/
.idea/
*.swp
*.swo

# OS
.DS_Store
Thumbs.db

# Logs
*.log
logs/

# Database
*.db
*.sqlite3

# AWS
.aws/
EOF

# Primeiro commit
git add .
git commit -m "feat: initial project setup with DATAMETRIA standards"
git push -u origin main
```

#### 2. Ambiente de Desenvolvimento

```bash
# Frontend setup
cd frontend
npm install
npm run dev

# Backend setup
cd ../backend
python -m venv venv
source venv/bin/activate  # Linux/Mac
# ou venv\Scripts\activate  # Windows
pip install -r requirements-dev.txt

# Configurar variáveis de ambiente
cp .env.example .env
# Editar .env com configurações locais

# Executar migrações
flask db upgrade

# Popular dados iniciais
python scripts/seed_data.py
```

#### 3. Banco de Dados Local

```bash
# Subir banco local com Docker
docker-compose up -d postgres redis

# Verificar se está funcionando
docker-compose ps

# Executar migrações
cd backend
flask db upgrade

# Popular dados de teste
python scripts/seed_data.py
```

#### 4. Verificação do Setup

```bash
# Executar testes backend
cd backend
pytest

# Executar testes frontend
cd ../frontend
npm test

# Iniciar aplicação completa
docker-compose up

# Verificar se está funcionando
curl http://localhost:5000/api/health
curl http://localhost:3000
```

---

## 🔧 Configurações de Desenvolvimento

### Linting e Formatação

#### ESLint + Prettier (Frontend)

```json
// frontend/.eslintrc.json
{
  "extends": [
    "eslint:recommended",
    "@vue/eslint-config-typescript",
    "@vue/eslint-config-prettier"
  ],
  "rules": {
    "no-console": "warn",
    "no-unused-vars": "error",
    "vue/multi-word-component-names": "off",
    "@typescript-eslint/no-unused-vars": "error"
  },
  "env": {
    "node": true,
    "browser": true
  }
}
```

```json
// frontend/.prettierrc
{
  "semi": true,
  "trailingComma": "es5",
  "singleQuote": true,
  "printWidth": 100,
  "tabWidth": 2,
  "useTabs": false
}
```

#### Python (Black + Flake8 + MyPy)

```ini
# backend/setup.cfg
[flake8]
max-line-length = 100
extend-ignore = E203, W503, E501
exclude = migrations/

[tool:pytest]
testpaths = tests
python_files = test_*.py
python_classes = Test*
python_functions = test_*
addopts = --cov=src --cov-report=html --cov-report=term-missing

[mypy]
python_version = 3.11
warn_return_any = True
warn_unused_configs = True
disallow_untyped_defs = True
```

```toml
# backend/pyproject.toml
[tool.black]
line-length = 100
target-version = ['py311']
include = '\.pyi?$'
extend-exclude = '''
/(
  migrations
)/
'''
```

### Pre-commit Hooks

```yaml
# .pre-commit-config.yaml
repos:
  - repo: https://github.com/pre-commit/pre-commit-hooks
    rev: v4.4.0
    hooks:
      - id: trailing-whitespace
      - id: end-of-file-fixer
      - id: check-yaml
      - id: check-json
      - id: check-merge-conflict

  # Python hooks
  - repo: https://github.com/psf/black
    rev: 23.1.0
    hooks:
      - id: black
        files: ^backend/

  - repo: https://github.com/pycqa/flake8
    rev: 6.0.0
    hooks:
      - id: flake8
        files: ^backend/

  - repo: https://github.com/pre-commit/mirrors-mypy
    rev: v1.0.1
    hooks:
      - id: mypy
        files: ^backend/
        additional_dependencies: [types-all]

  # Frontend hooks
  - repo: https://github.com/pre-commit/mirrors-eslint
    rev: v8.36.0
    hooks:
      - id: eslint
        files: ^frontend/
        additional_dependencies:
          - eslint@8.36.0
          - '@vue/eslint-config-typescript'
          - '@vue/eslint-config-prettier'

  - repo: https://github.com/pre-commit/mirrors-prettier
    rev: v3.0.0-alpha.6
    hooks:
      - id: prettier
        files: ^frontend/
```

### VS Code Configuration

```json
// .vscode/settings.json
{
  "editor.formatOnSave": true,
  "editor.codeActionsOnSave": {
    "source.fixAll.eslint": true,
    "source.organizeImports": true
  },
  "python.defaultInterpreterPath": "./backend/venv/bin/python",
  "python.linting.enabled": true,
  "python.linting.pylintEnabled": false,
  "python.linting.flake8Enabled": true,
  "python.formatting.provider": "black",
  "python.formatting.blackArgs": ["--line-length", "100"],
  "python.testing.pytestEnabled": true,
  "python.testing.pytestArgs": ["backend/tests"],
  "amazonQ.enableCodeSuggestions": true,
  "files.associations": {
    "*.vue": "vue"
  },
  "emmet.includeLanguages": {
    "vue": "html"
  }
}
```

```json
// .vscode/extensions.json
{
  "recommendations": [
    "ms-python.python",
    "ms-python.black-formatter",
    "ms-python.flake8",
    "vue.volar",
    "bradlc.vscode-tailwindcss",
    "esbenp.prettier-vscode",
    "dbaeumer.vscode-eslint",
    "amazonwebservices.amazon-q-vscode"
  ]
}
```

---

## 🧪 Configuração de Testes

### Estrutura de Testes

#### Frontend (Vitest + Vue Test Utils)

```typescript
// frontend/tests/unit/components/ProductCard.test.ts
import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import ProductCard from '@/components/ProductCard.vue'

describe('ProductCard', () => {
  const mockProduct = {
    id: 1,
    name: 'Produto Teste',
    sku: 'TEST-001',
    quantity: 10,
    price: 99.99
  }

  it('renders product information correctly', () => {
    const wrapper = mount(ProductCard, {
      props: { product: mockProduct }
    })

    expect(wrapper.text()).toContain('Produto Teste')
    expect(wrapper.text()).toContain('TEST-001')
    expect(wrapper.text()).toContain('10')
  })

  it('emits edit event when edit button is clicked', async () => {
    const wrapper = mount(ProductCard, {
      props: { product: mockProduct }
    })

    await wrapper.find('[data-testid="edit-button"]').trigger('click')
    expect(wrapper.emitted('edit')).toBeTruthy()
    expect(wrapper.emitted('edit')[0]).toEqual([mockProduct])
  })
})
```

```typescript
// frontend/tests/integration/stores/inventory.test.ts
import { describe, it, expect, beforeEach, vi } from 'vitest'
import { setActivePinia, createPinia } from 'pinia'
import { useInventoryStore } from '@/stores/inventory'
import * as api from '@/services/api'

vi.mock('@/services/api')

describe('Inventory Store', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
    vi.clearAllMocks()
  })

  it('fetches products successfully', async () => {
    const mockProducts = [
      { id: 1, name: 'Product 1', sku: 'P001' },
      { id: 2, name: 'Product 2', sku: 'P002' }
    ]

    vi.mocked(api.getProducts).mockResolvedValue(mockProducts)

    const store = useInventoryStore()
    await store.fetchProducts()

    expect(store.products).toEqual(mockProducts)
    expect(store.loading).toBe(false)
  })
})
```

#### Backend (Pytest + Factory Boy)

```python
# backend/tests/conftest.py
import pytest
from flask import Flask
from src.app import create_app
from src.models import db
from src.models.user import User
from src.models.product import Product

@pytest.fixture
def app():
    """Create application for testing."""
    app = create_app('testing')

    with app.app_context():
        db.create_all()
        yield app
        db.drop_all()

@pytest.fixture
def client(app):
    """Create test client."""
    return app.test_client()

@pytest.fixture
def auth_headers(client):
    """Create authentication headers for testing."""
    # Create test user
    user = User(email='test@datametria.io', name='Test User')
    user.set_password('password123')
    db.session.add(user)
    db.session.commit()

    # Login and get token
    response = client.post('/api/auth/login', json={
        'email': 'test@datametria.io',
        'password': 'password123'
    })

    token = response.json['access_token']
    return {'Authorization': f'Bearer {token}'}
```

```python
# backend/tests/unit/test_inventory_service.py
import pytest
from unittest.mock import Mock, patch
from src.services.inventory_service import InventoryService
from src.models.product import Product

class TestInventoryService:

    def test_create_product_success(self, app):
        """Test successful product creation."""
        with app.app_context():
            service = InventoryService()
            product_data = {
                'name': 'Test Product',
                'sku': 'TEST-001',
                'quantity': 100,
                'price': 99.99
            }

            product = service.create_product(product_data)

            assert product.name == 'Test Product'
            assert product.sku == 'TEST-001'
            assert product.quantity == 100

    def test_create_product_duplicate_sku(self, app):
        """Test product creation with duplicate SKU."""
        with app.app_context():
            service = InventoryService()

            # Create first product
            service.create_product({
                'name': 'Product 1',
                'sku': 'DUPLICATE',
                'quantity': 10,
                'price': 50.0
            })

            # Try to create second product with same SKU
            with pytest.raises(ValueError, match="SKU already exists"):
                service.create_product({
                    'name': 'Product 2',
                    'sku': 'DUPLICATE',
                    'quantity': 20,
                    'price': 60.0
                })
```

```python
# backend/tests/integration/test_api_endpoints.py
import pytest
from src.models.product import Product

class TestProductAPI:

    def test_get_products_success(self, client, auth_headers):
        """Test GET /api/products endpoint."""
        # Create test products
        products = [
            Product(name='Product 1', sku='P001', quantity=10, price=99.99),
            Product(name='Product 2', sku='P002', quantity=20, price=149.99)
        ]

        for product in products:
            db.session.add(product)
        db.session.commit()

        response = client.get('/api/products', headers=auth_headers)

        assert response.status_code == 200
        data = response.json
        assert len(data['products']) == 2
        assert data['products'][0]['name'] == 'Product 1'

    def test_create_product_success(self, client, auth_headers):
        """Test POST /api/products endpoint."""
        product_data = {
            'name': 'New Product',
            'sku': 'NEW-001',
            'quantity': 50,
            'price': 199.99,
            'category_id': 1
        }

        response = client.post('/api/products',
                             json=product_data,
                             headers=auth_headers)

        assert response.status_code == 201
        data = response.json
        assert data['product']['name'] == 'New Product'
        assert data['product']['sku'] == 'NEW-001'
```

### Configuração de Testes

#### Frontend (Vitest)

```typescript
// frontend/vitest.config.ts
import { defineConfig } from 'vitest/config'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'path'

export default defineConfig({
  plugins: [vue()],
  test: {
    globals: true,
    environment: 'jsdom',
    coverage: {
      reporter: ['text', 'html', 'lcov'],
      exclude: [
        'node_modules/',
        'tests/',
        '**/*.d.ts',
        '**/*.config.*'
      ]
    }
  },
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src')
    }
  }
})
```

#### Backend (Pytest)

```ini
# backend/pytest.ini
[tool:pytest]
testpaths = tests
python_files = test_*.py
python_classes = Test*
python_functions = test_*
addopts =
    --cov=src
    --cov-report=html
    --cov-report=term-missing
    --cov-fail-under=85
    --strict-markers
    --disable-warnings
markers =
    unit: Unit tests
    integration: Integration tests
    slow: Slow tests
    external: Tests that require external services
```

---

## 🚀 Configuração de Deploy

### Ambientes

#### Desenvolvimento Local

```yaml
# docker-compose.yml
version: '3.8'

services:
  frontend:
    build:
      context: ./frontend
      dockerfile: ../infrastructure/docker/Dockerfile.frontend
    ports:
      - "3000:3000"
    volumes:
      - ./frontend:/app
      - /app/node_modules
    environment:
      - VITE_API_URL=http://localhost:5000/api

  backend:
    build:
      context: ./backend
      dockerfile: ../infrastructure/docker/Dockerfile.backend
    ports:
      - "5000:5000"
    volumes:
      - ./backend:/app
    environment:
      - FLASK_ENV=development
      - DATABASE_URL=postgresql://postgres:password@postgres:5432/inventario_dev
      - REDIS_URL=redis://redis:6379/0
    depends_on:
      - postgres
      - redis

  postgres:
    image: postgres:15-alpine
    environment:
      - POSTGRES_DB=inventario_dev
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=password
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"

  worker:
    build:
      context: ./backend
      dockerfile: ../infrastructure/docker/Dockerfile.backend
    command: celery -A src.tasks.celery worker --loglevel=info
    volumes:
      - ./backend:/app
    environment:
      - DATABASE_URL=postgresql://postgres:password@postgres:5432/inventario_dev
      - REDIS_URL=redis://redis:6379/0
    depends_on:
      - postgres
      - redis

volumes:
  postgres_data:
```

#### Staging/Production

```yaml
# docker-compose.prod.yml
version: '3.8'

services:
  frontend:
    image: datametria/inventario-frontend:${VERSION}
    ports:
      - "80:80"
    environment:
      - VITE_API_URL=${API_URL}

  backend:
    image: datametria/inventario-backend:${VERSION}
    ports:
      - "5000:5000"
    environment:
      - FLASK_ENV=production
      - DATABASE_URL=${DATABASE_URL}
      - REDIS_URL=${REDIS_URL}
      - SECRET_KEY=${SECRET_KEY}
    deploy:
      replicas: 3
      resources:
        limits:
          cpus: '0.5'
          memory: 512M

  worker:
    image: datametria/inventario-backend:${VERSION}
    command: celery -A src.tasks.celery worker --loglevel=info
    environment:
      - DATABASE_URL=${DATABASE_URL}
      - REDIS_URL=${REDIS_URL}
    deploy:
      replicas: 2
```

### CI/CD Pipeline

#### GitHub Actions

```yaml
# .github/workflows/ci.yml
name: CI Pipeline

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  test-frontend:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '18'
          cache: 'npm'
          cache-dependency-path: frontend/package-lock.json

      - name: Install dependencies
        run: |
          cd frontend
          npm ci

      - name: Run linting
        run: |
          cd frontend
          npm run lint

      - name: Run tests
        run: |
          cd frontend
          npm run test:coverage

      - name: Upload coverage
        uses: codecov/codecov-action@v3
        with:
          file: frontend/coverage/lcov.info

  test-backend:
    runs-on: ubuntu-latest
    services:
      postgres:
        image: postgres:15
        env:
          POSTGRES_PASSWORD: postgres
          POSTGRES_DB: test_db
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

      redis:
        image: redis:7
        options: >-
          --health-cmd "redis-cli ping"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    steps:
      - uses: actions/checkout@v4

      - name: Setup Python
        uses: actions/setup-python@v4
        with:
          python-version: '3.11'

      - name: Install dependencies
        run: |
          cd backend
          pip install -r requirements-dev.txt

      - name: Run linting
        run: |
          cd backend
          flake8 src/
          black --check src/
          mypy src/

      - name: Run tests
        run: |
          cd backend
          pytest --cov=src --cov-report=xml
        env:
          DATABASE_URL: postgresql://postgres:postgres@localhost:5432/test_db
          REDIS_URL: redis://localhost:6379/0

      - name: Upload coverage
        uses: codecov/codecov-action@v3
        with:
          file: backend/coverage.xml

  security-scan:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Run Trivy vulnerability scanner
        uses: aquasecurity/trivy-action@master
        with:
          scan-type: 'fs'
          scan-ref: '.'
          format: 'sarif'
          output: 'trivy-results.sarif'

      - name: Upload Trivy scan results
        uses: github/codeql-action/upload-sarif@v2
        with:
          sarif_file: 'trivy-results.sarif'
```

```yaml
# .github/workflows/cd-production.yml
name: Production Deployment

on:
  push:
    tags:
      - 'v*'

jobs:
  deploy:
    runs-on: ubuntu-latest
    environment: production

    steps:
      - uses: actions/checkout@v4

      - name: Configure AWS credentials
        uses: aws-actions/configure-aws-credentials@v2
        with:
          aws-access-key-id: ${{ secrets.AWS_ACCESS_KEY_ID }}
          aws-secret-access-key: ${{ secrets.AWS_SECRET_ACCESS_KEY }}
          aws-region: us-east-1

      - name: Login to Amazon ECR
        uses: aws-actions/amazon-ecr-login@v1

      - name: Build and push Docker images
        run: |
          # Build frontend
          docker build -f infrastructure/docker/Dockerfile.frontend -t $ECR_REGISTRY/inventario-frontend:$GITHUB_SHA ./frontend
          docker push $ECR_REGISTRY/inventario-frontend:$GITHUB_SHA

          # Build backend
          docker build -f infrastructure/docker/Dockerfile.backend -t $ECR_REGISTRY/inventario-backend:$GITHUB_SHA ./backend
          docker push $ECR_REGISTRY/inventario-backend:$GITHUB_SHA
        env:
          ECR_REGISTRY: ${{ steps.login-ecr.outputs.registry }}

      - name: Deploy to ECS
        run: |
          aws ecs update-service --cluster inventario-prod --service inventario-frontend --force-new-deployment
          aws ecs update-service --cluster inventario-prod --service inventario-backend --force-new-deployment
          aws ecs update-service --cluster inventario-prod --service inventario-worker --force-new-deployment

      - name: Wait for deployment
        run: |
          aws ecs wait services-stable --cluster inventario-prod --services inventario-frontend inventario-backend inventario-worker

      - name: Run database migrations
        run: |
          aws ecs run-task --cluster inventario-prod --task-definition inventario-migrate --launch-type FARGATE --network-configuration "awsvpcConfiguration={subnets=[subnet-xxx],securityGroups=[sg-xxx],assignPublicIp=ENABLED}"
```

---

## 📊 Monitoramento e Observabilidade

### Métricas e Dashboards

#### Grafana Dashboard Configuration

```json
{
  "dashboard": {
    "title": "Inventário DATAMETRIA - Production",
    "panels": [
      {
        "title": "API Response Time",
        "type": "graph",
        "targets": [
          {
            "expr": "histogram_quantile(0.95, rate(flask_request_duration_seconds_bucket[5m]))",
            "legendFormat": "95th percentile"
          }
        ]
      },
      {
        "title": "Error Rate",
        "type": "stat",
        "targets": [
          {
            "expr": "rate(flask_request_exceptions_total[5m]) * 100",
            "legendFormat": "Error Rate %"
          }
        ]
      },
      {
        "title": "Database Connections",
        "type": "graph",
        "targets": [
          {
            "expr": "postgresql_connections_active",
            "legendFormat": "Active Connections"
          }
        ]
      }
    ]
  }
}
```

#### CloudWatch Alarms

```yaml
# infrastructure/cloudwatch-alarms.yml
Resources:
  HighErrorRateAlarm:
    Type: AWS::CloudWatch::Alarm
    Properties:
      AlarmName: InventarioHighErrorRate
      AlarmDescription: High error rate detected
      MetricName: ErrorRate
      Namespace: AWS/ApplicationELB
      Statistic: Average
      Period: 300
      EvaluationPeriods: 2
      Threshold: 5
      ComparisonOperator: GreaterThanThreshold
      AlarmActions:
        - !Ref SNSTopicArn

  HighResponseTimeAlarm:
    Type: AWS::CloudWatch::Alarm
    Properties:
      AlarmName: InventarioHighResponseTime
      AlarmDescription: High response time detected
      MetricName: TargetResponseTime
      Namespace: AWS/ApplicationELB
      Statistic: Average
      Period: 300
      EvaluationPeriods: 2
      Threshold: 2
      ComparisonOperator: GreaterThanThreshold
```

### Logging

#### Structured Logging (Backend)

```python
# backend/src/utils/logging.py
import logging
import json
from datetime import datetime
from flask import request, g
import uuid

class JSONFormatter(logging.Formatter):
    def format(self, record):
        log_entry = {
            'timestamp': datetime.utcnow().isoformat() + 'Z',
            'level': record.levelname,
            'service': 'inventario-backend',
            'message': record.getMessage(),
            'module': record.module,
            'function': record.funcName,
            'line': record.lineno
        }

        # Add request context if available
        if hasattr(g, 'request_id'):
            log_entry['request_id'] = g.request_id

        if hasattr(g, 'user_id'):
            log_entry['user_id'] = g.user_id

        # Add HTTP request info
        if request:
            log_entry['http'] = {
                'method': request.method,
                'url': request.url,
                'user_agent': request.headers.get('User-Agent'),
                'remote_addr': request.remote_addr
            }

        # Add exception info if present
        if record.exc_info:
            log_entry['exception'] = self.formatException(record.exc_info)

        return json.dumps(log_entry, ensure_ascii=False)

def setup_logging(app):
    """Setup structured logging for the application."""
    handler = logging.StreamHandler()
    handler.setFormatter(JSONFormatter())

    app.logger.addHandler(handler)
    app.logger.setLevel(logging.INFO)

    # Add request ID middleware
    @app.before_request
    def before_request():
        g.request_id = str(uuid.uuid4())
        g.start_time = datetime.utcnow()

    @app.after_request
    def after_request(response):
        duration = (datetime.utcnow() - g.start_time).total_seconds()

        app.logger.info("Request completed", extra={
            'http_status': response.status_code,
            'duration_seconds': duration,
            'response_size': len(response.get_data())
        })

        return response
```

---

## 🔒 Segurança

### Configurações de Segurança

#### Backend Security

```python
# backend/src/middleware/security.py
from flask import Flask
from flask_cors import CORS
from flask_talisman import Talisman

def setup_security(app: Flask):
    """Configure security middleware."""

    # CORS configuration
    CORS(app,
         origins=['https://inventario.datametria.io'],
         methods=['GET', 'POST', 'PUT', 'DELETE'],
         allow_headers=['Content-Type', 'Authorization'])

    # Security headers
    Talisman(app,
             force_https=True,
             strict_transport_security=True,
             content_security_policy={
                 'default-src': "'self'",
                 'script-src': "'self' 'unsafe-inline'",
                 'style-src': "'self' 'unsafe-inline'",
                 'img-src': "'self' data: https:",
                 'font-src': "'self' https://fonts.gstatic.com"
             })
```

#### Environment Variables

```bash
# .env.example
# Database
DATABASE_URL=postgresql://user:password@localhost:5432/inventario
REDIS_URL=redis://localhost:6379/0

# Security
SECRET_KEY=your-secret-key-here
JWT_SECRET_KEY=your-jwt-secret-here
BCRYPT_LOG_ROUNDS=12

# AWS
AWS_ACCESS_KEY_ID=your-access-key
AWS_SECRET_ACCESS_KEY=your-secret-key
AWS_REGION=us-east-1
S3_BUCKET=inventario-datametria-files

# External APIs
ERP_API_URL=https://erp.company.com/api
ERP_API_KEY=your-erp-api-key

# Email
SMTP_SERVER=smtp.gmail.com
SMTP_PORT=587
SMTP_USERNAME=noreply@datametria.io
SMTP_PASSWORD=your-email-password

# Monitoring
SENTRY_DSN=your-sentry-dsn
```

### Secrets Management

```yaml
# infrastructure/terraform/secrets.tf
resource "aws_secretsmanager_secret" "app_secrets" {
  name = "inventario/production"

  tags = {
    Environment = "production"
    Project     = "inventario"
  }
}

resource "aws_secretsmanager_secret_version" "app_secrets" {
  secret_id = aws_secretsmanager_secret.app_secrets.id
  secret_string = jsonencode({
    database_url = var.database_url
    redis_url    = var.redis_url
    secret_key   = var.secret_key
    jwt_secret   = var.jwt_secret
  })
}
```

---

## 📚 Documentação

### API Documentation

#### OpenAPI Specification

```yaml
# docs/api/swagger.yaml
openapi: 3.0.0
info:
  title: Inventário DATAMETRIA API
  version: 1.0.0
  description: API para gestão de inventário
  contact:
    name: Equipe DATAMETRIA
    email: tech@datametria.io

servers:
  - url: https://api.inventario.datametria.io/v1
    description: Production server
  - url: https://staging-api.inventario.datametria.io/v1
    description: Staging server

paths:
  /products:
    get:
      summary: List products
      tags: [Products]
      parameters:
        - name: page
          in: query
          schema:
            type: integer
            default: 1
        - name: limit
          in: query
          schema:
            type: integer
            default: 20
      responses:
        '200':
          description: List of products
          content:
            application/json:
              schema:
                type: object
                properties:
                  products:
                    type: array
                    items:
                      $ref: '#/components/schemas/Product'
                  pagination:
                    $ref: '#/components/schemas/Pagination'

components:
  schemas:
    Product:
      type: object
      properties:
        id:
          type: integer
        name:
          type: string
        sku:
          type: string
        quantity:
          type: integer
        price:
          type: number
          format: decimal
        created_at:
          type: string
          format: date-time
```

---

## 📈 Exemplos Práticos

### Exemplo Completo: Criação de Produto

#### Frontend (Vue.js)

```vue
<!-- frontend/src/components/ProductForm.vue -->
<template>
  <form @submit.prevent="handleSubmit" class="space-y-4">
    <div>
      <label for="name" class="block text-sm font-medium text-gray-700">
        Nome do Produto
      </label>
      <input
        id="name"
        v-model="form.name"
        type="text"
        required
        class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
      />
    </div>

    <div>
      <label for="sku" class="block text-sm font-medium text-gray-700">
        SKU
      </label>
      <input
        id="sku"
        v-model="form.sku"
        type="text"
        required
        class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
      />
    </div>

    <div>
      <label for="quantity" class="block text-sm font-medium text-gray-700">
        Quantidade
      </label>
      <input
        id="quantity"
        v-model.number="form.quantity"
        type="number"
        min="0"
        required
        class="mt-1 block w-full rounded-md border-gray-300 shadow-sm"
      />
    </div>

    <button
      type="submit"
      :disabled="loading"
      class="w-full bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 disabled:opacity-50"
    >
      {{ loading ? 'Salvando...' : 'Salvar Produto' }}
    </button>
  </form>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useInventoryStore } from '@/stores/inventory'
import { useNotificationStore } from '@/stores/notifications'

interface ProductForm {
  name: string
  sku: string
  quantity: number
  price: number
}

const inventoryStore = useInventoryStore()
const notificationStore = useNotificationStore()

const loading = ref(false)
const form = reactive<ProductForm>({
  name: '',
  sku: '',
  quantity: 0,
  price: 0
})

const handleSubmit = async () => {
  loading.value = true

  try {
    await inventoryStore.createProduct(form)
    notificationStore.showSuccess('Produto criado com sucesso!')

    // Reset form
    Object.assign(form, {
      name: '',
      sku: '',
      quantity: 0,
      price: 0
    })
  } catch (error) {
    notificationStore.showError('Erro ao criar produto')
  } finally {
    loading.value = false
  }
}
</script>
```

#### Backend (Flask)

```python
# backend/src/controllers/products.py
from flask import Blueprint, request, jsonify
from flask_jwt_extended import jwt_required, get_jwt_identity
from marshmallow import ValidationError
from src.services.inventory_service import InventoryService
from src.schemas.product_schema import ProductSchema, ProductCreateSchema
from src.utils.decorators import handle_exceptions
from src.utils.logging import get_logger

logger = get_logger(__name__)
products_bp = Blueprint('products', __name__)
inventory_service = InventoryService()

@products_bp.route('/products', methods=['POST'])
@jwt_required()
@handle_exceptions
def create_product():
    """Create a new product."""
    user_id = get_jwt_identity()

    # Validate input data
    schema = ProductCreateSchema()
    try:
        data = schema.load(request.json)
    except ValidationError as err:
        logger.warning(f"Validation error in create_product: {err.messages}")
        return jsonify({'errors': err.messages}), 400

    # Create product
    try:
        product = inventory_service.create_product(data, created_by=user_id)
        logger.info(f"Product created successfully: {product.sku}")

        # Return created product
        result_schema = ProductSchema()
        return jsonify({
            'product': result_schema.dump(product),
            'message': 'Product created successfully'
        }), 201

    except ValueError as e:
        logger.error(f"Business logic error in create_product: {str(e)}")
        return jsonify({'error': str(e)}), 400
    except Exception as e:
        logger.error(f"Unexpected error in create_product: {str(e)}")
        return jsonify({'error': 'Internal server error'}), 500

@products_bp.route('/products', methods=['GET'])
@jwt_required()
@handle_exceptions
def list_products():
    """List products with pagination."""
    page = request.args.get('page', 1, type=int)
    limit = request.args.get('limit', 20, type=int)
    search = request.args.get('search', '')

    try:
        result = inventory_service.list_products(
            page=page,
            limit=limit,
            search=search
        )

        schema = ProductSchema(many=True)
        return jsonify({
            'products': schema.dump(result['products']),
            'pagination': {
                'page': result['page'],
                'pages': result['pages'],
                'total': result['total'],
                'has_next': result['has_next'],
                'has_prev': result['has_prev']
            }
        })

    except Exception as e:
        logger.error(f"Error in list_products: {str(e)}")
        return jsonify({'error': 'Internal server error'}), 500
```

---

## 🔗 Referências

### Documentação Oficial

- **[Vue.js 3](https://vuejs.org/)**: Framework frontend
- **[Flask](https://flask.palletsprojects.com/)**: Framework backend
- **[PostgreSQL](https://www.postgresql.org/)**: Banco de dados
- **[Redis](https://redis.io/)**: Cache e message broker
- **[Docker](https://docs.docker.com/)**: Containerização

### Ferramentas DATAMETRIA

| Ferramenta | Propósito | Documentação |
|------------|-----------|--------------|
| **GitHub Actions** | CI/CD | [Workflows](.github/workflows/) |
| **AWS ECS** | Container orchestration | [Infrastructure](infrastructure/) |
| **Grafana** | Monitoring dashboards | [Monitoring](monitoring/) |
| **SonarCloud** | Code quality | [Quality gates](docs/quality/) |

### Padrões e Convenções

- **[Conventional Commits](https://www.conventionalcommits.org/)**: Padrão de commits
- **[Semantic Versioning](https://semver.org/)**: Versionamento semântico
- **[OpenAPI 3.0](https://swagger.io/specification/)**: Especificação de API
- **[12 Factor App](https://12factor.net/)**: Metodologia de desenvolvimento

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA
**Última Atualização**: 25/01/2025
**Versão**: 2.0.0

---

## Template de Setup de Projeto implementado! 🚀

</div>
