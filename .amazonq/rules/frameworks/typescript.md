# TypeScript Framework Rules - DATAMETRIA

**Versão:** 1.0  
**Data:** 12/11/2025  
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule TS.1: TypeScript Strict Mode Obrigatório

### Contexto

TypeScript sem strict mode causa:
- Bugs silenciosos (null/undefined não detectados)
- Type safety comprometido (any implícito)
- Refatoração arriscada

### Regra

TODO projeto TypeScript DEVE usar strict mode:
- `strict: true` no tsconfig.json
- `noImplicitAny: true`
- `strictNullChecks: true`
- `strictFunctionTypes: true`
- `noUnusedLocals: true`
- `noUnusedParameters: true`

### Justificativa

- Detecta 70% mais bugs em compile time
- Type safety completo
- Refatoração segura

### Exemplos

#### ✅ Correto (tsconfig.json)

```json
{
  "compilerOptions": {
    "target": "ES2022",
    "module": "ESNext",
    "lib": ["ES2022"],
    "moduleResolution": "bundler",
    "strict": true,
    "noImplicitAny": true,
    "strictNullChecks": true,
    "strictFunctionTypes": true,
    "strictBindCallApply": true,
    "strictPropertyInitialization": true,
    "noImplicitThis": true,
    "alwaysStrict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noImplicitReturns": true,
    "noFallthroughCasesInSwitch": true,
    "esModuleInterop": true,
    "skipLibCheck": true,
    "forceConsistentCasingInFileNames": true,
    "resolveJsonModule": true
  },
  "include": ["src/**/*"],
  "exclude": ["node_modules", "dist", "**/*.spec.ts"]
}
```

#### ✅ Correto (Código)

```typescript
// Type safety completo
interface User {
  id: string;
  email: string;
  name: string;
  age?: number; // Opcional explícito
}

function getUser(id: string): User | null {
  // Retorno explícito com null
  const user = findUserById(id);
  return user ?? null;
}

// Uso com null check
const user = getUser("123");
if (user !== null) {
  console.log(user.name); // Safe access
}

// Async com tipos
async function fetchUser(id: string): Promise<User> {
  const response = await fetch(`/api/users/${id}`);
  if (!response.ok) {
    throw new Error(`User not found: ${id}`);
  }
  return response.json() as User;
}
```

#### ❌ Incorreto

```typescript
// Sem strict mode
function getUser(id) { // any implícito
  return findUserById(id);
}

const user = getUser("123");
console.log(user.name); // Pode crashar se null
```

### Ferramentas

- **TypeScript Compiler**: `tsc --noEmit` para validação
- **ESLint**: `@typescript-eslint/eslint-plugin`

### Checklist

- [ ] `strict: true` no tsconfig.json?
- [ ] Sem `any` implícito?
- [ ] Null checks explícitos?
- [ ] Tipos de retorno declarados?

---

## Rule TS.2: ESLint + Prettier Obrigatórios

### Contexto

Código sem linting causa:
- Inconsistência de estilo
- Bugs comuns não detectados
- Code review focado em estilo

### Regra

TODO projeto TypeScript DEVE ter:
- ESLint configurado com `@typescript-eslint`
- Prettier para formatação
- Pre-commit hooks (husky + lint-staged)
- CI/CD validação

### Justificativa

- Estilo consistente automático
- Detecta bugs comuns
- Code review focado em lógica

### Exemplos

#### ✅ Correto (.eslintrc.json)

```json
{
  "parser": "@typescript-eslint/parser",
  "parserOptions": {
    "ecmaVersion": 2022,
    "sourceType": "module",
    "project": "./tsconfig.json"
  },
  "plugins": ["@typescript-eslint"],
  "extends": [
    "eslint:recommended",
    "plugin:@typescript-eslint/recommended",
    "plugin:@typescript-eslint/recommended-requiring-type-checking",
    "prettier"
  ],
  "rules": {
    "@typescript-eslint/no-explicit-any": "error",
    "@typescript-eslint/explicit-function-return-type": "warn",
    "@typescript-eslint/no-unused-vars": ["error", { "argsIgnorePattern": "^_" }],
    "@typescript-eslint/no-floating-promises": "error",
    "@typescript-eslint/await-thenable": "error",
    "no-console": ["warn", { "allow": ["warn", "error"] }],
    "max-params": ["error", 3],
    "max-lines-per-function": ["warn", 50]
  }
}
```

#### ✅ Correto (.prettierrc)

```json
{
  "semi": true,
  "singleQuote": true,
  "printWidth": 100,
  "tabWidth": 2,
  "trailingComma": "es5",
  "arrowParens": "always"
}
```

#### ✅ Correto (package.json)

```json
{
  "scripts": {
    "lint": "eslint src/**/*.ts",
    "lint:fix": "eslint src/**/*.ts --fix",
    "format": "prettier --write \"src/**/*.ts\"",
    "format:check": "prettier --check \"src/**/*.ts\"",
    "type-check": "tsc --noEmit"
  },
  "devDependencies": {
    "@typescript-eslint/eslint-plugin": "^6.0.0",
    "@typescript-eslint/parser": "^6.0.0",
    "eslint": "^8.50.0",
    "eslint-config-prettier": "^9.0.0",
    "prettier": "^3.0.0",
    "husky": "^8.0.0",
    "lint-staged": "^14.0.0"
  },
  "lint-staged": {
    "*.ts": [
      "eslint --fix",
      "prettier --write"
    ]
  }
}
```

### Ferramentas

- **ESLint**: Linting
- **Prettier**: Formatação
- **Husky**: Git hooks
- **lint-staged**: Pre-commit

### Checklist

- [ ] ESLint configurado?
- [ ] Prettier configurado?
- [ ] Pre-commit hooks instalados?
- [ ] CI/CD valida lint?

---

## Rule TS.3: Interfaces e Types Explícitos

### Contexto

Código sem tipos explícitos causa:
- Inferência incorreta
- Contratos de API não documentados
- Refatoração arriscada

### Regra

TODO código TypeScript DEVE ter:
- Interfaces para objetos complexos
- Types para unions e aliases
- Tipos de retorno explícitos em funções
- Generics quando apropriado

### Justificativa

- Documentação automática
- Type safety completo
- Autocomplete funciona

### Exemplos

#### ✅ Correto

```typescript
// Interfaces para objetos
interface User {
  id: string;
  email: string;
  name: string;
  role: UserRole;
  createdAt: Date;
}

// Types para unions
type UserRole = 'admin' | 'developer' | 'viewer';
type ApiResponse<T> = {
  data: T;
  status: number;
  message: string;
};

// Tipos de retorno explícitos
function createUser(email: string, name: string): User {
  return {
    id: generateId(),
    email,
    name,
    role: 'developer',
    createdAt: new Date(),
  };
}

// Async com tipos
async function fetchUser(id: string): Promise<User> {
  const response = await fetch(`/api/users/${id}`);
  const data: ApiResponse<User> = await response.json();
  return data.data;
}

// Generics
function createApiClient<T>(baseUrl: string): ApiClient<T> {
  return {
    get: async (path: string): Promise<T> => {
      const response = await fetch(`${baseUrl}${path}`);
      return response.json() as T;
    },
  };
}

// Classes com tipos
class UserService {
  private readonly apiClient: ApiClient<User>;

  constructor(apiClient: ApiClient<User>) {
    this.apiClient = apiClient;
  }

  async getUser(id: string): Promise<User> {
    return this.apiClient.get(`/users/${id}`);
  }

  async listUsers(filters?: UserFilters): Promise<User[]> {
    const query = filters ? `?${new URLSearchParams(filters).toString()}` : '';
    return this.apiClient.get(`/users${query}`);
  }
}
```

#### ❌ Incorreto

```typescript
// Sem tipos explícitos
function createUser(email, name) {
  return {
    id: generateId(),
    email,
    name,
    role: 'developer',
  };
}

// Inferência pode falhar
async function fetchUser(id) {
  const response = await fetch(`/api/users/${id}`);
  return response.json(); // any
}
```

### Ferramentas

- **TypeScript**: Type checking
- **ESLint**: `@typescript-eslint/explicit-function-return-type`

### Checklist

- [ ] Interfaces para objetos complexos?
- [ ] Tipos de retorno explícitos?
- [ ] Generics quando apropriado?
- [ ] Sem `any` desnecessário?

---

## Rule TS.4: Async/Await e Error Handling

### Contexto

Promises sem tratamento causam:
- Erros silenciosos
- Unhandled promise rejections
- Debugging difícil

### Regra

TODO código assíncrono DEVE:
- Usar async/await (não callbacks)
- Try/catch para error handling
- Tipos de erro específicos
- Logging de erros

### Justificativa

- Código mais legível
- Erros tratados explicitamente
- Stack traces úteis

### Exemplos

#### ✅ Correto

```typescript
// Custom error types
class ApiError extends Error {
  constructor(
    message: string,
    public statusCode: number,
    public response?: unknown
  ) {
    super(message);
    this.name = 'ApiError';
  }
}

class NotFoundError extends ApiError {
  constructor(resource: string, id: string) {
    super(`${resource} not found: ${id}`, 404);
    this.name = 'NotFoundError';
  }
}

// Async com error handling
async function fetchUser(id: string): Promise<User> {
  try {
    const response = await fetch(`/api/users/${id}`);

    if (!response.ok) {
      if (response.status === 404) {
        throw new NotFoundError('User', id);
      }
      throw new ApiError(
        `Failed to fetch user: ${response.statusText}`,
        response.status
      );
    }

    const data: ApiResponse<User> = await response.json();
    return data.data;
  } catch (error) {
    if (error instanceof ApiError) {
      console.error(`API Error: ${error.message}`, { statusCode: error.statusCode });
      throw error;
    }
    console.error('Unexpected error fetching user:', error);
    throw new ApiError('Internal server error', 500);
  }
}

// Promise.all para paralelismo
async function fetchUserWithProjects(userId: string): Promise<{
  user: User;
  projects: Project[];
}> {
  try {
    const [user, projects] = await Promise.all([
      fetchUser(userId),
      fetchUserProjects(userId),
    ]);

    return { user, projects };
  } catch (error) {
    console.error('Error fetching user data:', error);
    throw error;
  }
}

// Retry logic
async function fetchWithRetry<T>(
  fn: () => Promise<T>,
  maxRetries: number = 3
): Promise<T> {
  let lastError: Error;

  for (let i = 0; i < maxRetries; i++) {
    try {
      return await fn();
    } catch (error) {
      lastError = error as Error;
      console.warn(`Attempt ${i + 1} failed, retrying...`);
      await new Promise((resolve) => setTimeout(resolve, 1000 * (i + 1)));
    }
  }

  throw lastError!;
}
```

#### ❌ Incorreto

```typescript
// Sem error handling
async function fetchUser(id: string) {
  const response = await fetch(`/api/users/${id}`);
  return response.json(); // Pode falhar silenciosamente
}

// Promises sem await
function fetchUser(id: string) {
  return fetch(`/api/users/${id}`)
    .then((response) => response.json())
    .catch((error) => console.error(error)); // Callback hell
}
```

### Ferramentas

- **ESLint**: `@typescript-eslint/no-floating-promises`
- **ESLint**: `@typescript-eslint/await-thenable`

### Checklist

- [ ] Async/await usado?
- [ ] Try/catch para erros?
- [ ] Tipos de erro customizados?
- [ ] Logging de erros?

---

## Rule TS.5: Dependency Injection e Testabilidade

### Contexto

Dependências hardcoded causam:
- Testes impossíveis
- Acoplamento forte
- Mocking difícil

### Regra

Classes DEVEM:
- Receber dependências via construtor
- Usar interfaces para abstrações
- Evitar `new` dentro de métodos
- Facilitar mocking em testes

### Justificativa

- Testes unitários triviais
- Acoplamento baixo
- Mocking fácil

### Exemplos

#### ✅ Correto

```typescript
// Interfaces para abstrações
interface ApiClient {
  get<T>(path: string): Promise<T>;
  post<T>(path: string, data: unknown): Promise<T>;
}

interface Logger {
  info(message: string, meta?: unknown): void;
  error(message: string, error?: Error): void;
}

// Dependency Injection
class UserService {
  constructor(
    private readonly apiClient: ApiClient,
    private readonly logger: Logger
  ) {}

  async getUser(id: string): Promise<User> {
    this.logger.info('Fetching user', { id });

    try {
      const user = await this.apiClient.get<User>(`/users/${id}`);
      this.logger.info('User fetched successfully', { id });
      return user;
    } catch (error) {
      this.logger.error('Failed to fetch user', error as Error);
      throw error;
    }
  }

  async createUser(data: CreateUserDto): Promise<User> {
    this.logger.info('Creating user', { email: data.email });

    try {
      const user = await this.apiClient.post<User>('/users', data);
      this.logger.info('User created successfully', { id: user.id });
      return user;
    } catch (error) {
      this.logger.error('Failed to create user', error as Error);
      throw error;
    }
  }
}

// Teste com mocks
describe('UserService', () => {
  let userService: UserService;
  let mockApiClient: jest.Mocked<ApiClient>;
  let mockLogger: jest.Mocked<Logger>;

  beforeEach(() => {
    mockApiClient = {
      get: jest.fn(),
      post: jest.fn(),
    };

    mockLogger = {
      info: jest.fn(),
      error: jest.fn(),
    };

    userService = new UserService(mockApiClient, mockLogger);
  });

  it('should fetch user successfully', async () => {
    const mockUser: User = {
      id: '123',
      email: 'test@example.com',
      name: 'Test User',
      role: 'developer',
      createdAt: new Date(),
    };

    mockApiClient.get.mockResolvedValue(mockUser);

    const result = await userService.getUser('123');

    expect(result).toEqual(mockUser);
    expect(mockApiClient.get).toHaveBeenCalledWith('/users/123');
    expect(mockLogger.info).toHaveBeenCalledWith('Fetching user', { id: '123' });
  });
});
```

#### ❌ Incorreto

```typescript
// Dependências hardcoded
class UserService {
  async getUser(id: string): Promise<User> {
    const apiClient = new ApiClient(); // Hardcoded!
    const logger = new Logger(); // Hardcoded!

    logger.info('Fetching user', { id });
    return apiClient.get<User>(`/users/${id}`);
  }
}

// Impossível testar sem API real
```

### Ferramentas

- **Jest**: Testing framework
- **ts-mockito**: Mocking library

### Checklist

- [ ] Dependências via construtor?
- [ ] Interfaces para abstrações?
- [ ] Sem `new` em métodos?
- [ ] Testes com mocks funcionam?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Strict mode | 100% | `tsc --noEmit` |
| ESLint | 0 errors | `eslint src/` |
| Prettier | 100% | `prettier --check` |
| Type coverage | > 95% | `type-coverage` |
| Test coverage | > 80% | `jest --coverage` |

---

**Próxima revisão:** 12/02/2026
