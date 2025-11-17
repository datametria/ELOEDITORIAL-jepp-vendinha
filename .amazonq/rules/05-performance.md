# Performance: Padrões DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 5.1: Database Indexing Obrigatório

### Contexto

Queries sem índices causam:

- Queries lentas (>1s)
- Full table scans
- Degradação com crescimento de dados

### Regra

Criar índices para:

- **Foreign keys**: Sempre
- **Campos de busca**: WHERE, JOIN
- **Campos de ordenação**: ORDER BY
- **Campos únicos**: UNIQUE index
- **Validação**: EXPLAIN ANALYZE antes de produção

### Justificativa

- Queries 100-1000x mais rápidas
- Escalabilidade garantida
- Custo de storage mínimo vs benefício

### Exemplos

#### ✅ Correto (SQLAlchemy)

```python
from sqlalchemy import Column, String, Integer, ForeignKey, Index
from sqlalchemy.orm import relationship

class User(Base):
    __tablename__ = "users"

    id = Column(String, primary_key=True)
    email = Column(String, unique=True, nullable=False, index=True)  # Busca frequente
    nome = Column(String, nullable=False)
    created_at = Column(DateTime, nullable=False, index=True)  # Ordenação

    # Índice composto para queries comuns
    __table_args__ = (
        Index('idx_user_email_created', 'email', 'created_at'),
    )

class Order(Base):
    __tablename__ = "orders"

    id = Column(String, primary_key=True)
    user_id = Column(String, ForeignKey("users.id"), nullable=False, index=True)  # FK
    status = Column(String, nullable=False, index=True)  # Filtro frequente
    total = Column(Numeric, nullable=False)
    created_at = Column(DateTime, nullable=False, index=True)

    user = relationship("User", back_populates="orders")

    # Índice composto para dashboard
    __table_args__ = (
        Index('idx_order_user_status', 'user_id', 'status'),
        Index('idx_order_status_created', 'status', 'created_at'),
    )
```

```sql
-- Validação com EXPLAIN ANALYZE
EXPLAIN ANALYZE
SELECT * FROM orders
WHERE user_id = '123' AND status = 'pending'
ORDER BY created_at DESC
LIMIT 10;

-- Resultado esperado:
-- Index Scan using idx_order_user_status (cost=0.42..8.44 rows=1)
-- Execution Time: 0.123 ms
```

#### ❌ Incorreto

```python
class Order(Base):
    __tablename__ = "orders"

    id = Column(String, primary_key=True)
    user_id = Column(String, ForeignKey("users.id"))  # Sem índice!
    status = Column(String)  # Sem índice!
    created_at = Column(DateTime)  # Sem índice!

# Query lenta (full table scan):
# SELECT * FROM orders WHERE user_id = '123'
# Seq Scan on orders (cost=0.00..1234.56 rows=50000)
# Execution Time: 2500 ms
```

### Ferramentas

- **EXPLAIN ANALYZE**: Análise de query plan
- **pg_stat_statements**: Queries lentas (PostgreSQL)
- **Django Debug Toolbar**: Análise de queries
- **SQLAlchemy**: `echo=True` para debug

### Checklist

- [ ] Foreign keys indexadas?
- [ ] Campos de busca indexados?
- [ ] Campos de ordenação indexados?
- [ ] EXPLAIN ANALYZE validado?

---

## Rule 5.2: N+1 Query Prevention

### Contexto

N+1 queries causam:

- 1 query inicial + N queries adicionais
- Latência multiplicada
- Sobrecarga de BD

### Regra

Usar eager loading:

- **SQLAlchemy**: `joinedload()`, `selectinload()`
- **Django**: `select_related()`, `prefetch_related()`
- **Prisma**: `include`
- **Validação**: Django Debug Toolbar, query counter

### Justificativa

- 1 query vs N+1 queries
- Latência reduzida em 90%
- Menos carga no BD

### Exemplos

#### ✅ Correto (SQLAlchemy)

```python
from sqlalchemy.orm import joinedload, selectinload

# Eager loading com joinedload (1 query com JOIN)
def get_users_with_orders():
    return session.query(User).options(
        joinedload(User.orders)
    ).all()
    # SELECT users.*, orders.* FROM users LEFT JOIN orders ON ...
    # 1 query total

# Eager loading com selectinload (2 queries otimizadas)
def get_users_with_orders_and_items():
    return session.query(User).options(
        selectinload(User.orders).selectinload(Order.items)
    ).all()
    # Query 1: SELECT * FROM users
    # Query 2: SELECT * FROM orders WHERE user_id IN (...)
    # Query 3: SELECT * FROM items WHERE order_id IN (...)
    # 3 queries total (não N+1)
```

#### ❌ Incorreto (N+1 problem)

```python
# N+1 queries!
def get_users_with_orders():
    users = session.query(User).all()  # 1 query
    for user in users:
        orders = user.orders  # N queries (1 por usuário)!
        print(f"{user.nome}: {len(orders)} orders")

# Total: 1 + N queries
# 100 usuários = 101 queries!
```

### Ferramentas

- **Django Debug Toolbar**: Detecta N+1
- **nplusone**: Biblioteca Python para detecção
- **Bullet**: Gem Ruby para detecção
- **Query counter**: Testes automatizados

### Checklist

- [ ] Eager loading configurado?
- [ ] Query counter em testes?
- [ ] Debug toolbar em desenvolvimento?

---

## Rule 5.3: Caching Strategy

### Contexto

Dados não cacheados causam:

- Queries repetitivas
- Latência alta
- Custo de BD elevado

### Regra

Implementar caching em camadas:

1. **Application cache**: Redis (dados frequentes)
2. **Query cache**: PostgreSQL (queries complexas)
3. **HTTP cache**: CDN (assets estáticos)
4. **TTL**: Baseado em volatilidade dos dados

### Justificativa

- Latência reduzida em 95%
- Carga de BD reduzida em 80%
- Escalabilidade horizontal

### Exemplos

#### ✅ Correto (Redis + FastAPI)

```python
import redis.asyncio as redis
from functools import wraps
import json

redis_client = redis.from_url("redis://localhost")

def cache(ttl: int = 300):
    """Decorator para cache com TTL."""
    def decorator(func):
        @wraps(func)
        async def wrapper(*args, **kwargs):
            # Gera chave de cache
            cache_key = f"{func.__name__}:{args}:{kwargs}"

            # Tenta buscar do cache
            cached = await redis_client.get(cache_key)
            if cached:
                return json.loads(cached)

            # Executa função e cacheia resultado
            result = await func(*args, **kwargs)
            await redis_client.setex(
                cache_key,
                ttl,
                json.dumps(result, default=str)
            )
            return result
        return wrapper
    return decorator

@cache(ttl=300)  # 5 minutos
async def get_user_profile(user_id: str) -> dict:
    """Busca perfil de usuário (cacheado)."""
    user = await repository.find(user_id)
    return user.to_dict()

@cache(ttl=3600)  # 1 hora
async def get_product_catalog() -> list:
    """Busca catálogo de produtos (cacheado)."""
    products = await repository.find_all()
    return [p.to_dict() for p in products]

# Invalidação de cache
async def update_user(user_id: str, data: dict):
    """Atualiza usuário e invalida cache."""
    user = await repository.update(user_id, data)

    # Invalida cache
    cache_key = f"get_user_profile:{user_id}"
    await redis_client.delete(cache_key)

    return user
```

#### ❌ Incorreto

```python
# Sem cache - query repetitiva
async def get_user_profile(user_id: str):
    # Executado a cada request!
    return await repository.find(user_id)
```

### Ferramentas

- **Redis**: Cache distribuído
- **Memcached**: Cache simples
- **CloudFlare**: CDN com cache
- **Varnish**: HTTP cache

### Checklist

- [ ] Cache implementado?
- [ ] TTL adequado por tipo de dado?
- [ ] Invalidação de cache configurada?
- [ ] Monitoramento de hit rate?

---

## Rule 5.4: Async/Await para I/O

### Contexto

I/O síncrono causa:

- Threads bloqueadas
- Baixa concorrência
- Recursos desperdiçados

### Regra

Usar async/await para:

- **Database queries**: asyncpg, motor
- **HTTP requests**: httpx, aiohttp
- **File I/O**: aiofiles
- **Framework**: FastAPI, aiohttp (não Flask sync)

### Justificativa

- Concorrência 10-100x maior
- Recursos otimizados
- Latência reduzida

### Exemplos

#### ✅ Correto (Async)

```python
import httpx
import asyncio
from typing import List

async def fetch_user_data(user_id: str) -> dict:
    """Busca dados de usuário (async)."""
    async with httpx.AsyncClient() as client:
        response = await client.get(f"https://api.example.com/users/{user_id}")
        return response.json()

async def fetch_multiple_users(user_ids: List[str]) -> List[dict]:
    """Busca múltiplos usuários em paralelo."""
    tasks = [fetch_user_data(uid) for uid in user_ids]
    return await asyncio.gather(*tasks)
    # 100 usuários em ~1s (paralelo) vs 100s (sequencial)

# FastAPI endpoint async
@app.get("/users/{user_id}")
async def get_user(user_id: str):
    user = await repository.find(user_id)  # Async query
    external_data = await fetch_user_data(user_id)  # Async HTTP
    return {**user.to_dict(), **external_data}
```

#### ❌ Incorreto (Sync)

```python
import requests

def fetch_user_data(user_id: str) -> dict:
    """Busca síncrona - bloqueia thread."""
    response = requests.get(f"https://api.example.com/users/{user_id}")
    return response.json()

def fetch_multiple_users(user_ids: List[str]) -> List[dict]:
    """Busca sequencial - muito lento."""
    return [fetch_user_data(uid) for uid in user_ids]
    # 100 usuários em ~100s (sequencial)
```

### Ferramentas

- **FastAPI**: Framework async
- **httpx**: HTTP client async
- **asyncpg**: PostgreSQL async
- **motor**: MongoDB async

### Checklist

- [ ] Framework async (FastAPI)?
- [ ] DB queries async?
- [ ] HTTP requests async?
- [ ] File I/O async?

---

## Rule 5.5: Lazy Loading e Pagination

### Contexto

Carregar tudo causa:

- Memória excessiva
- Timeout de requests
- UX ruim (loading longo)

### Regra

Implementar:

- **Pagination**: Limit/offset ou cursor-based
- **Lazy loading**: Carregar sob demanda
- **Infinite scroll**: Para listas longas
- **Limites**: Max 100 itens por página

### Justificativa

- Memória controlada
- Response time consistente
- UX responsiva

### Exemplos

#### ✅ Correto (Cursor-based pagination)

```python
from typing import Optional, List
from pydantic import BaseModel

class PaginatedResponse(BaseModel):
    items: List[dict]
    next_cursor: Optional[str]
    has_more: bool

@app.get("/users", response_model=PaginatedResponse)
async def list_users(
    cursor: Optional[str] = None,
    limit: int = 20
):
    """Lista usuários com cursor-based pagination."""
    if limit > 100:
        limit = 100  # Limite máximo

    query = select(User).order_by(User.created_at.desc())

    if cursor:
        # Decodifica cursor (base64 do created_at)
        cursor_date = decode_cursor(cursor)
        query = query.where(User.created_at < cursor_date)

    query = query.limit(limit + 1)  # +1 para verificar has_more

    users = await session.execute(query)
    items = users.scalars().all()

    has_more = len(items) > limit
    if has_more:
        items = items[:limit]

    next_cursor = None
    if has_more and items:
        next_cursor = encode_cursor(items[-1].created_at)

    return PaginatedResponse(
        items=[u.to_dict() for u in items],
        next_cursor=next_cursor,
        has_more=has_more
    )
```

#### ❌ Incorreto

```python
@app.get("/users")
async def list_users():
    """Retorna TODOS os usuários - perigoso!"""
    users = await session.execute(select(User))
    return users.scalars().all()
    # 1 milhão de usuários = crash!
```

### Ferramentas

- **FastAPI Pagination**: Biblioteca de paginação
- **Django Paginator**: Paginação built-in
- **Cursor encoding**: Base64 para cursors

### Checklist

- [ ] Pagination implementada?
- [ ] Limite máximo configurado?
- [ ] Cursor-based para listas grandes?
- [ ] has_more indicator presente?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Queries com índices | 100% | EXPLAIN ANALYZE |
| N+1 queries | 0 | Query counter |
| Cache hit rate | ≥ 80% | Redis INFO |
| Async I/O | 100% | Code review |
| Pagination | 100% list endpoints | API review |

---

**Próxima revisão:** 2026-01-19
