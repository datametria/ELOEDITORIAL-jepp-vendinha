# Architecture: Padrões DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 2.1: Clean Architecture em Camadas

### Contexto

Código sem separação de camadas causa:

- Lógica de negócio misturada com infraestrutura
- Impossível testar sem banco de dados
- Mudança de framework requer reescrita completa

### Regra

Projetos DEVEM seguir Clean Architecture com camadas:

1. **Domain**: Entidades, regras de negócio
2. **Application**: Casos de uso, orquestração
3. **Infrastructure**: BD, APIs externas, frameworks
4. **Presentation**: Controllers, views, DTOs

Dependências: Presentation → Application → Domain ← Infrastructure

### Justificativa

- Lógica de negócio independente de frameworks
- Testes unitários sem mocks complexos
- Mudança de tecnologia sem reescrita

### Exemplos

#### ✅ Correto

```
src/
├── domain/
│   ├── entities/
│   │   └── user.py          # Entidade pura
│   ├── repositories/
│   │   └── user_repository.py  # Interface (ABC)
│   └── services/
│       └── user_service.py  # Regras de negócio
├── application/
│   └── use_cases/
│       └── create_user.py   # Caso de uso
├── infrastructure/
│   ├── database/
│   │   └── user_repository_impl.py  # Implementação
│   └── external/
│       └── email_service.py
└── presentation/
    └── api/
        └── user_controller.py
```

```python
# domain/entities/user.py
from dataclasses import dataclass

@dataclass
class User:
    """Entidade pura - sem dependências."""
    id: str
    email: str
    nome: str

    def is_valid(self) -> bool:
        return "@" in self.email and len(self.nome) >= 3

# domain/repositories/user_repository.py
from abc import ABC, abstractmethod

class UserRepository(ABC):
    """Interface - sem implementação."""
    @abstractmethod
    def save(self, user: User) -> User:
        pass

# application/use_cases/create_user.py
class CreateUserUseCase:
    def __init__(self, repository: UserRepository):
        self._repository = repository

    def execute(self, email: str, nome: str) -> User:
        user = User(id=generate_id(), email=email, nome=nome)
        if not user.is_valid():
            raise InvalidUserError()
        return self._repository.save(user)
```

#### ❌ Incorreto

```python
# Tudo misturado
class UserController:
    def create_user(self, request):
        # Validação + lógica + BD tudo junto
        email = request.json['email']
        if '@' not in email:
            return {"error": "Invalid"}, 400

        conn = psycopg2.connect("postgresql://...")
        cursor = conn.cursor()
        cursor.execute("INSERT INTO users...")
        conn.commit()
```

### Ferramentas

- **Import Linter**: Valida dependências entre camadas
- **Architecture Tests**: Testes automatizados de arquitetura

### Checklist

- [ ] Camadas separadas em diretórios?
- [ ] Domain sem dependências externas?
- [ ] Interfaces para infraestrutura?
- [ ] Dependency Injection configurada?

---

## Rule 2.2: Dependency Injection Obrigatória

### Contexto

Dependências hardcoded causam:

- Impossível testar com mocks
- Acoplamento forte
- Configuração inflexível

### Regra

Classes DEVEM receber dependências via construtor (DI).
Usar container DI em produção (dependency-injector, FastAPI Depends).

### Justificativa

- Testes com mocks triviais
- Configuração flexível
- Inversão de controle

### Exemplos

#### ✅ Correto

```python
# Com DI
class UserService:
    def __init__(
        self,
        repository: UserRepository,
        email_service: EmailService,
        logger: Logger
    ):
        self._repository = repository
        self._email_service = email_service
        self._logger = logger

    def create_user(self, email: str) -> User:
        user = User(email=email)
        saved = self._repository.save(user)
        self._email_service.send_welcome(email)
        self._logger.info(f"User created: {email}")
        return saved

# Teste fácil
def test_create_user():
    mock_repo = Mock(spec=UserRepository)
    mock_email = Mock(spec=EmailService)
    mock_logger = Mock(spec=Logger)

    service = UserService(mock_repo, mock_email, mock_logger)
    service.create_user("test@example.com")

    mock_repo.save.assert_called_once()
```

#### ❌ Incorreto

```python
# Sem DI - dependências hardcoded
class UserService:
    def __init__(self):
        self._repository = PostgresUserRepository()  # Hardcoded!
        self._email_service = SendGridEmailService()  # Hardcoded!
        self._logger = logging.getLogger(__name__)

    # Impossível testar sem BD real e SendGrid
```

### Ferramentas

- **Python**: `dependency-injector`, `injector`
- **FastAPI**: `Depends()`
- **TypeScript**: `tsyringe`, `inversify`

### Checklist

- [ ] Dependências via construtor?
- [ ] Container DI configurado?
- [ ] Testes usam mocks facilmente?

---

## Rule 2.3: Repository Pattern para Persistência

### Contexto

Acesso direto ao BD causa:

- SQL espalhado pelo código
- Impossível trocar BD
- Testes requerem BD real

### Regra

TODO acesso a dados DEVE usar Repository Pattern:

- Interface no domain
- Implementação na infrastructure
- Métodos de negócio (find_by_email, não query genérico)

### Justificativa

- Abstração de persistência
- Testes com repositórios in-memory
- Mudança de BD sem impacto

### Exemplos

#### ✅ Correto

```python
# domain/repositories/user_repository.py
class UserRepository(ABC):
    @abstractmethod
    def find_by_email(self, email: str) -> Optional[User]:
        pass

    @abstractmethod
    def save(self, user: User) -> User:
        pass

# infrastructure/database/sqlalchemy_user_repository.py
class SQLAlchemyUserRepository(UserRepository):
    def __init__(self, session: Session):
        self._session = session

    def find_by_email(self, email: str) -> Optional[User]:
        model = self._session.query(UserModel).filter_by(email=email).first()
        return self._to_entity(model) if model else None

    def save(self, user: User) -> User:
        model = self._to_model(user)
        self._session.add(model)
        self._session.commit()
        return user

# Teste com in-memory
class InMemoryUserRepository(UserRepository):
    def __init__(self):
        self._users: Dict[str, User] = {}

    def find_by_email(self, email: str) -> Optional[User]:
        return next((u for u in self._users.values() if u.email == email), None)
```

#### ❌ Incorreto

```python
# SQL direto no service
class UserService:
    def get_user(self, email: str):
        conn = psycopg2.connect("...")
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM users WHERE email = %s", (email,))
        return cursor.fetchone()
```

### Checklist

- [ ] Repository interface no domain?
- [ ] Implementação na infrastructure?
- [ ] Métodos de negócio (não genéricos)?
- [ ] Repositório in-memory para testes?

---

## Rule 2.4: Feature Folders (Vertical Slicing)

### Contexto

Organização por tipo técnico causa:

- Mudança em feature toca 5+ diretórios
- Difícil entender escopo de feature
- Merge conflicts frequentes

### Regra

Organizar por feature (vertical slicing) quando projeto > 10 features:

```
src/
├── features/
│   ├── authentication/
│   │   ├── domain/
│   │   ├── application/
│   │   ├── infrastructure/
│   │   └── presentation/
│   ├── payments/
│   └── notifications/
```

### Justificativa

- Mudanças localizadas em 1 diretório
- Escopo claro de feature
- Menos conflitos de merge

### Exemplos

#### ✅ Correto (Feature Folders)

```
src/
├── features/
│   ├── authentication/
│   │   ├── domain/
│   │   │   ├── user.py
│   │   │   └── user_repository.py
│   │   ├── application/
│   │   │   ├── login_use_case.py
│   │   │   └── register_use_case.py
│   │   ├── infrastructure/
│   │   │   └── jwt_service.py
│   │   └── presentation/
│   │       └── auth_controller.py
│   └── payments/
│       ├── domain/
│       ├── application/
│       └── ...
└── shared/
    └── kernel/
```

### Checklist

- [ ] Features em diretórios separados?
- [ ] Cada feature com camadas completas?
- [ ] Shared kernel para código comum?

---

## Rule 2.5: API Versioning Obrigatório

### Contexto

APIs sem versionamento causam:

- Breaking changes quebram clientes
- Impossível evoluir API
- Suporte a múltiplas versões caótico

### Regra

APIs públicas DEVEM ter versionamento:

- URL: `/api/v1/users`, `/api/v2/users`
- Header: `Accept: application/vnd.api+json; version=1`
- Manter N-1 versões ativas (deprecation de 6 meses)

### Justificativa

- Breaking changes sem impacto
- Evolução controlada
- Clientes migram gradualmente

### Exemplos

#### ✅ Correto

```python
# FastAPI com versionamento
from fastapi import FastAPI, APIRouter

app = FastAPI()

# V1
router_v1 = APIRouter(prefix="/api/v1")

@router_v1.get("/users/{user_id}")
async def get_user_v1(user_id: str):
    return {"id": user_id, "name": "John"}

# V2 - Breaking change: estrutura diferente
router_v2 = APIRouter(prefix="/api/v2")

@router_v2.get("/users/{user_id}")
async def get_user_v2(user_id: str):
    return {
        "data": {
            "id": user_id,
            "attributes": {"name": "John"}
        }
    }

app.include_router(router_v1)
app.include_router(router_v2)
```

### Checklist

- [ ] Versionamento em URL?
- [ ] Política de deprecation definida?
- [ ] Documentação por versão?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Clean Architecture | 100% projetos | Code review |
| Dependency Injection | 95% classes | Import linter |
| Repository Pattern | 100% persistência | Architecture tests |
| Feature Folders | Projetos > 10 features | Estrutura de diretórios |
| API Versioning | 100% APIs públicas | OpenAPI spec |

---

**Próxima revisão:** 2026-01-19
