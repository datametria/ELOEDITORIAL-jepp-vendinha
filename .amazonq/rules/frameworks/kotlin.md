# Kotlin/Android Framework Rules - DATAMETRIA Standards

**Versão:** 1.0.0
**Data:** 07/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 1: Jetpack Compose Obrigatório

### Contexto

XML layouts são legados. Jetpack Compose oferece UI declarativa, preview em tempo real e integração nativa com Kotlin Coroutines.

### Regra

Projetos Android DEVEM usar:
- **Jetpack Compose**: UI declarativa (Android 5.0+)
- **Material 3**: Design system moderno
- **Kotlin Coroutines**: Programação assíncrona
- **StateFlow/SharedFlow**: Gerenciamento de estado reativo

### Justificativa

- Compose reduz código em 40% vs XML
- Preview em tempo real acelera desenvolvimento
- Coroutines eliminam callback hell
- StateFlow simplifica gerenciamento de estado

### Exemplos

#### ✅ Correto (Jetpack Compose)

```kotlin
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch

// ViewModel com StateFlow
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

// Composable
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
                Box(
                    modifier = Modifier.fillMaxSize(),
                    contentAlignment = Alignment.Center
                ) {
                    CircularProgressIndicator()
                }
            }
            is UserUiState.Success -> {
                LazyColumn(
                    modifier = Modifier.padding(padding)
                ) {
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

@Composable
fun UserRow(user: User) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(8.dp)
    ) {
        Column(modifier = Modifier.padding(16.dp)) {
            Text(
                text = user.name,
                style = MaterialTheme.typography.titleMedium
            )
            Text(
                text = user.email,
                style = MaterialTheme.typography.bodyMedium
            )
        }
    }
}
```

#### ❌ Incorreto (XML layouts)

```kotlin
// XML - legado
class UserActivity : AppCompatActivity() {
    private lateinit var recyclerView: RecyclerView
    private lateinit var adapter: UserAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_user)

        recyclerView = findViewById(R.id.recyclerView)
        adapter = UserAdapter()
        recyclerView.adapter = adapter

        // Callback hell
        repository.fetchUsers { result ->
            runOnUiThread {
                when (result) {
                    is Result.Success -> adapter.submitList(result.data)
                    is Result.Error -> showError(result.error)
                }
            }
        }
    }
}
```

### Ferramentas

- **Android Studio Preview**: Preview em tempo real
- **Compose Compiler**: Otimizações de performance
- **Material 3 Theme Builder**: Geração de temas

### Checklist

- [ ] Jetpack Compose para todas as telas?
- [ ] Material 3 Design System?
- [ ] StateFlow para estado reativo?
- [ ] Coroutines para operações assíncronas?

---

## Rule 2: MVVM + Clean Architecture

### Contexto

Activities/Fragments com lógica de negócio resultam em código difícil de testar. MVVM separa responsabilidades.

### Regra

Arquitetura DEVE seguir:
- **Model**: Data classes (Kotlin)
- **View**: Composables (sem lógica de negócio)
- **ViewModel**: Lógica de apresentação (StateFlow)
- **Repository**: Abstração de dados (interface)
- **UseCase**: Lógica de negócio

### Justificativa

- Separação clara de responsabilidades
- Testabilidade 90% maior
- Reutilização de ViewModels
- Manutenção facilitada

### Exemplos

#### ✅ Correto (MVVM + Clean Architecture)

```kotlin
// Domain/Models/User.kt
data class User(
    val id: String,
    val name: String,
    val email: String
)

// Domain/Repositories/UserRepository.kt
interface UserRepository {
    suspend fun fetchUsers(): List<User>
    suspend fun fetchUser(id: String): User
    suspend fun createUser(user: User): User
}

// Data/Repositories/UserRepositoryImpl.kt
class UserRepositoryImpl(
    private val remoteDataSource: RemoteDataSource = RemoteDataSourceImpl(),
    private val localDataSource: LocalDataSource = LocalDataSourceImpl()
) : UserRepository {

    override suspend fun fetchUsers(): List<User> {
        return try {
            val users = remoteDataSource.fetchUsers()
            localDataSource.saveUsers(users)
            users
        } catch (e: Exception) {
            // Fallback to local cache
            localDataSource.fetchUsers()
        }
    }

    override suspend fun fetchUser(id: String): User {
        return remoteDataSource.fetchUser(id)
    }

    override suspend fun createUser(user: User): User {
        return remoteDataSource.createUser(user)
    }
}

// Presentation/ViewModels/UserViewModel.kt
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

// Presentation/Screens/UserListScreen.kt
@Composable
fun UserListScreen(
    viewModel: UserViewModel = viewModel()
) {
    val uiState by viewModel.uiState.collectAsState()

    Scaffold(
        topBar = { TopAppBar(title = { Text("Users") }) }
    ) { padding ->
        when (val state = uiState) {
            is UserUiState.Loading -> LoadingView()
            is UserUiState.Success -> UserList(state.users, padding)
            is UserUiState.Error -> ErrorView(state.message)
        }
    }
}
```

#### ❌ Incorreto (Lógica no Composable)

```kotlin
@Composable
fun UserListScreen() {
    var users by remember { mutableStateOf<List<User>>(emptyList()) }

    LaunchedEffect(Unit) {
        // Lógica de negócio no Composable - ERRADO!
        val response = Retrofit.Builder()
            .baseUrl("https://api.example.com")
            .build()
            .create(ApiService::class.java)
            .getUsers()
        users = response.body() ?: emptyList()
    }

    LazyColumn {
        items(users) { user ->
            Text(user.name)
        }
    }
}
```

### Checklist

- [ ] Models são data classes?
- [ ] ViewModels usam StateFlow?
- [ ] Composables não têm lógica de negócio?
- [ ] Repositories são interfaces?

---

## Rule 3: Kotlin Coroutines Obrigatório

### Contexto

Callbacks causam callback hell e são propensos a erros. Coroutines simplificam código assíncrono.

### Regra

Operações assíncronas DEVEM usar:
- **suspend functions**: Para operações assíncronas
- **viewModelScope**: Para operações em ViewModels
- **Flow**: Para streams de dados
- **Dispatchers**: Para controle de threads

### Justificativa

- Código 60% mais legível
- Menos erros de threading
- Cancelamento automático
- Integração nativa com Compose

### Exemplos

#### ✅ Correto (Coroutines)

```kotlin
// NetworkManager com Coroutines
class NetworkManager(
    private val client: OkHttpClient = OkHttpClient()
) {
    private val json = Json { ignoreUnknownKeys = true }

    suspend fun <T> request(
        endpoint: APIEndpoint,
        responseType: KClass<T>
    ): T = withContext(Dispatchers.IO) {
        val request = Request.Builder()
            .url(endpoint.url)
            .method(endpoint.method, endpoint.body)
            .build()

        val response = client.newCall(request).execute()

        if (!response.isSuccessful) {
            throw NetworkException("HTTP ${response.code}")
        }

        val body = response.body?.string() ?: throw NetworkException("Empty body")
        json.decodeFromString(responseType.serializer(), body) as T
    }
}

// ViewModel usando Coroutines
class UserViewModel(
    private val repository: UserRepository = UserRepositoryImpl()
) : ViewModel() {

    private val _users = MutableStateFlow<List<User>>(emptyList())
    val users: StateFlow<List<User>> = _users.asStateFlow()

    fun fetchUsers() {
        viewModelScope.launch {
            try {
                _users.value = repository.fetchUsers()
            } catch (e: Exception) {
                // Handle error
            }
        }
    }

    // Flow para updates em tempo real
    val userUpdates: Flow<User> = repository.observeUserUpdates()
        .flowOn(Dispatchers.IO)
}

// Composable com LaunchedEffect
@Composable
fun UserListScreen(viewModel: UserViewModel = viewModel()) {
    val users by viewModel.users.collectAsState()

    LaunchedEffect(Unit) {
        viewModel.fetchUsers()
    }

    LazyColumn {
        items(users) { user ->
            Text(user.name)
        }
    }
}
```

#### ❌ Incorreto (Callbacks)

```kotlin
// Callback hell
class NetworkManager {
    fun fetchUsers(callback: (Result<List<User>>) -> Unit) {
        Thread {
            try {
                val response = client.newCall(request).execute()
                val users = parseUsers(response.body?.string())
                callback(Result.success(users))
            } catch (e: Exception) {
                callback(Result.failure(e))
            }
        }.start()
    }
}
```

### Checklist

- [ ] Operações de rede usam suspend functions?
- [ ] ViewModels usam viewModelScope?
- [ ] Flow para streams de dados?
- [ ] Dispatchers apropriados?

---

## Rule 4: Room Database para Persistência

### Contexto

Persistência local é essencial para apps offline-first. Room simplifica SQLite com type-safety.

### Regra

Persistência local DEVE usar:
- **Room**: Database abstraction layer
- **@Entity**: Para entidades
- **@Dao**: Para operações CRUD
- **Flow**: Para queries reativas

### Justificativa

- Type-safe queries
- Compile-time verification
- Integração com Coroutines/Flow
- Migrations automáticas

### Exemplos

#### ✅ Correto (Room)

```kotlin
// Entity
@Entity(tableName = "users")
data class UserEntity(
    @PrimaryKey val id: String,
    @ColumnInfo(name = "name") val name: String,
    @ColumnInfo(name = "email") val email: String,
    @ColumnInfo(name = "created_at") val createdAt: Long = System.currentTimeMillis()
)

// DAO
@Dao
interface UserDao {
    @Query("SELECT * FROM users ORDER BY created_at DESC")
    fun observeUsers(): Flow<List<UserEntity>>

    @Query("SELECT * FROM users WHERE id = :id")
    suspend fun getUser(id: String): UserEntity?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertUser(user: UserEntity)

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertUsers(users: List<UserEntity>)

    @Delete
    suspend fun deleteUser(user: UserEntity)

    @Query("DELETE FROM users")
    suspend fun deleteAllUsers()
}

// Database
@Database(
    entities = [UserEntity::class],
    version = 1,
    exportSchema = true
)
abstract class AppDatabase : RoomDatabase() {
    abstract fun userDao(): UserDao

    companion object {
        @Volatile
        private var INSTANCE: AppDatabase? = null

        fun getInstance(context: Context): AppDatabase {
            return INSTANCE ?: synchronized(this) {
                val instance = Room.databaseBuilder(
                    context.applicationContext,
                    AppDatabase::class.java,
                    "app_database"
                )
                .fallbackToDestructiveMigration()
                .build()
                INSTANCE = instance
                instance
            }
        }
    }
}

// LocalDataSource
class UserLocalDataSource(
    private val userDao: UserDao
) {
    fun observeUsers(): Flow<List<User>> {
        return userDao.observeUsers()
            .map { entities -> entities.map { it.toDomain() } }
    }

    suspend fun getUser(id: String): User? {
        return userDao.getUser(id)?.toDomain()
    }

    suspend fun saveUser(user: User) {
        userDao.insertUser(user.toEntity())
    }

    suspend fun saveUsers(users: List<User>) {
        userDao.insertUsers(users.map { it.toEntity() })
    }
}

// Mappers
fun UserEntity.toDomain() = User(
    id = id,
    name = name,
    email = email
)

fun User.toEntity() = UserEntity(
    id = id,
    name = name,
    email = email
)
```

#### ❌ Incorreto (SharedPreferences para dados complexos)

```kotlin
// SharedPreferences não é adequado para dados complexos
class UserStorage(private val prefs: SharedPreferences) {
    fun saveUsers(users: List<User>) {
        val json = Json.encodeToString(users)
        prefs.edit().putString("users", json).apply()
    }

    fun getUsers(): List<User> {
        val json = prefs.getString("users", "[]") ?: "[]"
        return Json.decodeFromString(json)
    }
}
```

### Checklist

- [ ] Room para persistência?
- [ ] @Entity para modelos?
- [ ] @Dao para operações?
- [ ] Flow para queries reativas?

---

## Rule 5: Hilt para Dependency Injection

### Contexto

Dependências hardcoded dificultam testes e manutenção. Hilt (Dagger) permite injeção automática.

### Regra

Dependências DEVEM ser injetadas via:
- **Hilt**: DI framework oficial Android
- **@HiltViewModel**: Para ViewModels
- **@Inject**: Para injeção de dependências
- **Modules**: Para configuração de dependências

### Justificativa

- Testes com mocks triviais
- Desacoplamento de componentes
- Injeção automática
- Integração com Jetpack

### Exemplos

#### ✅ Correto (Hilt)

```kotlin
// Application
@HiltAndroidApp
class MyApplication : Application()

// Module
@Module
@InstallIn(SingletonComponent::class)
object NetworkModule {

    @Provides
    @Singleton
    fun provideOkHttpClient(): OkHttpClient {
        return OkHttpClient.Builder()
            .connectTimeout(30, TimeUnit.SECONDS)
            .readTimeout(30, TimeUnit.SECONDS)
            .build()
    }

    @Provides
    @Singleton
    fun provideNetworkManager(client: OkHttpClient): NetworkManager {
        return NetworkManager(client)
    }
}

@Module
@InstallIn(SingletonComponent::class)
abstract class RepositoryModule {

    @Binds
    @Singleton
    abstract fun bindUserRepository(
        impl: UserRepositoryImpl
    ): UserRepository
}

// Repository com injeção
class UserRepositoryImpl @Inject constructor(
    private val networkManager: NetworkManager,
    private val userDao: UserDao
) : UserRepository {

    override suspend fun fetchUsers(): List<User> {
        return try {
            val users = networkManager.request<List<User>>(APIEndpoint.Users)
            userDao.insertUsers(users.map { it.toEntity() })
            users
        } catch (e: Exception) {
            userDao.observeUsers().first().map { it.toDomain() }
        }
    }
}

// ViewModel com Hilt
@HiltViewModel
class UserViewModel @Inject constructor(
    private val repository: UserRepository
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

// Activity
@AndroidEntryPoint
class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            MyAppTheme {
                UserListScreen()
            }
        }
    }
}

// Teste com Hilt
@HiltAndroidTest
class UserViewModelTest {

    @get:Rule
    var hiltRule = HiltAndroidRule(this)

    @Inject
    lateinit var repository: UserRepository

    @Before
    fun setup() {
        hiltRule.inject()
    }

    @Test
    fun testFetchUsers() = runTest {
        val viewModel = UserViewModel(repository)
        viewModel.fetchUsers()

        val state = viewModel.uiState.value
        assertTrue(state is UserUiState.Success)
    }
}
```

#### ❌ Incorreto (Dependências hardcoded)

```kotlin
class UserViewModel : ViewModel() {
    // Dependências hardcoded - impossível testar com mock
    private val repository = UserRepositoryImpl()

    fun fetchUsers() {
        viewModelScope.launch {
            val users = repository.fetchUsers()
            // ...
        }
    }
}
```

### Ferramentas

- **Hilt**: DI oficial Android
- **Dagger**: Base do Hilt
- **Koin**: Alternativa lightweight

### Checklist

- [ ] Hilt configurado?
- [ ] @HiltViewModel para ViewModels?
- [ ] Modules para dependências?
- [ ] @Inject para injeção?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Jetpack Compose usage | 100% | Code review |
| MVVM architecture | 100% | Architecture tests |
| Coroutines usage | 100% async ops | Code review |
| Room database | 100% persistence | Code review |
| Hilt DI | 95% | Unit tests coverage |

---

**Próxima revisão:** 07/02/2026
