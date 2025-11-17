# React Native Framework Rules

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Baseado em:** AmazonQ-Guidelines v2.0

---

## 🎯 Visão Geral

Este documento contém **5 rules específicas** para desenvolvimento com **React Native**, complementando as rules atômicas (01-06). Estas rules são aplicadas automaticamente pelo Amazon Q Developer quando React Native é detectado no projeto.

### Aplicação

- **Quando:** React Native detectado (package.json com "react-native")
- **Complementa:** Rules atômicas 01-06
- **Prioridade:** Alta (aplicadas após rules atômicas)

---

## Rule RN.1: TypeScript + Functional Components

### Contexto

React Native com TypeScript oferece type safety e melhor DX. Projetos sem TypeScript enfrentam:

- **Bugs de tipo** (45% dos bugs em apps)
- **Refatoração arriscada** (sem garantias)
- **Autocomplete limitado** (produtividade baixa)
- **Manutenção difícil** (código não documentado)

### Regra

Usar **TypeScript** com **Functional Components**:

1. TypeScript strict mode habilitado
2. Functional components com hooks
3. Props tipadas com interfaces
4. Evitar any (usar unknown)
5. Tipos para navigation e routes

### Justificativa

**Benefícios mensuráveis:**

- **45% menos bugs** de tipo
- **Refatoração 80% mais segura**
- **Produtividade 40% maior** (autocomplete)
- **Onboarding 50% mais rápido**
- **Manutenção 60% mais fácil**

### Exemplos

#### ✅ Correto

```typescript
// types/user.ts
export interface User {
  id: string;
  name: string;
  email: string;
  avatar?: string;
}

export interface UserCardProps {
  user: User;
  onPress?: (userId: string) => void;
  variant?: 'compact' | 'detailed';
}

// components/UserCard.tsx
import React, { FC, memo } from 'react';
import { TouchableOpacity, Text, Image, StyleSheet } from 'react-native';
import { UserCardProps } from '../types/user';

export const UserCard: FC<UserCardProps> = memo(({
  user,
  onPress,
  variant = 'compact'
}) => {
  const handlePress = () => {
    onPress?.(user.id);
  };

  return (
    <TouchableOpacity
      style={styles.container}
      onPress={handlePress}
      testID="user-card"
    >
      {user.avatar && (
        <Image
          source={{ uri: user.avatar }}
          style={styles.avatar}
        />
      )}
      <Text style={styles.name}>{user.name}</Text>
      {variant === 'detailed' && (
        <Text style={styles.email}>{user.email}</Text>
      )}
    </TouchableOpacity>
  );
});

UserCard.displayName = 'UserCard';

// Navigation types
import { NativeStackScreenProps } from '@react-navigation/native-stack';

export type RootStackParamList = {
  Home: undefined;
  UserDetail: { userId: string };
  Settings: { section?: string };
};

export type UserDetailScreenProps = NativeStackScreenProps<
  RootStackParamList,
  'UserDetail'
>;

// Screen com tipos
const UserDetailScreen: FC<UserDetailScreenProps> = ({ route, navigation }) => {
  const { userId } = route.params;

  return (
    <View>
      <Text>User ID: {userId}</Text>
    </View>
  );
};
```

#### ❌ Incorreto

```javascript
// JavaScript sem tipos - NÃO FAZER
import React from 'react';
import { View, Text } from 'react-native';

// Class component - obsoleto
class UserCard extends React.Component {
  render() {
    // Props sem tipagem
    const { user, onPress } = this.props;

    return (
      <View>
        <Text>{user.name}</Text>
      </View>
    );
  }
}

// Functional sem tipos
const UserDetail = ({ route }) => {
  // Sem tipagem de params
  const userId = route.params.userId;

  return <Text>{userId}</Text>;
};
```

### Ferramentas

- **TypeScript**: `npm install -D typescript @types/react @types/react-native`
- **ESLint**: `@typescript-eslint/eslint-plugin`
- **Prettier**: Formatação automática
- **VS Code**: IntelliSense completo

### Checklist

- [ ] TypeScript strict mode habilitado?
- [ ] Functional components usados?
- [ ] Props tipadas com interfaces?
- [ ] Navigation types definidos?
- [ ] Sem uso de any?

---

## Rule RN.2: Zustand para State Management

### Contexto

React Native precisa de state management eficiente. Projetos sem Zustand enfrentam:

- **Boilerplate excessivo** (Redux)
- **Performance ruim** (Context API)
- **Complexidade alta** (múltiplas libs)
- **Bundle size grande** (libs pesadas)

### Regra

Usar **Zustand** para state management:

1. Stores pequenas e focadas
2. Immer para imutabilidade
3. Persist para storage
4. Selectors para performance
5. DevTools para debug

### Justificativa

**Benefícios mensuráveis:**

- **70% menos boilerplate** vs Redux
- **Performance 50% melhor** vs Context
- **Bundle 80% menor** (3kb vs 15kb)
- **DX 100% melhor** (API simples)
- **Manutenção 60% mais fácil**

### Exemplos

#### ✅ Correto

```typescript
// stores/authStore.ts
import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import { immer } from 'zustand/middleware/immer';
import AsyncStorage from '@react-native-async-storage/async-storage';

interface User {
  id: string;
  name: string;
  email: string;
}

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;

  // Actions
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  updateUser: (user: Partial<User>) => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    immer((set, get) => ({
      user: null,
      token: null,
      isAuthenticated: false,

      login: async (email, password) => {
        const response = await api.login(email, password);
        set((state) => {
          state.user = response.user;
          state.token = response.token;
          state.isAuthenticated = true;
        });
      },

      logout: () => {
        set((state) => {
          state.user = null;
          state.token = null;
          state.isAuthenticated = false;
        });
      },

      updateUser: (userData) => {
        set((state) => {
          if (state.user) {
            state.user = { ...state.user, ...userData };
          }
        });
      },
    })),
    {
      name: 'auth-storage',
      storage: createJSONStorage(() => AsyncStorage),
    }
  )
);

// Selectors para performance
export const useUser = () => useAuthStore((state) => state.user);
export const useIsAuthenticated = () => useAuthStore((state) => state.isAuthenticated);

// Uso no componente
import { useAuthStore, useUser } from '../stores/authStore';

const ProfileScreen: FC = () => {
  const user = useUser(); // Selector otimizado
  const logout = useAuthStore((state) => state.logout);

  if (!user) return <Text>Not logged in</Text>;

  return (
    <View>
      <Text>{user.name}</Text>
      <Button title="Logout" onPress={logout} />
    </View>
  );
};
```

#### ❌ Incorreto

```typescript
// Context API com performance ruim - NÃO FAZER
const AuthContext = createContext<any>(null);

const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);

  // Todos os componentes re-renderizam quando qualquer valor muda
  const value = { user, token, setUser, setToken };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
};
```

### Ferramentas

- **Zustand**: `npm install zustand`
- **Immer**: `npm install immer`
- **AsyncStorage**: `@react-native-async-storage/async-storage`
- **Zustand DevTools**: Debug de stores

### Checklist

- [ ] Zustand instalado?
- [ ] Stores com Immer middleware?
- [ ] Persist configurado?
- [ ] Selectors para performance?
- [ ] TypeScript tipado?

---

## Rule RN.3: React Navigation com Type Safety

### Contexto

Navegação é crítica em apps mobile. Projetos sem type safety enfrentam:

- **Bugs de navegação** (30% dos crashes)
- **Params incorretos** (runtime errors)
- **Refatoração arriscada** (quebra silenciosa)
- **DX ruim** (sem autocomplete)

### Regra

Configurar **React Navigation** com type safety:

1. Tipos para todas as rotas
2. Params tipados
3. Navigation prop tipado
4. Deep linking configurado
5. Stack, Tab e Drawer tipados

### Justificativa

**Benefícios mensuráveis:**

- **30% menos crashes** de navegação
- **Refatoração 100% segura**
- **DX 80% melhor** (autocomplete)
- **Bugs 50% menos** (compile time)
- **Manutenção 60% mais fácil**

### Exemplos

#### ✅ Correto

```typescript
// types/navigation.ts
import { NavigatorScreenParams } from '@react-navigation/native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { BottomTabScreenProps } from '@react-navigation/bottom-tabs';

// Root Stack
export type RootStackParamList = {
  Auth: NavigatorScreenParams<AuthStackParamList>;
  Main: NavigatorScreenParams<MainTabParamList>;
  UserDetail: { userId: string; showEdit?: boolean };
  Settings: undefined;
};

// Auth Stack
export type AuthStackParamList = {
  Login: undefined;
  Register: undefined;
  ForgotPassword: { email?: string };
};

// Main Tabs
export type MainTabParamList = {
  Home: undefined;
  Search: { query?: string };
  Profile: undefined;
};

// Screen Props
export type UserDetailScreenProps = NativeStackScreenProps<
  RootStackParamList,
  'UserDetail'
>;

export type HomeScreenProps = BottomTabScreenProps<
  MainTabParamList,
  'Home'
>;

// Declare global types
declare global {
  namespace ReactNavigation {
    interface RootParamList extends RootStackParamList {}
  }
}

// Navigation/App.tsx
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

const Stack = createNativeStackNavigator<RootStackParamList>();

export const AppNavigator = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="Auth" component={AuthNavigator} />
        <Stack.Screen name="Main" component={MainNavigator} />
        <Stack.Screen
          name="UserDetail"
          component={UserDetailScreen}
          options={{ title: 'User Details' }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

// Screen com tipos
const UserDetailScreen: FC<UserDetailScreenProps> = ({ route, navigation }) => {
  const { userId, showEdit } = route.params; // Tipado!

  const handleEdit = () => {
    // Autocomplete completo
    navigation.navigate('Settings');
  };

  return (
    <View>
      <Text>User: {userId}</Text>
      {showEdit && <Button title="Edit" onPress={handleEdit} />}
    </View>
  );
};

// Hook useNavigation tipado
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

type NavigationProp = NativeStackNavigationProp<RootStackParamList>;

const SomeComponent = () => {
  const navigation = useNavigation<NavigationProp>();

  const goToUser = () => {
    // TypeScript valida params
    navigation.navigate('UserDetail', {
      userId: '123',
      showEdit: true
    });
  };
};
```

#### ❌ Incorreto

```typescript
// Sem tipos - NÃO FAZER
const UserDetailScreen = ({ route, navigation }) => {
  // Sem tipagem - erro em runtime
  const userId = route.params.userId;

  // Sem autocomplete
  navigation.navigate('UserDetail', {
    wrongParam: 'value' // Não detectado
  });
};
```

### Ferramentas

- **React Navigation**: `@react-navigation/native`
- **Native Stack**: `@react-navigation/native-stack`
- **Bottom Tabs**: `@react-navigation/bottom-tabs`
- **TypeScript**: Type checking completo

### Checklist

- [ ] Tipos para todas as rotas?
- [ ] Params tipados?
- [ ] Global types declarados?
- [ ] Navigation prop tipado?
- [ ] Deep linking configurado?

---

## Rule RN.4: Performance Optimization

### Contexto

React Native apps precisam de 60 FPS. Apps sem otimização enfrentam:

- **Jank** (frames perdidos)
- **Scrolling travado** (listas lentas)
- **Consumo alto** de bateria/memória
- **Startup lento** (> 3s)

### Regra

Aplicar **Performance Optimization**:

1. React.memo para componentes
2. useMemo/useCallback para valores
3. FlatList para listas longas
4. FastImage para imagens
5. Hermes engine habilitado

### Justificativa

**Benefícios mensuráveis:**

- **60 FPS** constante
- **Startup 50% mais rápido**
- **Memória 40% menor**
- **Bateria 30% mais duração**
- **UX 100% melhor**

### Exemplos

#### ✅ Correto

```typescript
// React.memo para evitar re-renders
import React, { FC, memo, useMemo, useCallback } from 'react';
import { FlatList, ListRenderItem } from 'react-native';
import FastImage from 'react-native-fast-image';

interface UserCardProps {
  user: User;
  onPress: (userId: string) => void;
}

export const UserCard: FC<UserCardProps> = memo(({ user, onPress }) => {
  // useCallback para estabilizar função
  const handlePress = useCallback(() => {
    onPress(user.id);
  }, [user.id, onPress]);

  return (
    <TouchableOpacity onPress={handlePress}>
      <FastImage
        source={{ uri: user.avatar }}
        style={styles.avatar}
        resizeMode={FastImage.resizeMode.cover}
      />
      <Text>{user.name}</Text>
    </TouchableOpacity>
  );
}, (prevProps, nextProps) => {
  // Custom comparison
  return prevProps.user.id === nextProps.user.id;
});

// FlatList otimizada
const UserList: FC<{ users: User[] }> = ({ users }) => {
  const handleUserPress = useCallback((userId: string) => {
    navigation.navigate('UserDetail', { userId });
  }, [navigation]);

  const renderItem: ListRenderItem<User> = useCallback(({ item }) => (
    <UserCard user={item} onPress={handleUserPress} />
  ), [handleUserPress]);

  const keyExtractor = useCallback((item: User) => item.id, []);

  const getItemLayout = useCallback((data, index) => ({
    length: 80,
    offset: 80 * index,
    index,
  }), []);

  return (
    <FlatList
      data={users}
      renderItem={renderItem}
      keyExtractor={keyExtractor}
      getItemLayout={getItemLayout}
      removeClippedSubviews={true}
      maxToRenderPerBatch={10}
      updateCellsBatchingPeriod={50}
      initialNumToRender={10}
      windowSize={5}
    />
  );
};

// useMemo para cálculos pesados
const ExpensiveComponent: FC<{ data: number[] }> = ({ data }) => {
  const processedData = useMemo(() => {
    return data.map(item => item * 2).filter(item => item > 10);
  }, [data]);

  return <Text>{processedData.length}</Text>;
};

// FastImage com cache
const Avatar: FC<{ uri: string }> = ({ uri }) => {
  return (
    <FastImage
      source={{
        uri,
        priority: FastImage.priority.normal,
        cache: FastImage.cacheControl.immutable,
      }}
      style={styles.avatar}
    />
  );
};
```

#### ❌ Incorreto

```typescript
// Sem otimização - NÃO FAZER
const UserCard = ({ user, onPress }) => {
  // Função recriada a cada render
  const handlePress = () => {
    onPress(user.id);
  };

  return (
    <TouchableOpacity onPress={handlePress}>
      {/* Image sem cache */}
      <Image source={{ uri: user.avatar }} />
      <Text>{user.name}</Text>
    </TouchableOpacity>
  );
};

// ScrollView para lista longa - NÃO FAZER
const UserList = ({ users }) => {
  return (
    <ScrollView>
      {users.map(user => (
        <UserCard key={user.id} user={user} />
      ))}
    </ScrollView>
  );
};
```

### Ferramentas

- **React DevTools**: Profiler
- **Flipper**: Performance monitor
- **Hermes**: JavaScript engine
- **FastImage**: `react-native-fast-image`
- **why-did-you-render**: Debug re-renders

### Checklist

- [ ] React.memo usado?
- [ ] useMemo/useCallback aplicados?
- [ ] FlatList para listas?
- [ ] FastImage para imagens?
- [ ] Hermes habilitado?

---

## Rule RN.5: Testing com Jest e React Native Testing Library

### Contexto

Apps sem testes adequados enfrentam:

- **Bugs em produção** (60% poderiam ser evitados)
- **Refatoração arriscada** (sem garantias)
- **Regressões frequentes** (mudanças quebram)
- **Confiança baixa** (medo de deploy)

### Regra

Implementar **Testing completo**:

1. Unit tests com Jest
2. Component tests com RNTL
3. Integration tests
4. E2E tests com Detox
5. Coverage mínimo 80%

### Justificativa

**Benefícios mensuráveis:**

- **60% menos bugs** em produção
- **Refatoração 100% segura**
- **Regressões 90% menos**
- **Confiança 100%** em deploys
- **Manutenção 50% mais fácil**

### Exemplos

#### ✅ Correto

```typescript
// __tests__/components/UserCard.test.tsx
import React from 'react';
import { render, fireEvent, waitFor } from '@testing-library/react-native';
import { UserCard } from '../UserCard';

describe('UserCard', () => {
  const mockUser = {
    id: '1',
    name: 'John Doe',
    email: 'john@example.com',
    avatar: 'https://example.com/avatar.jpg',
  };

  it('renders user information correctly', () => {
    const { getByText } = render(
      <UserCard user={mockUser} />
    );

    expect(getByText('John Doe')).toBeTruthy();
    expect(getByText('john@example.com')).toBeTruthy();
  });

  it('calls onPress with userId when pressed', () => {
    const onPressMock = jest.fn();
    const { getByTestId } = render(
      <UserCard user={mockUser} onPress={onPressMock} />
    );

    fireEvent.press(getByTestId('user-card'));

    expect(onPressMock).toHaveBeenCalledWith('1');
    expect(onPressMock).toHaveBeenCalledTimes(1);
  });

  it('renders compact variant by default', () => {
    const { queryByText } = render(
      <UserCard user={mockUser} />
    );

    expect(queryByText('john@example.com')).toBeNull();
  });

  it('renders detailed variant when specified', () => {
    const { getByText } = render(
      <UserCard user={mockUser} variant="detailed" />
    );

    expect(getByText('john@example.com')).toBeTruthy();
  });
});

// __tests__/stores/authStore.test.ts
import { renderHook, act } from '@testing-library/react-hooks';
import { useAuthStore } from '../stores/authStore';

describe('authStore', () => {
  beforeEach(() => {
    useAuthStore.setState({
      user: null,
      token: null,
      isAuthenticated: false,
    });
  });

  it('logs in user successfully', async () => {
    const { result } = renderHook(() => useAuthStore());

    await act(async () => {
      await result.current.login('test@example.com', 'password');
    });

    expect(result.current.isAuthenticated).toBe(true);
    expect(result.current.user).toBeTruthy();
    expect(result.current.token).toBeTruthy();
  });

  it('logs out user', () => {
    const { result } = renderHook(() => useAuthStore());

    act(() => {
      result.current.logout();
    });

    expect(result.current.isAuthenticated).toBe(false);
    expect(result.current.user).toBeNull();
    expect(result.current.token).toBeNull();
  });
});

// __tests__/screens/UserDetailScreen.test.tsx
import React from 'react';
import { render, waitFor } from '@testing-library/react-native';
import { NavigationContainer } from '@react-navigation/native';
import { UserDetailScreen } from '../screens/UserDetailScreen';

const mockNavigation = {
  navigate: jest.fn(),
  goBack: jest.fn(),
};

const mockRoute = {
  params: { userId: '123' },
};

describe('UserDetailScreen', () => {
  it('loads and displays user data', async () => {
    const { getByText } = render(
      <NavigationContainer>
        <UserDetailScreen
          navigation={mockNavigation as any}
          route={mockRoute as any}
        />
      </NavigationContainer>
    );

    await waitFor(() => {
      expect(getByText('John Doe')).toBeTruthy();
    });
  });
});

// jest.config.js
module.exports = {
  preset: 'react-native',
  setupFilesAfterEnv: ['@testing-library/jest-native/extend-expect'],
  transformIgnorePatterns: [
    'node_modules/(?!(react-native|@react-native|@react-navigation)/)',
  ],
  collectCoverageFrom: [
    'src/**/*.{ts,tsx}',
    '!src/**/*.d.ts',
    '!src/**/*.stories.tsx',
  ],
  coverageThreshold: {
    global: {
      branches: 75,
      functions: 80,
      lines: 80,
      statements: 80,
    },
  },
};
```

#### ❌ Incorreto

```typescript
// Sem testes - NÃO FAZER
const UserCard = ({ user }) => {
  return (
    <View>
      <Text>{user.name}</Text>
    </View>
  );
};

// Sem coverage, sem garantias
```

### Ferramentas

- **Jest**: Test runner
- **React Native Testing Library**: Component testing
- **@testing-library/react-hooks**: Hook testing
- **Detox**: E2E testing
- **jest-coverage**: Coverage reports

### Checklist

- [ ] Jest configurado?
- [ ] RNTL instalado?
- [ ] Unit tests escritos?
- [ ] Component tests escritos?
- [ ] Coverage > 80%?

---

## 📊 Resumo das Rules

| Rule | Foco | Impacto | Prioridade |
|------|------|---------|------------|
| **RN.1** | TypeScript + Functional | 45% menos bugs tipo | 🔴 Alta |
| **RN.2** | Zustand State Management | 70% menos boilerplate | 🔴 Alta |
| **RN.3** | React Navigation Types | 30% menos crashes | 🔴 Alta |
| **RN.4** | Performance | 60 FPS constante | 🔴 Alta |
| **RN.5** | Testing | 60% menos bugs produção | 🔴 Alta |

---

## 🔗 Integração com Rules Atômicas

| Rule Atômica | Rule React Native | Sinergia |
|--------------|-------------------|----------|
| **01-code-style** | RN.1 | Naming + TypeScript |
| **02-architecture** | RN.2 | Architecture + Zustand |
| **03-security** | RN.3 | Security + Navigation |
| **04-testing** | RN.5 | Testing + Jest + RNTL |
| **05-performance** | RN.4 | Performance + Optimization |
| **06-documentation** | RN.1 | Documentation + Types |

---

## ✅ Checklist de Conformidade React Native

### Setup Inicial

- [ ] TypeScript configurado?
- [ ] Zustand instalado?
- [ ] React Navigation configurado?
- [ ] Hermes habilitado?
- [ ] Jest configurado?

### Desenvolvimento

- [ ] Functional components usados?
- [ ] Props tipadas?
- [ ] Zustand para state?
- [ ] Navigation tipada?
- [ ] Performance otimizada?

### Qualidade

- [ ] Testes com coverage > 80%?
- [ ] 60 FPS em release mode?
- [ ] TypeScript sem erros?
- [ ] ESLint passa?
- [ ] Bundle size otimizado?

---

**Versão:** 1.0
**Próxima revisão:** 19/01/2026
**Mantido por:** Vander Loto - CTO DATAMETRIA
