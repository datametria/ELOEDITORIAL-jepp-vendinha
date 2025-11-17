# Unity Framework Rules - DATAMETRIA Standards

**Versão:** 1.0.0
**Data:** 07/11/2025
**Autor:** Vander Loto - CTO DATAMETRIA

---

## Rule 1: Clean Architecture com Service Locator

### Contexto

MonoBehaviours acoplados resultam em código difícil de testar e manter. Clean Architecture separa responsabilidades.

### Regra

Projetos Unity DEVEM usar:
- **Service Locator**: Para Dependency Injection
- **Interfaces**: Para abstrações de serviços
- **Separation of Concerns**: Lógica separada de MonoBehaviours
- **Async/Await**: Para operações assíncronas

### Justificativa

- Testabilidade 90% maior
- Desacoplamento de componentes
- Reutilização de código
- Manutenção facilitada

### Exemplos

#### ✅ Correto (Service Locator + Clean Architecture)

```csharp
// Core/DI/ServiceLocator.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator _instance;
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ServiceLocator>();
                if (_instance == null)
                {
                    var go = new GameObject("ServiceLocator");
                    _instance = go.AddComponent<ServiceLocator>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private readonly Dictionary<Type, object> _services = new();

    public void Register<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service {type.Name} already registered");
            return;
        }
        _services[type] = service;
    }

    public T Get<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        throw new Exception($"Service {type.Name} not found");
    }
}

// Domain/Services/INetworkService.cs
public interface INetworkService
{
    Task<T> GetAsync<T>(string endpoint);
    Task<T> PostAsync<T>(string endpoint, object data);
}

// Data/Services/NetworkServiceImpl.cs
public class NetworkServiceImpl : INetworkService
{
    private readonly string _baseUrl = "https://api.example.com";

    public async Task<T> GetAsync<T>(string endpoint)
    {
        using var request = UnityWebRequest.Get($"{_baseUrl}/{endpoint}");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(request.error);
        }

        return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var json = JsonUtility.ToJson(data);
        using var request = UnityWebRequest.Post($"{_baseUrl}/{endpoint}", json, "application/json");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(request.error);
        }

        return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }
}

// Presentation/GameInitializer.cs
public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        // Register services
        ServiceLocator.Instance.Register<INetworkService>(new NetworkServiceImpl());
        ServiceLocator.Instance.Register<IARService>(new ARServiceImpl());
    }
}

// Usage in MonoBehaviour
public class UserController : MonoBehaviour
{
    private INetworkService _networkService;

    private void Start()
    {
        _networkService = ServiceLocator.Instance.Get<INetworkService>();
    }

    public async void LoadUser(string userId)
    {
        try
        {
            var user = await _networkService.GetAsync<User>($"users/{userId}");
            Debug.Log($"User loaded: {user.Name}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading user: {e.Message}");
        }
    }
}
```

#### ❌ Incorreto (Acoplamento direto)

```csharp
public class UserController : MonoBehaviour
{
    // Dependência hardcoded - impossível testar
    private NetworkService _networkService = new NetworkService();

    public void LoadUser(string userId)
    {
        StartCoroutine(LoadUserCoroutine(userId));
    }

    private IEnumerator LoadUserCoroutine(string userId)
    {
        // Callback hell
        yield return _networkService.GetUser(userId, (user) => {
            Debug.Log($"User loaded: {user.Name}");
        }, (error) => {
            Debug.LogError($"Error: {error}");
        });
    }
}
```

### Checklist

- [ ] Service Locator implementado?
- [ ] Serviços são interfaces?
- [ ] Lógica separada de MonoBehaviours?
- [ ] async/await para operações assíncronas?

---

## Rule 2: Scriptable Objects para Configuração

### Contexto

Valores hardcoded dificultam balanceamento e configuração. Scriptable Objects permitem configuração via Inspector.

### Regra

Configurações DEVEM usar:
- **ScriptableObject**: Para dados de configuração
- **[CreateAssetMenu]**: Para criação via menu
- **Serialização**: Para persistência
- **Singleton Pattern**: Para acesso global (quando necessário)

### Justificativa

- Configuração sem recompilação
- Balanceamento facilitado
- Reutilização de assets
- Versionamento no Git

### Exemplos

#### ✅ Correto (ScriptableObject)

```csharp
// Data/Config/GameConfig.cs
[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Player Settings")]
    public float playerSpeed = 5f;
    public float jumpForce = 10f;
    public int maxHealth = 100;

    [Header("AR Settings")]
    public float placementDistance = 2f;
    public LayerMask placementLayerMask;
    public bool enablePlaneVisualization = true;

    [Header("Network Settings")]
    public string apiBaseUrl = "https://api.example.com";
    public int requestTimeout = 30;
    public int maxRetries = 3;
}

// Data/Config/ObjectDatabase.cs
[CreateAssetMenu(fileName = "ObjectDatabase", menuName = "Config/Object Database")]
public class ObjectDatabase : ScriptableObject
{
    [System.Serializable]
    public class ObjectData
    {
        public string id;
        public string name;
        public GameObject prefab;
        public Sprite icon;
        public float scale = 1f;
    }

    public List<ObjectData> objects = new();

    public ObjectData GetObjectById(string id)
    {
        return objects.FirstOrDefault(o => o.id == id);
    }
}

// Usage
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfig config;
    [SerializeField] private ObjectDatabase objectDatabase;

    private void Start()
    {
        Debug.Log($"Player speed: {config.playerSpeed}");

        var obj = objectDatabase.GetObjectById("chair");
        if (obj != null)
        {
            Instantiate(obj.prefab);
        }
    }
}

// Singleton ScriptableObject (para acesso global)
public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<T>(typeof(T).Name);
                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} not found in Resources folder");
                }
            }
            return _instance;
        }
    }
}

// Usage
[CreateAssetMenu(fileName = "AppSettings", menuName = "Config/App Settings")]
public class AppSettings : SingletonScriptableObject<AppSettings>
{
    public string appVersion = "1.0.0";
    public bool debugMode = false;
}

// Access anywhere
public class AnyScript : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"App version: {AppSettings.Instance.appVersion}");
    }
}
```

#### ❌ Incorreto (Valores hardcoded)

```csharp
public class GameManager : MonoBehaviour
{
    // Valores hardcoded - difícil de balancear
    private float playerSpeed = 5f;
    private float jumpForce = 10f;
    private int maxHealth = 100;

    // Impossível reutilizar em outros scripts
}
```

### Checklist

- [ ] ScriptableObjects para configuração?
- [ ] [CreateAssetMenu] para criação?
- [ ] Singleton pattern quando necessário?
- [ ] Assets versionados no Git?

---

## Rule 3: Object Pooling Obrigatório

### Contexto

Instantiate/Destroy causam garbage collection e stuttering. Object Pooling reutiliza objetos.

### Regra

Objetos frequentes DEVEM usar:
- **Object Pool**: Para reutilização
- **Pré-alocação**: No Start/Awake
- **Reset**: Ao retornar ao pool
- **Expand**: Quando pool esgota

### Justificativa

- Performance 10x melhor
- Sem garbage collection spikes
- Frame rate estável
- Memória otimizada

### Exemplos

#### ✅ Correto (Object Pooling)

```csharp
// Core/ObjectPool.cs
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool = new();
    private readonly int _initialSize;

    public ObjectPool(T prefab, int initialSize = 10, Transform parent = null)
    {
        _prefab = prefab;
        _initialSize = initialSize;
        _parent = parent;

        // Pre-allocate
        for (int i = 0; i < initialSize; i++)
        {
            var obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        T obj;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else
        {
            // Expand pool
            obj = Object.Instantiate(_prefab, _parent);
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void Clear()
    {
        while (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            Object.Destroy(obj.gameObject);
        }
    }
}

// Core/PoolManager.cs
public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance => _instance;

    private readonly Dictionary<string, object> _pools = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePool<T>(string poolName, T prefab, int initialSize = 10) where T : Component
    {
        if (_pools.ContainsKey(poolName))
        {
            Debug.LogWarning($"Pool {poolName} already exists");
            return;
        }

        var poolParent = new GameObject($"Pool_{poolName}").transform;
        poolParent.SetParent(transform);

        var pool = new ObjectPool<T>(prefab, initialSize, poolParent);
        _pools[poolName] = pool;
    }

    public T Get<T>(string poolName) where T : Component
    {
        if (_pools.TryGetValue(poolName, out var pool))
        {
            return ((ObjectPool<T>)pool).Get();
        }
        throw new System.Exception($"Pool {poolName} not found");
    }

    public void Return<T>(string poolName, T obj) where T : Component
    {
        if (_pools.TryGetValue(poolName, out var pool))
        {
            ((ObjectPool<T>)pool).Return(obj);
        }
    }
}

// Usage
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    private const string BULLET_POOL = "Bullets";

    private void Start()
    {
        // Create pool
        PoolManager.Instance.CreatePool(BULLET_POOL, bulletPrefab, 50);
    }

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bullet = PoolManager.Instance.Get<Bullet>(BULLET_POOL);
        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.Initialize();
    }
}

public class Bullet : MonoBehaviour
{
    private const string BULLET_POOL = "Bullets";

    public void Initialize()
    {
        // Reset state
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Return to pool instead of Destroy
        PoolManager.Instance.Return(BULLET_POOL, this);
    }
}
```

#### ❌ Incorreto (Instantiate/Destroy)

```csharp
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        // Causa garbage collection
        var bullet = Instantiate(bulletPrefab, position, rotation);
    }
}

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Causa garbage collection
        Destroy(gameObject);
    }
}
```

### Checklist

- [ ] Object Pool para objetos frequentes?
- [ ] Pré-alocação no Start?
- [ ] Return ao invés de Destroy?
- [ ] Expand quando pool esgota?

---

## Rule 4: Universal Render Pipeline (URP)

### Contexto

Built-in Render Pipeline é legado. URP oferece melhor performance e recursos modernos.

### Regra

Projetos Unity DEVEM usar:
- **URP**: Universal Render Pipeline
- **Shader Graph**: Para shaders visuais
- **Post-Processing**: Para efeitos visuais
- **LOD System**: Para otimização

### Justificativa

- Performance 2x melhor em mobile
- Shaders visuais sem código
- Cross-platform consistente
- Recursos modernos (SSAO, Bloom, etc)

### Exemplos

#### ✅ Correto (URP Setup)

```csharp
// Rendering/URPSetup.cs
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Rendering.Universal;

public static class URPSetup
{
    [MenuItem("DATAMETRIA/Setup URP")]
    public static void SetupURP()
    {
        // Create URP Asset
        var urpAsset = ScriptableObject.CreateInstance<UniversalRenderPipelineAsset>();
        AssetDatabase.CreateAsset(urpAsset, "Assets/Settings/URPAsset.asset");

        // Create Renderer
        var renderer = ScriptableObject.CreateInstance<UniversalRendererData>();
        AssetDatabase.CreateAsset(renderer, "Assets/Settings/URPRenderer.asset");

        // Assign to Graphics Settings
        GraphicsSettings.renderPipelineAsset = urpAsset;

        Debug.Log("URP setup complete!");
    }
}
#endif

// Rendering/LODManager.cs
public class LODManager : MonoBehaviour
{
    [System.Serializable]
    public class LODLevel
    {
        public float screenRelativeHeight;
        public Mesh mesh;
        public Material[] materials;
    }

    [SerializeField] private LODLevel[] lodLevels;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    private Camera mainCamera;
    private int currentLOD = -1;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateLOD();
    }

    private void UpdateLOD()
    {
        var bounds = meshRenderer.bounds;
        var distance = Vector3.Distance(mainCamera.transform.position, bounds.center);
        var screenHeight = bounds.size.magnitude / distance;

        for (int i = 0; i < lodLevels.Length; i++)
        {
            if (screenHeight >= lodLevels[i].screenRelativeHeight)
            {
                if (currentLOD != i)
                {
                    SetLOD(i);
                }
                return;
            }
        }

        // Use lowest LOD
        if (currentLOD != lodLevels.Length - 1)
        {
            SetLOD(lodLevels.Length - 1);
        }
    }

    private void SetLOD(int lodIndex)
    {
        currentLOD = lodIndex;
        meshFilter.mesh = lodLevels[lodIndex].mesh;
        meshRenderer.materials = lodLevels[lodIndex].materials;
    }
}
```

#### ❌ Incorreto (Built-in Pipeline)

```csharp
// Usando Built-in Render Pipeline - legado
// Sem otimizações modernas
// Performance inferior em mobile
```

### Checklist

- [ ] URP configurado?
- [ ] Shader Graph para shaders?
- [ ] Post-Processing configurado?
- [ ] LOD System implementado?

---

## Rule 5: Async/Await para Operações Assíncronas

### Contexto

Coroutines são verbosas e difíceis de debugar. async/await simplifica código assíncrono.

### Regra

Operações assíncronas DEVEM usar:
- **async/await**: Para operações assíncronas
- **UniTask**: Para performance otimizada (opcional)
- **CancellationToken**: Para cancelamento
- **ConfigureAwait(false)**: Para evitar deadlocks

### Justificativa

- Código 60% mais legível
- Debugging facilitado
- Cancelamento automático
- Performance melhor (com UniTask)

### Exemplos

#### ✅ Correto (async/await)

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

// Data/Services/NetworkService.cs
public class NetworkService : INetworkService
{
    private readonly string _baseUrl;

    public NetworkService(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public async Task<T> GetAsync<T>(string endpoint, CancellationToken ct = default)
    {
        using var request = UnityWebRequest.Get($"{_baseUrl}/{endpoint}");

        // Send request
        var operation = request.SendWebRequest();

        // Wait with cancellation support
        while (!operation.isDone)
        {
            if (ct.IsCancellationRequested)
            {
                request.Abort();
                throw new OperationCanceledException();
            }
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(request.error);
        }

        return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }

    public async Task<T> PostAsync<T>(string endpoint, object data, CancellationToken ct = default)
    {
        var json = JsonUtility.ToJson(data);
        var bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        using var request = new UnityWebRequest($"{_baseUrl}/{endpoint}", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            if (ct.IsCancellationRequested)
            {
                request.Abort();
                throw new OperationCanceledException();
            }
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(request.error);
        }

        return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }
}

// Presentation/UserController.cs
public class UserController : MonoBehaviour
{
    private INetworkService _networkService;
    private CancellationTokenSource _cts;

    private void Start()
    {
        _networkService = ServiceLocator.Instance.Get<INetworkService>();
        _cts = new CancellationTokenSource();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    public async void LoadUser(string userId)
    {
        try
        {
            var user = await _networkService.GetAsync<User>($"users/{userId}", _cts.Token);
            Debug.Log($"User loaded: {user.Name}");
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Operation cancelled");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading user: {e.Message}");
        }
    }

    // Multiple async operations
    public async void LoadMultipleUsers(string[] userIds)
    {
        try
        {
            var tasks = userIds.Select(id =>
                _networkService.GetAsync<User>($"users/{id}", _cts.Token)
            );

            var users = await Task.WhenAll(tasks);
            Debug.Log($"Loaded {users.Length} users");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
    }
}
```

#### ❌ Incorreto (Coroutines)

```csharp
public class UserController : MonoBehaviour
{
    public void LoadUser(string userId)
    {
        StartCoroutine(LoadUserCoroutine(userId));
    }

    private IEnumerator LoadUserCoroutine(string userId)
    {
        // Verboso e difícil de debugar
        var request = UnityWebRequest.Get($"https://api.example.com/users/{userId}");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var user = JsonUtility.FromJson<User>(request.downloadHandler.text);
            Debug.Log($"User loaded: {user.Name}");
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}
```

### Checklist

- [ ] async/await para operações assíncronas?
- [ ] CancellationToken para cancelamento?
- [ ] Task.WhenAll para operações paralelas?
- [ ] Dispose de CancellationTokenSource?

---

---

## Rule 6: Mobile Input System (Touch + Virtual Joystick)

### Contexto

Jogos mobile 3D precisam de controles intuitivos. Input System legado não suporta touch moderno adequadamente.

### Regra

Jogos mobile DEVEM usar:
- **New Input System**: Para controles modernos
- **Virtual Joystick**: Para movimento do jogador
- **Touch Input**: Para rotação de câmera e interação
- **Deadzone**: Para evitar drift do joystick

### Justificativa

- Controles responsivos e precisos
- Suporte nativo a multi-touch
- Configuração flexível
- Melhor UX para mobile

### Quando Usar

- ✅ Jogos mobile 3D (FPS, TPS, aventura)
- ✅ Protótipos educacionais mobile
- ✅ Experiências interativas 3D
- ❌ Jogos 2D simples (usar Input System básico)
- ❌ AR/VR (usar XR Interaction Toolkit)

### Exemplos

#### ✅ Correto (Virtual Joystick + Touch)

```csharp
// VirtualJoystick.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Joystick Settings")]
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private float handleRange = 50f;
    [SerializeField] private float deadzone = 0.1f;
    
    private Vector2 inputVector;
    
    public Vector2 InputVector => inputVector;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out position
        );
        
        position = Vector2.ClampMagnitude(position, handleRange);
        joystickHandle.anchoredPosition = position;
        
        inputVector = position / handleRange;
        
        // Apply deadzone
        if (inputVector.magnitude < deadzone)
        {
            inputVector = Vector2.zero;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}

// MobileFPSController_InputSystem.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MobileFPSController_InputSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private VirtualJoystick joystick;
    [SerializeField] private Camera playerCamera;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float gravity = -9.81f;
    
    [Header("Camera")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 80f;
    
    private CharacterController controller;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyGravity();
    }
    
    private void HandleMovement()
    {
        Vector2 input = joystick.InputVector;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        // Touch input for camera rotation
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // Ignore if touching joystick area
            if (IsOverJoystick(touch.position))
                return;
            
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;
                
                // Horizontal rotation (Y axis)
                transform.Rotate(Vector3.up * delta.x * lookSensitivity * Time.deltaTime);
                
                // Vertical rotation (X axis)
                verticalRotation -= delta.y * lookSensitivity * Time.deltaTime;
                verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
                playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            }
        }
    }
    
    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    private bool IsOverJoystick(Vector2 touchPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            joystick.GetComponent<RectTransform>(),
            touchPosition
        );
    }
}
```

#### ❌ Incorreto (Input System Legado)

```csharp
public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        // Input.GetAxis não funciona bem em mobile
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // Sem suporte a touch adequado
        transform.Translate(h, 0, v);
    }
}
```

### Checklist

- [ ] New Input System instalado?
- [ ] Virtual Joystick implementado?
- [ ] Touch input para câmera?
- [ ] Deadzone configurado?
- [ ] Testado em dispositivo real?
- [ ] UI touch-friendly (botões grandes)?

---

## Métricas de Conformidade

| Métrica | Meta | Validação |
|---------|------|-----------|
| Service Locator usage | 100% | Code review |
| ScriptableObject config | 100% | Asset review |
| Object Pooling | 100% frequent objects | Profiler |
| URP usage | 100% | Project settings |
| async/await usage | 100% async ops | Code review |
| Mobile Input System | 100% mobile games | Code review |

---

**Versão:** 1.1.0  
**Última Atualização:** 17/11/2025  
**Próxima revisão:** 17/02/2026
