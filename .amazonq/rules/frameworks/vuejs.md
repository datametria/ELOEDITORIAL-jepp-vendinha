# Vue.js 3 Framework Rules

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Baseado em:** AmazonQ-Guidelines v2.0

---

## 🎯 Visão Geral

Este documento contém **5 rules específicas** para desenvolvimento com **Vue.js 3**, complementando as rules atômicas (01-06). Estas rules são aplicadas automaticamente pelo Amazon Q Developer quando Vue.js 3 é detectado no projeto.

### Aplicação

- **Quando:** Vue.js 3 detectado (package.json com "vue": "^3.x")
- **Complementa:** Rules atômicas 01-06
- **Prioridade:** Alta (aplicadas após rules atômicas)

---

## Rule V.1: Composition API Obrigatória

### Contexto

Vue.js 3 introduziu a Composition API como padrão recomendado, oferecendo melhor organização de lógica, reutilização de código e suporte a TypeScript. Projetos que ainda usam Options API enfrentam:

- **40% mais código** para mesma funcionalidade
- **Dificuldade de reutilização** de lógica entre componentes
- **TypeScript limitado** com inferência de tipos fraca
- **Manutenção complexa** em componentes grandes

### Regra

Todos os componentes Vue.js 3 devem usar **Composition API** com `<script setup>`:

1. Usar `<script setup>` para componentes
2. Usar `ref()` e `reactive()` para estado reativo
3. Usar `computed()` para valores derivados
4. Usar composables para lógica reutilizável
5. Evitar Options API (data, methods, computed)

### Justificativa

**Benefícios mensuráveis:**

- **40% menos código** comparado a Options API
- **TypeScript nativo** com inferência automática
- **Reutilização 3x maior** através de composables
- **Performance 15% melhor** (menos overhead)
- **Manutenção 50% mais rápida** (lógica agrupada)

### Exemplos

#### ✅ Correto

```vue
<script setup lang="ts">
import { ref, computed } from 'vue'
import { useAuth } from '@/composables/useAuth'

// Props com TypeScript
interface Props {
  userId: string
  showDetails?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  showDetails: false
})

// Emits tipados
const emit = defineEmits<{
  update: [userId: string]
  delete: [userId: string]
}>()

// Estado reativo
const user = ref<User | null>(null)
const loading = ref(false)

// Composable reutilizável
const { isAuthenticated, logout } = useAuth()

// Computed
const fullName = computed(() =>
  user.value ? `${user.value.firstName} ${user.value.lastName}` : ''
)

// Métodos
const fetchUser = async () => {
  loading.value = true
  try {
    user.value = await api.getUser(props.userId)
  } finally {
    loading.value = false
  }
}

// Lifecycle
onMounted(() => {
  fetchUser()
})
</script>

<template>
  <div v-if="loading">Carregando...</div>
  <div v-else-if="user">
    <h2>{{ fullName }}</h2>
    <p v-if="showDetails">{{ user.email }}</p>
  </div>
</template>
```

#### ❌ Incorreto

```vue
<script lang="ts">
import { defineComponent } from 'vue'

// Options API - NÃO USAR
export default defineComponent({
  props: {
    userId: String,
    showDetails: Boolean
  },
  data() {
    return {
      user: null,
      loading: false
    }
  },
  computed: {
    fullName() {
      return this.user
        ? `${this.user.firstName} ${this.user.lastName}`
        : ''
    }
  },
  methods: {
    async fetchUser() {
      this.loading = true
      try {
        this.user = await api.getUser(this.userId)
      } finally {
        this.loading = false
      }
    }
  },
  mounted() {
    this.fetchUser()
  }
})
</script>
```

### Ferramentas

- **ESLint**: `eslint-plugin-vue` com regra `vue/prefer-composition-api`
- **Volar**: Extension VS Code com suporte completo a `<script setup>`
- **Vue DevTools**: Inspeção de composables e estado reativo
- **TypeScript**: Inferência automática de tipos

### Checklist

- [ ] Componente usa `<script setup>`?
- [ ] Estado usa `ref()` ou `reactive()`?
- [ ] Lógica reutilizável está em composables?
- [ ] Props e emits estão tipados?
- [ ] Não há uso de Options API?

---

## Rule V.2: Pinia para State Management

### Contexto

Pinia é o state management oficial do Vue.js 3, substituindo Vuex. Projetos sem Pinia ou usando Vuex enfrentam:

- **Boilerplate 60% maior** (mutations, actions, getters)
- **TypeScript limitado** com tipagem manual
- **DevTools fragmentado** entre Vuex e Vue
- **Performance inferior** (mais overhead)

### Regra

Usar **Pinia** para gerenciamento de estado global:

1. Criar stores com `defineStore()`
2. Usar Composition API dentro das stores
3. Agrupar estado relacionado em stores específicas
4. Evitar estado global desnecessário (preferir props/provide)
5. Nunca usar Vuex em projetos novos

### Justificativa

**Benefícios mensuráveis:**

- **60% menos boilerplate** comparado a Vuex
- **TypeScript nativo** sem configuração
- **Performance 20% melhor** (menos overhead)
- **DevTools integrado** com Vue DevTools
- **Modular** (tree-shaking automático)

### Exemplos

#### ✅ Correto

```typescript
// stores/auth.ts
import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import type { User } from '@/types'

export const useAuthStore = defineStore('auth', () => {
  // Estado
  const user = ref<User | null>(null)
  const token = ref<string | null>(localStorage.getItem('token'))

  // Getters (computed)
  const isAuthenticated = computed(() => !!token.value)
  const userName = computed(() => user.value?.name ?? 'Guest')

  // Actions
  const login = async (email: string, password: string) => {
    const response = await api.login(email, password)
    token.value = response.token
    user.value = response.user
    localStorage.setItem('token', response.token)
  }

  const logout = () => {
    token.value = null
    user.value = null
    localStorage.removeItem('token')
  }

  const fetchUser = async () => {
    if (!token.value) return
    user.value = await api.getCurrentUser()
  }

  return {
    // Estado
    user,
    token,
    // Getters
    isAuthenticated,
    userName,
    // Actions
    login,
    logout,
    fetchUser
  }
})

// Uso no componente
<script setup lang="ts">
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()

const handleLogin = async () => {
  await authStore.login(email.value, password.value)
  router.push('/dashboard')
}
</script>

<template>
  <div v-if="authStore.isAuthenticated">
    Bem-vindo, {{ authStore.userName }}
  </div>
</template>
```

#### ❌ Incorreto

```typescript
// Vuex - NÃO USAR
import { createStore } from 'vuex'

export default createStore({
  state: {
    user: null,
    token: null
  },
  mutations: {
    SET_USER(state, user) {
      state.user = user
    },
    SET_TOKEN(state, token) {
      state.token = token
    }
  },
  actions: {
    async login({ commit }, { email, password }) {
      const response = await api.login(email, password)
      commit('SET_TOKEN', response.token)
      commit('SET_USER', response.user)
    }
  },
  getters: {
    isAuthenticated: state => !!state.token,
    userName: state => state.user?.name ?? 'Guest'
  }
})
```

### Ferramentas

- **Pinia**: `npm install pinia`
- **Pinia Plugin Persistedstate**: Persistência automática
- **Vue DevTools**: Inspeção de stores e time-travel
- **TypeScript**: Inferência automática de tipos

### Checklist

- [ ] Pinia instalado e configurado?
- [ ] Stores usam Composition API?
- [ ] Estado global está em stores específicas?
- [ ] TypeScript configurado corretamente?
- [ ] Não há uso de Vuex?

---

## Rule V.3: Vue Router com Guards Tipados

### Contexto

Vue Router 4 é essencial para SPAs, mas projetos sem guards tipados e estrutura adequada enfrentam:

- **Bugs de navegação** (40% dos bugs em SPAs)
- **Segurança fraca** (rotas protegidas sem validação)
- **TypeScript limitado** (rotas sem tipagem)
- **Manutenção difícil** (guards espalhados)

### Regra

Configurar **Vue Router 4** com guards tipados e estrutura modular:

1. Definir rotas com TypeScript
2. Usar navigation guards para autenticação
3. Implementar lazy loading de componentes
4. Agrupar rotas por feature
5. Usar meta fields para configuração

### Justificativa

**Benefícios mensuráveis:**

- **40% menos bugs** de navegação
- **Segurança 100%** em rotas protegidas
- **Performance 30% melhor** (lazy loading)
- **TypeScript completo** em rotas
- **Manutenção 50% mais fácil** (guards centralizados)

### Exemplos

#### ✅ Correto

```typescript
// router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

// Tipagem de meta fields
declare module 'vue-router' {
  interface RouteMeta {
    requiresAuth?: boolean
    roles?: string[]
    title?: string
  }
}

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/views/HomeView.vue'),
    meta: { title: 'Home' }
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: () => import('@/views/DashboardView.vue'),
    meta: {
      requiresAuth: true,
      title: 'Dashboard'
    }
  },
  {
    path: '/admin',
    name: 'admin',
    component: () => import('@/views/AdminView.vue'),
    meta: {
      requiresAuth: true,
      roles: ['admin'],
      title: 'Admin'
    }
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

// Navigation guard global
router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  // Atualizar título
  document.title = to.meta.title
    ? `${to.meta.title} - App`
    : 'App'

  // Verificar autenticação
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next({ name: 'login', query: { redirect: to.fullPath } })
    return
  }

  // Verificar roles
  if (to.meta.roles && authStore.user) {
    const hasRole = to.meta.roles.some(role =>
      authStore.user?.roles.includes(role)
    )
    if (!hasRole) {
      next({ name: 'forbidden' })
      return
    }
  }

  next()
})

export default router
```

#### ❌ Incorreto

```typescript
// Sem tipagem e guards inadequados
const routes = [
  {
    path: '/dashboard',
    component: DashboardView, // Sem lazy loading
    // Sem meta fields
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Guard no componente - NÃO FAZER
// DashboardView.vue
export default {
  mounted() {
    if (!this.$store.state.isAuthenticated) {
      this.$router.push('/login')
    }
  }
}
```

### Ferramentas

- **Vue Router 4**: `npm install vue-router@4`
- **TypeScript**: Tipagem de rotas e meta fields
- **Vue DevTools**: Inspeção de rotas e navegação
- **ESLint**: Validação de estrutura de rotas

### Checklist

- [ ] Rotas definidas com TypeScript?
- [ ] Navigation guards implementados?
- [ ] Lazy loading configurado?
- [ ] Meta fields tipados?
- [ ] Segurança validada em rotas protegidas?

---

## Rule V.4: Composables para Lógica Reutilizável

### Contexto

Composables são o padrão Vue.js 3 para reutilização de lógica. Projetos sem composables enfrentam:

- **Duplicação de código** (60% de lógica repetida)
- **Manutenção difícil** (mudanças em múltiplos lugares)
- **Testes complexos** (lógica acoplada a componentes)
- **Baixa reutilização** (mixins obsoletos)

### Regra

Criar **composables** para lógica reutilizável:

1. Prefixo `use` para composables (ex: `useAuth`, `useFetch`)
2. Retornar objeto com estado e métodos
3. Usar TypeScript para tipagem
4. Manter composables focados (Single Responsibility)
5. Documentar com JSDoc

### Justificativa

**Benefícios mensuráveis:**

- **60% menos duplicação** de código
- **Reutilização 5x maior** comparado a mixins
- **Testes 70% mais fáceis** (lógica isolada)
- **TypeScript completo** com inferência
- **Manutenção 50% mais rápida** (mudanças centralizadas)

### Exemplos

#### ✅ Correto

```typescript
// composables/useFetch.ts
import { ref, type Ref } from 'vue'

interface UseFetchOptions {
  immediate?: boolean
  onError?: (error: Error) => void
}

interface UseFetchReturn<T> {
  data: Ref<T | null>
  error: Ref<Error | null>
  loading: Ref<boolean>
  execute: () => Promise<void>
}

/**
 * Composable para requisições HTTP com loading e error handling
 * @param url - URL da API
 * @param options - Opções de configuração
 * @returns Estado reativo e método execute
 */
export function useFetch<T>(
  url: string,
  options: UseFetchOptions = {}
): UseFetchReturn<T> {
  const data = ref<T | null>(null)
  const error = ref<Error | null>(null)
  const loading = ref(false)

  const execute = async () => {
    loading.value = true
    error.value = null

    try {
      const response = await fetch(url)
      if (!response.ok) {
        throw new Error(`HTTP ${response.status}`)
      }
      data.value = await response.json()
    } catch (e) {
      error.value = e as Error
      options.onError?.(e as Error)
    } finally {
      loading.value = false
    }
  }

  if (options.immediate) {
    execute()
  }

  return { data, error, loading, execute }
}

// Uso no componente
<script setup lang="ts">
import { useFetch } from '@/composables/useFetch'
import type { User } from '@/types'

const { data: users, loading, error, execute } = useFetch<User[]>(
  '/api/users',
  { immediate: true }
)
</script>

<template>
  <div v-if="loading">Carregando...</div>
  <div v-else-if="error">Erro: {{ error.message }}</div>
  <ul v-else-if="users">
    <li v-for="user in users" :key="user.id">
      {{ user.name }}
    </li>
  </ul>
</template>
```

#### ❌ Incorreto

```typescript
// Lógica duplicada em cada componente - NÃO FAZER
<script setup lang="ts">
import { ref } from 'vue'

const users = ref([])
const loading = ref(false)
const error = ref(null)

const fetchUsers = async () => {
  loading.value = true
  try {
    const response = await fetch('/api/users')
    users.value = await response.json()
  } catch (e) {
    error.value = e
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchUsers()
})
</script>
```

### Ferramentas

- **VueUse**: Biblioteca com 200+ composables prontos
- **TypeScript**: Tipagem completa de composables
- **Vitest**: Testes unitários de composables
- **JSDoc**: Documentação inline

### Checklist

- [ ] Composable tem prefixo `use`?
- [ ] Retorna objeto com estado e métodos?
- [ ] Está tipado com TypeScript?
- [ ] Tem responsabilidade única?
- [ ] Está documentado com JSDoc?

---

## Rule V.5: Vite + TypeScript + ESLint

### Contexto

Vite é o build tool oficial do Vue.js 3, oferecendo desenvolvimento rápido e builds otimizados. Projetos sem Vite + TypeScript + ESLint enfrentam:

- **Dev server lento** (10-30s de inicialização)
- **Hot reload lento** (2-5s por mudança)
- **Builds grandes** (sem tree-shaking adequado)
- **Bugs de tipo** (sem TypeScript)
- **Código inconsistente** (sem ESLint)

### Regra

Configurar **Vite + TypeScript + ESLint** em todos os projetos Vue.js 3:

1. Usar Vite como build tool
2. TypeScript em modo strict
3. ESLint com plugin Vue oficial
4. Prettier para formatação
5. Husky + lint-staged para pre-commit

### Justificativa

**Benefícios mensuráveis:**

- **Dev server 10x mais rápido** (< 1s inicialização)
- **Hot reload instantâneo** (< 100ms)
- **Builds 50% menores** (tree-shaking otimizado)
- **Bugs de tipo reduzidos em 80%** (TypeScript strict)
- **Código 100% consistente** (ESLint + Prettier)

### Exemplos

#### ✅ Correto

```typescript
// vite.config.ts
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { fileURLToPath, URL } from 'node:url'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  build: {
    target: 'esnext',
    minify: 'terser',
    sourcemap: true,
    rollupOptions: {
      output: {
        manualChunks: {
          'vue-vendor': ['vue', 'vue-router', 'pinia']
        }
      }
    }
  }
})

// tsconfig.json
{
  "compilerOptions": {
    "target": "ES2020",
    "module": "ESNext",
    "lib": ["ES2020", "DOM", "DOM.Iterable"],
    "moduleResolution": "bundler",
    "strict": true,
    "jsx": "preserve",
    "resolveJsonModule": true,
    "isolatedModules": true,
    "esModuleInterop": true,
    "skipLibCheck": true,
    "allowImportingTsExtensions": true,
    "noEmit": true,
    "baseUrl": ".",
    "paths": {
      "@/*": ["./src/*"]
    }
  },
  "include": ["src/**/*.ts", "src/**/*.vue"],
  "exclude": ["node_modules"]
}

// .eslintrc.cjs
module.exports = {
  root: true,
  env: {
    node: true,
    'vue/setup-compiler-macros': true
  },
  extends: [
    'plugin:vue/vue3-recommended',
    'eslint:recommended',
    '@vue/eslint-config-typescript',
    '@vue/eslint-config-prettier'
  ],
  rules: {
    'vue/multi-word-component-names': 'error',
    'vue/no-unused-vars': 'error',
    '@typescript-eslint/no-unused-vars': 'error'
  }
}

// package.json
{
  "scripts": {
    "dev": "vite",
    "build": "vue-tsc && vite build",
    "preview": "vite preview",
    "lint": "eslint . --ext .vue,.js,.jsx,.cjs,.mjs,.ts,.tsx,.cts,.mts --fix",
    "format": "prettier --write src/"
  },
  "devDependencies": {
    "@vitejs/plugin-vue": "^5.0.0",
    "@vue/eslint-config-prettier": "^9.0.0",
    "@vue/eslint-config-typescript": "^13.0.0",
    "eslint": "^8.57.0",
    "eslint-plugin-vue": "^9.20.0",
    "prettier": "^3.2.0",
    "typescript": "^5.3.0",
    "vite": "^5.0.0",
    "vue-tsc": "^1.8.0"
  }
}
```

#### ❌ Incorreto

```javascript
// webpack.config.js - NÃO USAR
module.exports = {
  entry: './src/main.js',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js'
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: 'vue-loader'
      }
    ]
  }
}

// JavaScript sem TypeScript - NÃO FAZER
// main.js
import { createApp } from 'vue'
import App from './App.vue'

createApp(App).mount('#app')
```

### Ferramentas

- **Vite**: `npm create vue@latest`
- **TypeScript**: Incluído no template oficial
- **ESLint**: `eslint-plugin-vue` + `@vue/eslint-config-typescript`
- **Prettier**: Formatação automática
- **Husky**: Pre-commit hooks

### Checklist

- [ ] Vite configurado como build tool?
- [ ] TypeScript em modo strict?
- [ ] ESLint com plugin Vue configurado?
- [ ] Prettier integrado?
- [ ] Pre-commit hooks funcionando?

---

## 📊 Resumo das Rules

| Rule | Foco | Impacto | Prioridade |
|------|------|---------|------------|
| **V.1** | Composition API | 40% menos código | 🔴 Alta |
| **V.2** | Pinia State Management | 60% menos boilerplate | 🔴 Alta |
| **V.3** | Vue Router Guards | 40% menos bugs navegação | 🟡 Média |
| **V.4** | Composables | 60% menos duplicação | 🔴 Alta |
| **V.5** | Vite + TypeScript | 10x dev server mais rápido | 🔴 Alta |

---

## 🔗 Integração com Rules Atômicas

### Aplicação Conjunta

Estas rules Vue.js complementam as rules atômicas:

| Rule Atômica | Rule Vue.js | Sinergia |
|--------------|-------------|----------|
| **01-code-style** | V.1, V.4 | Naming conventions + Composition API |
| **02-architecture** | V.2, V.3 | Clean Architecture + Pinia + Router |
| **03-security** | V.3 | Security + Navigation Guards |
| **04-testing** | V.4 | Testing + Composables isolados |
| **05-performance** | V.5 | Performance + Vite optimizations |
| **06-documentation** | V.4 | Documentation + JSDoc composables |

---

## ✅ Checklist de Conformidade Vue.js

### Setup Inicial

- [ ] Vite configurado?
- [ ] TypeScript em modo strict?
- [ ] ESLint + Prettier configurados?
- [ ] Pinia instalado?
- [ ] Vue Router configurado?

### Desenvolvimento

- [ ] Componentes usam `<script setup>`?
- [ ] Estado global em Pinia stores?
- [ ] Rotas com guards tipados?
- [ ] Lógica reutilizável em composables?
- [ ] TypeScript sem erros?

### Qualidade

- [ ] ESLint passa sem erros?
- [ ] Prettier aplicado?
- [ ] Testes de composables?
- [ ] Build otimizado?
- [ ] Performance validada?

---

**Versão:** 1.0
**Próxima revisão:** 19/01/2026
**Mantido por:** Vander Loto - CTO DATAMETRIA
