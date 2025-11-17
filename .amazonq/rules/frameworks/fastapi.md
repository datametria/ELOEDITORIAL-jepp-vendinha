# FastAPI Framework Rules

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Baseado em:** AmazonQ-Guidelines v2.0

---

## 🎯 Visão Geral

Este documento contém **5 rules específicas** para desenvolvimento com **FastAPI**, complementando as rules atômicas (01-06). Estas rules são aplicadas automaticamente pelo Amazon Q Developer quando FastAPI é detectado no projeto.

### Aplicação

- **Quando:** FastAPI detectado (pyproject.toml ou requirements.txt com "fastapi")
- **Complementa:** Rules atômicas 01-06
- **Prioridade:** Alta (aplicadas após rules atômicas)

---

## Rule FA.1: Pydantic Models para Validação

### Contexto

FastAPI usa Pydantic para validação automática de dados. Projetos sem Pydantic models adequados enfrentam:

- **Bugs de validação** (50% dos bugs em APIs)
- **Documentação manual** (OpenAPI incompleto)
- **Código duplicado** (validação em múltiplos lugares)
- **Segurança fraca** (dados não validados)

### Regra

Usar **Pydantic models** para todos os endpoints:

1. Criar models para request body (BaseModel)
2. Criar models para response (BaseModel)
3. Usar Field() para validações customizadas
4. Implementar validators para lógica complexa
5. Documentar com docstrings e examples

### Justificativa

**Benefícios mensuráveis:**

- **50% menos bugs** de validação
- **OpenAPI automático** (100% documentado)
- **Performance 30% melhor** (validação em C)
- **Segurança 100%** (dados sempre validados)
- **Código 40% menor** (sem validação manual)

### Exemplos

#### ✅ Correto

```python
from pydantic import BaseModel, Field, EmailStr, validator
from typing import Optional
from datetime import datetime

class UserCreate(BaseModel):
    """Schema para criação de usuário."""

    email: EmailStr = Field(..., description="Email válido do usuário")
    nome: str = Field(..., min_length=3, max_length=100)
    idade: int = Field(..., ge=18, le=120, description="Idade entre 18 e 120")
    senha: str = Field(..., min_length=8)
    telefone: Optional[str] = Field(None, regex=r'^\+?[1-9]\d{1,14}$')

    @validator('senha')
    def senha_forte(cls, v):
        if not any(c.isupper() for c in v):
            raise ValueError('Senha deve ter ao menos 1 maiúscula')
        if not any(c.isdigit() for c in v):
            raise ValueError('Senha deve ter ao menos 1 número')
        return v

    class Config:
        schema_extra = {
            "example": {
                "email": "user@example.com",
                "nome": "João Silva",
                "idade": 25,
                "senha": "Senha123",
                "telefone": "+5511999999999"
            }
        }

class UserResponse(BaseModel):
    """Schema para resposta de usuário."""

    id: int
    email: EmailStr
    nome: str
    idade: int
    criado_em: datetime
    atualizado_em: datetime

    class Config:
        orm_mode = True  # Permite conversão de ORM models

@app.post("/users/", response_model=UserResponse, status_code=201)
async def criar_usuario(user: UserCreate):
    """Cria novo usuário com validação automática."""
    db_user = await create_user_in_db(user)
    return db_user
```

#### ❌ Incorreto

```python
# Sem Pydantic - validação manual - NÃO FAZER
@app.post("/users/")
async def criar_usuario(request: Request):
    data = await request.json()

    # Validação manual - propenso a erros
    if 'email' not in data:
        raise HTTPException(400, "Email obrigatório")
    if '@' not in data['email']:
        raise HTTPException(400, "Email inválido")
    if len(data.get('nome', '')) < 3:
        raise HTTPException(400, "Nome muito curto")

    # Sem tipagem, sem documentação automática
    return {"id": 1, "email": data['email']}
```

### Ferramentas

- **Pydantic**: Validação automática em C (performance)
- **FastAPI**: Integração nativa com OpenAPI
- **mypy**: Verificação de tipos estática
- **pytest**: Testes de validação

### Checklist

- [ ] Request body tem Pydantic model?
- [ ] Response tem response_model definido?
- [ ] Validações customizadas com Field()?
- [ ] Validators para lógica complexa?
- [ ] Examples documentados?

---

## Rule FA.2: Dependency Injection para Recursos

### Contexto

FastAPI oferece sistema de Dependency Injection poderoso. Projetos sem DI adequado enfrentam:

- **Código acoplado** (difícil de testar)
- **Duplicação** (mesma lógica em múltiplos endpoints)
- **Segurança fraca** (autenticação inconsistente)
- **Manutenção difícil** (mudanças em múltiplos lugares)

### Regra

Usar **Dependency Injection** para recursos compartilhados:

1. Database sessions via Depends()
2. Autenticação/autorização via Depends()
3. Configurações via Depends()
4. Serviços externos via Depends()
5. Validações complexas via Depends()

### Justificativa

**Benefícios mensuráveis:**

- **Testes 80% mais fáceis** (mocking simples)
- **Código 50% menos duplicado**
- **Segurança 100%** (autenticação centralizada)
- **Manutenção 60% mais rápida**
- **Reutilização 5x maior**

### Exemplos

#### ✅ Correto

```python
from fastapi import Depends, HTTPException, status
from fastapi.security import OAuth2PasswordBearer
from sqlalchemy.orm import Session
from typing import Generator

# Database dependency
def get_db() -> Generator[Session, None, None]:
    """Dependency para sessão de banco de dados."""
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

# Auth dependency
oauth2_scheme = OAuth2PasswordBearer(tokenUrl="token")

async def get_current_user(
    token: str = Depends(oauth2_scheme),
    db: Session = Depends(get_db)
) -> User:
    """Dependency para usuário autenticado."""
    credentials_exception = HTTPException(
        status_code=status.HTTP_401_UNAUTHORIZED,
        detail="Credenciais inválidas",
        headers={"WWW-Authenticate": "Bearer"},
    )

    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
        user_id: int = payload.get("sub")
        if user_id is None:
            raise credentials_exception
    except JWTError:
        raise credentials_exception

    user = db.query(User).filter(User.id == user_id).first()
    if user is None:
        raise credentials_exception

    return user

# Admin dependency
async def get_current_admin(
    current_user: User = Depends(get_current_user)
) -> User:
    """Dependency para usuário admin."""
    if not current_user.is_admin:
        raise HTTPException(
            status_code=status.HTTP_403_FORBIDDEN,
            detail="Permissão insuficiente"
        )
    return current_user

# Uso nos endpoints
@app.get("/users/me")
async def read_users_me(current_user: User = Depends(get_current_user)):
    """Retorna usuário autenticado."""
    return current_user

@app.delete("/users/{user_id}")
async def delete_user(
    user_id: int,
    db: Session = Depends(get_db),
    admin: User = Depends(get_current_admin)
):
    """Deleta usuário (apenas admin)."""
    user = db.query(User).filter(User.id == user_id).first()
    if not user:
        raise HTTPException(404, "Usuário não encontrado")

    db.delete(user)
    db.commit()
    return {"message": "Usuário deletado"}
```

#### ❌ Incorreto

```python
# Sem Dependency Injection - NÃO FAZER
@app.get("/users/me")
async def read_users_me(token: str):
    # Validação duplicada em cada endpoint
    try:
        payload = jwt.decode(token, SECRET_KEY)
        user_id = payload.get("sub")
    except:
        raise HTTPException(401, "Token inválido")

    # Sessão de DB criada manualmente
    db = SessionLocal()
    user = db.query(User).filter(User.id == user_id).first()
    db.close()

    return user
```

### Ferramentas

- **FastAPI Depends()**: Sistema de DI nativo
- **pytest**: Fixtures para override de dependencies
- **SQLAlchemy**: Session management
- **python-jose**: JWT handling

### Checklist

- [ ] Database session via Depends()?
- [ ] Autenticação via Depends()?
- [ ] Dependencies reutilizáveis?
- [ ] Testes com override de dependencies?
- [ ] Documentação automática de dependencies?

---

## Rule FA.3: Async/Await para I/O Operations

### Contexto

FastAPI suporta async/await nativo para operações I/O. Projetos sem async enfrentam:

- **Performance 10x pior** (blocking I/O)
- **Escalabilidade limitada** (threads limitadas)
- **Timeout frequente** (operações lentas)
- **Recursos desperdiçados** (CPU idle)

### Regra

Usar **async/await** para todas as operações I/O:

1. Endpoints async quando possível
2. Database queries async (SQLAlchemy async)
3. HTTP requests async (httpx)
4. File operations async (aiofiles)
5. Cache operations async (aioredis)

### Justificativa

**Benefícios mensuráveis:**

- **Performance 10x melhor** (non-blocking I/O)
- **Escalabilidade 100x maior** (10k+ concurrent requests)
- **Latência 50% menor** (operações paralelas)
- **Recursos 80% menos** (CPU/memória)
- **Throughput 5x maior**

### Exemplos

#### ✅ Correto

```python
from fastapi import FastAPI
from sqlalchemy.ext.asyncio import AsyncSession, create_async_engine
from sqlalchemy.orm import sessionmaker
import httpx
import aioredis
from aiofiles import open as aio_open

# Async database
engine = create_async_engine("postgresql+asyncpg://user:pass@localhost/db")
AsyncSessionLocal = sessionmaker(engine, class_=AsyncSession, expire_on_commit=False)

async def get_async_db() -> AsyncSession:
    async with AsyncSessionLocal() as session:
        yield session

# Async Redis
redis = aioredis.from_url("redis://localhost")

@app.get("/users/{user_id}")
async def get_user(
    user_id: int,
    db: AsyncSession = Depends(get_async_db)
):
    """Endpoint async com database async."""

    # Cache check (async)
    cached = await redis.get(f"user:{user_id}")
    if cached:
        return json.loads(cached)

    # Database query (async)
    result = await db.execute(
        select(User).where(User.id == user_id)
    )
    user = result.scalar_one_or_none()

    if not user:
        raise HTTPException(404, "Usuário não encontrado")

    # Cache set (async)
    await redis.setex(
        f"user:{user_id}",
        3600,
        json.dumps(user.dict())
    )

    return user

@app.post("/users/{user_id}/notify")
async def notify_user(user_id: int, message: str):
    """Notifica usuário via API externa (async)."""

    async with httpx.AsyncClient() as client:
        # HTTP request async
        response = await client.post(
            "https://api.notifications.com/send",
            json={"user_id": user_id, "message": message}
        )
        response.raise_for_status()

    # File write async
    async with aio_open(f"logs/notifications.log", "a") as f:
        await f.write(f"{user_id}: {message}\n")

    return {"status": "sent"}

@app.get("/users/{user_id}/report")
async def generate_report(user_id: int):
    """Gera relatório com múltiplas operações paralelas."""

    # Executar múltiplas operações em paralelo
    user_task = get_user_data(user_id)
    orders_task = get_user_orders(user_id)
    stats_task = get_user_stats(user_id)

    # Aguardar todas as operações
    user, orders, stats = await asyncio.gather(
        user_task,
        orders_task,
        stats_task
    )

    return {
        "user": user,
        "orders": orders,
        "stats": stats
    }
```

#### ❌ Incorreto

```python
# Sync operations - blocking I/O - NÃO FAZER
@app.get("/users/{user_id}")
def get_user(user_id: int, db: Session = Depends(get_db)):
    # Blocking database query
    user = db.query(User).filter(User.id == user_id).first()

    # Blocking HTTP request
    response = requests.get(f"https://api.example.com/users/{user_id}")

    # Blocking file read
    with open("data.json", "r") as f:
        data = f.read()

    return user
```

### Ferramentas

- **SQLAlchemy async**: `sqlalchemy.ext.asyncio`
- **httpx**: HTTP client async
- **aiofiles**: File operations async
- **aioredis**: Redis client async
- **asyncpg**: PostgreSQL driver async

### Checklist

- [ ] Endpoints são async?
- [ ] Database queries são async?
- [ ] HTTP requests são async?
- [ ] File operations são async?
- [ ] Cache operations são async?

---

## Rule FA.4: APIRouter para Modularização

### Contexto

FastAPI oferece APIRouter para organizar endpoints. Projetos sem modularização enfrentam:

- **Arquivo único gigante** (1000+ linhas)
- **Manutenção impossível** (conflitos de merge)
- **Testes difíceis** (setup complexo)
- **Reuso zero** (código acoplado)

### Regra

Usar **APIRouter** para modularizar endpoints:

1. Um router por domínio/feature
2. Prefixo e tags consistentes
3. Dependencies compartilhadas no router
4. Estrutura de pastas por feature
5. Incluir routers no app principal

### Justificativa

**Benefícios mensuráveis:**

- **Arquivos 90% menores** (< 200 linhas)
- **Manutenção 70% mais fácil**
- **Testes 80% mais simples**
- **Reuso 5x maior**
- **Conflitos de merge 95% menos**

### Exemplos

#### ✅ Correto

```python
# app/routers/users.py
from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session

router = APIRouter(
    prefix="/users",
    tags=["users"],
    dependencies=[Depends(get_db)],
    responses={404: {"description": "Not found"}},
)

@router.get("/")
async def list_users(
    skip: int = 0,
    limit: int = 100,
    db: Session = Depends(get_db)
):
    """Lista todos os usuários."""
    users = db.query(User).offset(skip).limit(limit).all()
    return users

@router.post("/", response_model=UserResponse, status_code=201)
async def create_user(
    user: UserCreate,
    db: Session = Depends(get_db)
):
    """Cria novo usuário."""
    db_user = User(**user.dict())
    db.add(db_user)
    db.commit()
    db.refresh(db_user)
    return db_user

@router.get("/{user_id}")
async def get_user(
    user_id: int,
    db: Session = Depends(get_db)
):
    """Retorna usuário por ID."""
    user = db.query(User).filter(User.id == user_id).first()
    if not user:
        raise HTTPException(404, "Usuário não encontrado")
    return user

# app/routers/orders.py
router = APIRouter(
    prefix="/orders",
    tags=["orders"],
    dependencies=[Depends(get_current_user)],
)

@router.get("/")
async def list_orders(
    current_user: User = Depends(get_current_user),
    db: Session = Depends(get_db)
):
    """Lista pedidos do usuário."""
    orders = db.query(Order).filter(Order.user_id == current_user.id).all()
    return orders

# app/main.py
from fastapi import FastAPI
from app.routers import users, orders, products

app = FastAPI(title="API", version="1.0.0")

# Incluir routers
app.include_router(users.router)
app.include_router(orders.router)
app.include_router(products.router)

# Estrutura de pastas
"""
app/
├── main.py
├── routers/
│   ├── __init__.py
│   ├── users.py
│   ├── orders.py
│   └── products.py
├── models/
│   ├── user.py
│   ├── order.py
│   └── product.py
└── schemas/
    ├── user.py
    ├── order.py
    └── product.py
"""
```

#### ❌ Incorreto

```python
# Tudo em um arquivo - NÃO FAZER
# main.py (1500+ linhas)
from fastapi import FastAPI

app = FastAPI()

@app.get("/users/")
async def list_users():
    pass

@app.post("/users/")
async def create_user():
    pass

@app.get("/orders/")
async def list_orders():
    pass

@app.post("/orders/")
async def create_order():
    pass

# ... 50+ endpoints no mesmo arquivo
```

### Ferramentas

- **FastAPI APIRouter**: Modularização nativa
- **pytest**: Testes isolados por router
- **OpenAPI**: Documentação agrupada por tags
- **Import linter**: Validação de estrutura

### Checklist

- [ ] Um router por domínio?
- [ ] Prefixo e tags definidos?
- [ ] Dependencies compartilhadas?
- [ ] Estrutura de pastas por feature?
- [ ] Routers incluídos no app?

---

## Rule FA.5: Background Tasks e Celery

### Contexto

FastAPI oferece BackgroundTasks para operações assíncronas. Projetos sem background tasks enfrentam:

- **Timeout em requests** (operações longas)
- **UX ruim** (usuário aguardando)
- **Escalabilidade limitada** (requests bloqueadas)
- **Recursos desperdiçados** (CPU idle)

### Regra

Usar **BackgroundTasks** para operações não-críticas e **Celery** para operações complexas:

1. BackgroundTasks para operações simples (< 5s)
2. Celery para operações longas (> 5s)
3. Redis como message broker
4. Retry automático em falhas
5. Monitoring de tasks

### Justificativa

**Benefícios mensuráveis:**

- **Response time 90% menor** (operações em background)
- **UX 100% melhor** (resposta imediata)
- **Escalabilidade 10x maior** (workers paralelos)
- **Confiabilidade 95%** (retry automático)
- **Throughput 5x maior**

### Exemplos

#### ✅ Correto

```python
from fastapi import BackgroundTasks
from celery import Celery
import smtplib
from email.mime.text import MIMEText

# Celery setup
celery_app = Celery(
    "tasks",
    broker="redis://localhost:6379/0",
    backend="redis://localhost:6379/0"
)

# Background task simples (< 5s)
def send_email_notification(email: str, message: str):
    """Envia email em background."""
    msg = MIMEText(message)
    msg['Subject'] = 'Notificação'
    msg['From'] = 'noreply@example.com'
    msg['To'] = email

    with smtplib.SMTP('localhost') as server:
        server.send_message(msg)

@app.post("/users/")
async def create_user(
    user: UserCreate,
    background_tasks: BackgroundTasks,
    db: Session = Depends(get_db)
):
    """Cria usuário e envia email de boas-vindas."""
    db_user = User(**user.dict())
    db.add(db_user)
    db.commit()

    # Email em background (não bloqueia response)
    background_tasks.add_task(
        send_email_notification,
        user.email,
        "Bem-vindo!"
    )

    return db_user

# Celery task complexa (> 5s)
@celery_app.task(bind=True, max_retries=3)
def generate_report(self, user_id: int):
    """Gera relatório complexo (operação longa)."""
    try:
        # Operação que pode levar minutos
        data = fetch_user_data(user_id)
        report = process_data(data)
        pdf = generate_pdf(report)

        # Upload para S3
        upload_to_s3(pdf, f"reports/{user_id}.pdf")

        # Notificar usuário
        send_email_notification(
            data['email'],
            "Seu relatório está pronto!"
        )

        return {"status": "completed", "file": f"reports/{user_id}.pdf"}

    except Exception as exc:
        # Retry automático em caso de falha
        raise self.retry(exc=exc, countdown=60)

@app.post("/reports/{user_id}")
async def request_report(user_id: int):
    """Solicita geração de relatório (async via Celery)."""
    task = generate_report.delay(user_id)

    return {
        "message": "Relatório em processamento",
        "task_id": task.id,
        "status_url": f"/tasks/{task.id}"
    }

@app.get("/tasks/{task_id}")
async def get_task_status(task_id: str):
    """Verifica status de task Celery."""
    task = celery_app.AsyncResult(task_id)

    if task.state == 'PENDING':
        response = {'state': task.state, 'status': 'Aguardando...'}
    elif task.state == 'SUCCESS':
        response = {'state': task.state, 'result': task.result}
    elif task.state == 'FAILURE':
        response = {'state': task.state, 'error': str(task.info)}
    else:
        response = {'state': task.state, 'status': task.info}

    return response
```

#### ❌ Incorreto

```python
# Operação longa bloqueando request - NÃO FAZER
@app.post("/reports/{user_id}")
async def request_report(user_id: int):
    # Operação que leva 5 minutos - BLOQUEIA REQUEST
    data = fetch_user_data(user_id)  # 1 min
    report = process_data(data)       # 3 min
    pdf = generate_pdf(report)        # 1 min

    # Usuário aguardando 5 minutos!
    return {"file": pdf}
```

### Ferramentas

- **FastAPI BackgroundTasks**: Tasks simples
- **Celery**: Tasks complexas e distribuídas
- **Redis**: Message broker
- **Flower**: Monitoring de Celery
- **pytest-celery**: Testes de tasks

### Checklist

- [ ] BackgroundTasks para operações < 5s?
- [ ] Celery para operações > 5s?
- [ ] Redis configurado como broker?
- [ ] Retry automático implementado?
- [ ] Monitoring de tasks configurado?

---

## 📊 Resumo das Rules

| Rule | Foco | Impacto | Prioridade |
|------|------|---------|------------|
| **FA.1** | Pydantic Models | 50% menos bugs validação | 🔴 Alta |
| **FA.2** | Dependency Injection | 80% testes mais fáceis | 🔴 Alta |
| **FA.3** | Async/Await | 10x performance | 🔴 Alta |
| **FA.4** | APIRouter | 90% arquivos menores | 🟡 Média |
| **FA.5** | Background Tasks | 90% response time menor | 🟡 Média |

---

## 🔗 Integração com Rules Atômicas

### Aplicação Conjunta

| Rule Atômica | Rule FastAPI | Sinergia |
|--------------|--------------|----------|
| **01-code-style** | FA.1, FA.4 | Naming + Pydantic + Modularização |
| **02-architecture** | FA.2, FA.4 | Clean Architecture + DI + Routers |
| **03-security** | FA.1, FA.2 | Security + Validation + Auth |
| **04-testing** | FA.2, FA.5 | Testing + DI + Background Tasks |
| **05-performance** | FA.3, FA.5 | Performance + Async + Tasks |
| **06-documentation** | FA.1 | Documentation + OpenAPI automático |

---

## ✅ Checklist de Conformidade FastAPI

### Setup Inicial

- [ ] FastAPI instalado?
- [ ] Pydantic configurado?
- [ ] SQLAlchemy async configurado?
- [ ] Redis configurado?
- [ ] Celery configurado?

### Desenvolvimento

- [ ] Endpoints usam Pydantic models?
- [ ] Dependencies via Depends()?
- [ ] Operações I/O são async?
- [ ] Routers modularizados?
- [ ] Background tasks implementadas?

### Qualidade

- [ ] OpenAPI 100% documentado?
- [ ] Testes com coverage > 80%?
- [ ] Performance validada?
- [ ] Security checklist completo?
- [ ] Monitoring configurado?

---

**Versão:** 1.0
**Próxima revisão:** 19/01/2026
**Mantido por:** Vander Loto - CTO DATAMETRIA
