# Testing: Padrões DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 4.1: Cobertura Mínima 80%

### Contexto

Baixa cobertura causa:

- Bugs em produção (60% mais bugs com <50% coverage)
- Refatoração arriscada
- Regressões não detectadas

### Regra

Projetos DEVEM ter:

- **Cobertura total**: ≥ 80%
- **Cobertura de branches**: ≥ 75%
- **CI/CD**: Falha se cobertura cair
- **Exclusões**: Apenas migrations, **init**.py, scripts

### Justificativa

- 80% coverage reduz bugs em 40%
- Refatoração segura
- Documentação viva do comportamento

### Exemplos

#### ✅ Correto (pytest + coverage)

```python
# pyproject.toml
[tool.coverage.run]
source = ["src"]
omit = [
    "*/migrations/*",
    "*/tests/*",
    "*/__init__.py",
    "*/scripts/*"
]

[tool.coverage.report]
fail_under = 80
show_missing = true
skip_covered = false
```

```yaml
# .github/workflows/test.yml
- name: Run tests with coverage
  run: |
    pytest --cov=src --cov-report=term --cov-report=html --cov-fail-under=80

- name: Upload coverage to Codecov
  uses: codecov/codecov-action@v3
```

```python
# Teste completo
def test_user_service_create_user():
    """Testa criação de usuário com validações."""
    # Arrange
    mock_repo = Mock(spec=UserRepository)
    mock_email = Mock(spec=EmailService)
    service = UserService(mock_repo, mock_email)

    # Act
    user = service.create_user("test@example.com", "John Doe")

    # Assert
    assert user.email == "test@example.com"
    mock_repo.save.assert_called_once()
    mock_email.send_welcome.assert_called_once_with("test@example.com")

def test_user_service_create_user_invalid_email():
    """Testa validação de email inválido."""
    service = UserService(Mock(), Mock())

    with pytest.raises(InvalidEmailError):
        service.create_user("invalid-email", "John")
```

#### ❌ Incorreto

```python
# Teste superficial - não testa branches
def test_user_service():
    service = UserService(Mock(), Mock())
    user = service.create_user("test@example.com", "John")
    assert user is not None  # Teste fraco
```

### Ferramentas

- **pytest-cov**: Coverage para Python
- **Jest**: Coverage para JavaScript/TypeScript
- **Codecov**: Visualização de coverage
- **SonarQube**: Análise de qualidade

### Checklist

- [ ] Coverage ≥ 80%?
- [ ] Branch coverage ≥ 75%?
- [ ] CI/CD valida coverage?
- [ ] Exclusões justificadas?

---

## Rule 4.2: AAA Pattern (Arrange-Act-Assert)

### Contexto

Testes desorganizados causam:

- Difícil entender o que está sendo testado
- Testes frágeis
- Manutenção custosa

### Regra

Testes DEVEM seguir AAA pattern:

1. **Arrange**: Setup de dados e mocks
2. **Act**: Execução da ação testada
3. **Assert**: Verificação do resultado

Separar seções com linha em branco.

### Justificativa

- Testes legíveis
- Intenção clara
- Fácil manutenção

### Exemplos

#### ✅ Correto

```python
def test_calculate_discount_for_premium_user():
    """Premium users recebem 20% de desconto."""
    # Arrange
    user = User(id="123", tier="premium")
    product = Product(id="456", price=Decimal("100.00"))
    calculator = DiscountCalculator()

    # Act
    final_price = calculator.calculate(user, product)

    # Assert
    assert final_price == Decimal("80.00")
    assert calculator.discount_applied == Decimal("20.00")

def test_calculate_discount_for_basic_user():
    """Basic users não recebem desconto."""
    # Arrange
    user = User(id="123", tier="basic")
    product = Product(id="456", price=Decimal("100.00"))
    calculator = DiscountCalculator()

    # Act
    final_price = calculator.calculate(user, product)

    # Assert
    assert final_price == Decimal("100.00")
    assert calculator.discount_applied == Decimal("0.00")
```

#### ❌ Incorreto

```python
def test_discount():
    """Teste confuso sem estrutura."""
    user = User(id="123", tier="premium")
    calculator = DiscountCalculator()
    product = Product(id="456", price=Decimal("100.00"))
    assert calculator.calculate(user, product) == Decimal("80.00")
    # Mistura arrange, act e assert
```

### Checklist

- [ ] Seção Arrange clara?
- [ ] Seção Act isolada?
- [ ] Seção Assert específica?
- [ ] Linhas em branco separando seções?

---

## Rule 4.3: Naming Convention para Testes

### Contexto

Nomes vagos causam:

- Impossível entender o que falhou
- Testes duplicados
- Documentação pobre

### Regra

Testes DEVEM seguir naming:

- **Formato**: `test_<método>_<cenário>_<resultado_esperado>`
- **Exemplo**: `test_create_user_with_invalid_email_raises_error`
- **Docstring**: Descrição em português do comportamento

### Justificativa

- Nome descreve comportamento completo
- Falhas são autoexplicativas
- Documentação automática

### Exemplos

#### ✅ Correto

```python
def test_create_user_with_valid_data_returns_user():
    """Criação de usuário com dados válidos retorna usuário salvo."""
    pass

def test_create_user_with_duplicate_email_raises_conflict_error():
    """Criação com email duplicado lança ConflictError."""
    pass

def test_create_user_with_invalid_email_raises_validation_error():
    """Criação com email inválido lança ValidationError."""
    pass

def test_create_user_sends_welcome_email():
    """Criação de usuário envia email de boas-vindas."""
    pass

def test_login_with_correct_credentials_returns_tokens():
    """Login com credenciais corretas retorna access e refresh tokens."""
    pass

def test_login_with_incorrect_password_raises_unauthorized_error():
    """Login com senha incorreta lança UnauthorizedError."""
    pass
```

#### ❌ Incorreto

```python
def test_user():
    """Teste vago."""
    pass

def test_create():
    """Qual método? Qual cenário?"""
    pass

def test_error():
    """Qual erro? Quando?"""
    pass
```

### Checklist

- [ ] Nome segue formato `test_<método>_<cenário>_<resultado>`?
- [ ] Nome é autoexplicativo?
- [ ] Docstring descreve comportamento?

---

## Rule 4.4: Fixtures e Factories

### Contexto

Setup duplicado causa:

- Código repetitivo
- Testes frágeis
- Manutenção difícil

### Regra

Usar fixtures (pytest) ou factories (factory_boy) para:

- Dados de teste reutilizáveis
- Setup complexo
- Teardown automático

### Justificativa

- DRY em testes
- Setup consistente
- Testes mais limpos

### Exemplos

#### ✅ Correto (pytest fixtures)

```python
# conftest.py
import pytest
from factory import Factory, Faker

class UserFactory(Factory):
    class Meta:
        model = User

    id = Faker('uuid4')
    email = Faker('email')
    nome = Faker('name')
    idade = Faker('pyint', min_value=18, max_value=80)

@pytest.fixture
def user():
    """Fixture de usuário padrão."""
    return UserFactory()

@pytest.fixture
def premium_user():
    """Fixture de usuário premium."""
    return UserFactory(tier="premium")

@pytest.fixture
def mock_user_repository():
    """Mock de repositório de usuários."""
    return Mock(spec=UserRepository)

@pytest.fixture
def user_service(mock_user_repository):
    """Service com dependências mockadas."""
    return UserService(repository=mock_user_repository)

# test_user_service.py
def test_create_user(user_service, user):
    """Usa fixtures para setup limpo."""
    created = user_service.create(user.email, user.nome)
    assert created.email == user.email

def test_premium_discount(user_service, premium_user):
    """Usa fixture específica."""
    discount = user_service.calculate_discount(premium_user)
    assert discount == Decimal("20.00")
```

#### ❌ Incorreto

```python
# Setup duplicado em cada teste
def test_create_user():
    repo = Mock(spec=UserRepository)
    service = UserService(repo)
    user = User(id="123", email="test@example.com", nome="John")
    # ...

def test_update_user():
    repo = Mock(spec=UserRepository)  # Duplicado!
    service = UserService(repo)       # Duplicado!
    user = User(id="123", email="test@example.com", nome="John")  # Duplicado!
    # ...
```

### Ferramentas

- **pytest fixtures**: Setup reutilizável
- **factory_boy**: Factories para modelos
- **faker**: Dados fake realistas

### Checklist

- [ ] Fixtures para setup comum?
- [ ] Factories para modelos complexos?
- [ ] Fixtures com escopo adequado (function, module, session)?

---

## Rule 4.5: Testes de Integração Separados

### Contexto

Testes misturados causam:

- Testes lentos (unit + integration juntos)
- CI/CD demorado
- Feedback lento

### Regra

Separar testes por tipo:

- **Unit**: `tests/unit/` - Rápidos, sem I/O
- **Integration**: `tests/integration/` - BD, APIs externas
- **E2E**: `tests/e2e/` - Fluxos completos
- **CI/CD**: Unit sempre, integration em PRs, E2E em deploy

### Justificativa

- Feedback rápido (unit em 5s)
- Testes integration apenas quando necessário
- CI/CD otimizado

### Exemplos

#### ✅ Correto

```
tests/
├── unit/
│   ├── test_user_service.py      # Mocks, sem BD
│   ├── test_discount_calculator.py
│   └── test_validators.py
├── integration/
│   ├── test_user_repository.py   # BD real (testcontainers)
│   ├── test_payment_gateway.py   # API externa
│   └── test_email_service.py
└── e2e/
    ├── test_user_registration_flow.py
    └── test_checkout_flow.py
```

```python
# pytest.ini
[pytest]
markers =
    unit: Unit tests (fast, no I/O)
    integration: Integration tests (DB, external APIs)
    e2e: End-to-end tests (full flows)

# Rodar apenas unit tests
# pytest -m unit

# Rodar unit + integration
# pytest -m "unit or integration"
```

```yaml
# .github/workflows/test.yml
- name: Unit tests (sempre)
  run: pytest -m unit --cov=src --cov-fail-under=80

- name: Integration tests (apenas em PRs)
  if: github.event_name == 'pull_request'
  run: pytest -m integration

- name: E2E tests (apenas em deploy)
  if: github.ref == 'refs/heads/main'
  run: pytest -m e2e
```

#### ❌ Incorreto

```python
# Tudo misturado
tests/
├── test_user.py  # Unit + integration + e2e juntos
└── test_payment.py
```

### Ferramentas

- **pytest markers**: Categorização de testes
- **testcontainers**: BD real em containers
- **pytest-xdist**: Paralelização

### Checklist

- [ ] Testes separados por tipo?
- [ ] Markers configurados?
- [ ] CI/CD roda unit sempre?
- [ ] Integration/E2E apenas quando necessário?

---

## Métricas de Conformidade

| Métrica | Meta | Comando |
|---------|------|---------|
| Coverage total | ≥ 80% | `pytest --cov=src --cov-report=term` |
| Branch coverage | ≥ 75% | `pytest --cov-branch` |
| AAA pattern | 100% | Code review |
| Naming convention | 100% | Code review |
| Fixtures usage | ≥ 80% testes | Code review |
| Testes separados | 100% | Estrutura de diretórios |

---

**Próxima revisão:** 2026-01-19
