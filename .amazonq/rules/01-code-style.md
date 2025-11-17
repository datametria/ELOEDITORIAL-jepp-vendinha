# Code Style: Padrões DATAMETRIA

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 1.1: Naming Conventions por Linguagem

### Contexto

Naming inconsistente causa:

- 40% mais tempo em code review
- Confusão entre tipos (classe vs função)
- Dificuldade de busca no código

### Regra

**Python:**

- `snake_case`: funções, variáveis, módulos
- `PascalCase`: classes
- `UPPER_SNAKE_CASE`: constantes
- `_prefixo`: métodos/atributos privados

**JavaScript/TypeScript:**

- `camelCase`: funções, variáveis
- `PascalCase`: classes, componentes React/Vue
- `UPPER_SNAKE_CASE`: constantes
- `kebab-case`: arquivos de componentes

**Dart/Flutter:**

- `camelCase`: variáveis, funções
- `PascalCase`: classes, enums, widgets
- `lowerCamelCase`: arquivos
- `kPrefixo`: constantes (convenção Flutter)

**Vue.js 3:**

- `PascalCase`: componentes SFC (Single File Components)
- `camelCase`: props, emits, computed, methods
- `kebab-case`: eventos customizados, slots
- `UPPER_SNAKE_CASE`: constantes
- Arquivos: `PascalCase.vue` ou `kebab-case.vue`

**React Native:**

- `PascalCase`: componentes, interfaces, types
- `camelCase`: funções, variáveis, hooks
- `UPPER_SNAKE_CASE`: constantes
- `use` prefixo: custom hooks (useState, useEffect)
- Arquivos: `PascalCase.tsx` para componentes

**Java:**

- `camelCase`: variáveis, métodos
- `PascalCase`: classes, interfaces, enums
- `UPPER_SNAKE_CASE`: constantes
- Packages: `lowercase.sem.separador`

**SQL:**

- `snake_case`: tabelas, colunas, funções
- `UPPER_CASE`: palavras-chave SQL (SELECT, FROM)
- Evitar palavras reservadas

### Justificativa

- Reconhecimento visual imediato de tipos
- Conformidade com style guides oficiais:
  - Python: PEP 8
  - JavaScript/TypeScript: Airbnb Style Guide
  - Dart: Effective Dart
  - Vue.js: Vue Style Guide (Official)
  - React Native: React/TypeScript conventions
  - Java: Oracle Code Conventions
- Autocomplete e refactoring funcionam melhor
- Interoperabilidade entre linguagens facilitada

### Exemplos

#### ✅ Correto (Python)

```python
# Constantes
MAX_RETRY_ATTEMPTS = 3
DATABASE_URL = "postgresql://..."

# Classes
class UserRepository:
    def __init__(self):
        self._connection = None  # Privado

    def find_by_email(self, email: str) -> Optional[User]:
        pass

# Funções e variáveis
def calculate_total_price(items: List[Item]) -> Decimal:
    total_amount = Decimal("0.00")
    return total_amount
```

#### ❌ Incorreto (Python)

```python
maxRetryAttempts = 3  # Deveria ser UPPER_SNAKE_CASE
class userRepository:  # Deveria ser PascalCase
    def FindByEmail(self, Email):  # Deveria ser snake_case
        pass
```

#### ✅ Correto (TypeScript)

```typescript
// Constantes
const MAX_RETRY_ATTEMPTS = 3;

// Classes e Interfaces
class UserRepository {
  private connection: Connection;

  findByEmail(email: string): Promise<User | null> {
    return null;
  }
}

// Componentes React
export const UserProfile: React.FC<UserProfileProps> = ({ user }) => {
  const [isLoading, setIsLoading] = useState(false);
  return <div>{user.name}</div>;
};
```

#### ✅ Correto (Vue.js 3)

```vue
<!-- UserProfile.vue -->
<script setup lang="ts">
import { ref, computed } from 'vue';

// Constantes
const MAX_RETRY_ATTEMPTS = 3;

// Props
interface Props {
  user: User;
  isActive?: boolean;
}

const props = withDefaults(defineProps<Props>(), {
  isActive: true
});

// Emits
const emit = defineEmits<{
  'user-updated': [user: User];
  'profile-clicked': [];
}>();

// State e computed
const isLoading = ref(false);
const displayName = computed(() => props.user.name);

// Methods
function handleClick() {
  emit('profile-clicked');
}
</script>

<template>
  <div class="user-profile">
    <h2>{{ displayName }}</h2>
    <slot name="actions" />
  </div>
</template>
```

#### ✅ Correto (React Native)

```typescript
// UserProfile.tsx
import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet } from 'react-native';

// Constantes
const MAX_RETRY_ATTEMPTS = 3;
const DEFAULT_PADDING = 16;

// Interface
interface UserProfileProps {
  user: User;
  onPress?: () => void;
}

// Custom Hook
function useUserData(userId: string) {
  const [userData, setUserData] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    // Fetch user data
  }, [userId]);

  return { userData, isLoading };
}

// Componente
export const UserProfile: React.FC<UserProfileProps> = ({ user, onPress }) => {
  const [isExpanded, setIsExpanded] = useState(false);

  return (
    <View style={styles.container}>
      <Text style={styles.name}>{user.name}</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: DEFAULT_PADDING,
  },
  name: {
    fontSize: 18,
  },
});
```

#### ✅ Correto (Dart/Flutter)

```dart
// Constantes
const kDefaultPadding = 16.0;
const MAX_RETRY_ATTEMPTS = 3;

// Classes e Widgets
class UserRepository {
  final DatabaseConnection _connection; // Privado

  Future<User?> findByEmail(String email) async {
    return null;
  }
}

// Widget Flutter
class UserProfile extends StatelessWidget {
  final User user;

  const UserProfile({Key? key, required this.user}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Text(user.name);
  }
}
```

#### ✅ Correto (Java)

```java
// Constantes
public static final int MAX_RETRY_ATTEMPTS = 3;
public static final String DATABASE_URL = "jdbc:postgresql://...";

// Classes e Interfaces
public class UserRepository {
    private Connection connection;

    public Optional<User> findByEmail(String email) {
        return Optional.empty();
    }
}

// Variáveis e métodos
public BigDecimal calculateTotalPrice(List<Item> items) {
    BigDecimal totalAmount = BigDecimal.ZERO;
    return totalAmount;
}
```

### Ferramentas

- **Python**: `pylint`, `flake8`, `black`
- **JavaScript/TypeScript**: `eslint`, `prettier`
- **Dart**: `dart analyze`, `dart format`
- **Vue.js**: `eslint-plugin-vue`, `prettier`
- **React Native**: `eslint`, `prettier`, `@typescript-eslint`
- **Java**: `checkstyle`, `spotless`

### Checklist

- [ ] Variáveis e funções em snake_case/camelCase?
- [ ] Classes em PascalCase?
- [ ] Constantes em UPPER_SNAKE_CASE?
- [ ] Privados com prefixo `_`?

---

## Rule 1.2: Formatação Automática

### Contexto

Formatação manual causa:

- 30min/dia em discussões de estilo
- Diffs poluídos em PRs
- Inconsistência entre desenvolvedores

### Regra

TODO código DEVE ser formatado automaticamente antes de commit:

- **Python**: Black (line-length=100)
- **JavaScript/TypeScript/Vue.js/React Native**: Prettier
- **Dart/Flutter**: dart format
- **Java**: Spotless ou Google Java Format
- **Pre-commit hooks**: Obrigatórios

### Justificativa

- Zero discussões sobre estilo
- Diffs limpos focados em lógica
- Conformidade 100% automática

### Exemplos

#### ✅ Correto (Configuração)

```toml
# pyproject.toml
[tool.black]
line-length = 100
target-version = ['py311']
include = '\.pyi?$'
```

```json
// .prettierrc
{
  "semi": true,
  "singleQuote": true,
  "printWidth": 100,
  "tabWidth": 2
}
```

```yaml
# .pre-commit-config.yaml
repos:
  - repo: https://github.com/psf/black
    rev: 23.9.1
    hooks:
      - id: black

  - repo: https://github.com/pre-commit/mirrors-prettier
    rev: v3.0.3
    hooks:
      - id: prettier
        types_or: [javascript, jsx, ts, tsx, vue]
```

```xml
<!-- pom.xml (Java) -->
<plugin>
  <groupId>com.diffplug.spotless</groupId>
  <artifactId>spotless-maven-plugin</artifactId>
  <configuration>
    <java>
      <googleJavaFormat>
        <version>1.17.0</version>
      </googleJavaFormat>
    </java>
  </configuration>
</plugin>
```

### Ferramentas

- **Black**: `black src/`
- **Prettier**: `prettier --write "**/*.{js,jsx,ts,tsx,vue}"`
- **Dart Format**: `dart format lib/`
- **Spotless**: `mvn spotless:apply`
- **Pre-commit**: `pre-commit install`

### Checklist

- [ ] Formatter configurado?
- [ ] Pre-commit hooks instalados?
- [ ] CI/CD valida formatação?

---

## Rule 1.3: Imports Organization

### Contexto

Imports desorganizados causam:

- Dificuldade de encontrar dependências
- Imports duplicados
- Conflitos de merge desnecessários

### Regra

Imports DEVEM seguir ordem:

1. Standard library
2. Third-party
3. Local/projeto
4. Linha em branco entre grupos
5. Ordem alfabética dentro de cada grupo

### Exemplos

#### ✅ Correto (Python)

```python
# Standard library
import os
import sys
from datetime import datetime
from typing import Dict, List, Optional

# Third-party
import boto3
import redis
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel

# Local
from src.config import settings
from src.models.user import User
from src.repositories.user_repository import UserRepository
```

#### ❌ Incorreto (Python)

```python
from src.models.user import User
import os
from fastapi import FastAPI
import sys
from src.config import settings
import boto3
```

### Ferramentas

- **Python**: `isort`
- **JavaScript/TypeScript/Vue.js/React Native**: `eslint-plugin-import`
- **Dart**: Organização automática com `dart format`
- **Java**: Organização automática com IDE (IntelliJ, Eclipse)

### Checklist

- [ ] Imports agrupados corretamente?
- [ ] Ordem alfabética dentro de grupos?
- [ ] Linhas em branco entre grupos?

---

## Rule 1.4: Funções Máximo 50 Linhas

### Contexto

Funções longas causam:

- Dificuldade de entender lógica
- Impossível testar adequadamente
- Violação de Single Responsibility

### Regra

Funções DEVEM ter máximo 50 linhas (excluindo docstrings).
Se exceder: refatorar em funções menores.

### Justificativa

- Funções pequenas são fáceis de testar
- Código mais legível e manutenível
- Facilita reuso

### Exemplos

#### ✅ Correto

```python
def processar_pedido(pedido_id: str) -> Dict[str, Any]:
    """Processa pedido completo."""
    pedido = _validar_pedido(pedido_id)
    pagamento = _processar_pagamento(pedido)
    estoque = _atualizar_estoque(pedido)
    notificacao = _enviar_notificacao(pedido, pagamento)

    return {
        "pedido_id": pedido_id,
        "status": "processado",
        "pagamento": pagamento,
        "estoque": estoque
    }

def _validar_pedido(pedido_id: str) -> Pedido:
    """Valida existência e status do pedido."""
    pedido = repository.find(pedido_id)
    if not pedido:
        raise PedidoNaoEncontrado(pedido_id)
    if pedido.status != "pendente":
        raise PedidoInvalido("Pedido já processado")
    return pedido
```

#### ❌ Incorreto

```python
def processar_pedido(pedido_id: str) -> Dict[str, Any]:
    """Processa pedido completo."""
    # 150 linhas de código misturando:
    # - Validação
    # - Processamento de pagamento
    # - Atualização de estoque
    # - Envio de notificações
    # - Logging
    # - Tratamento de erros
    pass  # Função gigante
```

### Ferramentas

- **Radon**: `radon cc src/ -a -nb`
- **Pylint**: `pylint --max-function-lines=50`

### Checklist

- [ ] Função tem < 50 linhas?
- [ ] Responsabilidade única?
- [ ] Funções auxiliares extraídas?

---

## Rule 1.5: Máximo 3 Parâmetros

### Contexto

Muitos parâmetros causam:

- Explosão combinatória em testes
- Erros de ordem de argumentos
- Dificuldade de uso

### Regra

Funções DEVEM ter máximo 3 parâmetros.
Se precisar mais: usar objeto de configuração, dataclass ou builder pattern.

### Justificativa

- Testes mais simples
- Menos erros de uso
- Código mais legível

### Exemplos

#### ✅ Correto

```python
from dataclasses import dataclass
from typing import Optional

@dataclass
class EmailConfig:
    destinatario: str
    assunto: str
    corpo: str
    anexos: Optional[List[str]] = None
    prioridade: str = "normal"
    cc: Optional[List[str]] = None
    bcc: Optional[List[str]] = None

def enviar_email(config: EmailConfig) -> bool:
    """Envia email com configuração."""
    # Implementação
    pass

# Uso
config = EmailConfig(
    destinatario="user@example.com",
    assunto="Bem-vindo",
    corpo="Olá!",
    prioridade="alta"
)
enviar_email(config)
```

#### ❌ Incorreto

```python
def enviar_email(
    destinatario: str,
    assunto: str,
    corpo: str,
    anexos: List[str],
    prioridade: str,
    cc: List[str],
    bcc: List[str],
    reply_to: str
) -> bool:
    """8 parâmetros - muito complexo!"""
    pass
```

### Ferramentas

- **Pylint**: `pylint --max-args=3`
- **ESLint**: `max-params: 3`

### Checklist

- [ ] Função tem ≤ 3 parâmetros?
- [ ] Se > 3, usa dataclass/interface?
- [ ] Parâmetros têm defaults quando apropriado?

---

## Métricas de Conformidade

| Métrica | Meta | Comando |
|---------|------|---------|
| Naming conventions | 100% | `pylint --disable=all --enable=invalid-name` |
| Formatação | 100% | `black --check src/` |
| Imports organizados | 100% | `isort --check-only src/` |
| Funções < 50 linhas | 95% | `radon cc src/ -a -nb` |
| Parâmetros ≤ 3 | 90% | `pylint --max-args=3` |

---

**Próxima revisão:** 2026-01-19
