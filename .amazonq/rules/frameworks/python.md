# Python Framework: Padrões DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Contexto

Este documento define 5 rules específicas para desenvolvimento Python puro (scripts, bibliotecas, CLI tools, automação). Para frameworks específicos (Flask, FastAPI), consulte os respectivos arquivos.

---

## Rule Python.1: Poetry para Gerenciamento de Dependências

### Contexto

Gerenciamento manual de dependências causa:

- Conflitos de versões
- Builds não reproduzíveis
- Dificuldade de distribuição

### Regra

TODO projeto Python DEVE usar Poetry:

- `pyproject.toml` como fonte única de verdade
- Lock file (`poetry.lock`) versionado
- Ambientes virtuais isolados
- Grupos de dependências (dev, test, docs)

### Justificativa

- Builds reproduzíveis 100%
- Resolução automática de conflitos
- Publicação simplificada no PyPI

### Exemplos

#### ✅ Correto

```toml
# pyproject.toml
[tool.poetry]
name = "meu-projeto"
version = "1.0.0"
description = "Descrição do projeto"
authors = ["Nome <email@example.com>"]

[tool.poetry.dependencies]
python = "^3.11"
requests = "^2.31.0"
pydantic = "^2.0.0"

[tool.poetry.group.dev.dependencies]
pytest = "^7.4.0"
black = "^23.7.0"
mypy = "^1.5.0"

[tool.poetry.group.test.dependencies]
pytest-cov = "^4.1.0"
pytest-mock = "^3.11.0"

[build-system]
requires = ["poetry-core"]
build-backend = "poetry.core.masonry.api"
```

```bash
# Comandos essenciais
poetry new meu-projeto --src
poetry add requests pydantic
poetry add --group dev pytest black mypy
poetry install
poetry run pytest
poetry build
poetry publish
```

#### ❌ Incorreto

```bash
# requirements.txt manual
pip install requests
pip install pydantic
pip freeze > requirements.txt  # Inclui dependências transitivas
```

### Ferramentas

- **Poetry**: `curl -sSL https://install.python-poetry.org | python3 -`
- **Poetry Plugin Export**: `poetry self add poetry-plugin-export`

### Checklist

- [ ] `pyproject.toml` configurado?
- [ ] `poetry.lock` versionado?
- [ ] Grupos de dependências separados?
- [ ] CI/CD usa `poetry install`?

---

## Rule Python.2: Type Hints Obrigatórios

### Contexto

Código sem type hints causa:

- Bugs de tipo em runtime
- IDEs sem autocomplete
- Refatoração arriscada

### Regra

TODO código Python DEVE ter type hints:

- Parâmetros de função
- Retornos de função
- Atributos de classe
- Variáveis complexas
- Validação com mypy strict mode

### Justificativa

- Bugs detectados em tempo de desenvolvimento
- Autocomplete e refactoring seguros
- Documentação automática

### Exemplos

#### ✅ Correto

```python
from typing import List, Dict, Optional, Union
from decimal import Decimal

def calcular_total(
    itens: List[Dict[str, Union[str, Decimal]]],
    desconto: Optional[Decimal] = None
) -> Decimal:
    """Calcula total com desconto opcional."""
    subtotal: Decimal = sum(item["preco"] for item in itens)

    if desconto:
        return subtotal * (1 - desconto)
    return subtotal

class Usuario:
    """Usuário do sistema."""

    def __init__(self, nome: str, idade: int, email: str) -> None:
        self.nome: str = nome
        self.idade: int = idade
        self.email: str = email

    def to_dict(self) -> Dict[str, Union[str, int]]:
        """Converte para dicionário."""
        return {
            "nome": self.nome,
            "idade": self.idade,
            "email": self.email
        }
```

#### ❌ Incorreto

```python
def calcular_total(itens, desconto=None):
    """Sem type hints."""
    subtotal = sum(item["preco"] for item in itens)
    if desconto:
        return subtotal * (1 - desconto)
    return subtotal
```

### Configuração MyPy

```toml
# pyproject.toml
[tool.mypy]
python_version = "3.11"
strict = true
warn_return_any = true
warn_unused_configs = true
disallow_untyped_defs = true
```

### Ferramentas

- **mypy**: `poetry add --group dev mypy`
- **pyright**: Alternativa da Microsoft

### Checklist

- [ ] Type hints em todas as funções?
- [ ] mypy strict mode configurado?
- [ ] CI/CD valida tipos?

---

## Rule Python.3: Dataclasses para Estruturas de Dados

### Contexto

Classes manuais para dados causam:

- Boilerplate excessivo
- Falta de métodos úteis (**eq**, **repr**)
- Código verboso

### Regra

Estruturas de dados DEVEM usar dataclasses:

- `@dataclass` para classes simples
- `frozen=True` para imutabilidade
- `slots=True` para performance (Python 3.10+)
- Pydantic para validação complexa

### Justificativa

- 70% menos código
- Métodos automáticos (**init**, **repr**, **eq**)
- Imutabilidade opcional

### Exemplos

#### ✅ Correto

```python
from dataclasses import dataclass, field
from typing import List, Optional
from decimal import Decimal

@dataclass(frozen=True, slots=True)
class Produto:
    """Produto imutável com slots."""
    id: str
    nome: str
    preco: Decimal
    estoque: int = 0
    tags: List[str] = field(default_factory=list)

@dataclass
class Pedido:
    """Pedido mutável."""
    id: str
    usuario_id: str
    itens: List[Produto]
    desconto: Optional[Decimal] = None

    @property
    def total(self) -> Decimal:
        """Calcula total do pedido."""
        subtotal = sum(item.preco for item in self.itens)
        if self.desconto:
            return subtotal * (1 - self.desconto)
        return subtotal

# Uso
produto = Produto(
    id="123",
    nome="Notebook",
    preco=Decimal("2500.00"),
    estoque=10,
    tags=["eletrônicos", "informática"]
)

pedido = Pedido(
    id="456",
    usuario_id="789",
    itens=[produto],
    desconto=Decimal("0.1")
)

print(pedido.total)  # Decimal('2250.00')
```

#### ❌ Incorreto

```python
class Produto:
    """Classe manual verbosa."""
    def __init__(self, id, nome, preco, estoque=0, tags=None):
        self.id = id
        self.nome = nome
        self.preco = preco
        self.estoque = estoque
        self.tags = tags or []

    def __repr__(self):
        return f"Produto(id={self.id}, nome={self.nome}...)"

    def __eq__(self, other):
        if not isinstance(other, Produto):
            return False
        return self.id == other.id
```

### Pydantic para Validação

```python
from pydantic import BaseModel, EmailStr, Field

class Usuario(BaseModel):
    """Usuário com validação Pydantic."""
    nome: str = Field(..., min_length=3, max_length=100)
    email: EmailStr
    idade: int = Field(..., ge=18, le=120)

    class Config:
        frozen = True  # Imutável
```

### Ferramentas

- **dataclasses**: Built-in Python 3.7+
- **Pydantic**: Validação avançada

### Checklist

- [ ] Dataclasses para estruturas simples?
- [ ] `frozen=True` para imutabilidade?
- [ ] Pydantic para validação?

---

## Rule Python.4: Context Managers para Recursos

### Contexto

Gerenciamento manual de recursos causa:

- Leaks de memória/conexões
- Arquivos não fechados
- Locks não liberados

### Regra

Recursos DEVEM usar context managers:

- Arquivos: `with open()`
- Conexões BD: `with connection`
- Locks: `with lock`
- Custom: implementar `__enter__` e `__exit__`

### Justificativa

- Cleanup automático garantido
- Código mais limpo
- Exception-safe

### Exemplos

#### ✅ Correto

```python
from contextlib import contextmanager
from typing import Generator
import sqlite3

# Arquivos
with open("dados.txt", "r") as f:
    conteudo = f.read()
# Arquivo fechado automaticamente

# Conexão BD
with sqlite3.connect("db.sqlite") as conn:
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM users")
    users = cursor.fetchall()
# Conexão fechada automaticamente

# Context manager customizado
@contextmanager
def timer(nome: str) -> Generator[None, None, None]:
    """Mede tempo de execução."""
    import time
    inicio = time.time()
    try:
        yield
    finally:
        fim = time.time()
        print(f"{nome}: {fim - inicio:.2f}s")

with timer("Processamento"):
    # Código a ser medido
    processar_dados()

# Classe com context manager
class DatabaseConnection:
    """Conexão com context manager."""

    def __init__(self, db_url: str):
        self.db_url = db_url
        self.connection = None

    def __enter__(self):
        self.connection = sqlite3.connect(self.db_url)
        return self.connection

    def __exit__(self, exc_type, exc_val, exc_tb):
        if self.connection:
            self.connection.close()
        return False

with DatabaseConnection("db.sqlite") as conn:
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM users")
```

#### ❌ Incorreto

```python
# Sem context manager - perigoso!
f = open("dados.txt", "r")
conteudo = f.read()
f.close()  # Pode não executar se houver exceção

conn = sqlite3.connect("db.sqlite")
cursor = conn.cursor()
cursor.execute("SELECT * FROM users")
conn.close()  # Pode não executar
```

### Ferramentas

- **contextlib**: Built-in para context managers
- **contextlib.suppress**: Suprimir exceções específicas

### Checklist

- [ ] Arquivos usam `with open()`?
- [ ] Conexões usam context managers?
- [ ] Resources customizados implementam `__enter__`/`__exit__`?

---

## Rule Python.5: Async/Await para I/O

### Contexto

I/O síncrono causa:

- Threads bloqueadas
- Baixa concorrência
- Recursos desperdiçados

### Regra

Operações I/O DEVEM usar async/await:

- HTTP requests: `httpx`, `aiohttp`
- Database: `asyncpg`, `motor`
- File I/O: `aiofiles`
- Framework: FastAPI, aiohttp (não Flask sync)

### Justificativa

- Concorrência 10-100x maior
- Recursos otimizados
- Latência reduzida

### Exemplos

#### ✅ Correto

```python
import asyncio
import httpx
from typing import List

async def fetch_user(user_id: str) -> dict:
    """Busca usuário async."""
    async with httpx.AsyncClient() as client:
        response = await client.get(f"https://api.example.com/users/{user_id}")
        return response.json()

async def fetch_multiple_users(user_ids: List[str]) -> List[dict]:
    """Busca múltiplos usuários em paralelo."""
    tasks = [fetch_user(uid) for uid in user_ids]
    return await asyncio.gather(*tasks)
    # 100 usuários em ~1s (paralelo) vs 100s (sequencial)

# Database async
import asyncpg

async def get_users_from_db() -> List[dict]:
    """Busca usuários do BD async."""
    conn = await asyncpg.connect("postgresql://localhost/db")
    try:
        rows = await conn.fetch("SELECT * FROM users")
        return [dict(row) for row in rows]
    finally:
        await conn.close()

# File I/O async
import aiofiles

async def read_file_async(filepath: str) -> str:
    """Lê arquivo async."""
    async with aiofiles.open(filepath, "r") as f:
        return await f.read()

# Executar
async def main():
    user_ids = ["1", "2", "3", "4", "5"]
    users = await fetch_multiple_users(user_ids)
    print(f"Buscados {len(users)} usuários")

if __name__ == "__main__":
    asyncio.run(main())
```

#### ❌ Incorreto

```python
import requests

def fetch_user(user_id: str) -> dict:
    """Busca síncrona - bloqueia thread."""
    response = requests.get(f"https://api.example.com/users/{user_id}")
    return response.json()

def fetch_multiple_users(user_ids: List[str]) -> List[dict]:
    """Busca sequencial - muito lento."""
    return [fetch_user(uid) for uid in user_ids]
    # 100 usuários em ~100s (sequencial)
```

### Quando NÃO usar Async

- CPU-bound tasks (use multiprocessing)
- Scripts simples de curta duração
- Bibliotecas sem suporte async

### Ferramentas

- **httpx**: HTTP client async
- **asyncpg**: PostgreSQL async
- **motor**: MongoDB async
- **aiofiles**: File I/O async

### Checklist

- [ ] HTTP requests async?
- [ ] Database queries async?
- [ ] Framework suporta async?
- [ ] `asyncio.gather()` para paralelismo?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Poetry usage | 100% | `pyproject.toml` existe |
| Type hints coverage | ≥ 90% | `mypy --strict` |
| Dataclasses usage | ≥ 80% estruturas | Code review |
| Context managers | 100% recursos | Code review |
| Async I/O | 100% operações I/O | Code review |

---

**Próxima revisão:** 2026-01-19
