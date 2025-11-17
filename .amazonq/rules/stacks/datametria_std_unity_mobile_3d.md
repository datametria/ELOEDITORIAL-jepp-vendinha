# 📱 DATAMETRIA Standards - Unity Mobile 3D Development

**Versão:** 1.0.0 | **Última Atualização:** 17/11/2025 | **Autor:** Vander Loto - CTO DATAMETRIA

<div align="center">

![DATAMETRIA Unity Mobile 3D](https://img.shields.io/badge/DATAMETRIA-Unity%20Mobile%203D-blue?style=for-the-badge)

## Diretrizes para Desenvolvimento Mobile 3D com Unity (Android/iOS)

[![Unity](https://img.shields.io/badge/Unity-6000.2.8f1+-black)](https://unity.com/)
[![Android](https://img.shields.io/badge/Android-API%2024+-green)](https://developer.android.com/)
[![iOS](https://img.shields.io/badge/iOS-13.0+-orange)](https://developer.apple.com/)
[![URP](https://img.shields.io/badge/URP-Mobile-blue)](https://unity.com/srp/universal-render-pipeline)
[![Version](https://img.shields.io/badge/version-1.0.0-blue)](https://github.com/datametria/DATAMETRIA-standards)

[🎯 Visão Geral](#-visão-geral) • [🏗️ Arquitetura](#️-arquitetura) • [🎮 Input System](#-input-system) • [⚡ Performance](#-performance) • [📦 Build](#-build) • [✅ Checklist](#-checklist)

</div>

---

## 🎯 Visão Geral

### Filosofia Mobile 3D

**Unity Mobile 3D** (sem AR/VR) é ideal para:
- **Jogos mobile 3D tradicionais**: FPS, TPS, aventura, puzzle
- **Protótipos educacionais**: Gamificação, simulações
- **Experiências interativas**: Tours virtuais, visualizações 3D
- **Apps de entretenimento**: Casual games, experiências narrativas

### Quando Usar Unity Mobile 3D vs AR/VR

| Critério | Mobile 3D | AR/VR |
|----------|-----------|-------|
| **Complexidade** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Performance** | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Desenvolvimento** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Compatibilidade** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Custo** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ |

**Use Mobile 3D quando:**
- Não precisa de AR/VR features
- Foco em gameplay tradicional
- Target dispositivos médios/antigos
- Prazo curto (protótipos)
- Orçamento limitado

### Stack Tecnológico

| Componente | Tecnologia | Versão | Uso |
|------------|------------|--------|-----|
| **Engine** | Unity | 6000.2.8f1+ | Game engine |
| **Render Pipeline** | URP | 17.0+ | Mobile renderer |
| **Input** | New Input System | 1.7+ | Touch + virtual controls |
| **Audio** | Unity Audio | Built-in | Som e música |
| **Build** | IL2CPP | - | Scripting backend |
| **Versionamento** | Git + Git LFS | 3.4+ | Assets binários |

---

## 🏗️ Arquitetura

### Estrutura de Projeto Mobile 3D

```
Assets/
├── _Project/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── Gameplay.unity
│   │   └── Loading.unity
│   ├── Scripts/
│   │   ├── Core/
│   │   │   ├── GameManager.cs          # Singleton
│   │   │   ├── SceneLoader.cs
│   │   │   └── AudioManager.cs
│   │   ├── Player/
│   │   │   ├── MobileFPSController.cs
│   │   │   ├── CameraController.cs
│   │   │   └── PlayerInput.cs
│   │   ├── Gameplay/
│   │   │   ├── Interactable.cs
│   │   │   ├── InteractionController.cs
│   │   │   └── GameLogic.cs
│   │   └── UI/
│   │       ├── VirtualJoystick.cs
│   │       ├── UIManager.cs
│   │       └── MenuController.cs
│   ├── Prefabs/
│   │   ├── Player.prefab
│   │   ├── UI/
│   │   └── Gameplay/
│   ├── Materials/
│   ├── Models/
│   ├── Textures/
│   └── Audio/
│       ├── Music/
│       └── SFX/
├── Settings/
│   ├── Mobile_RPAsset.asset        # URP Mobile
│   └── Mobile_Renderer.asset
└── Plugins/
    ├── Android/
    └── iOS/
```

### Padrões Arquiteturais para Mobile

#### Singleton Pattern (Protótipos)

```csharp
// GameManager.cs
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game State")]
    public GameState currentState = GameState.Menu;
    
    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);
        
        while (!operation.isDone)
        {
            // Update loading progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver
}
```

#### ScriptableObject para Configuração

```csharp
// GameConfig.cs
[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Player Settings")]
    public float moveSpeed = 3f;
    public float lookSensitivity = 2f;
    public float jumpForce = 5f;
    
    [Header("Mobile Settings")]
    public float joystickDeadzone = 0.1f;
    public bool invertYAxis = false;
    
    [Header("Performance")]
    public int targetFrameRate = 30;
    public bool enableVSync = false;
    
    [Header("Audio")]
    [Range(0f, 1f)] public float musicVolume = 0.7f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
}
```

---

## 🎮 Input System

### New Input System para Mobile

#### Virtual Joystick

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
    private Vector2 joystickPosition;
    
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
```

#### Mobile FPS Controller

```csharp
// MobileFPSController.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MobileFPSController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private VirtualJoystick joystick;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameConfig config;
    
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
        
        // Load config if available
        if (config != null)
        {
            moveSpeed = config.moveSpeed;
            lookSensitivity = config.lookSensitivity;
        }
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
        // Check if touch is over joystick UI
        return RectTransformUtility.RectangleContainsScreenPoint(
            joystick.GetComponent<RectTransform>(),
            touchPosition
        );
    }
}
```

#### Camera Bobbing

```csharp
// CameraBobbing.cs
public class CameraBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [SerializeField] private float bobbingSpeed = 14f;
    [SerializeField] private float bobbingAmount = 0.05f;
    [SerializeField] private CharacterController controller;
    
    private float defaultPosY;
    private float timer = 0f;
    
    private void Start()
    {
        defaultPosY = transform.localPosition.y;
    }
    
    private void Update()
    {
        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            // Player is moving
            timer += Time.deltaTime * bobbingSpeed;
            float newY = defaultPosY + Mathf.Sin(timer) * bobbingAmount;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newY,
                transform.localPosition.z
            );
        }
        else
        {
            // Reset position
            timer = 0f;
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                new Vector3(transform.localPosition.x, defaultPosY, transform.localPosition.z),
                Time.deltaTime * bobbingSpeed
            );
        }
    }
}
```

### Sistema de Interação

```csharp
// Interactable.cs
public class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private string interactionPrompt = "Interagir";
    [SerializeField] private float interactionDistance = 3f;
    
    [Header("Visual Feedback")]
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;
    
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }
    
    public void Highlight(bool enable)
    {
        if (objectRenderer != null && highlightMaterial != null)
        {
            objectRenderer.material = enable ? highlightMaterial : originalMaterial;
        }
    }
    
    public virtual void Interact()
    {
        Debug.Log($"Interagindo com {gameObject.name}");
    }
    
    public string GetPrompt()
    {
        return interactionPrompt;
    }
}

// InteractionController.cs
public class InteractionController : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    
    [Header("UI")]
    [SerializeField] private GameObject interactionPromptUI;
    [SerializeField] private TMPro.TextMeshProUGUI promptText;
    
    private Interactable currentInteractable;
    
    private void Update()
    {
        CheckForInteractable();
        HandleInteraction();
    }
    
    private void CheckForInteractable()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            if (interactable != null)
            {
                SetCurrentInteractable(interactable);
                return;
            }
        }
        
        SetCurrentInteractable(null);
    }
    
    private void SetCurrentInteractable(Interactable interactable)
    {
        if (currentInteractable != interactable)
        {
            // Unhighlight previous
            if (currentInteractable != null)
            {
                currentInteractable.Highlight(false);
            }
            
            currentInteractable = interactable;
            
            // Highlight new
            if (currentInteractable != null)
            {
                currentInteractable.Highlight(true);
                ShowPrompt(currentInteractable.GetPrompt());
            }
            else
            {
                HidePrompt();
            }
        }
    }
    
    private void HandleInteraction()
    {
        if (currentInteractable != null && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                currentInteractable.Interact();
            }
        }
    }
    
    private void ShowPrompt(string text)
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(true);
            if (promptText != null)
            {
                promptText.text = text;
            }
        }
    }
    
    private void HidePrompt()
    {
        if (interactionPromptUI != null)
        {
            interactionPromptUI.SetActive(false);
        }
    }
}
```

---

## ⚡ Performance

### URP Mobile Renderer

```csharp
// Mobile_RPAsset Settings
{
  "renderScale": 0.8,
  "msaa": "Disabled",
  "hdr": false,
  "shadowResolution": 512,
  "maxLights": 4,
  "depthTexture": false,
  "opaqueDownsampling": "None",
  "supportsTerrainHoles": false
}
```

### Texture Optimization

```csharp
// TextureOptimizer.cs (Editor Script)
#if UNITY_EDITOR
using UnityEditor;

public class TextureOptimizer : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;
        
        // Android settings
        var androidSettings = importer.GetPlatformTextureSettings("Android");
        androidSettings.overridden = true;
        androidSettings.format = TextureImporterFormat.ASTC_6x6;
        androidSettings.maxTextureSize = 1024;
        importer.SetPlatformTextureSettings(androidSettings);
        
        // iOS settings
        var iosSettings = importer.GetPlatformTextureSettings("iPhone");
        iosSettings.overridden = true;
        iosSettings.format = TextureImporterFormat.ASTC_6x6;
        iosSettings.maxTextureSize = 1024;
        importer.SetPlatformTextureSettings(iosSettings);
        
        // General settings
        importer.mipmapEnabled = true;
        importer.anisoLevel = 2;
    }
}
#endif
```

### LOD System

```csharp
// LODManager.cs
public class LODManager : MonoBehaviour
{
    [System.Serializable]
    public class LODLevel
    {
        public float screenRelativeHeight;
        public Mesh mesh;
    }
    
    [SerializeField] private LODLevel[] lodLevels;
    [SerializeField] private MeshFilter meshFilter;
    
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
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
        float screenHeight = CalculateScreenHeight(distance);
        
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
    }
    
    private float CalculateScreenHeight(float distance)
    {
        return Mathf.Atan(1f / distance) * 2f / Mathf.PI;
    }
    
    private void SetLOD(int lodIndex)
    {
        currentLOD = lodIndex;
        meshFilter.mesh = lodLevels[lodIndex].mesh;
    }
}
```

### Performance Manager

```csharp
// PerformanceManager.cs
public class PerformanceManager : MonoBehaviour
{
    [Header("Target Performance")]
    [SerializeField] private int targetFrameRate = 30;
    [SerializeField] private bool enableVSync = false;
    
    [Header("Quality Settings")]
    [SerializeField] private int pixelLightCount = 1;
    [SerializeField] private ShadowQuality shadowQuality = ShadowQuality.HardOnly;
    [SerializeField] private float shadowDistance = 50f;
    
    private void Awake()
    {
        ConfigurePerformance();
    }
    
    private void ConfigurePerformance()
    {
        // Frame rate
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = enableVSync ? 1 : 0;
        
        // Quality
        QualitySettings.pixelLightCount = pixelLightCount;
        QualitySettings.shadows = shadowQuality;
        QualitySettings.shadowDistance = shadowDistance;
        
        // Physics
        Physics.defaultSolverIterations = 6;
        Physics.defaultSolverVelocityIterations = 1;
        
        // Rendering
        QualitySettings.antiAliasing = 0; // MSAA disabled (use URP)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
    }
}
```

---

## 📦 Build

### Android Build Settings

```csharp
// AndroidBuildSettings.cs (Editor Script)
#if UNITY_EDITOR
using UnityEditor;

public class AndroidBuildSettings
{
    [MenuItem("DATAMETRIA/Configure Android Build")]
    public static void ConfigureAndroidBuild()
    {
        // Player settings
        PlayerSettings.productName = "Jepp Vendinha";
        PlayerSettings.companyName = "ELOEDITORIAL";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.eloeditorial.jeppvendinha");
        
        // Android settings
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel33;
        
        // Scripting
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        
        // Graphics
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 });
        PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
        
        // Optimization
        PlayerSettings.stripEngineCode = true;
        PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.Android, ManagedStrippingLevel.High);
        
        // Compression
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.androidBuildType = AndroidBuildType.Release;
        
        Debug.Log("Android build configured!");
    }
}
#endif
```

### iOS Build Settings

```csharp
// iOSBuildSettings.cs (Editor Script)
#if UNITY_EDITOR
using UnityEditor;

public class iOSBuildSettings
{
    [MenuItem("DATAMETRIA/Configure iOS Build")]
    public static void ConfigureiOSBuild()
    {
        // Player settings
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.eloeditorial.jeppvendinha");
        
        // iOS settings
        PlayerSettings.iOS.targetOSVersionString = "13.0";
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
        
        // Scripting
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
        
        // Graphics
        PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, new[] { UnityEngine.Rendering.GraphicsDeviceType.Metal });
        
        // Optimization
        PlayerSettings.stripEngineCode = true;
        PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.iOS, ManagedStrippingLevel.High);
        
        Debug.Log("iOS build configured!");
    }
}
#endif
```

---

## ✅ Checklist

### Setup Inicial

- [ ] Unity 6000.2.8f1+ instalado
- [ ] URP Mobile Renderer configurado
- [ ] New Input System instalado
- [ ] Git + Git LFS configurado
- [ ] Estrutura de pastas criada
- [ ] GameConfig ScriptableObject criado

### Input System

- [ ] Virtual Joystick implementado
- [ ] Touch camera rotation funcionando
- [ ] Deadzone configurado
- [ ] Input responsivo em dispositivos reais
- [ ] UI touch-friendly (botões grandes)

### Performance

- [ ] Target 30+ FPS em dispositivos médios
- [ ] URP Mobile preset ativo
- [ ] Texturas comprimidas (ASTC 6x6)
- [ ] LOD system implementado
- [ ] Occlusion culling ativo
- [ ] Baked lighting quando possível
- [ ] Draw calls < 100
- [ ] Memory usage < 500MB

### Build Android

- [ ] Min SDK API 24+ (Android 7.0+)
- [ ] Target SDK API 33+
- [ ] IL2CPP scripting backend
- [ ] ARM64 architecture
- [ ] OpenGL ES 3.0+
- [ ] APK < 150MB
- [ ] Managed stripping: High
- [ ] Testado em 3+ dispositivos

### Build iOS

- [ ] Target iOS 13.0+
- [ ] IL2CPP scripting backend
- [ ] Metal graphics API
- [ ] Universal build (iPhone + iPad)
- [ ] App size < 150MB
- [ ] Testado em dispositivos reais

### Quality

- [ ] Singleton pattern para managers
- [ ] ScriptableObjects para configs
- [ ] Clean code (naming conventions)
- [ ] Error handling robusto
- [ ] Loading screens implementadas
- [ ] Audio manager funcional
- [ ] Scene transitions suaves

### Mobile UX

- [ ] Controles intuitivos para crianças (se aplicável)
- [ ] Feedback visual claro
- [ ] Feedback tátil (vibração)
- [ ] UI adaptável (portrait/landscape)
- [ ] Sessões curtas (5-10min)
- [ ] Tutorial inicial (se necessário)

### Documentação

- [ ] README.md completo
- [ ] Technical specification
- [ ] Memory Bank (idea, state, vibe, decisions)
- [ ] ADRs para decisões importantes
- [ ] Setup guide para novos devs

---

<div align="center">

## 🎯 DATAMETRIA Unity Mobile 3D Standard v1.0.0

**Desenvolvido com ❤️ pela equipe DATAMETRIA**

[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Unity%20Ready-blue?style=for-the-badge)](https://github.com/datametria/DATAMETRIA-standards)

**CTO**: Vander Loto | **Email**: vander.loto@datametria.io

**Baseado no projeto**: Jepp Vendinha (Protótipo Educacional Mobile 3D)

</div>
