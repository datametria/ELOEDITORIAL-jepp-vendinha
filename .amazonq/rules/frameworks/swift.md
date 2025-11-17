# Swift/Xcode Framework Rules - DATAMETRIA Standards

**Versão:** 1.0.0
**Data:** 07/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 1: SwiftUI + Combine Obrigatório

### Contexto

UIKit é legado. SwiftUI oferece UI declarativa, preview em tempo real e integração nativa com Swift Concurrency.

### Regra

Projetos iOS DEVEM usar:
- **SwiftUI**: UI declarativa (iOS 17+)
- **Combine**: Programação reativa
- **Swift Concurrency**: async/await para operações assíncronas
- **@StateObject/@ObservedObject**: Gerenciamento de estado

### Justificativa

- SwiftUI reduz código em 40% vs UIKit
- Preview em tempo real acelera desenvolvimento
- Combine elimina callback hell
- async/await simplifica código assíncrono

### Exemplos

#### ✅ Correto (SwiftUI + Combine)

```swift
import SwiftUI
import Combine

// ViewModel com Combine
@MainActor
class UserViewModel: ObservableObject {
    @Published var users: [User] = []
    @Published var isLoading = false
    @Published var errorMessage: String?

    private let repository: UserRepository
    private var cancellables = Set<AnyCancellable>()

    init(repository: UserRepository = UserRepositoryImpl()) {
        self.repository = repository
    }

    func fetchUsers() async {
        isLoading = true
        defer { isLoading = false }

        do {
            users = try await repository.fetchUsers()
        } catch {
            errorMessage = error.localizedDescription
        }
    }
}

// View SwiftUI
struct UserListView: View {
    @StateObject private var viewModel = UserViewModel()

    var body: some View {
        NavigationStack {
            List(viewModel.users) { user in
                UserRow(user: user)
            }
            .navigationTitle("Users")
            .task {
                await viewModel.fetchUsers()
            }
            .overlay {
                if viewModel.isLoading {
                    ProgressView()
                }
            }
            .alert("Error", isPresented: .constant(viewModel.errorMessage != nil)) {
                Button("OK") { viewModel.errorMessage = nil }
            } message: {
                Text(viewModel.errorMessage ?? "")
            }
        }
    }
}
```

#### ❌ Incorreto (UIKit)

```swift
// UIKit - legado
class UserViewController: UIViewController {
    var tableView: UITableView!
    var users: [User] = []

    override func viewDidLoad() {
        super.viewDidLoad()
        setupTableView()
        fetchUsers()
    }

    func fetchUsers() {
        // Callback hell
        repository.fetchUsers { [weak self] result in
            DispatchQueue.main.async {
                switch result {
                case .success(let users):
                    self?.users = users
                    self?.tableView.reloadData()
                case .failure(let error):
                    self?.showError(error)
                }
            }
        }
    }
}
```

### Ferramentas

- **Xcode Previews**: Preview em tempo real
- **SwiftLint**: Linting de código Swift
- **Swift Package Manager**: Gerenciamento de dependências

### Checklist

- [ ] SwiftUI para todas as views?
- [ ] Combine para streams de dados?
- [ ] async/await para operações assíncronas?
- [ ] @Published para propriedades observáveis?

---

## Rule 2: MVVM + Clean Architecture

### Contexto

MVC tradicional resulta em Massive View Controllers. MVVM separa lógica de apresentação da view.

### Regra

Arquitetura DEVE seguir:
- **Model**: Entidades de domínio (structs Codable)
- **View**: SwiftUI views (sem lógica de negócio)
- **ViewModel**: Lógica de apresentação (@ObservableObject)
- **Repository**: Abstração de dados (protocol)
- **UseCase**: Lógica de negócio

### Justificativa

- Separação clara de responsabilidades
- Testabilidade 90% maior
- Reutilização de ViewModels
- Manutenção facilitada

### Exemplos

#### ✅ Correto (MVVM + Clean Architecture)

```swift
// Domain/Models/User.swift
struct User: Codable, Identifiable {
    let id: UUID
    let name: String
    let email: String
}

// Domain/Repositories/UserRepository.swift
protocol UserRepository {
    func fetchUsers() async throws -> [User]
    func fetchUser(id: UUID) async throws -> User
    func createUser(_ user: User) async throws -> User
}

// Data/Repositories/UserRepositoryImpl.swift
class UserRepositoryImpl: UserRepository {
    private let remoteDataSource: RemoteDataSource
    private let localDataSource: LocalDataSource

    init(
        remoteDataSource: RemoteDataSource = RemoteDataSourceImpl(),
        localDataSource: LocalDataSource = LocalDataSourceImpl()
    ) {
        self.remoteDataSource = remoteDataSource
        self.localDataSource = localDataSource
    }

    func fetchUsers() async throws -> [User] {
        do {
            let users = try await remoteDataSource.fetchUsers()
            try await localDataSource.saveUsers(users)
            return users
        } catch {
            // Fallback to local cache
            return try await localDataSource.fetchUsers()
        }
    }

    func fetchUser(id: UUID) async throws -> User {
        try await remoteDataSource.fetchUser(id: id)
    }

    func createUser(_ user: User) async throws -> User {
        try await remoteDataSource.createUser(user)
    }
}

// Presentation/ViewModels/UserViewModel.swift
@MainActor
class UserViewModel: ObservableObject {
    @Published var users: [User] = []
    @Published var isLoading = false
    @Published var errorMessage: String?

    private let repository: UserRepository

    init(repository: UserRepository = UserRepositoryImpl()) {
        self.repository = repository
    }

    func fetchUsers() async {
        isLoading = true
        defer { isLoading = false }

        do {
            users = try await repository.fetchUsers()
        } catch {
            errorMessage = error.localizedDescription
        }
    }
}

// Presentation/Views/UserListView.swift
struct UserListView: View {
    @StateObject private var viewModel = UserViewModel()

    var body: some View {
        NavigationStack {
            List(viewModel.users) { user in
                UserRow(user: user)
            }
            .navigationTitle("Users")
            .task {
                await viewModel.fetchUsers()
            }
        }
    }
}
```

#### ❌ Incorreto (Lógica na View)

```swift
struct UserListView: View {
    @State private var users: [User] = []

    var body: some View {
        List(users) { user in
            Text(user.name)
        }
        .task {
            // Lógica de negócio na view - ERRADO!
            let url = URL(string: "https://api.example.com/users")!
            let (data, _) = try! await URLSession.shared.data(from: url)
            users = try! JSONDecoder().decode([User].self, from: data)
        }
    }
}
```

### Checklist

- [ ] Models são structs Codable?
- [ ] ViewModels são @ObservableObject?
- [ ] Views não têm lógica de negócio?
- [ ] Repositories são protocols?

---

## Rule 3: Swift Concurrency (async/await)

### Contexto

Completion handlers causam callback hell e são propensos a erros. Swift Concurrency simplifica código assíncrono.

### Regra

Operações assíncronas DEVEM usar:
- **async/await**: Para operações assíncronas
- **Task**: Para iniciar trabalho assíncrono
- **Actor**: Para sincronização thread-safe
- **@MainActor**: Para operações de UI

### Justificativa

- Código 60% mais legível
- Menos erros de threading
- Cancelamento automático
- Integração nativa com SwiftUI

### Exemplos

#### ✅ Correto (async/await)

```swift
// NetworkManager com async/await
actor NetworkManager {
    static let shared = NetworkManager()

    private let session: URLSession

    private init() {
        let config = URLSessionConfiguration.default
        config.timeoutIntervalForRequest = 30
        self.session = URLSession(configuration: config)
    }

    func request<T: Decodable>(_ endpoint: APIEndpoint) async throws -> T {
        let request = URLRequest(url: endpoint.url)
        let (data, response) = try await session.data(for: request)

        guard let httpResponse = response as? HTTPURLResponse,
              (200...299).contains(httpResponse.statusCode) else {
            throw NetworkError.invalidResponse
        }

        return try JSONDecoder().decode(T.self, from: data)
    }
}

// ViewModel usando async/await
@MainActor
class UserViewModel: ObservableObject {
    @Published var users: [User] = []

    func fetchUsers() async {
        do {
            users = try await NetworkManager.shared.request(.users)
        } catch {
            print("Error: \(error)")
        }
    }
}

// View com Task
struct UserListView: View {
    @StateObject private var viewModel = UserViewModel()

    var body: some View {
        List(viewModel.users) { user in
            Text(user.name)
        }
        .task {
            await viewModel.fetchUsers()
        }
    }
}
```

#### ❌ Incorreto (Completion handlers)

```swift
// Callback hell
class NetworkManager {
    func fetchUsers(completion: @escaping (Result<[User], Error>) -> Void) {
        let url = URL(string: "https://api.example.com/users")!
        URLSession.shared.dataTask(with: url) { data, response, error in
            if let error = error {
                completion(.failure(error))
                return
            }

            guard let data = data else {
                completion(.failure(NetworkError.noData))
                return
            }

            do {
                let users = try JSONDecoder().decode([User].self, from: data)
                completion(.success(users))
            } catch {
                completion(.failure(error))
            }
        }.resume()
    }
}
```

### Checklist

- [ ] Operações de rede usam async/await?
- [ ] ViewModels marcados com @MainActor?
- [ ] Actors para sincronização thread-safe?
- [ ] Task para iniciar trabalho assíncrono?

---

## Rule 4: SwiftData / Core Data para Persistência

### Contexto

Persistência local é essencial para apps offline-first. SwiftData (iOS 17+) simplifica Core Data.

### Regra

Persistência local DEVE usar:
- **SwiftData**: Para iOS 17+ (preferencial)
- **Core Data**: Para iOS < 17
- **@Model**: Para entidades SwiftData
- **ModelContext**: Para operações CRUD

### Justificativa

- SwiftData reduz boilerplate em 70%
- Type-safe queries
- Integração nativa com SwiftUI
- Migrations automáticas

### Exemplos

#### ✅ Correto (SwiftData)

```swift
import SwiftData

// Model
@Model
class User {
    @Attribute(.unique) var id: UUID
    var name: String
    var email: String
    var createdAt: Date

    init(id: UUID = UUID(), name: String, email: String) {
        self.id = id
        self.name = name
        self.email = email
        self.createdAt = Date()
    }
}

// App setup
@main
struct MyApp: App {
    var body: some Scene {
        WindowGroup {
            ContentView()
        }
        .modelContainer(for: User.self)
    }
}

// Repository
class UserLocalDataSource {
    private let modelContext: ModelContext

    init(modelContext: ModelContext) {
        self.modelContext = modelContext
    }

    func fetchUsers() throws -> [User] {
        let descriptor = FetchDescriptor<User>(
            sortBy: [SortDescriptor(\.createdAt, order: .reverse)]
        )
        return try modelContext.fetch(descriptor)
    }

    func saveUser(_ user: User) throws {
        modelContext.insert(user)
        try modelContext.save()
    }

    func deleteUser(_ user: User) throws {
        modelContext.delete(user)
        try modelContext.save()
    }
}

// View
struct UserListView: View {
    @Environment(\.modelContext) private var modelContext
    @Query(sort: \.createdAt, order: .reverse) private var users: [User]

    var body: some View {
        List(users) { user in
            Text(user.name)
        }
    }
}
```

#### ❌ Incorreto (UserDefaults para dados complexos)

```swift
// UserDefaults não é adequado para dados complexos
class UserStorage {
    func saveUsers(_ users: [User]) {
        let data = try! JSONEncoder().encode(users)
        UserDefaults.standard.set(data, forKey: "users")
    }

    func fetchUsers() -> [User] {
        guard let data = UserDefaults.standard.data(forKey: "users") else {
            return []
        }
        return try! JSONDecoder().decode([User].self, from: data)
    }
}
```

### Checklist

- [ ] SwiftData para iOS 17+?
- [ ] @Model para entidades?
- [ ] ModelContext para operações CRUD?
- [ ] @Query para queries reativas?

---

## Rule 5: Dependency Injection com Protocols

### Contexto

Dependências hardcoded dificultam testes e manutenção. DI permite injeção de mocks.

### Regra

Dependências DEVEM ser injetadas via:
- **Protocols**: Para abstrações
- **Initializer injection**: Injeção via construtor
- **@EnvironmentObject**: Para dependências globais
- **Container**: Para gerenciamento centralizado (opcional)

### Justificativa

- Testes com mocks triviais
- Desacoplamento de componentes
- Flexibilidade de implementação
- Inversão de controle

### Exemplos

#### ✅ Correto (DI com Protocols)

```swift
// Protocol
protocol UserRepository {
    func fetchUsers() async throws -> [User]
}

// Implementação real
class UserRepositoryImpl: UserRepository {
    private let networkManager: NetworkManager

    init(networkManager: NetworkManager = .shared) {
        self.networkManager = networkManager
    }

    func fetchUsers() async throws -> [User] {
        try await networkManager.request(.users)
    }
}

// Mock para testes
class MockUserRepository: UserRepository {
    var mockUsers: [User] = []
    var shouldThrowError = false

    func fetchUsers() async throws -> [User] {
        if shouldThrowError {
            throw NetworkError.serverError
        }
        return mockUsers
    }
}

// ViewModel com DI
@MainActor
class UserViewModel: ObservableObject {
    @Published var users: [User] = []

    private let repository: UserRepository

    init(repository: UserRepository = UserRepositoryImpl()) {
        self.repository = repository
    }

    func fetchUsers() async {
        do {
            users = try await repository.fetchUsers()
        } catch {
            print("Error: \(error)")
        }
    }
}

// Teste
class UserViewModelTests: XCTestCase {
    func testFetchUsers() async {
        // Arrange
        let mockRepo = MockUserRepository()
        mockRepo.mockUsers = [
            User(name: "John", email: "john@example.com")
        ]
        let viewModel = UserViewModel(repository: mockRepo)

        // Act
        await viewModel.fetchUsers()

        // Assert
        XCTAssertEqual(viewModel.users.count, 1)
        XCTAssertEqual(viewModel.users.first?.name, "John")
    }
}
```

#### ❌ Incorreto (Dependências hardcoded)

```swift
class UserViewModel: ObservableObject {
    @Published var users: [User] = []

    // Dependência hardcoded - impossível testar com mock
    private let repository = UserRepositoryImpl()

    func fetchUsers() async {
        do {
            users = try await repository.fetchUsers()
        } catch {
            print("Error: \(error)")
        }
    }
}
```

### Ferramentas

- **Resolver**: DI container para Swift
- **Swinject**: DI framework
- **Factory**: Lightweight DI

### Checklist

- [ ] Dependências são protocols?
- [ ] Injeção via construtor?
- [ ] Mocks para testes?
- [ ] Container DI (se necessário)?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| SwiftUI usage | 100% | Code review |
| MVVM architecture | 100% | Architecture tests |
| async/await usage | 100% async ops | Code review |
| SwiftData/Core Data | 100% persistence | Code review |
| Dependency Injection | 95% | Unit tests coverage |

---

**Próxima revisão:** 07/02/2026
