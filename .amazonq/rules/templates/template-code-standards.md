# Template de Padrões de Código

<div align="center">

## Guia Completo de Padrões e Convenções de Código

[![Code Standards](https://img.shields.io/badge/code-standards-blue)](https://github.com/datametria/standards)
[![Quality](https://img.shields.io/badge/quality-assured-green)](https://github.com/datametria/standards)
[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Standards-blue)](https://github.com/datametria/standards)

[📝 Convenções](#-convenções-gerais) • [🐍 Python](#-python) • [🌐 JavaScript](#-javascripttypescript) • [🎨 CSS](#-css) • [🔧 Ferramentas](#-ferramentas-de-qualidade)

</div>

---

## 📋 Informações do Projeto

| Campo | Descrição |
|-------|-----------|
| **Nome do Projeto** | [Nome do projeto] |
| **Stack Principal** | [Python/JavaScript/TypeScript/etc.] |
| ## Framework | [Flask/React/Vue.js/etc.] |
| **Responsável Técnico** | [Nome do tech lead] |
| **Data de Criação** | [DD/MM/AAAA] |
| **Última Revisão** | [DD/MM/AAAA] |

---

## 📝 Convenções Gerais

### Princípios Fundamentais

#### Clean Code Principles

- **Legibilidade**: Código deve ser fácil de ler e entender
- **Simplicidade**: Prefira soluções simples e diretas
- **Consistência**: Mantenha padrões consistentes em todo o projeto
- **Responsabilidade Única**: Cada função/classe deve ter uma responsabilidade
- **DRY (Don't Repeat Yourself)**: Evite duplicação de código

#### Nomenclatura Universal

#### Variáveis e Funções

```python
# ✅ Bom - descritivo e claro
user_email = "user@example.com"
calculate_total_price(items)

# ❌ Ruim - abreviado e confuso
usr_eml = "user@example.com"
calc_tot(items)
```

## Constantes

```python
# ✅ Bom - maiúsculas com underscore
MAX_RETRY_ATTEMPTS = 3
DATABASE_CONNECTION_TIMEOUT = 30

# ❌ Ruim - minúsculas ou camelCase
max_retry_attempts = 3
databaseConnectionTimeout = 30
```

## Classes

```python
# ✅ Bom - PascalCase descritivo
class UserAuthenticationService:
    pass

class PaymentProcessor:
    pass

# ❌ Ruim - snake_case ou abreviado
class user_auth_service:
    pass

class PayProc:
    pass
```

## Estrutura de Arquivos

### Organização por Funcionalidade

```
src/
├── components/          # Componentes reutilizáveis
│   ├── common/         # Componentes comuns
│   ├── forms/          # Componentes de formulário
│   └── layout/         # Componentes de layout
├── services/           # Lógica de negócio
│   ├── api/           # Serviços de API
│   ├── auth/          # Serviços de autenticação
│   └── utils/         # Utilitários
├── models/            # Modelos de dados
├── views/             # Views/Páginas
├── assets/            # Recursos estáticos
│   ├── images/
│   ├── styles/
│   └── fonts/
└── tests/             # Testes
    ├── unit/
    ├── integration/
    └── e2e/
```

---

## 🐍 Python

### Convenções de Nomenclatura

#### PEP 8 Compliance

```python
# Variáveis e funções - snake_case
user_name = "João Silva"
total_amount = 150.75

def calculate_discount(price, discount_rate):
    """Calcula desconto aplicado ao preço."""
    return price * (1 - discount_rate)

# Classes - PascalCase
class UserService:
    """Serviço para gerenciamento de usuários."""

    def __init__(self, database_connection):
        self.db = database_connection

    def create_user(self, user_data):
        """Cria novo usuário no sistema."""
        pass

# Constantes - UPPER_SNAKE_CASE
MAX_LOGIN_ATTEMPTS = 3
DEFAULT_TIMEOUT = 30
API_BASE_URL = "https://api.example.com"

# Métodos privados - prefixo com underscore
class PaymentService:
    def process_payment(self, amount):
        """Processa pagamento público."""
        return self._validate_amount(amount)

    def _validate_amount(self, amount):
        """Valida valor do pagamento (método privado)."""
        return amount > 0
```

## Docstrings (Google Style)

### Funções

```python
def calculate_tax(amount: float, tax_rate: float, region: str = "BR") -> float:
    """Calcula imposto sobre valor base.

    Args:
        amount (float): Valor base para cálculo.
        tax_rate (float): Taxa de imposto (0.0 a 1.0).
        region (str, optional): Região para cálculo. Defaults to "BR".

    Returns:
        float: Valor do imposto calculado.

    Raises:
        ValueError: Se amount for negativo ou tax_rate inválido.

    Example:
        >>> calculate_tax(100.0, 0.15)
        15.0

        >>> calculate_tax(100.0, 0.20, "US")
        20.0
    """
    if amount < 0:
        raise ValueError("Amount cannot be negative")

    if not 0 <= tax_rate <= 1:
        raise ValueError("Tax rate must be between 0 and 1")

    return amount * tax_rate
```

#### Classes

```python
class DatabaseConnection:
    """Gerencia conexões com banco de dados.

    Esta classe fornece uma interface para conectar e executar
    operações no banco de dados com pool de conexões.

    Attributes:
        host (str): Endereço do servidor de banco.
        port (int): Porta de conexão.
        database (str): Nome do banco de dados.
        pool_size (int): Tamanho do pool de conexões.

    Example:
        >>> db = DatabaseConnection("localhost", 5432, "mydb")
        >>> db.connect()
        >>> result = db.execute("SELECT * FROM users")
        >>> db.close()
    """

    def __init__(self, host: str, port: int, database: str, pool_size: int = 10):
        """Inicializa conexão com banco de dados.

        Args:
            host (str): Endereço do servidor.
            port (int): Porta de conexão.
            database (str): Nome do banco.
            pool_size (int, optional): Tamanho do pool. Defaults to 10.
        """
        self.host = host
        self.port = port
        self.database = database
        self.pool_size = pool_size
        self._connection = None
```

### Type Hints

#### Tipos Básicos

```python
from typing import List, Dict, Optional, Union, Tuple, Callable

# Tipos básicos
name: str = "João"
age: int = 30
height: float = 1.75
is_active: bool = True

# Coleções
users: List[str] = ["João", "Maria", "Pedro"]
user_data: Dict[str, Union[str, int]] = {
    "name": "João",
    "age": 30
}

# Opcional
email: Optional[str] = None

# Tuplas
coordinates: Tuple[float, float] = (10.5, 20.3)

# Funções
def process_data(
    data: List[Dict[str, Any]],
    callback: Callable[[Dict[str, Any]], bool]
) -> List[Dict[str, Any]]:
    """Processa lista de dados com callback."""
    return [item for item in data if callback(item)]
```

## Classes e Protocolos

```python
from typing import Protocol, TypeVar, Generic
from abc import ABC, abstractmethod

# Protocol para duck typing
class Drawable(Protocol):
    def draw(self) -> None:
        ...

# Generic types
T = TypeVar('T')

class Repository(Generic[T]):
    """Repositório genérico para qualquer tipo."""

    def save(self, entity: T) -> T:
        """Salva entidade no repositório."""
        pass

    def find_by_id(self, id: int) -> Optional[T]:
        """Busca entidade por ID."""
        pass

# Abstract base class
class BaseService(ABC):
    """Classe base para serviços."""

    @abstractmethod
    def process(self, data: Any) -> Any:
        """Processa dados (deve ser implementado)."""
        pass
```

## Error Handling

### Exceções Customizadas

```python
class ValidationError(Exception):
    """Erro de validação de dados."""

    def __init__(self, message: str, field: str = None):
        self.message = message
        self.field = field
        super().__init__(self.message)

class BusinessLogicError(Exception):
    """Erro de regra de negócio."""
    pass

# Uso das exceções
def validate_email(email: str) -> None:
    """Valida formato de email."""
    if "@" not in email:
        raise ValidationError("Invalid email format", field="email")

def create_user(user_data: Dict[str, Any]) -> User:
    """Cria usuário com validações."""
    try:
        validate_email(user_data["email"])

        if User.exists(user_data["email"]):
            raise BusinessLogicError("User already exists")

        return User.create(user_data)

    except ValidationError as e:
        logger.error(f"Validation error: {e.message}")
        raise
    except BusinessLogicError as e:
        logger.error(f"Business logic error: {e}")
        raise
    except Exception as e:
        logger.error(f"Unexpected error: {e}")
        raise
```

## Code Organization

### Imports

```python
# Ordem dos imports:
# 1. Standard library
import os
import sys
from datetime import datetime
from typing import List, Dict, Optional

# 2. Third-party packages
import requests
from flask import Flask, request, jsonify
from sqlalchemy import create_engine

# 3. Local imports
from .models import User, Product
from .services import UserService
from .utils import validate_email
```

---

## 🌐 JavaScript/TypeScript

### Convenções de Nomenclatura

#### Variáveis e Funções - camelCase

```javascript
// ✅ Bom
const userName = 'João Silva';
const totalAmount = 150.75;

function calculateDiscount(price, discountRate) {
  return price * (1 - discountRate);
}

// Classes - PascalCase
class UserService {
  constructor(databaseConnection) {
    this.db = databaseConnection;
  }

  createUser(userData) {
    // Cria novo usuário
  }
}

// Constantes - UPPER_SNAKE_CASE
const MAX_LOGIN_ATTEMPTS = 3;
const DEFAULT_TIMEOUT = 30;
const API_BASE_URL = 'https://api.example.com';

// ❌ Ruim
const user_name = 'João'; // snake_case
const UserName = 'João';  // PascalCase
function Calculate_Discount() {} // mixed case
```

### TypeScript Types

```typescript
// Interfaces - PascalCase
interface User {
  id: number;
  name: string;
  email: string;
  isActive: boolean;
  createdAt: Date;
}

// Types - PascalCase
type UserRole = 'admin' | 'user' | 'moderator';
type ApiResponse<T> = {
  data: T;
  status: number;
  message: string;
};

// Enums - PascalCase
enum OrderStatus {
  PENDING = 'pending',
  PROCESSING = 'processing',
  COMPLETED = 'completed',
  CANCELLED = 'cancelled'
}

// Generic functions
function processApiResponse<T>(
  response: ApiResponse<T>
): T | null {
  if (response.status === 200) {
    return response.data;
  }
  return null;
}
```

### JSDoc Documentation

```javascript
/**
 * Calcula o preço total com desconto e impostos
 * @param {number} basePrice - Preço base do produto
 * @param {number} discountRate - Taxa de desconto (0-1)
 * @param {number} taxRate - Taxa de imposto (0-1)
 * @param {string} [region='BR'] - Região para cálculo
 * @returns {number} Preço final calculado
 * @throws {Error} Se os parâmetros forem inválidos
 * @example
 * // Calcula preço com 10% desconto e 15% imposto
 * const finalPrice = calculateFinalPrice(100, 0.1, 0.15);
 * console.log(finalPrice); // 103.5
 */
function calculateFinalPrice(basePrice, discountRate, taxRate, region = 'BR') {
  if (basePrice < 0) {
    throw new Error('Base price cannot be negative');
  }

  if (discountRate < 0 || discountRate > 1) {
    throw new Error('Discount rate must be between 0 and 1');
  }

  const discountedPrice = basePrice * (1 - discountRate);
  const finalPrice = discountedPrice * (1 + taxRate);

  return Math.round(finalPrice * 100) / 100; // Round to 2 decimal places
}
```

### Modern JavaScript/ES6+

```javascript
// Arrow functions
const users = [
  { name: 'João', age: 30 },
  { name: 'Maria', age: 25 },
  { name: 'Pedro', age: 35 }
];

// ✅ Bom - arrow function concisa
const adultUsers = users.filter(user => user.age >= 18);
const userNames = users.map(user => user.name);

// Destructuring
const { name, age } = users[0];
const [firstUser, secondUser] = users;

// Template literals
const greeting = `Olá, ${name}! Você tem ${age} anos.`;

// Async/await
async function fetchUserData(userId) {
  try {
    const response = await fetch(`/api/users/${userId}`);

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const userData = await response.json();
    return userData;
  } catch (error) {
    console.error('Error fetching user data:', error);
    throw error;
  }
}

// Promises
function createUser(userData) {
  return fetch('/api/users', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(userData)
  })
  .then(response => {
    if (!response.ok) {
      throw new Error('Failed to create user');
    }
    return response.json();
  })
  .catch(error => {
    console.error('Error creating user:', error);
    throw error;
  });
}
```

---

## 🎨 CSS

### Metodologia BEM

```css
/* Block - Componente principal */
.user-card {
  display: flex;
  padding: 1rem;
  border: 1px solid #ddd;
  border-radius: 8px;
}

/* Element - Parte do componente */
.user-card__avatar {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  margin-right: 1rem;
}

.user-card__content {
  flex: 1;
}

.user-card__name {
  font-size: 1.2rem;
  font-weight: bold;
  margin-bottom: 0.5rem;
}

.user-card__email {
  color: #666;
  font-size: 0.9rem;
}

/* Modifier - Variação do componente */
.user-card--premium {
  border-color: #gold;
  background-color: #fffbf0;
}

.user-card--disabled {
  opacity: 0.5;
  pointer-events: none;
}

.user-card__name--highlighted {
  color: #007bff;
}
```

### Organização SCSS

```scss
// _variables.scss
$primary-color: #007bff;
$secondary-color: #6c757d;
$success-color: #28a745;
$danger-color: #dc3545;

$font-family-base: 'Roboto', sans-serif;
$font-size-base: 1rem;
$line-height-base: 1.5;

$border-radius: 4px;
$box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);

// _mixins.scss
@mixin button-style($bg-color, $text-color: white) {
  background-color: $bg-color;
  color: $text-color;
  border: none;
  padding: 0.75rem 1.5rem;
  border-radius: $border-radius;
  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background-color: darken($bg-color, 10%);
  }

  &:disabled {
    opacity: 0.6;
    cursor: not-allowed;
  }
}

@mixin responsive($breakpoint) {
  @if $breakpoint == mobile {
    @media (max-width: 767px) { @content; }
  }
  @if $breakpoint == tablet {
    @media (min-width: 768px) and (max-width: 1023px) { @content; }
  }
  @if $breakpoint == desktop {
    @media (min-width: 1024px) { @content; }
  }
}

// _components.scss
.btn {
  @include button-style($primary-color);

  &--secondary {
    @include button-style($secondary-color);
  }

  &--success {
    @include button-style($success-color);
  }

  &--danger {
    @include button-style($danger-color);
  }
}

.card {
  background: white;
  border-radius: $border-radius;
  box-shadow: $box-shadow;
  padding: 1.5rem;

  @include responsive(mobile) {
    padding: 1rem;
  }

  &__header {
    border-bottom: 1px solid #eee;
    padding-bottom: 1rem;
    margin-bottom: 1rem;
  }

  &__title {
    font-size: 1.25rem;
    font-weight: 600;
    margin: 0;
  }
}
```

### CSS Custom Properties

```css
:root {
  /* Colors */
  --color-primary: #007bff;
  --color-secondary: #6c757d;
  --color-success: #28a745;
  --color-danger: #dc3545;
  --color-warning: #ffc107;
  --color-info: #17a2b8;

  /* Typography */
  --font-family-base: 'Roboto', -apple-system, BlinkMacSystemFont, sans-serif;
  --font-size-base: 1rem;
  --font-size-lg: 1.25rem;
  --font-size-sm: 0.875rem;
  --line-height-base: 1.5;

  /* Spacing */
  --spacing-xs: 0.25rem;
  --spacing-sm: 0.5rem;
  --spacing-md: 1rem;
  --spacing-lg: 1.5rem;
  --spacing-xl: 3rem;

  /* Layout */
  --border-radius: 0.375rem;
  --border-width: 1px;
  --box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
  --transition: all 0.15s ease-in-out;
}

/* Dark theme */
[data-theme="dark"] {
  --color-primary: #0d6efd;
  --color-bg: #212529;
  --color-text: #ffffff;
  --color-border: #495057;
}

/* Usage */
.button {
  background-color: var(--color-primary);
  color: white;
  border: var(--border-width) solid transparent;
  border-radius: var(--border-radius);
  padding: var(--spacing-sm) var(--spacing-md);
  font-family: var(--font-family-base);
  font-size: var(--font-size-base);
  transition: var(--transition);
}
```

---

## 🔧 Ferramentas de Qualidade

### Linting e Formatting

#### Python

```json
// .flake8
[flake8]
max-line-length = 88
ignore = E203, E266, E501, W503
max-complexity = 10
select = B,C,E,F,W,T4,B9

// pyproject.toml
[tool.black]
line-length = 88
target-version = ['py38']
include = '\.pyi?$'

[tool.isort]
profile = "black"
line_length = 88
multi_line_output = 3

[tool.mypy]
python_version = "3.8"
warn_return_any = true
warn_unused_configs = true
disallow_untyped_defs = true
```

#### JavaScript/TypeScript

```json
// .eslintrc.json
{
  "extends": [
    "eslint:recommended",
    "@typescript-eslint/recommended",
    "prettier"
  ],
  "parser": "@typescript-eslint/parser",
  "plugins": ["@typescript-eslint"],
  "rules": {
    "no-console": "warn",
    "no-unused-vars": "error",
    "prefer-const": "error",
    "no-var": "error",
    "@typescript-eslint/no-explicit-any": "warn",
    "@typescript-eslint/explicit-function-return-type": "warn"
  }
}

// .prettierrc
{
  "semi": true,
  "trailingComma": "es5",
  "singleQuote": true,
  "printWidth": 80,
  "tabWidth": 2,
  "useTabs": false
}
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
      - id: check-added-large-files

  - repo: https://github.com/psf/black
    rev: 22.10.0
    hooks:
      - id: black
        language_version: python3

  - repo: https://github.com/pycqa/isort
    rev: 5.10.1
    hooks:
      - id: isort
        args: ["--profile", "black"]

  - repo: https://github.com/pycqa/flake8
    rev: 5.0.4
    hooks:
      - id: flake8

  - repo: https://github.com/pre-commit/mirrors-eslint
    rev: v8.28.0
    hooks:
      - id: eslint
        files: \.[jt]sx?$
        types: [file]
```

### CI/CD Integration

```yaml
# .github/workflows/quality.yml
name: Code Quality

on: [push, pull_request]

jobs:
  quality:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Set up Python
        uses: actions/setup-python@v4
        with:
          python-version: '3.8'

      - name: Install dependencies
        run: |
          pip install black isort flake8 mypy pytest
          pip install -r requirements.txt

      - name: Run Black
        run: black --check .

      - name: Run isort
        run: isort --check-only .

      - name: Run Flake8
        run: flake8 .

      - name: Run MyPy
        run: mypy .

      - name: Run Tests
        run: pytest --cov=. --cov-report=xml

      - name: Upload coverage
        uses: codecov/codecov-action@v3
```

---

## 📋 Checklist de Qualidade

### Código

- [ ] **Nomenclatura**: Nomes descritivos e consistentes
- [ ] **Estrutura**: Código bem organizado e modular
- [ ] **Documentação**: Funções e classes documentadas
- [ ] **Type Hints**: Tipos especificados (Python/TypeScript)
- [ ] **Error Handling**: Tratamento adequado de erros
- [ ] **Performance**: Código otimizado e eficiente

### Testes

- [ ] **Unit Tests**: Cobertura ≥ 85%
- [ ] **Integration Tests**: Fluxos principais testados
- [ ] **E2E Tests**: Cenários críticos cobertos
- [ ] **Test Names**: Nomes descritivos e claros

### Ferramentas

- [ ] **Linting**: ESLint/Flake8 configurado
- [ ] **Formatting**: Prettier/Black configurado
- [ ] **Type Checking**: MyPy/TypeScript configurado
- [ ] **Pre-commit**: Hooks configurados
- [ ] **CI/CD**: Pipeline de qualidade ativo

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA Standards
**Última Atualização**: 15/09/2025
**Versão**: 1.0.0

---

## Padrões de código definidos! Qualidade garantida! ✅

</div>e * (1 - discountRate);
}

// Arrow functions
const processPayment = (amount) => {
  return validateAmount(amount);
};

// ❌ Ruim
const user_name = 'João Silva';
const UserName = 'João Silva';

```
#### Classes - PascalCase

```javascript
// ✅ Bom
class UserService {
  constructor(databaseConnection) {
    this.db = databaseConnection;
  }

  async createUser(userData) {
    return await this.db.users.create(userData);
  }
}

class PaymentProcessor {
  // Métodos privados com #
  #validateAmount(amount) {
    return amount > 0;
  }

  processPayment(amount) {
    if (!this.#validateAmount(amount)) {
      throw new Error('Invalid amount');
    }
    // Process payment
  }
}
```

#### Constantes - UPPER_SNAKE_CASE

```javascript
const MAX_RETRY_ATTEMPTS = 3;
const API_BASE_URL = 'https://api.example.com';
const DEFAULT_TIMEOUT = 30000;

// Enums (TypeScript)
enum UserRole {
  ADMIN = 'admin',
  USER = 'user',
  MODERATOR = 'moderator'
}
```

### TypeScript Types

#### Interfaces

```typescript
// Interface para dados
interface User {
  id: number;
  name: string;
  email: string;
  isActive: boolean;
  createdAt: Date;
  profile?: UserProfile; // Opcional
}

interface UserProfile {
  avatar: string;
  bio: string;
  preferences: UserPreferences;
}

// Interface para funções
interface ApiService {
  get<T>(url: string): Promise<T>;
  post<T>(url: string, data: any): Promise<T>;
  put<T>(url: string, data: any): Promise<T>;
  delete(url: string): Promise<void>;
}
```

#### Types e Unions

```typescript
// Union types
type Status = 'pending' | 'approved' | 'rejected';
type Theme = 'light' | 'dark' | 'auto';

// Generic types
type ApiResponse<T> = {
  data: T;
  status: number;
  message: string;
};

// Utility types
type CreateUserRequest = Omit<User, 'id' | 'createdAt'>;
type UpdateUserRequest = Partial<Pick<User, 'name' | 'email'>>;

// Function types
type EventHandler<T> = (event: T) => void;
type AsyncValidator<T> = (value: T) => Promise<boolean>;
```

### JSDoc Comments

#### Funções

```javascript
/**
 * Calcula o desconto aplicado ao preço.
 *
 * @param {number} price - Preço original do produto
 * @param {number} discountRate - Taxa de desconto (0-1)
 * @param {string} [couponCode] - Código de cupom opcional
 * @returns {number} Valor do desconto calculado
 *
 * @example
 * // Calcular 15% de desconto
 * const discount = calculateDiscount(100, 0.15);
 * console.log(discount); // 15
 *
 * @example
 * // Com cupom de desconto
 * const discount = calculateDiscount(100, 0.15, 'SAVE20');
 * console.log(discount); // 20
 */
function calculateDiscount(price, discountRate, couponCode) {
  let discount = price * discountRate;

  if (couponCode === 'SAVE20') {
    discount = Math.max(discount, price * 0.20);
  }

  return discount;
}
```

### Modern JavaScript/TypeScript

#### Async/Await

```typescript
// ✅ Bom - async/await
async function fetchUserData(userId: number): Promise<User> {
  try {
    const response = await fetch(`/api/users/${userId}`);

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const userData = await response.json();
    return userData;

  } catch (error) {
    console.error('Failed to fetch user data:', error);
    throw error;
  }
}

// ❌ Ruim - callback hell
function fetchUserDataOld(userId, callback) {
  fetch(`/api/users/${userId}`)
    .then(response => {
      if (!response.ok) {
        callback(new Error('HTTP error'));
        return;
      }
      response.json().then(data => {
        callback(null, data);
      });
    })
    .catch(error => {
      callback(error);
    });
}
```

#### Destructuring e Spread

```javascript
// ✅ Bom - destructuring
const { name, email, profile: { avatar } } = user;

// ✅ Bom - spread operator
const updatedUser = {
  ...user,
  lastLogin: new Date(),
  isActive: true
};

// ✅ Bom - array destructuring
const [first, second, ...rest] = items;

// ✅ Bom - function parameters
function createUser({ name, email, role = 'user' }) {
  return {
    id: generateId(),
    name,
    email,
    role,
    createdAt: new Date()
  };
}
```

---

## 🎨 CSS/SCSS

### Metodologia BEM

#### Block Element Modifier

```scss
// Block
.card {
  padding: 1rem;
  border-radius: 8px;
  background: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

// Element
.card__header {
  margin-bottom: 1rem;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid #eee;
}

.card__title {
  font-size: 1.25rem;
  font-weight: 600;
  color: #333;
}

.card__content {
  line-height: 1.6;
  color: #666;
}

// Modifier
.card--featured {
  border: 2px solid #007bff;
  background: #f8f9ff;
}

.card--large {
  padding: 2rem;
}

.card__title--highlighted {
  color: #007bff;
}
```

### Variáveis CSS/SCSS

#### Design System

```scss
// Colors
$primary-color: #007bff;
$secondary-color: #6c757d;
$success-color: #28a745;
$warning-color: #ffc107;
$error-color: #dc3545;

$text-primary: #212529;
$text-secondary: #6c757d;
$text-muted: #868e96;

// Typography
$font-family-base: 'Inter', -apple-system, BlinkMacSystemFont, sans-serif;
$font-family-mono: 'Fira Code', 'Monaco', 'Consolas', monospace;

$font-size-xs: 0.75rem;
$font-size-sm: 0.875rem;
$font-size-base: 1rem;
$font-size-lg: 1.125rem;
$font-size-xl: 1.25rem;

// Spacing
$spacing-xs: 0.25rem;
$spacing-sm: 0.5rem;
$spacing-md: 1rem;
$spacing-lg: 1.5rem;
$spacing-xl: 2rem;

// Breakpoints
$breakpoint-sm: 576px;
$breakpoint-md: 768px;
$breakpoint-lg: 992px;
$breakpoint-xl: 1200px;
```

#### CSS Custom Properties

```css
:root {
  /* Colors */
  --color-primary: #007bff;
  --color-secondary: #6c757d;
  --color-success: #28a745;
  --color-warning: #ffc107;
  --color-error: #dc3545;

  /* Typography */
  --font-family-base: 'Inter', sans-serif;
  --font-size-base: 1rem;
  --line-height-base: 1.6;

  /* Spacing */
  --spacing-xs: 0.25rem;
  --spacing-sm: 0.5rem;
  --spacing-md: 1rem;
  --spacing-lg: 1.5rem;
  --spacing-xl: 2rem;

  /* Shadows */
  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.05);
  --shadow-md: 0 4px 6px rgba(0, 0, 0, 0.1);
  --shadow-lg: 0 10px 15px rgba(0, 0, 0, 0.1);
}

/* Dark theme */
[data-theme="dark"] {
  --color-primary: #4dabf7;
  --color-background: #1a1a1a;
  --color-text: #ffffff;
}
```

### Responsive Design

#### Mobile First

```scss
// ✅ Bom - Mobile first
.component {
  // Mobile styles (default)
  padding: 1rem;
  font-size: 1rem;

  // Tablet and up
  @media (min-width: $breakpoint-md) {
    padding: 1.5rem;
    font-size: 1.125rem;
  }

  // Desktop and up
  @media (min-width: $breakpoint-lg) {
    padding: 2rem;
    font-size: 1.25rem;
  }
}

// ❌ Ruim - Desktop first
.component {
  padding: 2rem;
  font-size: 1.25rem;

  @media (max-width: $breakpoint-lg) {
    padding: 1.5rem;
    font-size: 1.125rem;
  }

  @media (max-width: $breakpoint-md) {
    padding: 1rem;
    font-size: 1rem;
  }
}
```

---

## 🔧 Ferramentas de Qualidade

### Linting Configuration

#### ESLint (.eslintrc.json)

```json
{
  "extends": [
    "eslint:recommended",
    "@typescript-eslint/recommended",
    "prettier"
  ],
  "parser": "@typescript-eslint/parser",
  "plugins": ["@typescript-eslint"],
  "rules": {
    "no-console": "warn",
    "no-unused-vars": "error",
    "prefer-const": "error",
    "no-var": "error",
    "object-shorthand": "error",
    "prefer-arrow-callback": "error",
    "@typescript-eslint/no-explicit-any": "warn",
    "@typescript-eslint/explicit-function-return-type": "warn"
  },
  "env": {
    "browser": true,
    "node": true,
    "es2022": true
  }
}
```

#### Flake8 (setup.cfg)

```ini
[flake8]
max-line-length = 88
extend-ignore = E203, W503, E501
exclude =
    .git,
    __pycache__,
    .venv,
    venv,
    migrations,
    node_modules
per-file-ignores =
    __init__.py:F401
    tests/*:S101
```

### Formatação

#### Prettier (.prettierrc)

```json
{
  "semi": true,
  "trailingComma": "es5",
  "singleQuote": true,
  "printWidth": 80,
  "tabWidth": 2,
  "useTabs": false,
  "bracketSpacing": true,
  "arrowParens": "avoid",
  "endOfLine": "lf"
}
```

#### Black (pyproject.toml)

```toml
[tool.black]
line-length = 88
target-version = ['py39']
include = '\.pyi?$'
extend-exclude = '''
/(
  # directories
  \.eggs
  | \.git
  | \.hg
  | \.mypy_cache
  | \.tox
  | \.venv
  | venv
  | _build
  | buck-out
  | build
  | dist
)/
'''
```

### Pre-commit Hooks

#### .pre-commit-config.yaml

```yaml
repos:
  - repo: https://github.com/pre-commit/pre-commit-hooks
    rev: v4.4.0
    hooks:
      - id: trailing-whitespace
      - id: end-of-file-fixer
      - id: check-yaml
      - id: check-json
      - id: check-merge-conflict
      - id: check-added-large-files

  - repo: https://github.com/psf/black
    rev: 22.10.0
    hooks:
      - id: black

  - repo: https://github.com/pycqa/flake8
    rev: 5.0.4
    hooks:
      - id: flake8

  - repo: https://github.com/pre-commit/mirrors-eslint
    rev: v8.28.0
    hooks:
      - id: eslint
        files: \.(js|jsx|ts|tsx)$
        additional_dependencies:
          - eslint@8.28.0
          - '@typescript-eslint/eslint-plugin@5.44.0'
          - '@typescript-eslint/parser@5.44.0'

  - repo: https://github.com/pre-commit/mirrors-prettier
    rev: v3.0.0-alpha.4
    hooks:
      - id: prettier
        files: \.(js|jsx|ts|tsx|json|css|scss|md)$
```

---

## 📊 Métricas de Qualidade

### Code Coverage

#### Jest (JavaScript/TypeScript)

```json
{
  "jest": {
    "collectCoverage": true,
    "coverageDirectory": "coverage",
    "coverageReporters": ["text", "lcov", "html"],
    "collectCoverageFrom": [
      "src/**/*.{js,jsx,ts,tsx}",
      "!src/**/*.d.ts",
      "!src/index.tsx"
    ],
    "coverageThreshold": {
      "global": {
        "branches": 80,
        "functions": 80,
        "lines": 80,
        "statements": 80
      }
    }
  }
}
```

#### pytest (Python)

```ini
[tool:pytest]
testpaths = tests
python_files = test_*.py
python_classes = Test*
python_functions = test_*
addopts =
    --cov=src
    --cov-report=html
    --cov-report=term-missing
    --cov-fail-under=80
```

### SonarQube Configuration

#### sonar-project.properties

```properties
sonar.projectKey=datametria-project
sonar.projectName=DATAMETRIA Project
sonar.projectVersion=1.0

sonar.sources=src
sonar.tests=tests
sonar.exclusions=**/node_modules/**,**/coverage/**,**/*.spec.ts

sonar.javascript.lcov.reportPaths=coverage/lcov.info
sonar.python.coverage.reportPaths=coverage.xml

sonar.qualitygate.wait=true
```

---

## ✅ Checklist de Qualidade

### Código Python

- [ ] **PEP 8** compliance verificado
- [ ] **Type hints** adicionados
- [ ] **Docstrings** no formato Google
- [ ] **Imports** organizados corretamente
- [ ] **Exceções** tratadas adequadamente
- [ ] **Testes** com cobertura > 80%

### Código JavaScript/TypeScript

- [ ] **ESLint** sem erros
- [ ] **Prettier** formatação aplicada
- [ ] **TypeScript** tipos definidos
- [ ] **JSDoc** comentários adicionados
- [ ] **Modern syntax** utilizada (ES6+)
- [ ] **Testes** com cobertura > 80%

### CSS/SCSS

- [ ] **BEM** metodologia seguida
- [ ] **Variáveis** utilizadas para valores repetidos
- [ ] **Mobile first** abordagem
- [ ] **Acessibilidade** considerada
- [ ] **Performance** otimizada

### Geral

- [ ] **Nomenclatura** consistente
- [ ] **Estrutura** de arquivos organizada
- [ ] ## Documentação atualizada
- [ ] **Git hooks** configurados
- [ ] **CI/CD** pipeline passando

---

<div align="center">

**Desenvolvido por**: Equipe DATAMETRIA
**Última Atualização**: [DD/MM/AAAA]
**Versão**: 1.0.0

---

## Padrões de código definidos! Qualidade garantida! 🚀

</div>
