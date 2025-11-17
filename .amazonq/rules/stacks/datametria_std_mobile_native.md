# 📱 DATAMETRIA Standards - Mobile Native Development

**Versão:** 1.0.0 | **Última Atualização:** 07/11/2025 | **Autor:** Vander Loto - CTO DATAMETRIA

<div align="center">

![DATAMETRIA Mobile Native](https://img.shields.io/badge/DATAMETRIA-Mobile%20Native-blue?style=for-the-badge)

## Diretrizes para Desenvolvimento Mobile Nativo (iOS + Android)

[![iOS](https://img.shields.io/badge/iOS-17+-black)](https://developer.apple.com/ios/)
[![Android](https://img.shields.io/badge/Android-14+-green)](https://developer.android.com/)
[![Swift](https://img.shields.io/badge/Swift-5.9+-orange)](https://swift.org/)
[![Kotlin](https://img.shields.io/badge/Kotlin-1.9+-purple)](https://kotlinlang.org/)
[![Version](https://img.shields.io/badge/version-1.0.0-blue)](https://github.com/datametria/DATAMETRIA-standards)

[🎯 Visão Geral](#-visão-geral) • [🏗️ Arquitetura](#️-arquitetura) • [📱 iOS](#-desenvolvimento-ios) • [🤖 Android](#-desenvolvimento-android) • [🔄 Sincronização](#-sincronização-entre-plataformas) • [✅ Checklist](#-checklist)

</div>

---

## 🎯 Visão Geral

### Filosofia Mobile Native

**Mobile Native** oferece:
- **Performance máxima**: Acesso direto às APIs nativas
- **UX superior**: Componentes nativos e gestos da plataforma
- **Recursos avançados**: Câmera, sensores, ARKit/ARCore
- **Integração profunda**: Widgets, Siri/Google Assistant, Shortcuts

### Quando Usar Native vs Cross-Platform

| Critério | Native | Flutter | React Native |
|----------|--------|---------|--------------|
| **Performance** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| **UX Nativa** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Recursos Avançados** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Velocidade Dev** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Custo** | ⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐ |

**Use Native quando:**
- App exige performance máxima (jogos, AR/VR)
- Integração profunda com plataforma (widgets, extensions)
- Recursos específicos da plataforma (ARKit, HealthKit)
- Equipe especializada em iOS/Android

---

## 🏗️ Arquitetura

### Clean Architecture Mobile

```
┌─────────────────────────────────────┐
│         Presentation Layer          │  ← SwiftUI/Jetpack Compose
├─────────────────────────────────────┤
│          Domain Layer               │  ← Use Cases, Entities
├─────────────────────────────────────┤
│           Data Layer                │  ← Repositories, Data Sources
├─────────────────────────────────────┤
│        Infrastructure Layer         │  ← Network, Database, Storage
└─────────────────────────────────────┘
```

### Padrões Arquiteturais

#### MVVM (Model-View-ViewModel)

**iOS (SwiftUI + Combine)**
```swift
// Model
struct User: Codable, Identifiable {
    let id: UUID
    let name: String
    let email: String
}

// ViewModel
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

// View
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
        }
    }
}
```

**Android (Jetpack Compose + ViewModel)**
```kotlin
// Model
data class User(
    val id: String,
    val name: String,
    val email: String
)

// ViewModel
class UserViewModel(
    private val repository: UserRepository = UserRepositoryImpl()
) : ViewModel() {

    private val _uiState = MutableStateFlow<UserUiState>(UserUiState.Loading)
    val uiState: StateFlow<UserUiState> = _uiState.asStateFlow()

    init {
        fetchUsers()
    }

    fun fetchUsers() {
        viewModelScope.launch {
            _uiState.value = UserUiState.Loading
            try {
                val users = repository.fetchUsers()
                _uiState.value = UserUiState.Success(users)
            } catch (e: Exception) {
                _uiState.value = UserUiState.Error(e.message ?: "Unknown error")
            }
        }
    }
}

sealed class UserUiState {
    object Loading : UserUiState()
    data class Success(val users: List<User>) : UserUiState()
    data class Error(val message: String) : UserUiState()
}

// View
@Composable
fun UserListScreen(
    viewModel: UserViewModel = viewModel()
) {
    val uiState by viewModel.uiState.collectAsState()

    Scaffold(
        topBar = {
            TopAppBar(title = { Text("Users") })
        }
    ) { padding ->
        when (val state = uiState) {
            is UserUiState.Loading -> {
                Box(Modifier.fillMaxSize()) {
                    CircularProgressIndicator(Modifier.align(Alignment.Center))
                }
            }
            is UserUiState.Success -> {
                LazyColumn(Modifier.padding(padding)) {
                    items(state.users) { user ->
                        UserRow(user = user)
                    }
                }
            }
            is UserUiState.Error -> {
                ErrorView(message = state.message)
            }
        }
    }
}
```

---

## 📱 Desenvolvimento iOS

### Stack Tecnológico iOS

| Componente | Tecnologia | Versão Mínima |
|------------|------------|---------------|
| **Linguagem** | Swift | 5.9+ |
| **UI Framework** | SwiftUI | iOS 17+ |
| **Async** | Swift Concurrency | async/await |
| **Networking** | URLSession + Async | Nativo |
| **Database** | SwiftData / Core Data | iOS 17+ |
| **DI** | Swift DI / Resolver | - |
| **Testing** | XCTest + XCUITest | Nativo |

### Estrutura de Projeto iOS

```
MyApp/
├── App/
│   ├── MyAppApp.swift              # Entry point
│   └── AppDelegate.swift           # App lifecycle
├── Core/
│   ├── Network/
│   │   ├── NetworkManager.swift
│   │   └── APIEndpoint.swift
│   ├── Storage/
│   │   └── StorageManager.swift
│   └── DI/
│       └── DependencyContainer.swift
├── Domain/
│   ├── Models/
│   │   └── User.swift
│   ├── UseCases/
│   │   └── FetchUsersUseCase.swift
│   └── Repositories/
│       └── UserRepository.swift
├── Data/
│   ├── Repositories/
│   │   └── UserRepositoryImpl.swift
│   └── DataSources/
│       ├── RemoteDataSource.swift
│       └── LocalDataSource.swift
├── Presentation/
│   ├── Screens/
│   │   ├── Home/
│   │   │   ├── HomeView.swift
│   │   │   └── HomeViewModel.swift
│   │   └── Profile/
│   │       ├── ProfileView.swift
│   │       └── ProfileViewModel.swift
│   └── Components/
│       └── CustomButton.swift
├── Resources/
│   ├── Assets.xcassets
│   └── Localizable.strings
└── Tests/
    ├── UnitTests/
    └── UITests/
```

### Networking iOS

```swift
// APIEndpoint.swift
enum APIEndpoint {
    case users
    case user(id: String)
    case createUser

    var url: URL {
        let baseURL = "https://api.example.com/v1"
        switch self {
        case .users:
            return URL(string: "\(baseURL)/users")!
        case .user(let id):
            return URL(string: "\(baseURL)/users/\(id)")!
        case .createUser:
            return URL(string: "\(baseURL)/users")!
        }
    }

    var method: String {
        switch self {
        case .users, .user:
            return "GET"
        case .createUser:
            return "POST"
        }
    }
}

// NetworkManager.swift
actor NetworkManager {
    static let shared = NetworkManager()

    private let session: URLSession
    private let decoder: JSONDecoder

    private init() {
        let config = URLSessionConfiguration.default
        config.timeoutIntervalForRequest = 30
        config.waitsForConnectivity = true
        self.session = URLSession(configuration: config)
        self.decoder = JSONDecoder()
        decoder.keyDecodingStrategy = .convertFromSnakeCase
    }

    func request<T: Decodable>(
        _ endpoint: APIEndpoint,
        body: Encodable? = nil
    ) async throws -> T {
        var request = URLRequest(url: endpoint.url)
        request.httpMethod = endpoint.method
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")

        if let body = body {
            request.httpBody = try JSONEncoder().encode(body)
        }

        let (data, response) = try await session.data(for: request)

        guard let httpResponse = response as? HTTPURLResponse else {
            throw NetworkError.invalidResponse
        }

        guard (200...299).contains(httpResponse.statusCode) else {
            throw NetworkError.httpError(statusCode: httpResponse.statusCode)
        }

        return try decoder.decode(T.self, from: data)
    }
}

enum NetworkError: LocalizedError {
    case invalidResponse
    case httpError(statusCode: Int)

    var errorDescription: String? {
        switch self {
        case .invalidResponse:
            return "Invalid server response"
        case .httpError(let code):
            return "HTTP error: \(code)"
        }
    }
}
```

### Persistência iOS (SwiftData)

```swift
import SwiftData

@Model
final class UserModel {
    @Attribute(.unique) var id: String
    var name: String
    var email: String
    var createdAt: Date

    init(id: String, name: String, email: String) {
        self.id = id
        self.name = name
        self.email = email
        self.createdAt = Date()
    }
}

// Repository
protocol LocalUserRepository {
    func save(_ user: User) async throws
    func fetchAll() async throws -> [User]
    func delete(id: String) async throws
}

actor LocalUserRepositoryImpl: LocalUserRepository {
    private let modelContext: ModelContext

    init(modelContext: ModelContext) {
        self.modelContext = modelContext
    }

    func save(_ user: User) async throws {
        let userModel = UserModel(
            id: user.id.uuidString,
            name: user.name,
            email: user.email
        )
        modelContext.insert(userModel)
        try modelContext.save()
    }

    func fetchAll() async throws -> [User] {
        let descriptor = FetchDescriptor<UserModel>(
            sortBy: [SortDescriptor(\.createdAt, order: .reverse)]
        )
        let models = try modelContext.fetch(descriptor)
        return models.map { User(id: UUID(uuidString: $0.id)!, name: $0.name, email: $0.email) }
    }

    func delete(id: String) async throws {
        let predicate = #Predicate<UserModel> { $0.id == id }
        try modelContext.delete(model: UserModel.self, where: predicate)
    }
}
```

---

## 🤖 Desenvolvimento Android

### Stack Tecnológico Android

| Componente | Tecnologia | Versão Mínima |
|------------|------------|---------------|
| **Linguagem** | Kotlin | 1.9+ |
| **UI Framework** | Jetpack Compose | 1.5+ |
| **Async** | Coroutines + Flow | 1.7+ |
| **Networking** | Retrofit + OkHttp | 2.9+ |
| **Database** | Room | 2.6+ |
| **DI** | Hilt (Dagger) | 2.48+ |
| **Testing** | JUnit + Espresso | - |

### Estrutura de Projeto Android

```
app/
├── src/
│   ├── main/
│   │   ├── java/com/datametria/myapp/
│   │   │   ├── MyApplication.kt
│   │   │   ├── core/
│   │   │   │   ├── network/
│   │   │   │   │   ├── NetworkModule.kt
│   │   │   │   │   └── ApiService.kt
│   │   │   │   ├── database/
│   │   │   │   │   ├── AppDatabase.kt
│   │   │   │   │   └── DatabaseModule.kt
│   │   │   │   └── di/
│   │   │   │       └── AppModule.kt
│   │   │   ├── domain/
│   │   │   │   ├── model/
│   │   │   │   │   └── User.kt
│   │   │   │   ├── usecase/
│   │   │   │   │   └── GetUsersUseCase.kt
│   │   │   │   └── repository/
│   │   │   │       └── UserRepository.kt
│   │   │   ├── data/
│   │   │   │   ├── repository/
│   │   │   │   │   └── UserRepositoryImpl.kt
│   │   │   │   ├── remote/
│   │   │   │   │   └── UserRemoteDataSource.kt
│   │   │   │   └── local/
│   │   │   │       ├── UserDao.kt
│   │   │   │       └── UserEntity.kt
│   │   │   └── presentation/
│   │   │       ├── home/
│   │   │       │   ├── HomeScreen.kt
│   │   │       │   └── HomeViewModel.kt
│   │   │       └── profile/
│   │   │           ├── ProfileScreen.kt
│   │   │           └── ProfileViewModel.kt
│   │   └── res/
│   │       ├── values/
│   │       │   ├── strings.xml
│   │       │   └── themes.xml
│   │       └── drawable/
│   └── test/
│       └── java/com/datametria/myapp/
└── build.gradle.kts
```

### Networking Android

```kotlin
// ApiService.kt
interface ApiService {
    @GET("users")
    suspend fun getUsers(): List<UserDto>

    @GET("users/{id}")
    suspend fun getUser(@Path("id") id: String): UserDto

    @POST("users")
    suspend fun createUser(@Body user: CreateUserRequest): UserDto
}

// NetworkModule.kt
@Module
@InstallIn(SingletonComponent::class)
object NetworkModule {

    @Provides
    @Singleton
    fun provideOkHttpClient(): OkHttpClient {
        return OkHttpClient.Builder()
            .connectTimeout(30, TimeUnit.SECONDS)
            .readTimeout(30, TimeUnit.SECONDS)
            .addInterceptor(HttpLoggingInterceptor().apply {
                level = if (BuildConfig.DEBUG) {
                    HttpLoggingInterceptor.Level.BODY
                } else {
                    HttpLoggingInterceptor.Level.NONE
                }
            })
            .build()
    }

    @Provides
    @Singleton
    fun provideRetrofit(okHttpClient: OkHttpClient): Retrofit {
        return Retrofit.Builder()
            .baseUrl("https://api.example.com/v1/")
            .client(okHttpClient)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }

    @Provides
    @Singleton
    fun provideApiService(retrofit: Retrofit): ApiService {
        return retrofit.create(ApiService::class.java)
    }
}

// UserRemoteDataSource.kt
class UserRemoteDataSource @Inject constructor(
    private val apiService: ApiService
) {
    suspend fun getUsers(): Result<List<User>> = runCatching {
        apiService.getUsers().map { it.toDomain() }
    }

    suspend fun getUser(id: String): Result<User> = runCatching {
        apiService.getUser(id).toDomain()
    }
}
```

### Persistência Android (Room)

```kotlin
// UserEntity.kt
@Entity(tableName = "users")
data class UserEntity(
    @PrimaryKey val id: String,
    @ColumnInfo(name = "name") val name: String,
    @ColumnInfo(name = "email") val email: String,
    @ColumnInfo(name = "created_at") val createdAt: Long = System.currentTimeMillis()
)

// UserDao.kt
@Dao
interface UserDao {
    @Query("SELECT * FROM users ORDER BY created_at DESC")
    fun getAllUsers(): Flow<List<UserEntity>>

    @Query("SELECT * FROM users WHERE id = :id")
    suspend fun getUserById(id: String): UserEntity?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertUser(user: UserEntity)

    @Delete
    suspend fun deleteUser(user: UserEntity)

    @Query("DELETE FROM users WHERE id = :id")
    suspend fun deleteUserById(id: String)
}

// AppDatabase.kt
@Database(
    entities = [UserEntity::class],
    version = 1,
    exportSchema = true
)
abstract class AppDatabase : RoomDatabase() {
    abstract fun userDao(): UserDao
}

// DatabaseModule.kt
@Module
@InstallIn(SingletonComponent::class)
object DatabaseModule {

    @Provides
    @Singleton
    fun provideDatabase(@ApplicationContext context: Context): AppDatabase {
        return Room.databaseBuilder(
            context,
            AppDatabase::class.java,
            "datametria_db"
        )
            .fallbackToDestructiveMigration()
            .build()
    }

    @Provides
    fun provideUserDao(database: AppDatabase): UserDao {
        return database.userDao()
    }
}

// UserLocalDataSource.kt
class UserLocalDataSource @Inject constructor(
    private val userDao: UserDao
) {
    fun getAllUsers(): Flow<List<User>> {
        return userDao.getAllUsers().map { entities ->
            entities.map { it.toDomain() }
        }
    }

    suspend fun saveUser(user: User) {
        userDao.insertUser(user.toEntity())
    }

    suspend fun deleteUser(id: String) {
        userDao.deleteUserById(id)
    }
}
```

---

## 🔄 Sincronização entre Plataformas

### Shared Business Logic

**Kotlin Multiplatform Mobile (KMM)**

```kotlin
// shared/src/commonMain/kotlin/
expect class Platform() {
    val name: String
}

// Shared Repository Interface
interface UserRepository {
    suspend fun getUsers(): Result<List<User>>
    suspend fun getUser(id: String): Result<User>
    suspend fun saveUser(user: User): Result<Unit>
}

// Shared Use Case
class GetUsersUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(): Result<List<User>> {
        return repository.getUsers()
    }
}

// shared/src/iosMain/kotlin/
actual class Platform actual constructor() {
    actual val name: String = "iOS ${UIDevice.currentDevice.systemVersion}"
}

// shared/src/androidMain/kotlin/
actual class Platform actual constructor() {
    actual val name: String = "Android ${android.os.Build.VERSION.SDK_INT}"
}
```

### API Contract Sharing

```yaml
# openapi.yaml (compartilhado)
openapi: 3.0.0
info:
  title: MyApp API
  version: 1.0.0
paths:
  /users:
    get:
      summary: Get all users
      responses:
        '200':
          description: Success
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/User'
components:
  schemas:
    User:
      type: object
      required:
        - id
        - name
        - email
      properties:
        id:
          type: string
          format: uuid
        name:
          type: string
        email:
          type: string
          format: email
```

---

## ✅ Checklist

### iOS Development

- [ ] Swift 5.9+ com async/await
- [ ] SwiftUI para UI (iOS 17+)
- [ ] MVVM architecture
- [ ] SwiftData/Core Data para persistência
- [ ] URLSession async para networking
- [ ] XCTest para testes (>80% coverage)
- [ ] SwiftLint configurado
- [ ] Localização (pt-BR + en)
- [ ] Dark mode suportado
- [ ] Acessibilidade (VoiceOver)

### Android Development

- [ ] Kotlin 1.9+ com Coroutines
- [ ] Jetpack Compose para UI
- [ ] MVVM + Clean Architecture
- [ ] Room para persistência
- [ ] Retrofit + OkHttp para networking
- [ ] Hilt para DI
- [ ] JUnit + Espresso (>80% coverage)
- [ ] Ktlint configurado
- [ ] Localização (pt-BR + en)
- [ ] Dark theme suportado
- [ ] TalkBack acessibilidade

### Cross-Platform

- [ ] API contracts compartilhados (OpenAPI)
- [ ] Design system consistente
- [ ] Fluxos de usuário idênticos
- [ ] Mesma lógica de negócio
- [ ] Testes E2E em ambas plataformas

---

<div align="center">

## 🎯 DATAMETRIA Mobile Native Standard v1.0.0

**Desenvolvido com ❤️ pela equipe DATAMETRIA**

[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Mobile%20Ready-blue?style=for-the-badge)](https://github.com/datametria/DATAMETRIA-standards)

**CTO**: Vander Loto | **Email**: vander.loto@datametria.io

</div>
