# 🌐 Web Development - Diretrizes DATAMETRIA

**Diretrizes para Desenvolvimento de Aplicações Web Modernas**

**Versão**: 1.3 | **Data**: 15/09/2025 | **Autor**: Vander Loto - CTO DATAMETRIA

[![Flask](https://img.shields.io/badge/Flask-2.3+-blue)](https://flask.palletsprojects.com)
[![Vue.js](https://img.shields.io/badge/Vue.js-3.0+-green)](https://vuejs.org)
[![SQLAlchemy](https://img.shields.io/badge/SQLAlchemy-2.0+-orange)](https://sqlalchemy.org)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/DATAMETRIA-standards)

[🔧 Backend](#4-desenvolvimento-backend-flask) • [🎨 Frontend](#5-desenvolvimento-frontend-vuejs-3) • [🔒 Segurança](#6-seguranca-flask-vuejs) • [🚀 Deploy](#8-deployment-e-operacoes)

> **🤖 AI-First Development**: Esta diretriz está otimizada para execução 90% Amazon Q Developer + 10% supervisão humana

---

## 📋 Índice

1. [Stack Tecnológica](#1-stack-tecnologica)
2. [Estimativas AI-First](#2-estimativas-ai-first)
3. [Estrutura de Pastas](#3-estrutura-de-pastas)
4. [Desenvolvimento Backend (Flask)](#4-desenvolvimento-backend-flask)
5. [Desenvolvimento Frontend (Vue.js 3)](#5-desenvolvimento-frontend-vuejs-3)
6. [Segurança (Flask + Vue.js)](#6-seguranca-flask-vuejs)
7. [Testes](#7-testes)
8. [Deployment e Operações](#8-deployment-e-operacoes)
9. [Reutilização de Código e Manutenibilidade](#9-reutilizacao-de-codigo-e-manutenibilidade)
10. [Padrões de Documentação](#10-padroes-de-documentacao)
11. [UI Framework & CDN Strategy](#11-ui-framework-cdn-strategy)
12. [Framework de Desenvolvimento](#12-framework-de-desenvolvimento)
13. [Melhores Práticas Essenciais](#13-melhores-praticas-essenciais)
14. [ADR (Architectural Decision Record)](#14-adr-architectural-decision-record)
15. [Checklist de Implantação](#15-checklist-de-implantacao)
16. [Templates e Diretrizes Relacionadas](#16-templates-e-diretrizes-relacionadas)

---

## 1. Stack Tecnológica

### 1.1. Arquitetura Geral

```mermaid
graph TB
    A[Cliente Web] --> B[Nginx]
    B --> C[Vue.js 3 SPA]
    C --> D[Flask API]
    D --> E[SQLAlchemy]
    E --> F[PostgreSQL]

    D --> G[Redis]
    D --> H[Celery]

    I[Docker] --> J[Containers]
    J --> B
    J --> C
    J --> D
    J --> F
    J --> G
```

### 1.2. Componentes Principais

| Componente | Tecnologias | Observações |
|------------|-------------|-------------|
| **Backend** | Flask + Flask-RESTX, SQLAlchemy Core, Celery, Redis | Suporte multi-banco via SQLAlchemy |
| **Frontend** | Vue.js 3 (Composition API), Pinia, Vite | SSR opcional com Vite-SSG |
| **UI Framework** | Vue Material (MDC) via CDN + Fallback Local | Implementado com estratégia de resiliência |
| **Banco de Dados** | SQLAlchemy + Drivers: cx_Oracle, pyodbc, psycopg2 | Conexões para Oracle, SQL Server, PostgreSQL e SQLite |
| **Infraestrutura** | Docker, Nginx, Gunicorn | Configuração de produção otimizada |
| **Monitoramento** | Flower (Celery), Sentry, Prometheus + Grafana | Observabilidade completa |

---

## 2. Estimativas AI-First

### 2.1. Métricas de Desenvolvimento Web

| Tipo de Projeto Web | Tradicional | AI-First (90% Amazon Q) | Supervisão Humana (10%) | Redução Total |
|---------------------|-------------|-------------------------|-------------------------|---------------|
| **SPA Vue.js Simples** | 120h | 12h | 12h | 80% |
| **Dashboard Administrativo** | 200h | 20h | 20h | 80% |
| **E-commerce Completo** | 800h | 80h | 80h | 80% |
| **API REST + Frontend** | 160h | 16h | 16h | 80% |
| **Sistema CRUD** | 100h | 10h | 10h | 80% |
| **PWA com Offline** | 300h | 30h | 30h | 80% |

### 2.2. Estimativas por Componente

#### Backend Flask

| Componente | Tradicional | AI-First | Supervisão | Observações |
|------------|-------------|----------|------------|-------------|
| **API REST Básica** | 40h | 4h | 4h | Amazon Q excelente com Flask |
| **Autenticação JWT** | 16h | 1.6h | 1.6h | Padrões bem estabelecidos |
| **CRUD Operations** | 24h | 2.4h | 2.4h | SQLAlchemy totalmente suportado |
| **Middleware Segurança** | 12h | 1.2h | 1.2h | Implementação automática |
| **Testes Backend** | 20h | 2h | 2h | Pytest com fixtures |

#### Frontend Vue.js 3

| Componente | Tradicional | AI-First | Supervisão | Observações |
|------------|-------------|----------|------------|-------------|
| **Componentes Base** | 32h | 3.2h | 3.2h | Composition API suportada |
| **Roteamento** | 8h | 0.8h | 0.8h | Vue Router padrão |
| **Estado (Pinia)** | 16h | 1.6h | 1.6h | Gerenciamento automático |
| **UI Components** | 48h | 4.8h | 4.8h | Vue Material integrado |
| **Testes Frontend** | 24h | 2.4h | 2.4h | Vitest + Testing Library |

### 2.3. Fórmula Específica Web Development

```
Tempo_Web_AI = (Backend_Flask × 0.1) + (Frontend_Vue × 0.1) + Supervisão_Humana

Onde:
- Backend_Flask: Estimativa tradicional Flask
- Frontend_Vue: Estimativa tradicional Vue.js
- Supervisão_Humana: 10% do tempo total para validação
```

### 2.4. Fatores de Ajuste Web

| Fator | Multiplicador | Aplicação |
|-------|---------------|----------|
| **Stack Familiar (Flask+Vue)** | 0.9 | Amazon Q otimizado |
| **Integração Complexa** | 1.2 | APIs externas múltiplas |
| **UI Customizada** | 1.1 | Além do Vue Material |
| **Performance Crítica** | 1.3 | Otimizações específicas |
| **Segurança Avançada** | 1.2 | Compliance rigoroso |

---

## 3. Estrutura de Pastas

### 3.1. Organização Modular

```
project-root/
├── backend/
│   ├── app/
│   │   ├── api/           # Blueprints e recursos (Módulos REST)
│   │   ├── core/          # Configs, middlewares e extensões
│   │   ├── db/            # Multi-banco (sessions.py, models/)
│   │   ├── tasks/         # Celery tasks (rpa_integration.py)
│   │   ├── utils/         # Helpers, validators e segurança
│   │   └── services/      # Lógica de negócio
│   ├── tests/             # Pytest (unit, integration)
│   ├── Dockerfile
│   └── requirements.txt
│
├── frontend/
│   ├── public/            # Assets estáticos (fallback/)
│   ├── src/
│   │   ├── assets/        # Fonts/imagens locais (fallback)
│   │   ├── composables/   # Lógica reutilizável Vue
│   │   ├── layouts/       # Templates de layout
│   │   ├── router/        # Vue Router
│   │   ├── services/      # API service (axios com interceptors)
│   │   ├── stores/        # Pinia (gerenciamento de estado)
│   │   └── views/         # Páginas (components/)
│   ├── Dockerfile
│   └── vite.config.js     # Config CDN fallback
│
├── docs/
│   ├── ADRs/              # Architectural Decision Records
│   ├── API_DOCS.md        # Swagger exportado
│   ├── DEPLOY_GUIDE.md
│   └── DB_SCHEMAS.md
│
├── scripts/
│   ├── deploy/
│   └── infra/             # Terraform/Ansible (opcional)
│
├── .github/
│   ├── workflows/         # CI/CD pipelines
│   └── ISSUE_TEMPLATE.md
│
├── CHANGELOG.md           # Histórico de versões (Keep a Changelog)
├── BACKLOG.md             # Roadmap priorizado
├── RELEASE_NOTES.md       # Notas por versão
└── README.md              # Documentação principal
```

### 3.2. Princípios de Organização

- **Separação de Responsabilidades**: Cada módulo/pasta deve ter uma responsabilidade única
- **Coesão**: Agrupar arquivos relacionados
- **Baixo Acoplamento**: Minimizar dependências entre módulos
- **Escalabilidade**: Estrutura que suporte crescimento do projeto

---

## 4. Desenvolvimento Backend (Flask)

### 4.1. Estrutura da Aplicação Flask

#### Application Factory Pattern

```python
# app/__init__.py
from flask import Flask
from flask_cors import CORS
from flask_jwt_extended import JWTManager
from app.core.config import Config
from app.core.database import db
from app.core.extensions import init_extensions

def create_app(config_class=Config):
    """Application Factory Pattern"""
    app = Flask(__name__)
    app.config.from_object(config_class)

    # Initialize extensions
    init_extensions(app)

    # Register blueprints
    from app.api import api_bp
    app.register_blueprint(api_bp, url_prefix='/api/v1')

    # CORS configuration
    CORS(app, resources={
        r"/api/*": {
            "origins": app.config['ALLOWED_ORIGINS'],
            "methods": ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
            "allow_headers": ["Content-Type", "Authorization"]
        }
    })

    return app
```

#### Configuration Management

```python
# app/core/config.py
import os
from datetime import timedelta

class Config:
    """Base configuration"""
    SECRET_KEY = os.environ.get('SECRET_KEY') or 'dev-secret-key'

    # Database
    SQLALCHEMY_DATABASE_URI = os.environ.get('DATABASE_URL') or 'sqlite:///app.db'
    SQLALCHEMY_TRACK_MODIFICATIONS = False
    SQLALCHEMY_ENGINE_OPTIONS = {
        'pool_pre_ping': True,
        'pool_recycle': 300,
        'pool_timeout': 20,
        'max_overflow': 0
    }

    # JWT
    JWT_SECRET_KEY = os.environ.get('JWT_SECRET_KEY') or SECRET_KEY
    JWT_ACCESS_TOKEN_EXPIRES = timedelta(hours=1)
    JWT_REFRESH_TOKEN_EXPIRES = timedelta(days=30)

    # Redis/Celery
    REDIS_URL = os.environ.get('REDIS_URL') or 'redis://localhost:6379/0'
    CELERY_BROKER_URL = REDIS_URL
    CELERY_RESULT_BACKEND = REDIS_URL

    # Security
    ALLOWED_ORIGINS = os.environ.get('ALLOWED_ORIGINS', '*').split(',')

class DevelopmentConfig(Config):
    DEBUG = True

class ProductionConfig(Config):
    DEBUG = False
    SQLALCHEMY_ENGINE_OPTIONS = {
        **Config.SQLALCHEMY_ENGINE_OPTIONS,
        'pool_size': 20,
        'max_overflow': 30
    }
```

### 4.2. API Design RESTful

#### Resource-Based Endpoints

```python
# app/api/resources/users.py
from flask import request
from flask_restx import Resource, Namespace, fields
from flask_jwt_extended import jwt_required, get_jwt_identity
from app.services.user_service import UserService
from app.utils.validators import validate_json

api = Namespace('users', description='User operations')

user_model = api.model('User', {
    'id': fields.Integer(readonly=True),
    'email': fields.String(required=True, description='User email'),
    'name': fields.String(required=True, description='User name'),
    'created_at': fields.DateTime(readonly=True)
})

@api.route('/')
class UserList(Resource):
    @api.doc('list_users')
    @api.marshal_list_with(user_model)
    @jwt_required()
    def get(self):
        """Fetch all users with pagination"""
        page = request.args.get('page', 1, type=int)
        per_page = min(request.args.get('per_page', 10, type=int), 100)

        users = UserService.get_paginated_users(page, per_page)
        return users

    @api.doc('create_user')
    @api.expect(user_model)
    @api.marshal_with(user_model, code=201)
    @jwt_required()
    @validate_json(['email', 'name'])
    def post(self):
        """Create a new user"""
        data = request.get_json()
        user = UserService.create_user(data)
        return user, 201

@api.route('/<int:user_id>')
class User(Resource):
    @api.doc('get_user')
    @api.marshal_with(user_model)
    @jwt_required()
    def get(self, user_id):
        """Fetch a user by ID"""
        user = UserService.get_user_by_id(user_id)
        if not user:
            api.abort(404, "User not found")
        return user
```

### 4.3. Service Layer Pattern

```python
# app/services/user_service.py
from typing import Optional, List, Dict, Any
from app.core.database import db
from app.models.user import User
from app.utils.exceptions import ValidationError, NotFoundError

class UserService:
    """Business logic for user operations"""

    @staticmethod
    def create_user(data: Dict[str, Any]) -> User:
        """Create a new user with validation"""
        # Validate email uniqueness
        if User.query.filter_by(email=data['email']).first():
            raise ValidationError("Email already exists")

        user = User(
            email=data['email'],
            name=data['name']
        )

        db.session.add(user)
        db.session.commit()
        return user

    @staticmethod
    def get_user_by_id(user_id: int) -> Optional[User]:
        """Get user by ID"""
        return User.query.get(user_id)

    @staticmethod
    def get_paginated_users(page: int, per_page: int) -> List[User]:
        """Get paginated list of users"""
        return User.query.paginate(
            page=page,
            per_page=per_page,
            error_out=False
        ).items

    @staticmethod
    def update_user(user_id: int, data: Dict[str, Any]) -> User:
        """Update user information"""
        user = User.query.get(user_id)
        if not user:
            raise NotFoundError("User not found")

        for key, value in data.items():
            if hasattr(user, key):
                setattr(user, key, value)

        db.session.commit()
        return user
```

### 4.4. Database Models

```python
# app/models/user.py
from datetime import datetime
from app.core.database import db

class User(db.Model):
    """User model with SQLAlchemy"""
    __tablename__ = 'users'

    id = db.Column(db.Integer, primary_key=True)
    email = db.Column(db.String(120), unique=True, nullable=False, index=True)
    name = db.Column(db.String(100), nullable=False)
    is_active = db.Column(db.Boolean, default=True, nullable=False)
    created_at = db.Column(db.DateTime, default=datetime.utcnow, nullable=False)
    updated_at = db.Column(db.DateTime, default=datetime.utcnow, onupdate=datetime.utcnow)

    def __repr__(self):
        return f'<User {self.email}>'

    def to_dict(self):
        """Convert model to dictionary"""
        return {
            'id': self.id,
            'email': self.email,
            'name': self.name,
            'is_active': self.is_active,
            'created_at': self.created_at.isoformat(),
            'updated_at': self.updated_at.isoformat()
        }
```

---

## 5. Desenvolvimento Frontend (Vue.js 3)

### 5.1. Estrutura e Organização

#### Composables (Vue 3 Composition API)

```javascript
// src/composables/useApi.js
import { ref, reactive } from 'vue'
import axios from 'axios'

export function useApi() {
  const loading = ref(false)
  const error = ref(null)
  const data = ref(null)

  const request = async (config) => {
    loading.value = true
    error.value = null

    try {
      const response = await axios(config)
      data.value = response.data
      return response.data
    } catch (err) {
      error.value = err.response?.data?.message || err.message
      throw err
    } finally {
      loading.value = false
    }
  }

  const get = (url, config = {}) => request({ method: 'GET', url, ...config })
  const post = (url, data, config = {}) => request({ method: 'POST', url, data, ...config })
  const put = (url, data, config = {}) => request({ method: 'PUT', url, data, ...config })
  const del = (url, config = {}) => request({ method: 'DELETE', url, ...config })

  return {
    loading: readonly(loading),
    error: readonly(error),
    data: readonly(data),
    get,
    post,
    put,
    delete: del
  }
}
```

#### Pinia Store

```javascript
// src/stores/user.js
import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { userApi } from '@/services/api'

export const useUserStore = defineStore('user', () => {
  // State
  const users = ref([])
  const currentUser = ref(null)
  const loading = ref(false)
  const error = ref(null)

  // Getters
  const activeUsers = computed(() =>
    users.value.filter(user => user.is_active)
  )

  const userCount = computed(() => users.value.length)

  // Actions
  async function fetchUsers() {
    loading.value = true
    error.value = null

    try {
      const response = await userApi.getUsers()
      users.value = response.data
    } catch (err) {
      error.value = err.message
      console.error('Error fetching users:', err)
    } finally {
      loading.value = false
    }
  }

  async function createUser(userData) {
    try {
      const response = await userApi.createUser(userData)
      users.value.push(response.data)
      return response.data
    } catch (err) {
      error.value = err.message
      throw err
    }
  }

  async function updateUser(userId, userData) {
    try {
      const response = await userApi.updateUser(userId, userData)
      const index = users.value.findIndex(user => user.id === userId)
      if (index !== -1) {
        users.value[index] = response.data
      }
      return response.data
    } catch (err) {
      error.value = err.message
      throw err
    }
  }

  function clearError() {
    error.value = null
  }

  return {
    // State
    users,
    currentUser,
    loading,
    error,
    // Getters
    activeUsers,
    userCount,
    // Actions
    fetchUsers,
    createUser,
    updateUser,
    clearError
  }
})
```

### 5.2. Component Structure

#### Single File Component (SFC)

```vue
<!-- src/components/UserCard.vue -->
<template>
  <div class="user-card" :class="{ 'user-card--inactive': !user.is_active }">
    <div class="user-card__header">
      <h3 class="user-card__name">{{ user.name }}</h3>
      <span class="user-card__status" :class="statusClass">
        {{ user.is_active ? 'Ativo' : 'Inativo' }}
      </span>
    </div>

    <div class="user-card__content">
      <p class="user-card__email">{{ user.email }}</p>
      <p class="user-card__date">
        Criado em: {{ formatDate(user.created_at) }}
      </p>
    </div>

    <div class="user-card__actions">
      <button
        @click="$emit('edit', user)"
        class="btn btn--primary"
        :disabled="loading"
      >
        Editar
      </button>
      <button
        @click="$emit('delete', user)"
        class="btn btn--danger"
        :disabled="loading"
      >
        Excluir
      </button>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { formatDate } from '@/utils/dateUtils'

// Props
const props = defineProps({
  user: {
    type: Object,
    required: true
  },
  loading: {
    type: Boolean,
    default: false
  }
})

// Emits
const emit = defineEmits(['edit', 'delete'])

// Computed
const statusClass = computed(() => ({
  'status--active': props.user.is_active,
  'status--inactive': !props.user.is_active
}))
</script>

<style scoped>
.user-card {
  @apply bg-white rounded-lg shadow-md p-6 border border-gray-200;
  @apply transition-all duration-200 hover:shadow-lg;
}

.user-card--inactive {
  @apply opacity-60;
}

.user-card__header {
  @apply flex justify-between items-center mb-4;
}

.user-card__name {
  @apply text-lg font-semibold text-gray-900;
}

.user-card__status {
  @apply px-2 py-1 rounded-full text-xs font-medium;
}

.status--active {
  @apply bg-green-100 text-green-800;
}

.status--inactive {
  @apply bg-red-100 text-red-800;
}

.user-card__content {
  @apply mb-4;
}

.user-card__email {
  @apply text-gray-600 mb-2;
}

.user-card__date {
  @apply text-sm text-gray-500;
}

.user-card__actions {
  @apply flex gap-2;
}

.btn {
  @apply px-4 py-2 rounded-md font-medium transition-colors duration-200;
  @apply focus:outline-none focus:ring-2 focus:ring-offset-2;
}

.btn--primary {
  @apply bg-blue-600 text-white hover:bg-blue-700 focus:ring-blue-500;
}

.btn--danger {
  @apply bg-red-600 text-white hover:bg-red-700 focus:ring-red-500;
}

.btn:disabled {
  @apply opacity-50 cursor-not-allowed;
}
</style>
```

### 5.3. Router Configuration

```javascript
// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/views/HomeView.vue')
  },
  {
    path: '/users',
    name: 'Users',
    component: () => import('@/views/UsersView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/LoginView.vue'),
    meta: { guest: true }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/NotFoundView.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Navigation guards
router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next('/login')
  } else if (to.meta.guest && authStore.isAuthenticated) {
    next('/')
  } else {
    next()
  }
})

export default router
```

---

## 6. Segurança (Flask + Vue.js)

### 6.1. Autenticação JWT

#### Backend JWT Implementation

```python
# app/auth/routes.py
from flask import request, jsonify
from flask_jwt_extended import create_access_token, create_refresh_token, jwt_required, get_jwt_identity
from werkzeug.security import check_password_hash
from app.models.user import User
from app.utils.validators import validate_json

@auth_bp.route('/login', methods=['POST'])
@validate_json(['email', 'password'])
def login():
    """User authentication endpoint"""
    data = request.get_json()

    user = User.query.filter_by(email=data['email']).first()

    if not user or not check_password_hash(user.password_hash, data['password']):
        return jsonify({'message': 'Invalid credentials'}), 401

    if not user.is_active:
        return jsonify({'message': 'Account is disabled'}), 401

    access_token = create_access_token(
        identity=user.id,
        additional_claims={'email': user.email, 'name': user.name}
    )
    refresh_token = create_refresh_token(identity=user.id)

    return jsonify({
        'access_token': access_token,
        'refresh_token': refresh_token,
        'user': user.to_dict()
    }), 200

@auth_bp.route('/refresh', methods=['POST'])
@jwt_required(refresh=True)
def refresh():
    """Token refresh endpoint"""
    current_user_id = get_jwt_identity()
    user = User.query.get(current_user_id)

    if not user or not user.is_active:
        return jsonify({'message': 'User not found or inactive'}), 404

    new_token = create_access_token(
        identity=current_user_id,
        additional_claims={'email': user.email, 'name': user.name}
    )

    return jsonify({'access_token': new_token}), 200
```

#### Frontend Auth Service

```javascript
// src/services/authService.js
import axios from 'axios'
import { useAuthStore } from '@/stores/auth'

class AuthService {
  constructor() {
    this.setupInterceptors()
  }

  setupInterceptors() {
    // Request interceptor to add token
    axios.interceptors.request.use(
      (config) => {
        const authStore = useAuthStore()
        const token = authStore.accessToken

        if (token) {
          config.headers.Authorization = `Bearer ${token}`
        }

        return config
      },
      (error) => Promise.reject(error)
    )

    // Response interceptor for token refresh
    axios.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalRequest = error.config

        if (error.response?.status === 401 && !originalRequest._retry) {
          originalRequest._retry = true

          try {
            await this.refreshToken()
            return axios(originalRequest)
          } catch (refreshError) {
            this.logout()
            return Promise.reject(refreshError)
          }
        }

        return Promise.reject(error)
      }
    )
  }

  async login(credentials) {
    try {
      const response = await axios.post('/api/v1/auth/login', credentials)
      const { access_token, refresh_token, user } = response.data

      const authStore = useAuthStore()
      authStore.setTokens(access_token, refresh_token)
      authStore.setUser(user)

      return user
    } catch (error) {
      throw new Error(error.response?.data?.message || 'Login failed')
    }
  }

  async refreshToken() {
    const authStore = useAuthStore()
    const refreshToken = authStore.refreshToken

    if (!refreshToken) {
      throw new Error('No refresh token available')
    }

    try {
      const response = await axios.post('/api/v1/auth/refresh', {}, {
        headers: { Authorization: `Bearer ${refreshToken}` }
      })

      const { access_token } = response.data
      authStore.setAccessToken(access_token)

      return access_token
    } catch (error) {
      authStore.clearAuth()
      throw error
    }
  }

  logout() {
    const authStore = useAuthStore()
    authStore.clearAuth()
    window.location.href = '/login'
  }
}

export default new AuthService()
```

### 6.2. Input Validation & Sanitization

```python
# app/utils/validators.py
from functools import wraps
from flask import request, jsonify
import re
from typing import List, Dict, Any

def validate_json(required_fields: List[str]):
    """Decorator to validate JSON input"""
    def decorator(f):
        @wraps(f)
        def decorated_function(*args, **kwargs):
            if not request.is_json:
                return jsonify({'message': 'Content-Type must be application/json'}), 400

            data = request.get_json()
            if not data:
                return jsonify({'message': 'No JSON data provided'}), 400

            # Check required fields
            missing_fields = [field for field in required_fields if field not in data]
            if missing_fields:
                return jsonify({
                    'message': f'Missing required fields: {", ".join(missing_fields)}'
                }), 400

            # Sanitize input
            sanitized_data = sanitize_input(data)
            request._cached_json = sanitized_data

            return f(*args, **kwargs)
        return decorated_function
    return decorator

def sanitize_input(data: Dict[str, Any]) -> Dict[str, Any]:
    """Sanitize input data"""
    if isinstance(data, dict):
        return {key: sanitize_input(value) for key, value in data.items()}
    elif isinstance(data, list):
        return [sanitize_input(item) for item in data]
    elif isinstance(data, str):
        # Remove potentially dangerous characters
        return re.sub(r'[<>"\']', '', data.strip())
    else:
        return data

def validate_email(email: str) -> bool:
    """Validate email format"""
    pattern = r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'
    return re.match(pattern, email) is not None
```

### 6.3. CORS & Security Headers

```python
# app/core/security.py
from flask import Flask
from flask_talisman import Talisman

def init_security(app: Flask):
    """Initialize security configurations"""

    # Content Security Policy
    csp = {
        'default-src': "'self'",
        'script-src': [
            "'self'",
            "'unsafe-inline'",  # For Vue.js development
            "https://unpkg.com",  # CDN fallback
            "https://cdn.jsdelivr.net"
        ],
        'style-src': [
            "'self'",
            "'unsafe-inline'",
            "https://fonts.googleapis.com",
            "https://unpkg.com"
        ],
        'font-src': [
            "'self'",
            "https://fonts.gstatic.com"
        ],
        'img-src': [
            "'self'",
            "data:",
            "https:"
        ]
    }

    # Initialize Talisman for security headers
    Talisman(
        app,
        force_https=app.config.get('FORCE_HTTPS', False),
        content_security_policy=csp,
        content_security_policy_nonce_in=['script-src', 'style-src']
    )

    @app.after_request
    def after_request(response):
        # Additional security headers
        response.headers['X-Content-Type-Options'] = 'nosniff'
        response.headers['X-Frame-Options'] = 'DENY'
        response.headers['X-XSS-Protection'] = '1; mode=block'
        response.headers['Referrer-Policy'] = 'strict-origin-when-cross-origin'

        return response
```

---

## 7. Testes

### 7.1. Backend Testing (Pytest)

```python
# tests/conftest.py
import pytest
from app import create_app
from app.core.database import db
from app.core.config import TestingConfig

@pytest.fixture
def app():
    """Create application for testing"""
    app = create_app(TestingConfig)

    with app.app_context():
        db.create_all()
        yield app
        db.drop_all()

@pytest.fixture
def client(app):
    """Create test client"""
    return app.test_client()

@pytest.fixture
def auth_headers(client):
    """Create authentication headers"""
    # Create test user and login
    response = client.post('/api/v1/auth/login', json={
        'email': 'test@example.com',
        'password': 'testpassword'
    })

    token = response.json['access_token']
    return {'Authorization': f'Bearer {token}'}
```

```python
# tests/test_users.py
import pytest
from app.models.user import User

class TestUserAPI:
    """Test user API endpoints"""

    def test_get_users_success(self, client, auth_headers):
        """Test successful user retrieval"""
        response = client.get('/api/v1/users/', headers=auth_headers)

        assert response.status_code == 200
        assert isinstance(response.json, list)

    def test_create_user_success(self, client, auth_headers):
        """Test successful user creation"""
        user_data = {
            'email': 'newuser@example.com',
            'name': 'New User'
        }

        response = client.post('/api/v1/users/',
                             json=user_data,
                             headers=auth_headers)

        assert response.status_code == 201
        assert response.json['email'] == user_data['email']
        assert response.json['name'] == user_data['name']

    def test_create_user_duplicate_email(self, client, auth_headers):
        """Test user creation with duplicate email"""
        user_data = {
            'email': 'existing@example.com',
            'name': 'Existing User'
        }

        # Create first user
        client.post('/api/v1/users/', json=user_data, headers=auth_headers)

        # Try to create duplicate
        response = client.post('/api/v1/users/',
                             json=user_data,
                             headers=auth_headers)

        assert response.status_code == 400
        assert 'already exists' in response.json['message']
```

### 7.2. Frontend Testing (Vitest + Testing Library)

```javascript
// tests/components/UserCard.test.js
import { describe, it, expect, vi } from 'vitest'
import { mount } from '@vue/test-utils'
import UserCard from '@/components/UserCard.vue'

describe('UserCard', () => {
  const mockUser = {
    id: 1,
    name: 'John Doe',
    email: 'john@example.com',
    is_active: true,
    created_at: '2023-01-01T00:00:00Z'
  }

  it('renders user information correctly', () => {
    const wrapper = mount(UserCard, {
      props: { user: mockUser }
    })

    expect(wrapper.text()).toContain('John Doe')
    expect(wrapper.text()).toContain('john@example.com')
    expect(wrapper.text()).toContain('Ativo')
  })

  it('emits edit event when edit button is clicked', async () => {
    const wrapper = mount(UserCard, {
      props: { user: mockUser }
    })

    await wrapper.find('.btn--primary').trigger('click')

    expect(wrapper.emitted('edit')).toBeTruthy()
    expect(wrapper.emitted('edit')[0]).toEqual([mockUser])
  })

  it('shows inactive status for inactive users', () => {
    const inactiveUser = { ...mockUser, is_active: false }
    const wrapper = mount(UserCard, {
      props: { user: inactiveUser }
    })

    expect(wrapper.text()).toContain('Inativo')
    expect(wrapper.classes()).toContain('user-card--inactive')
  })
})
```

### 7.3. E2E Testing (Playwright)

```javascript
// tests/e2e/user-management.spec.js
import { test, expect } from '@playwright/test'

test.describe('User Management', () => {
  test.beforeEach(async ({ page }) => {
    // Login before each test
    await page.goto('/login')
    await page.fill('[data-testid="email"]', 'admin@example.com')
    await page.fill('[data-testid="password"]', 'password')
    await page.click('[data-testid="login-button"]')
    await expect(page).toHaveURL('/')
  })

  test('should create a new user', async ({ page }) => {
    await page.goto('/users')
    await page.click('[data-testid="add-user-button"]')

    await page.fill('[data-testid="user-name"]', 'Test User')
    await page.fill('[data-testid="user-email"]', 'test@example.com')
    await page.click('[data-testid="save-button"]')

    await expect(page.locator('[data-testid="user-card"]')).toContainText('Test User')
  })

  test('should edit existing user', async ({ page }) => {
    await page.goto('/users')
    await page.click('[data-testid="edit-user-button"]')

    await page.fill('[data-testid="user-name"]', 'Updated Name')
    await page.click('[data-testid="save-button"]')

    await expect(page.locator('[data-testid="user-card"]')).toContainText('Updated Name')
  })
})
```

---

## 8. Deployment e Operações

### 8.1. Docker Configuration

#### Backend Dockerfile

```dockerfile
# backend/Dockerfile
FROM python:3.11-slim

WORKDIR /app

# Install system dependencies
RUN apt-get update && apt-get install -y \
    gcc \
    g++ \
    && rm -rf /var/lib/apt/lists/*

# Copy requirements and install Python dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy application code
COPY . .

# Create non-root user
RUN useradd --create-home --shell /bin/bash app \
    && chown -R app:app /app
USER app

# Health check
HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:5000/health || exit 1

EXPOSE 5000

CMD ["gunicorn", "--bind", "0.0.0.0:5000", "--workers", "4", "--timeout", "120", "app:create_app()"]
```

#### Frontend Dockerfile

```dockerfile
# frontend/Dockerfile
FROM node:18-alpine as build

WORKDIR /app

# Copy package files
COPY package*.json ./
RUN npm ci --only=production

# Copy source code and build
COPY . .
RUN npm run build

# Production stage
FROM nginx:alpine

# Copy built assets
COPY --from=build /app/dist /usr/share/nginx/html

# Copy nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:80 || exit 1

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
```

### 8.2. Docker Compose

```yaml
# docker-compose.yml
version: '3.8'

services:
  backend:
    build: ./backend
    ports:
      - "5000:5000"
    environment:
      - DATABASE_URL=postgresql://user:password@db:5432/appdb
      - REDIS_URL=redis://redis:6379/0
      - SECRET_KEY=${SECRET_KEY}
      - JWT_SECRET_KEY=${JWT_SECRET_KEY}
    depends_on:
      - db
      - redis
    volumes:
      - ./backend:/app
    restart: unless-stopped

  frontend:
    build: ./frontend
    ports:
      - "80:80"
    depends_on:
      - backend
    restart: unless-stopped

  db:
    image: postgres:15-alpine
    environment:
      - POSTGRES_DB=appdb
      - POSTGRES_USER=user
      - POSTGRES_PASSWORD=password
    volumes:
      - postgres_data:/var/lib/postgresql/data
    restart: unless-stopped

  redis:
    image: redis:7-alpine
    volumes:
      - redis_data:/data
    restart: unless-stopped

  celery:
    build: ./backend
    command: celery -A app.celery worker --loglevel=info
    environment:
      - DATABASE_URL=postgresql://user:password@db:5432/appdb
      - REDIS_URL=redis://redis:6379/0
    depends_on:
      - db
      - redis
    volumes:
      - ./backend:/app
    restart: unless-stopped

volumes:
  postgres_data:
  redis_data:
```

### 8.3. CI/CD Pipeline (GitHub Actions)

```yaml
# .github/workflows/ci-cd.yml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
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

    steps:
    - uses: actions/checkout@v4

    - name: Set up Python
      uses: actions/setup-python@v4
      with:
        python-version: '3.11'

    - name: Install dependencies
      run: |
        cd backend
        pip install -r requirements.txt
        pip install pytest pytest-cov

    - name: Run tests
      run: |
        cd backend
        pytest --cov=app --cov-report=xml
      env:
        DATABASE_URL: postgresql://postgres:postgres@localhost:5432/test_db

    - name: Upload coverage
      uses: codecov/codecov-action@v3
      with:
        file: ./backend/coverage.xml

  test-frontend:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v4

    - name: Set up Node.js
      uses: actions/setup-node@v4
      with:
        node-version: '18'
        cache: 'npm'
        cache-dependency-path: frontend/package-lock.json

    - name: Install dependencies
      run: |
        cd frontend
        npm ci

    - name: Run tests
      run: |
        cd frontend
        npm run test:unit
        npm run test:e2e

    - name: Build
      run: |
        cd frontend
        npm run build

  deploy:
    needs: [test-backend, test-frontend]
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'

    steps:
    - uses: actions/checkout@v4

    - name: Deploy to production
      run: |
        echo "Deploying to production..."
        # Add deployment commands here
```

---

## 9. Métricas e KPIs

### 9.1. Métricas de Performance

| Métrica | Objetivo | Ferramenta |
|---------|----------|------------|
| **Response Time API** | < 200ms | Prometheus + Grafana |
| **Frontend Load Time** | < 2s | Lighthouse CI |
| **Database Query Time** | < 50ms | SQLAlchemy logging |
| **Memory Usage** | < 512MB | Docker stats |
| **CPU Usage** | < 70% | System monitoring |

### 9.2. Métricas de Qualidade

| Métrica | Objetivo | Ferramenta |
|---------|----------|------------|
| **Test Coverage** | > 80% | pytest-cov, vitest |
| **Code Quality** | Grade A | SonarQube |
| **Security Score** | 0 vulnerabilities | Snyk, Bandit |
| **Bundle Size** | < 1MB | Webpack Bundle Analyzer |
| **Accessibility** | WCAG 2.1 AA | axe-core |

---

## 10. Segurança

### 10.1. Boas Práticas

- **Nunca** hardcode credenciais no código
- Use **variáveis de ambiente** para configurações sensíveis
- Implemente **rate limiting** em APIs
- Use **HTTPS** em produção
- Valide **todas as entradas** do usuário
- Implemente **logging de segurança**

### 10.2. Checklist de Segurança

- [ ] Autenticação JWT implementada
- [ ] Validação de entrada em todas as rotas
- [ ] CORS configurado corretamente
- [ ] Headers de segurança configurados
- [ ] Rate limiting implementado
- [ ] Logs de segurança configurados
- [ ] Secrets gerenciados adequadamente
- [ ] HTTPS configurado em produção

---

## 11. Acessibilidade

### 11.1. Padrões WCAG 2.1

- **Contraste**: Mínimo 4.5:1 para texto normal
- **Navegação**: Suporte completo por teclado
- **Screen readers**: ARIA labels apropriados
- **Responsive**: Funcional em todos os dispositivos
- **Performance**: Carregamento rápido

---

## 12. Versionamento

### 12.1. Histórico de Versões

| Versão | Data | Principais Mudanças |
|--------|------|-------------------|
| **1.3** | 15/09/2025 | Estimativas AI-First, Vue 3 Composition API |
| **1.2** | 01/08/2025 | Segurança aprimorada, Docker otimizado |
| **1.1** | 15/07/2025 | Testes E2E, CI/CD melhorado |
| **1.0** | 01/07/2025 | Versão inicial |

---

## 13. Contribuição

### 13.1. Como Contribuir

1. **Fork** o repositório
2. **Crie** uma branch: `git checkout -b feature/nova-funcionalidade`
3. **Implemente** seguindo estas diretrizes
4. **Teste** completamente
5. **Submeta** um Pull Request

---

## 14. Suporte

### 14.1. Canais de Suporte

- **📧 Email**: <dev@datametria.io>
- **💬 Discord**: [discord.gg/datametria](https://discord.gg/datametria)
- **🐛 Issues**: [GitHub Issues](https://github.com/datametria/web-standards/issues)

---

## 15. Referências

### 15.1. Documentação Oficial

- **[Flask](https://flask.palletsprojects.com/)** - Framework backend
- **[Vue.js 3](https://vuejs.org/)** - Framework frontend
- **[SQLAlchemy](https://sqlalchemy.org/)** - ORM Python
- **[Pinia](https://pinia.vuejs.org/)** - State management

### 15.2. Diretrizes DATAMETRIA Relacionadas

- **[UX/UI Design](datametria_std_ux_ui.md)** - Padrões de interface
- **[Security Development](datametria_std_security.md)** - Segurança
- **[Python Automation](datametria_std_python_automation.md)** - Python
- **[Documentation](datametria_std_documentation.md)** - Documentação

---

**Desenvolvido com ❤️ pela equipe DATAMETRIA**

*Para dúvidas ou sugestões sobre este documento, entre em contato com a equipe de arquitetura.*
