# Security: Padrões DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 3.1: JWT com Refresh Tokens

### Contexto

Autenticação insegura causa:

- Tokens roubados válidos indefinidamente
- Impossível revogar acesso
- Sessões sem controle

### Regra

Autenticação DEVE usar:

- **Access Token**: JWT curto (15min), stateless
- **Refresh Token**: Longo (7 dias), armazenado em BD
- **Rotation**: Refresh token rotacionado a cada uso
- **HttpOnly Cookies**: Para web (não localStorage)

### Justificativa

- Access token curto limita janela de ataque
- Refresh token permite revogação
- Rotation detecta roubo de token

### Exemplos

#### ✅ Correto

```python
from datetime import datetime, timedelta
import jwt

class AuthService:
    ACCESS_TOKEN_EXPIRE = timedelta(minutes=15)
    REFRESH_TOKEN_EXPIRE = timedelta(days=7)

    def create_tokens(self, user_id: str) -> dict:
        """Cria par de tokens."""
        access_token = jwt.encode(
            {
                "sub": user_id,
                "type": "access",
                "exp": datetime.utcnow() + self.ACCESS_TOKEN_EXPIRE
            },
            settings.SECRET_KEY,
            algorithm="HS256"
        )

        refresh_token = self._generate_refresh_token()
        self._store_refresh_token(user_id, refresh_token)

        return {
            "access_token": access_token,
            "refresh_token": refresh_token,
            "token_type": "bearer"
        }

    def refresh_access_token(self, refresh_token: str) -> dict:
        """Rotaciona refresh token e gera novo access token."""
        user_id = self._validate_refresh_token(refresh_token)

        # Invalida token antigo
        self._revoke_refresh_token(refresh_token)

        # Cria novos tokens
        return self.create_tokens(user_id)
```

```typescript
// Frontend - HttpOnly cookies
async function login(email: string, password: string) {
  const response = await fetch('/api/v1/auth/login', {
    method: 'POST',
    credentials: 'include', // Envia cookies
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password })
  });

  // Tokens em HttpOnly cookies (não acessíveis via JS)
  // Access token: 15min
  // Refresh token: 7 dias
}
```

#### ❌ Incorreto

```python
# Token sem expiração
def create_token(user_id: str) -> str:
    return jwt.encode({"sub": user_id}, SECRET_KEY)

# Token em localStorage (vulnerável a XSS)
localStorage.setItem('token', access_token);
```

### Ferramentas

- **PyJWT**: Geração e validação de tokens
- **Redis**: Armazenamento de refresh tokens
- **OWASP JWT Cheat Sheet**

### Checklist

- [ ] Access token expira em ≤ 15min?
- [ ] Refresh token armazenado em BD?
- [ ] Refresh token rotacionado?
- [ ] HttpOnly cookies para web?

---

## Rule 3.2: Input Validation com Pydantic/Zod

### Contexto

Validação manual causa:

- SQL Injection
- XSS
- Buffer overflow
- 60% das vulnerabilidades OWASP

### Regra

TODO input externo DEVE ser validado com schemas:

- **Python**: Pydantic models
- **TypeScript**: Zod schemas
- **Validação**: Tipo, formato, range, regex
- **Sanitização**: Automática após validação

### Justificativa

- Previne injeções (SQL, XSS, Command)
- Validação declarativa e testável
- Documentação automática

### Exemplos

#### ✅ Correto (Python + Pydantic)

```python
from pydantic import BaseModel, EmailStr, Field, validator
from typing import Optional

class UserCreate(BaseModel):
    email: EmailStr  # Valida formato de email
    nome: str = Field(..., min_length=3, max_length=100, regex=r'^[a-zA-Z\s]+$')
    idade: int = Field(..., ge=18, le=120)
    cpf: str = Field(..., regex=r'^\d{3}\.\d{3}\.\d{3}-\d{2}$')
    senha: str = Field(..., min_length=8)

    @validator('senha')
    def senha_forte(cls, v):
        if not any(c.isupper() for c in v):
            raise ValueError('Senha deve ter maiúscula')
        if not any(c.isdigit() for c in v):
            raise ValueError('Senha deve ter número')
        return v

    @validator('cpf')
    def cpf_valido(cls, v):
        # Validação de CPF
        if not validar_cpf(v):
            raise ValueError('CPF inválido')
        return v

@app.post("/users/")
async def create_user(user: UserCreate):
    # user já validado e sanitizado
    return await service.create(user)
```

#### ✅ Correto (TypeScript + Zod)

```typescript
import { z } from 'zod';

const UserCreateSchema = z.object({
  email: z.string().email(),
  nome: z.string().min(3).max(100).regex(/^[a-zA-Z\s]+$/),
  idade: z.number().int().min(18).max(120),
  cpf: z.string().regex(/^\d{3}\.\d{3}\.\d{3}-\d{2}$/),
  senha: z.string().min(8)
    .refine(s => /[A-Z]/.test(s), 'Senha deve ter maiúscula')
    .refine(s => /\d/.test(s), 'Senha deve ter número')
});

type UserCreate = z.infer<typeof UserCreateSchema>;

app.post('/users', async (req, res) => {
  const user = UserCreateSchema.parse(req.body); // Valida ou lança erro
  const created = await service.create(user);
  res.json(created);
});
```

#### ❌ Incorreto

```python
@app.post("/users/")
async def create_user(request: Request):
    data = await request.json()
    email = data['email']  # Sem validação!
    nome = data['nome']    # Pode ser SQL injection

    # Vulnerável a SQL injection
    query = f"INSERT INTO users (email, nome) VALUES ('{email}', '{nome}')"
```

### Ferramentas

- **Pydantic**: Validação Python
- **Zod**: Validação TypeScript
- **OWASP Input Validation Cheat Sheet**

### Checklist

- [ ] Todos os inputs validados com schemas?
- [ ] Validação de tipo, formato, range?
- [ ] Regex para formatos específicos?
- [ ] Validadores customizados para regras de negócio?

---

## Rule 3.3: Secrets em Variáveis de Ambiente

### Contexto

Secrets hardcoded causam:

- Vazamento em repositórios Git
- Impossível rotacionar credenciais
- Violação de compliance

### Regra

Secrets NUNCA no código:

- **Variáveis de ambiente**: `.env` (gitignored)
- **Produção**: AWS Secrets Manager, HashiCorp Vault
- **Rotação**: Automática a cada 90 dias
- **Validação**: Pre-commit hook detecta secrets

### Justificativa

- Zero secrets em Git
- Rotação sem deploy
- Auditoria centralizada

### Exemplos

#### ✅ Correto

```python
# settings.py
from pydantic import BaseSettings, SecretStr

class Settings(BaseSettings):
    database_url: SecretStr
    jwt_secret_key: SecretStr
    aws_access_key_id: SecretStr
    aws_secret_access_key: SecretStr
    sendgrid_api_key: SecretStr

    class Config:
        env_file = ".env"
        env_file_encoding = "utf-8"

settings = Settings()

# Uso
db_url = settings.database_url.get_secret_value()
```

```bash
# .env (gitignored)
DATABASE_URL=postgresql://user:pass@localhost/db
JWT_SECRET_KEY=super-secret-key-change-in-production
AWS_ACCESS_KEY_ID=AKIAIOSFODNN7EXAMPLE
AWS_SECRET_ACCESS_KEY=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
SENDGRID_API_KEY=SG.xxxxxxxxxxxxxxxxxxxxx
```

```yaml
# .gitignore
.env
.env.local
.env.*.local
secrets/
```

#### ❌ Incorreto

```python
# NUNCA FAZER ISSO!
DATABASE_URL = "postgresql://admin:senha123@prod.db.com/app"
JWT_SECRET = "meu-secret-super-secreto"
AWS_KEY = "AKIAIOSFODNN7EXAMPLE"
```

### Ferramentas

- **detect-secrets**: Pre-commit hook
- **git-secrets**: Previne commits com secrets
- **AWS Secrets Manager**: Produção
- **HashiCorp Vault**: Enterprise

### Checklist

- [ ] Nenhum secret hardcoded?
- [ ] `.env` no `.gitignore`?
- [ ] Pre-commit hook configurado?
- [ ] Secrets Manager em produção?

---

## Rule 3.4: Rate Limiting em APIs Públicas

### Contexto

APIs sem rate limiting sofrem:

- DDoS attacks
- Abuso de recursos
- Custos descontrolados
- Degradação de serviço

### Regra

APIs públicas DEVEM ter rate limiting:

- **Anônimos**: 10 req/min
- **Autenticados**: 100 req/min
- **Premium**: 1000 req/min
- **Headers**: `X-RateLimit-*` informativos
- **Resposta**: 429 Too Many Requests

### Justificativa

- Previne abuso
- Protege infraestrutura
- Monetização por tier

### Exemplos

#### ✅ Correto (FastAPI + Redis)

```python
from fastapi import FastAPI, Request, HTTPException
from fastapi_limiter import FastAPILimiter
from fastapi_limiter.depends import RateLimiter
import redis.asyncio as redis

app = FastAPI()

@app.on_event("startup")
async def startup():
    redis_client = await redis.from_url("redis://localhost")
    await FastAPILimiter.init(redis_client)

@app.get(
    "/api/v1/public/data",
    dependencies=[RateLimiter(times=10, seconds=60)]  # 10 req/min
)
async def get_public_data():
    return {"data": "public"}

@app.get(
    "/api/v1/users/me",
    dependencies=[RateLimiter(times=100, seconds=60)]  # 100 req/min
)
async def get_user_data(current_user: User = Depends(get_current_user)):
    return current_user
```

```python
# Rate limiter customizado com tiers
class TieredRateLimiter:
    LIMITS = {
        "anonymous": (10, 60),      # 10/min
        "basic": (100, 60),         # 100/min
        "premium": (1000, 60),      # 1000/min
        "enterprise": (10000, 60)   # 10000/min
    }

    async def __call__(self, request: Request):
        user = await get_current_user_optional(request)
        tier = user.tier if user else "anonymous"

        times, seconds = self.LIMITS[tier]
        key = f"rate_limit:{tier}:{user.id if user else request.client.host}"

        current = await redis.incr(key)
        if current == 1:
            await redis.expire(key, seconds)

        if current > times:
            raise HTTPException(
                status_code=429,
                detail="Rate limit exceeded",
                headers={
                    "X-RateLimit-Limit": str(times),
                    "X-RateLimit-Remaining": "0",
                    "X-RateLimit-Reset": str(await redis.ttl(key))
                }
            )
```

#### ❌ Incorreto

```python
# Sem rate limiting
@app.get("/api/v1/data")
async def get_data():
    return expensive_operation()  # Vulnerável a abuso
```

### Ferramentas

- **fastapi-limiter**: Rate limiting para FastAPI
- **express-rate-limit**: Node.js/Express
- **Redis**: Storage de contadores
- **CloudFlare**: Rate limiting em edge

### Checklist

- [ ] Rate limiting configurado?
- [ ] Limites por tier de usuário?
- [ ] Headers informativos?
- [ ] Resposta 429 adequada?

---

## Rule 3.5: SQL Injection Prevention

### Contexto

SQL Injection é:

- #1 em OWASP Top 10
- Permite roubo de dados
- Permite destruição de BD

### Regra

NUNCA concatenar SQL:

- **ORM**: SQLAlchemy, Prisma (preferencial)
- **Prepared Statements**: Se SQL raw necessário
- **Validação**: Pydantic/Zod antes de query
- **Least Privilege**: User BD com mínimos privilégios

### Justificativa

- Previne 100% SQL injection
- Código mais legível
- Performance (query plan cache)

### Exemplos

#### ✅ Correto (SQLAlchemy ORM)

```python
from sqlalchemy import select

# ORM - seguro
async def find_user_by_email(email: str) -> Optional[User]:
    stmt = select(UserModel).where(UserModel.email == email)
    result = await session.execute(stmt)
    return result.scalar_one_or_none()

# Prepared statement - seguro
async def find_users_by_status(status: str) -> List[User]:
    query = "SELECT * FROM users WHERE status = :status"
    result = await session.execute(text(query), {"status": status})
    return result.fetchall()
```

#### ❌ Incorreto

```python
# SQL injection vulnerável!
async def find_user_by_email(email: str):
    query = f"SELECT * FROM users WHERE email = '{email}'"
    # Ataque: email = "' OR '1'='1"
    # Query: SELECT * FROM users WHERE email = '' OR '1'='1'
    # Retorna TODOS os usuários!
    result = await session.execute(query)
    return result.fetchall()

# Também vulnerável
async def delete_user(user_id: str):
    query = f"DELETE FROM users WHERE id = {user_id}"
    # Ataque: user_id = "1; DROP TABLE users; --"
    await session.execute(query)
```

### Ferramentas

- **SQLAlchemy**: ORM Python
- **Prisma**: ORM TypeScript
- **Bandit**: Detecta SQL injection em Python
- **SQLMap**: Testa vulnerabilidades

### Checklist

- [ ] Usando ORM?
- [ ] Se SQL raw, prepared statements?
- [ ] Validação de input antes de query?
- [ ] User BD com least privilege?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| JWT com refresh tokens | 100% | Code review |
| Input validation | 100% endpoints | Pydantic/Zod coverage |
| Secrets em env vars | 100% | detect-secrets |
| Rate limiting | 100% APIs públicas | Config review |
| SQL injection prevention | 100% | Bandit, code review |

---

**Próxima revisão:** 2026-01-19
