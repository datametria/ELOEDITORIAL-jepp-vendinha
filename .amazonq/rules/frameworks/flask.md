# Flask: Rules Específicas DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule F.1: Blueprints para Modularização

### Contexto

Flask sem blueprints causa:

- Código monolítico difícil de manter
- Impossível escalar equipe (conflitos constantes)
- Rotas desorganizadas em arquivo único
- Dificuldade de testar módulos isoladamente

### Regra

Projetos Flask DEVEM usar Blueprints para:

- **Separação por domínio**: auth, users, products, orders
- **Prefixo de URL**: `/api/v1/auth`, `/api/v1/users`
- **Estrutura**: 1 blueprint por feature/domínio
- **Registro**: Centralizado em `app/__init__.py`

### Justificativa

- Modularização facilita manutenção
- Equipes trabalham em paralelo sem conflitos
- Testes isolados por módulo
- Reutilização de blueprints entre projetos

### Exemplos

#### ✅ Correto

```python
# app/blueprints/auth/__init__.py
from flask import Blueprint

auth_bp = Blueprint('auth', __name__, url_prefix='/api/v1/auth')

from . import routes

# app/blueprints/auth/routes.py
from . import auth_bp
from flask import request, jsonify

@auth_bp.route('/login', methods=['POST'])
def login():
    """Endpoint de login."""
    data = request.get_json()
    # Lógica de autenticação
    return jsonify({"token": "..."}), 200

@auth_bp.route('/logout', methods=['POST'])
def logout():
    """Endpoint de logout."""
    return jsonify({"message": "Logged out"}), 200

# app/blueprints/users/__init__.py
from flask import Blueprint

users_bp = Blueprint('users', __name__, url_prefix='/api/v1/users')

from . import routes

# app/__init__.py
from flask import Flask

def create_app():
    app = Flask(__name__)

    # Registrar blueprints
    from app.blueprints.auth import auth_bp
    from app.blueprints.users import users_bp

    app.register_blueprint(auth_bp)
    app.register_blueprint(users_bp)

    return app
```

#### ❌ Incorreto

```python
# app.py - Tudo em um arquivo
from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/api/v1/auth/login', methods=['POST'])
def login():
    pass

@app.route('/api/v1/auth/logout', methods=['POST'])
def logout():
    pass

@app.route('/api/v1/users', methods=['GET'])
def get_users():
    pass

# 50+ rotas no mesmo arquivo!
```

### Ferramentas

- **Flask-Blueprints**: Built-in do Flask
- **Flask-RESTX**: Blueprints com Swagger automático

### Checklist

- [ ] Blueprints por domínio/feature?
- [ ] Prefixo de URL definido?
- [ ] Registro centralizado em create_app()?
- [ ] Estrutura de diretórios por blueprint?

---

## Rule F.2: Application Factory Pattern

### Contexto

Flask sem factory pattern causa:

- Impossível ter múltiplas instâncias (dev, test, prod)
- Configuração hardcoded
- Testes difíceis (app global)
- Circular imports

### Regra

Flask DEVE usar Application Factory:

- **Função create_app()**: Retorna instância configurada
- **Configuração por ambiente**: dev, test, prod
- **Extensões**: Inicializadas com `init_app()`
- **Sem app global**: Apenas dentro de create_app()

### Justificativa

- Múltiplas instâncias para testes
- Configuração flexível por ambiente
- Evita circular imports
- Padrão recomendado pela documentação Flask

### Exemplos

#### ✅ Correto

```python
# app/__init__.py
from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from flask_migrate import Migrate
from flask_jwt_extended import JWTManager

# Extensões (não inicializadas)
db = SQLAlchemy()
migrate = Migrate()
jwt = JWTManager()

def create_app(config_name='development'):
    """Application Factory."""
    app = Flask(__name__)

    # Carregar configuração
    app.config.from_object(f'config.{config_name.capitalize()}Config')

    # Inicializar extensões
    db.init_app(app)
    migrate.init_app(app, db)
    jwt.init_app(app)

    # Registrar blueprints
    from app.blueprints.auth import auth_bp
    from app.blueprints.users import users_bp

    app.register_blueprint(auth_bp)
    app.register_blueprint(users_bp)

    # Error handlers
    register_error_handlers(app)

    return app

# config.py
import os

class Config:
    """Configuração base."""
    SECRET_KEY = os.getenv('SECRET_KEY', 'dev-secret-key')
    SQLALCHEMY_TRACK_MODIFICATIONS = False

class DevelopmentConfig(Config):
    """Configuração de desenvolvimento."""
    DEBUG = True
    SQLALCHEMY_DATABASE_URI = os.getenv('DEV_DATABASE_URL')

class TestingConfig(Config):
    """Configuração de testes."""
    TESTING = True
    SQLALCHEMY_DATABASE_URI = 'sqlite:///:memory:'

class ProductionConfig(Config):
    """Configuração de produção."""
    DEBUG = False
    SQLALCHEMY_DATABASE_URI = os.getenv('DATABASE_URL')

# run.py
from app import create_app

app = create_app('development')

if __name__ == '__main__':
    app.run()

# tests/conftest.py
import pytest
from app import create_app, db

@pytest.fixture
def app():
    """Fixture de app para testes."""
    app = create_app('testing')

    with app.app_context():
        db.create_all()
        yield app
        db.drop_all()

@pytest.fixture
def client(app):
    """Fixture de client para testes."""
    return app.test_client()
```

#### ❌ Incorreto

```python
# app.py - App global
from flask import Flask
from flask_sqlalchemy import SQLAlchemy

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'postgresql://...'  # Hardcoded!
db = SQLAlchemy(app)

@app.route('/users')
def get_users():
    pass

# Impossível testar com configuração diferente!
```

### Ferramentas

- **Flask Application Factory**: Padrão oficial
- **Flask-Env**: Gerenciamento de ambientes

### Checklist

- [ ] Função create_app() implementada?
- [ ] Configuração por ambiente?
- [ ] Extensões com init_app()?
- [ ] Sem app global?

---

## Rule F.3: SQLAlchemy com Migrations (Alembic)

### Contexto

Flask sem migrations causa:

- Schema de BD desatualizado
- Mudanças manuais propensas a erro
- Impossível rollback de schema
- Ambientes inconsistentes (dev vs prod)

### Regra

Flask com SQLAlchemy DEVE usar Alembic/Flask-Migrate:

- **Migrations automáticas**: `flask db migrate`
- **Versionamento**: Cada mudança = 1 migration
- **Rollback**: Possível voltar versões
- **CI/CD**: Migrations em deploy automático

### Justificativa

- Schema versionado como código
- Rollback seguro de mudanças
- Ambientes sincronizados
- Auditoria de mudanças de schema

### Exemplos

#### ✅ Correto

```python
# app/models/user.py
from app import db
from datetime import datetime

class User(db.Model):
    __tablename__ = 'users'

    id = db.Column(db.String(36), primary_key=True)
    email = db.Column(db.String(120), unique=True, nullable=False, index=True)
    nome = db.Column(db.String(100), nullable=False)
    created_at = db.Column(db.DateTime, nullable=False, default=datetime.utcnow)

    def __repr__(self):
        return f'<User {self.email}>'

# Comandos de migration
# flask db init                    # Inicializa migrations (1x)
# flask db migrate -m "Add users"  # Cria migration
# flask db upgrade                 # Aplica migrations
# flask db downgrade               # Rollback

# migrations/versions/abc123_add_users.py (gerado automaticamente)
"""Add users table

Revision ID: abc123
Revises:
Create Date: 2025-10-19 10:00:00.000000

"""
from alembic import op
import sqlalchemy as sa

revision = 'abc123'
down_revision = None
branch_labels = None
depends_on = None

def upgrade():
    op.create_table('users',
        sa.Column('id', sa.String(36), nullable=False),
        sa.Column('email', sa.String(120), nullable=False),
        sa.Column('nome', sa.String(100), nullable=False),
        sa.Column('created_at', sa.DateTime(), nullable=False),
        sa.PrimaryKeyConstraint('id'),
        sa.UniqueConstraint('email')
    )
    op.create_index('ix_users_email', 'users', ['email'])

def downgrade():
    op.drop_index('ix_users_email', 'users')
    op.drop_table('users')
```

#### ❌ Incorreto

```python
# Sem migrations - SQL manual
# schema.sql
CREATE TABLE users (
    id VARCHAR(36) PRIMARY KEY,
    email VARCHAR(120) UNIQUE NOT NULL,
    nome VARCHAR(100) NOT NULL
);

# Problemas:
# - Sem versionamento
# - Sem rollback
# - Mudanças manuais propensas a erro
# - Ambientes desincronizados
```

### Ferramentas

- **Flask-Migrate**: Wrapper do Alembic para Flask
- **Alembic**: Engine de migrations
- **flask db**: CLI para migrations

### Checklist

- [ ] Flask-Migrate configurado?
- [ ] Migrations versionadas?
- [ ] CI/CD aplica migrations?
- [ ] Rollback testado?

---

## Rule F.4: Error Handlers Centralizados

### Contexto

Flask sem error handlers causa:

- Erros expostos ao cliente (stack traces)
- Respostas inconsistentes
- Informações sensíveis vazadas
- Logs inadequados

### Regra

Flask DEVE ter error handlers centralizados:

- **400-499**: Erros de cliente (Bad Request, Unauthorized, etc)
- **500-599**: Erros de servidor (Internal Error)
- **Formato JSON**: Resposta padronizada
- **Logging**: Todos os erros logados

### Justificativa

- Respostas consistentes
- Segurança (sem stack traces)
- Debugging facilitado (logs centralizados)
- UX melhor (mensagens claras)

### Exemplos

#### ✅ Correto

```python
# app/errors.py
from flask import jsonify
import logging

logger = logging.getLogger(__name__)

class APIError(Exception):
    """Erro base da API."""
    status_code = 500

    def __init__(self, message, status_code=None, payload=None):
        super().__init__()
        self.message = message
        if status_code is not None:
            self.status_code = status_code
        self.payload = payload

    def to_dict(self):
        rv = dict(self.payload or ())
        rv['error'] = self.message
        rv['status_code'] = self.status_code
        return rv

class ValidationError(APIError):
    """Erro de validação (400)."""
    status_code = 400

class UnauthorizedError(APIError):
    """Erro de autenticação (401)."""
    status_code = 401

class NotFoundError(APIError):
    """Recurso não encontrado (404)."""
    status_code = 404

def register_error_handlers(app):
    """Registra error handlers centralizados."""

    @app.errorhandler(APIError)
    def handle_api_error(error):
        """Handler para erros customizados."""
        logger.error(f"API Error: {error.message}", exc_info=True)
        response = jsonify(error.to_dict())
        response.status_code = error.status_code
        return response

    @app.errorhandler(400)
    def bad_request(error):
        """Handler para Bad Request."""
        logger.warning(f"Bad Request: {error}")
        return jsonify({
            'error': 'Bad Request',
            'message': str(error),
            'status_code': 400
        }), 400

    @app.errorhandler(401)
    def unauthorized(error):
        """Handler para Unauthorized."""
        logger.warning(f"Unauthorized: {error}")
        return jsonify({
            'error': 'Unauthorized',
            'message': 'Authentication required',
            'status_code': 401
        }), 401

    @app.errorhandler(404)
    def not_found(error):
        """Handler para Not Found."""
        return jsonify({
            'error': 'Not Found',
            'message': 'Resource not found',
            'status_code': 404
        }), 404

    @app.errorhandler(500)
    def internal_error(error):
        """Handler para Internal Server Error."""
        logger.error(f"Internal Error: {error}", exc_info=True)
        return jsonify({
            'error': 'Internal Server Error',
            'message': 'An unexpected error occurred',
            'status_code': 500
        }), 500

# Uso em routes
from app.errors import ValidationError, NotFoundError

@users_bp.route('/<user_id>', methods=['GET'])
def get_user(user_id):
    """Busca usuário por ID."""
    user = User.query.get(user_id)

    if not user:
        raise NotFoundError(f'User {user_id} not found')

    return jsonify(user.to_dict()), 200

@users_bp.route('/', methods=['POST'])
def create_user():
    """Cria novo usuário."""
    data = request.get_json()

    if not data.get('email'):
        raise ValidationError('Email is required')

    # Lógica de criação
    return jsonify(user.to_dict()), 201
```

#### ❌ Incorreto

```python
# Sem error handlers
@app.route('/users/<user_id>')
def get_user(user_id):
    user = User.query.get(user_id)
    return jsonify(user.to_dict())  # Erro se user = None!

# Resposta de erro expõe stack trace:
# Traceback (most recent call last):
#   File "app.py", line 10, in get_user
#     return jsonify(user.to_dict())
# AttributeError: 'NoneType' object has no attribute 'to_dict'
```

### Ferramentas

- **Flask errorhandler**: Decorator built-in
- **Python logging**: Logging estruturado

### Checklist

- [ ] Error handlers para 400-499?
- [ ] Error handlers para 500-599?
- [ ] Formato JSON padronizado?
- [ ] Logging de erros configurado?

---

## Rule F.5: Request Validation com Marshmallow/Pydantic

### Contexto

Flask sem validação de request causa:

- Dados inválidos no BD
- Erros em runtime
- Vulnerabilidades de segurança
- Código de validação espalhado

### Regra

Flask DEVE validar requests com schemas:

- **Marshmallow** ou **Pydantic**: Validação declarativa
- **Deserialização**: JSON → Objeto Python
- **Serialização**: Objeto → JSON
- **Validação**: Tipo, formato, required, custom

### Justificativa

- Validação centralizada e reutilizável
- Código limpo (sem ifs de validação)
- Documentação automática (OpenAPI)
- Segurança (previne injeções)

### Exemplos

#### ✅ Correto (Marshmallow)

```python
# app/schemas/user.py
from marshmallow import Schema, fields, validate, validates, ValidationError

class UserCreateSchema(Schema):
    """Schema para criação de usuário."""
    email = fields.Email(required=True)
    nome = fields.Str(required=True, validate=validate.Length(min=3, max=100))
    idade = fields.Int(required=True, validate=validate.Range(min=18, max=120))
    senha = fields.Str(required=True, validate=validate.Length(min=8))

    @validates('senha')
    def validate_senha(self, value):
        """Valida força da senha."""
        if not any(c.isupper() for c in value):
            raise ValidationError('Senha deve ter maiúscula')
        if not any(c.isdigit() for c in value):
            raise ValidationError('Senha deve ter número')

class UserSchema(Schema):
    """Schema de usuário (resposta)."""
    id = fields.Str(dump_only=True)
    email = fields.Email()
    nome = fields.Str()
    idade = fields.Int()
    created_at = fields.DateTime(dump_only=True)

# app/blueprints/users/routes.py
from app.schemas.user import UserCreateSchema, UserSchema
from marshmallow import ValidationError as MarshmallowValidationError

user_create_schema = UserCreateSchema()
user_schema = UserSchema()

@users_bp.route('/', methods=['POST'])
def create_user():
    """Cria novo usuário."""
    try:
        # Validação e deserialização
        data = user_create_schema.load(request.get_json())
    except MarshmallowValidationError as err:
        return jsonify({'errors': err.messages}), 400

    # Criar usuário
    user = User(**data)
    db.session.add(user)
    db.session.commit()

    # Serialização
    return user_schema.dump(user), 201

@users_bp.route('/<user_id>', methods=['GET'])
def get_user(user_id):
    """Busca usuário por ID."""
    user = User.query.get_or_404(user_id)
    return user_schema.dump(user), 200
```

#### ❌ Incorreto

```python
# Sem validação
@users_bp.route('/', methods=['POST'])
def create_user():
    data = request.get_json()

    # Validação manual espalhada
    if not data.get('email'):
        return jsonify({'error': 'Email required'}), 400
    if '@' not in data['email']:
        return jsonify({'error': 'Invalid email'}), 400
    if len(data.get('nome', '')) < 3:
        return jsonify({'error': 'Nome too short'}), 400
    # ... 20+ linhas de validação manual

    user = User(**data)
    db.session.add(user)
    db.session.commit()

    return jsonify({'id': user.id}), 201
```

### Ferramentas

- **Marshmallow**: Serialização/validação
- **Flask-Marshmallow**: Integração com Flask
- **Pydantic**: Alternativa com type hints

### Checklist

- [ ] Schemas para validação?
- [ ] Deserialização automática?
- [ ] Serialização automática?
- [ ] Validações customizadas?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Blueprints por feature | 100% | Estrutura de diretórios |
| Application Factory | 100% | Code review |
| Migrations versionadas | 100% | Diretório migrations/ |
| Error handlers | 100% | Testes de erro |
| Request validation | 100% endpoints | Schema coverage |

---

**Próxima revisão:** 2026-01-19
**Mantido por:** Vander Loto - CTO DATAMETRIA
