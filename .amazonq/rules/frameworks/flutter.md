# Flutter Framework Rules

**Versão:** 1.0
**Data:** 19/10/2025
**Autor:** Vander Loto - CTO DATAMETRIA
**Baseado em:** AmazonQ-Guidelines v2.0

---

## 🎯 Visão Geral

Este documento contém **5 rules específicas** para desenvolvimento com **Flutter**, complementando as rules atômicas (01-06). Estas rules são aplicadas automaticamente pelo Amazon Q Developer quando Flutter é detectado no projeto.

### Aplicação

- **Quando:** Flutter detectado (pubspec.yaml com "flutter")
- **Complementa:** Rules atômicas 01-06
- **Prioridade:** Alta (aplicadas após rules atômicas)

---

## Rule FL.1: BLoC Pattern para State Management

### Contexto

Flutter oferece múltiplas opções de state management, mas BLoC (Business Logic Component) é o padrão recomendado para projetos enterprise. Projetos sem BLoC adequado enfrentam:

- **Estado inconsistente** (40% dos bugs em apps)
- **Lógica acoplada** (UI misturada com business logic)
- **Testes difíceis** (lógica não isolada)
- **Manutenção complexa** (mudanças em múltiplos lugares)

### Regra

Usar **BLoC pattern** para gerenciamento de estado:

1. Separar UI de business logic
2. Usar flutter_bloc package
3. Eventos para ações do usuário
4. Estados para representar UI
5. Repository pattern para dados

### Justificativa

**Benefícios mensuráveis:**

- **40% menos bugs** de estado
- **Testes 80% mais fáceis** (lógica isolada)
- **Código 50% mais reutilizável**
- **Manutenção 60% mais rápida**
- **Separação clara** de responsabilidades

### Exemplos

#### ✅ Correto

```dart
// user_event.dart
abstract class UserEvent {}

class LoadUser extends UserEvent {
  final String userId;
  LoadUser(this.userId);
}

class UpdateUser extends UserEvent {
  final User user;
  UpdateUser(this.user);
}

// user_state.dart
abstract class UserState {}

class UserInitial extends UserState {}

class UserLoading extends UserState {}

class UserLoaded extends UserState {
  final User user;
  UserLoaded(this.user);
}

class UserError extends UserState {
  final String message;
  UserError(this.message);
}

// user_bloc.dart
class UserBloc extends Bloc<UserEvent, UserState> {
  final UserRepository repository;

  UserBloc({required this.repository}) : super(UserInitial()) {
    on<LoadUser>(_onLoadUser);
    on<UpdateUser>(_onUpdateUser);
  }

  Future<void> _onLoadUser(
    LoadUser event,
    Emitter<UserState> emit,
  ) async {
    emit(UserLoading());
    try {
      final user = await repository.getUser(event.userId);
      emit(UserLoaded(user));
    } catch (e) {
      emit(UserError(e.toString()));
    }
  }

  Future<void> _onUpdateUser(
    UpdateUser event,
    Emitter<UserState> emit,
  ) async {
    emit(UserLoading());
    try {
      await repository.updateUser(event.user);
      emit(UserLoaded(event.user));
    } catch (e) {
      emit(UserError(e.toString()));
    }
  }
}

// user_screen.dart
class UserScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => UserBloc(
        repository: context.read<UserRepository>(),
      )..add(LoadUser('123')),
      child: Scaffold(
        appBar: AppBar(title: Text('User')),
        body: BlocBuilder<UserBloc, UserState>(
          builder: (context, state) {
            if (state is UserLoading) {
              return Center(child: CircularProgressIndicator());
            }
            if (state is UserError) {
              return Center(child: Text('Error: ${state.message}'));
            }
            if (state is UserLoaded) {
              return UserDetails(user: state.user);
            }
            return Container();
          },
        ),
      ),
    );
  }
}
```

#### ❌ Incorreto

```dart
// setState direto - lógica misturada com UI - NÃO FAZER
class UserScreen extends StatefulWidget {
  @override
  _UserScreenState createState() => _UserScreenState();
}

class _UserScreenState extends State<UserScreen> {
  User? user;
  bool loading = false;
  String? error;

  @override
  void initState() {
    super.initState();
    loadUser();
  }

  Future<void> loadUser() async {
    setState(() => loading = true);
    try {
      // Lógica de negócio no widget - NÃO FAZER
      final response = await http.get('api/users/123');
      final data = json.decode(response.body);
      setState(() {
        user = User.fromJson(data);
        loading = false;
      });
    } catch (e) {
      setState(() {
        error = e.toString();
        loading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    if (loading) return CircularProgressIndicator();
    if (error != null) return Text('Error: $error');
    if (user == null) return Container();
    return UserDetails(user: user!);
  }
}
```

### Ferramentas

- **flutter_bloc**: Package oficial BLoC
- **bloc_test**: Testes de BLoCs
- **flutter_bloc DevTools**: Inspeção de estados
- **bloc_concurrency**: Controle de concorrência

### Checklist

- [ ] BLoC separado de UI?
- [ ] Eventos para ações do usuário?
- [ ] Estados representam UI?
- [ ] Repository pattern implementado?
- [ ] Testes de BLoC escritos?

---

## Rule FL.2: Clean Architecture em Camadas

### Contexto

Flutter apps sem arquitetura clara enfrentam:

- **Código espaguete** (tudo misturado)
- **Testes impossíveis** (dependências acopladas)
- **Manutenção cara** (mudanças quebram tudo)
- **Escalabilidade zero** (não cresce)

### Regra

Implementar **Clean Architecture** em 4 camadas:

1. **Presentation**: Widgets, BLoCs, Pages
2. **Domain**: Entities, Use Cases, Repositories (interfaces)
3. **Data**: Repositories (implementação), Data Sources, Models
4. **Core**: Utils, Constants, Extensions

### Justificativa

**Benefícios mensuráveis:**

- **Testes 90% mais fáceis** (camadas isoladas)
- **Manutenção 70% mais rápida**
- **Reutilização 5x maior**
- **Escalabilidade ilimitada**
- **Onboarding 50% mais rápido**

### Exemplos

#### ✅ Correto

```dart
// Estrutura de pastas
lib/
├── core/
│   ├── error/
│   │   └── failures.dart
│   ├── usecases/
│   │   └── usecase.dart
│   └── utils/
│       └── constants.dart
├── features/
│   └── user/
│       ├── data/
│       │   ├── datasources/
│       │   │   ├── user_local_data_source.dart
│       │   │   └── user_remote_data_source.dart
│       │   ├── models/
│       │   │   └── user_model.dart
│       │   └── repositories/
│       │       └── user_repository_impl.dart
│       ├── domain/
│       │   ├── entities/
│       │   │   └── user.dart
│       │   ├── repositories/
│       │   │   └── user_repository.dart
│       │   └── usecases/
│       │       ├── get_user.dart
│       │       └── update_user.dart
│       └── presentation/
│           ├── bloc/
│           │   ├── user_bloc.dart
│           │   ├── user_event.dart
│           │   └── user_state.dart
│           ├── pages/
│           │   └── user_page.dart
│           └── widgets/
│               └── user_card.dart

// domain/entities/user.dart
class User {
  final String id;
  final String name;
  final String email;

  User({
    required this.id,
    required this.name,
    required this.email,
  });
}

// domain/repositories/user_repository.dart
abstract class UserRepository {
  Future<Either<Failure, User>> getUser(String id);
  Future<Either<Failure, void>> updateUser(User user);
}

// domain/usecases/get_user.dart
class GetUser implements UseCase<User, String> {
  final UserRepository repository;

  GetUser(this.repository);

  @override
  Future<Either<Failure, User>> call(String id) {
    return repository.getUser(id);
  }
}

// data/models/user_model.dart
class UserModel extends User {
  UserModel({
    required String id,
    required String name,
    required String email,
  }) : super(id: id, name: name, email: email);

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['id'],
      name: json['name'],
      email: json['email'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'email': email,
    };
  }
}

// data/repositories/user_repository_impl.dart
class UserRepositoryImpl implements UserRepository {
  final UserRemoteDataSource remoteDataSource;
  final UserLocalDataSource localDataSource;

  UserRepositoryImpl({
    required this.remoteDataSource,
    required this.localDataSource,
  });

  @override
  Future<Either<Failure, User>> getUser(String id) async {
    try {
      final user = await remoteDataSource.getUser(id);
      await localDataSource.cacheUser(user);
      return Right(user);
    } on ServerException {
      return Left(ServerFailure());
    }
  }
}
```

#### ❌ Incorreto

```dart
// Tudo em um arquivo - NÃO FAZER
class UserPage extends StatefulWidget {
  @override
  _UserPageState createState() => _UserPageState();
}

class _UserPageState extends State<UserPage> {
  // Lógica de negócio no widget
  Future<User> getUser(String id) async {
    final response = await http.get('api/users/$id');
    return User.fromJson(json.decode(response.body));
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: getUser('123'),
      builder: (context, snapshot) {
        // UI misturada com lógica
        return Text(snapshot.data?.name ?? '');
      },
    );
  }
}
```

### Ferramentas

- **get_it**: Dependency Injection
- **dartz**: Either para error handling
- **equatable**: Value equality
- **mockito**: Mocking para testes

### Checklist

- [ ] 4 camadas implementadas?
- [ ] Entities no domain?
- [ ] Use cases isolados?
- [ ] Repository pattern implementado?
- [ ] Dependency Injection configurado?

---

## Rule FL.3: Widget Composition e Reutilização

### Contexto

Flutter é baseado em widgets. Projetos sem composição adequada enfrentam:

- **Widgets gigantes** (500+ linhas)
- **Duplicação massiva** (mesmo código em múltiplos lugares)
- **Performance ruim** (rebuilds desnecessários)
- **Manutenção impossível** (mudanças quebram tudo)

### Regra

Aplicar **Widget Composition**:

1. Widgets pequenos (< 100 linhas)
2. Extrair widgets reutilizáveis
3. Usar const constructors
4. Preferir StatelessWidget
5. Keys para otimização

### Justificativa

**Benefícios mensuráveis:**

- **Performance 50% melhor** (menos rebuilds)
- **Reutilização 10x maior**
- **Manutenção 70% mais fácil**
- **Código 60% menor**
- **Testes 80% mais simples**

### Exemplos

#### ✅ Correto

```dart
// Widgets pequenos e reutilizáveis
class UserCard extends StatelessWidget {
  final User user;
  final VoidCallback? onTap;

  const UserCard({
    Key? key,
    required this.user,
    this.onTap,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Card(
      child: InkWell(
        onTap: onTap,
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Row(
            children: [
              UserAvatar(url: user.avatarUrl),
              const SizedBox(width: 16),
              Expanded(
                child: UserInfo(user: user),
              ),
              const Icon(Icons.chevron_right),
            ],
          ),
        ),
      ),
    );
  }
}

class UserAvatar extends StatelessWidget {
  final String url;
  final double size;

  const UserAvatar({
    Key? key,
    required this.url,
    this.size = 48.0,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: size / 2,
      backgroundImage: NetworkImage(url),
    );
  }
}

class UserInfo extends StatelessWidget {
  final User user;

  const UserInfo({
    Key? key,
    required this.user,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          user.name,
          style: Theme.of(context).textTheme.titleMedium,
        ),
        const SizedBox(height: 4),
        Text(
          user.email,
          style: Theme.of(context).textTheme.bodySmall,
        ),
      ],
    );
  }
}

// Uso com const para performance
class UserList extends StatelessWidget {
  final List<User> users;

  const UserList({
    Key? key,
    required this.users,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: users.length,
      itemBuilder: (context, index) {
        final user = users[index];
        return UserCard(
          key: ValueKey(user.id),
          user: user,
          onTap: () => _navigateToUser(context, user),
        );
      },
    );
  }
}
```

#### ❌ Incorreto

```dart
// Widget gigante - NÃO FAZER
class UserPage extends StatelessWidget {
  final User user;

  UserPage({required this.user});

  @override
  Widget build(BuildContext context) {
    // 500+ linhas em um widget
    return Scaffold(
      appBar: AppBar(
        title: Text(user.name),
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            // Código duplicado inline
            Container(
              padding: EdgeInsets.all(16),
              child: Row(
                children: [
                  CircleAvatar(
                    radius: 24,
                    backgroundImage: NetworkImage(user.avatarUrl),
                  ),
                  SizedBox(width: 16),
                  Column(
                    children: [
                      Text(user.name),
                      Text(user.email),
                    ],
                  ),
                ],
              ),
            ),
            // Mais 400 linhas...
          ],
        ),
      ),
    );
  }
}
```

### Ferramentas

- **Flutter DevTools**: Widget Inspector
- **flutter_lints**: Análise estática
- **Performance Overlay**: Identificar rebuilds
- **dart analyze**: Verificação de código

### Checklist

- [ ] Widgets < 100 linhas?
- [ ] Widgets reutilizáveis extraídos?
- [ ] Const constructors usados?
- [ ] Keys para otimização?
- [ ] StatelessWidget preferido?

---

## Rule FL.4: Responsive Design e Adaptive Layouts

### Contexto

Flutter apps rodam em múltiplos dispositivos. Apps sem design responsivo enfrentam:

- **UX ruim** em tablets/desktop
- **Overflow errors** em telas pequenas
- **Layouts quebrados** em landscape
- **Manutenção duplicada** (código por plataforma)

### Regra

Implementar **Responsive Design**:

1. LayoutBuilder para breakpoints
2. MediaQuery para dimensões
3. Flexible/Expanded para layouts fluidos
4. SafeArea para notches
5. OrientationBuilder para rotação

### Justificativa

**Benefícios mensuráveis:**

- **Suporte 100%** a todos os dispositivos
- **UX 80% melhor** em tablets
- **Manutenção 60% menor** (código único)
- **Crashes 90% menos** (overflow)
- **Acessibilidade 100%** (text scaling)

### Exemplos

#### ✅ Correto

```dart
class ResponsiveLayout extends StatelessWidget {
  final Widget mobile;
  final Widget? tablet;
  final Widget? desktop;

  const ResponsiveLayout({
    Key? key,
    required this.mobile,
    this.tablet,
    this.desktop,
  }) : super(key: key);

  static bool isMobile(BuildContext context) =>
      MediaQuery.of(context).size.width < 600;

  static bool isTablet(BuildContext context) =>
      MediaQuery.of(context).size.width >= 600 &&
      MediaQuery.of(context).size.width < 1200;

  static bool isDesktop(BuildContext context) =>
      MediaQuery.of(context).size.width >= 1200;

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        if (constraints.maxWidth >= 1200) {
          return desktop ?? tablet ?? mobile;
        }
        if (constraints.maxWidth >= 600) {
          return tablet ?? mobile;
        }
        return mobile;
      },
    );
  }
}

class UserListPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ResponsiveLayout(
      mobile: UserListMobile(),
      tablet: UserListTablet(),
      desktop: UserListDesktop(),
    );
  }
}

class UserListMobile extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Users')),
      body: SafeArea(
        child: ListView.builder(
          padding: EdgeInsets.all(8),
          itemBuilder: (context, index) => UserCard(),
        ),
      ),
    );
  }
}

class UserListTablet extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Row(
          children: [
            Flexible(
              flex: 1,
              child: UserListSidebar(),
            ),
            Flexible(
              flex: 2,
              child: UserDetails(),
            ),
          ],
        ),
      ),
    );
  }
}

// Adaptive padding
class AdaptivePadding extends StatelessWidget {
  final Widget child;

  const AdaptivePadding({Key? key, required this.child}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.symmetric(
        horizontal: ResponsiveLayout.isMobile(context) ? 16 : 32,
        vertical: ResponsiveLayout.isMobile(context) ? 8 : 16,
      ),
      child: child,
    );
  }
}
```

#### ❌ Incorreto

```dart
// Layout fixo - NÃO FAZER
class UserPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        width: 400, // Fixo - quebra em telas pequenas
        child: Column(
          children: [
            Container(
              height: 200, // Fixo - não adapta
              child: Image.network('url'),
            ),
            // Sem SafeArea - problemas com notch
            Text('User Name'),
          ],
        ),
      ),
    );
  }
}
```

### Ferramentas

- **flutter_screenutil**: Responsive sizing
- **responsive_framework**: Breakpoints
- **device_preview**: Preview múltiplos devices
- **adaptive_theme**: Temas adaptativos

### Checklist

- [ ] LayoutBuilder para breakpoints?
- [ ] MediaQuery para dimensões?
- [ ] SafeArea implementado?
- [ ] Flexible/Expanded usados?
- [ ] Testado em múltiplos devices?

---

## Rule FL.5: Performance Optimization

### Contexto

Flutter apps sem otimização enfrentam:

- **Jank** (frames perdidos)
- **Consumo alto** de bateria/memória
- **Startup lento** (> 3s)
- **Scrolling travado**

### Regra

Aplicar **Performance Optimization**:

1. Const constructors sempre que possível
2. ListView.builder para listas longas
3. CachedNetworkImage para imagens
4. Lazy loading de dados
5. Profile mode para testes

### Justificativa

**Benefícios mensuráveis:**

- **FPS 60** constante (sem jank)
- **Startup 70% mais rápido**
- **Memória 50% menor**
- **Bateria 40% mais duração**
- **UX 100% melhor**

### Exemplos

#### ✅ Correto

```dart
// Const constructors
class UserCard extends StatelessWidget {
  final User user;

  const UserCard({Key? key, required this.user}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const Card( // Const quando possível
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: UserInfo(),
      ),
    );
  }
}

// ListView.builder para performance
class UserList extends StatelessWidget {
  final List<User> users;

  const UserList({Key? key, required this.users}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListView.builder( // Builder para lazy loading
      itemCount: users.length,
      itemBuilder: (context, index) {
        return UserCard(
          key: ValueKey(users[index].id),
          user: users[index],
        );
      },
    );
  }
}

// CachedNetworkImage
class UserAvatar extends StatelessWidget {
  final String url;

  const UserAvatar({Key? key, required this.url}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return CachedNetworkImage(
      imageUrl: url,
      placeholder: (context, url) => CircularProgressIndicator(),
      errorWidget: (context, url, error) => Icon(Icons.error),
      memCacheWidth: 200, // Limitar tamanho em memória
    );
  }
}

// Lazy loading com pagination
class UserListPage extends StatefulWidget {
  @override
  _UserListPageState createState() => _UserListPageState();
}

class _UserListPageState extends State<UserListPage> {
  final ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_onScroll);
  }

  void _onScroll() {
    if (_scrollController.position.pixels ==
        _scrollController.position.maxScrollExtent) {
      context.read<UserBloc>().add(LoadMoreUsers());
    }
  }

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      controller: _scrollController,
      itemBuilder: (context, index) => UserCard(),
    );
  }
}
```

#### ❌ Incorreto

```dart
// Sem otimização - NÃO FAZER
class UserList extends StatelessWidget {
  final List<User> users;

  UserList({required this.users}); // Sem const

  @override
  Widget build(BuildContext context) {
    return ListView( // Sem builder - carrega tudo
      children: users.map((user) {
        return Card( // Sem const
          child: Image.network(user.avatar), // Sem cache
        );
      }).toList(),
    );
  }
}
```

### Ferramentas

- **Flutter DevTools**: Performance profiler
- **cached_network_image**: Cache de imagens
- **flutter_native_splash**: Splash screen otimizado
- **Performance Overlay**: FPS monitor

### Checklist

- [ ] Const constructors usados?
- [ ] ListView.builder implementado?
- [ ] Imagens com cache?
- [ ] Lazy loading configurado?
- [ ] Testado em profile mode?

---

## 📊 Resumo das Rules

| Rule | Foco | Impacto | Prioridade |
|------|------|---------|------------|
| **FL.1** | BLoC Pattern | 40% menos bugs estado | 🔴 Alta |
| **FL.2** | Clean Architecture | 90% testes mais fáceis | 🔴 Alta |
| **FL.3** | Widget Composition | 50% performance melhor | 🔴 Alta |
| **FL.4** | Responsive Design | 100% suporte devices | 🟡 Média |
| **FL.5** | Performance | 60 FPS constante | 🔴 Alta |

---

## 🔗 Integração com Rules Atômicas

| Rule Atômica | Rule Flutter | Sinergia |
|--------------|--------------|----------|
| **01-code-style** | FL.3 | Naming + Widget Composition |
| **02-architecture** | FL.1, FL.2 | Clean Architecture + BLoC |
| **03-security** | FL.2 | Security + Repository Pattern |
| **04-testing** | FL.1, FL.2 | Testing + BLoC + Clean Arch |
| **05-performance** | FL.5 | Performance + Optimization |
| **06-documentation** | FL.2 | Documentation + Architecture |

---

## ✅ Checklist de Conformidade Flutter

### Setup Inicial

- [ ] Flutter SDK instalado?
- [ ] flutter_bloc configurado?
- [ ] get_it configurado?
- [ ] Clean Architecture implementada?
- [ ] DevTools instalado?

### Desenvolvimento

- [ ] BLoC para state management?
- [ ] 4 camadas implementadas?
- [ ] Widgets < 100 linhas?
- [ ] Responsive design implementado?
- [ ] Performance otimizada?

### Qualidade

- [ ] Testes com coverage > 80%?
- [ ] 60 FPS em profile mode?
- [ ] Suporte a múltiplos devices?
- [ ] Const constructors usados?
- [ ] Análise estática passa?

---

**Versão:** 1.0
**Próxima revisão:** 19/01/2026
**Mantido por:** Vander Loto - CTO DATAMETRIA
