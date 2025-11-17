# 🥽 DATAMETRIA Standards - Unity AR/VR Development

**Versão:** 1.0.0 | **Última Atualização:** 07/11/2025 | **Autor:** Vander Loto - CTO DATAMETRIA

<div align="center">

![DATAMETRIA Unity AR/VR](https://img.shields.io/badge/DATAMETRIA-Unity%20AR%2FVR-blue?style=for-the-badge)

## Diretrizes para Desenvolvimento AR/VR com Unity (Multi-Platform)

[![Unity](https://img.shields.io/badge/Unity-2023.2+-black)](https://unity.com/)
[![ARKit](https://img.shields.io/badge/ARKit-6.0+-orange)](https://developer.apple.com/arkit/)
[![ARCore](https://img.shields.io/badge/ARCore-1.40+-green)](https://developers.google.com/ar)
[![WebXR](https://img.shields.io/badge/WebXR-Enabled-blue)](https://www.w3.org/TR/webxr/)
[![Meta Quest](https://img.shields.io/badge/Meta%20Quest-3-purple)](https://www.meta.com/quest/)
[![Version](https://img.shields.io/badge/version-1.0.0-blue)](https://github.com/datametria/DATAMETRIA-standards)

[🎯 Visão Geral](#-visão-geral) • [🏗️ Arquitetura](#️-arquitetura) • [📱 AR Mobile](#-ar-mobile) • [🥽 VR Standalone](#-vr-standalone) • [🌐 WebXR](#-webxr) • [⚡ Performance](#-performance) • [✅ Checklist](#-checklist)

</div>

---

## 🎯 Visão Geral

### Filosofia AR/VR

**Unity AR/VR** oferece:
- **Cross-platform**: Um código para iOS e Android
- **Performance nativa**: Acesso direto a ARKit/ARCore
- **Ecossistema rico**: Asset Store, plugins, comunidade
- **Ferramentas avançadas**: Visual scripting, profiler, analytics

### Quando Usar Unity vs Native

| Critério | Unity | Native (ARKit/ARCore) |
|----------|-------|----------------------|
| **Cross-platform** | ⭐⭐⭐⭐⭐ | ⭐⭐ |
| **3D/Rendering** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Desenvolvimento** | ⭐⭐⭐⭐ | ⭐⭐⭐ |
| **Performance** | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Integração Nativa** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |

**Use Unity quando:**
- Precisa suportar múltiplas plataformas (iOS, Android, Web, VR Headsets)
- App focado em 3D/rendering complexo
- Equipe com experiência Unity
- Prototipagem rápida de AR/VR
- Deploy em Meta Quest Store, Pico Store, etc.

### Plataformas Suportadas

| Plataforma | Build Target | SDK/Plugin | Distribuição |
|------------|--------------|------------|-------------|
| **iOS** | iOS | ARKit XR Plugin | App Store |
| **Android** | Android | ARCore XR Plugin | Google Play |
| **Meta Quest** | Android (Quest) | Meta XR SDK | Meta Quest Store |
| **Pico** | Android (Pico) | Pico XR SDK | Pico Store |
| **WebXR** | WebGL | WebXR Export | Web Browsers |
| **PC VR** | Windows | OpenXR | Steam, Oculus PC |

---

## 🏗️ Arquitetura

### Clean Architecture Unity

```
┌─────────────────────────────────────┐
│         Presentation Layer          │  ← UI, Input, Camera
├─────────────────────────────────────┤
│          Domain Layer               │  ← Game Logic, Use Cases
├─────────────────────────────────────┤
│           Data Layer                │  ← Persistence, Network
├─────────────────────────────────────┤
│        Infrastructure Layer         │  ← AR Foundation, XR Toolkit
└─────────────────────────────────────┘
```

### Estrutura de Projeto Unity

```
Assets/
├── _Project/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   ├── ARScene.unity
│   │   └── VRScene.unity
│   ├── Scripts/
│   │   ├── Core/
│   │   │   ├── Managers/
│   │   │   │   ├── ARManager.cs
│   │   │   │   ├── VRManager.cs
│   │   │   │   └── NetworkManager.cs
│   │   │   ├── Services/
│   │   │   │   ├── IARService.cs
│   │   │   │   └── ARServiceImpl.cs
│   │   │   └── DI/
│   │   │       └── ServiceLocator.cs
│   │   ├── Domain/
│   │   │   ├── Models/
│   │   │   │   └── ARObject.cs
│   │   │   ├── UseCases/
│   │   │   │   └── PlaceObjectUseCase.cs
│   │   │   └── Repositories/
│   │   │       └── IObjectRepository.cs
│   │   ├── Data/
│   │   │   ├── Repositories/
│   │   │   │   └── ObjectRepositoryImpl.cs
│   │   │   └── DataSources/
│   │   │       ├── LocalDataSource.cs
│   │   │       └── RemoteDataSource.cs
│   │   ├── Presentation/
│   │   │   ├── AR/
│   │   │   │   ├── ARController.cs
│   │   │   │   └── ARUIManager.cs
│   │   │   └── VR/
│   │   │       ├── VRController.cs
│   │   │       └── VRUIManager.cs
│   │   └── Utils/
│   │       ├── Extensions.cs
│   │       └── Constants.cs
│   ├── Prefabs/
│   │   ├── AR/
│   │   │   └── ARPlaceable.prefab
│   │   └── VR/
│   │       └── VRInteractable.prefab
│   ├── Materials/
│   ├── Textures/
│   └── Audio/
├── Plugins/
│   ├── iOS/
│   └── Android/
└── Tests/
    ├── PlayMode/
    └── EditMode/
```

### Padrões Arquiteturais

#### Service Locator Pattern

```csharp
// ServiceLocator.cs
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

// Usage
public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Instance.Register<IARService>(new ARServiceImpl());
        ServiceLocator.Instance.Register<INetworkService>(new NetworkServiceImpl());
    }
}
```

#### MVC Pattern for AR/VR

```csharp
// Model
[Serializable]
public class ARObjectModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
}

// View
public class ARObjectView : MonoBehaviour
{
    [SerializeField] private GameObject visualRepresentation;
    [SerializeField] private TextMeshPro nameLabel;

    public void UpdateView(ARObjectModel model)
    {
        transform.position = model.Position;
        transform.rotation = model.Rotation;
        transform.localScale = model.Scale;
        nameLabel.text = model.Name;
    }

    public void SetSelected(bool selected)
    {
        // Visual feedback
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = selected;
        }
    }
}

// Controller
public class ARObjectController : MonoBehaviour
{
    private ARObjectModel _model;
    private ARObjectView _view;
    private IObjectRepository _repository;

    private void Awake()
    {
        _view = GetComponent<ARObjectView>();
        _repository = ServiceLocator.Instance.Get<IObjectRepository>();
    }

    public async void Initialize(string objectId)
    {
        _model = await _repository.GetObjectAsync(objectId);
        _view.UpdateView(_model);
    }

    public void OnTap()
    {
        _view.SetSelected(true);
        // Handle interaction
    }

    public async void UpdatePosition(Vector3 newPosition)
    {
        _model.Position = newPosition;
        _view.UpdateView(_model);
        await _repository.UpdateObjectAsync(_model);
    }
}
```

---

## 📱 AR Mobile

### Stack Tecnológico AR

| Componente | Tecnologia | Versão |
|------------|------------|--------|
| **Engine** | Unity | 2023.2+ |
| **AR Framework** | AR Foundation | 5.1+ |
| **iOS AR** | ARKit XR Plugin | 5.1+ |
| **Android AR** | ARCore XR Plugin | 5.1+ |
| **Interaction** | XR Interaction Toolkit | 2.5+ |
| **Networking** | Unity Netcode | 1.7+ |

### AR Foundation Setup

```csharp
// ARManager.cs
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    [Header("AR Components")]
    [SerializeField] private ARSession arSession;
    [SerializeField] private ARSessionOrigin arSessionOrigin;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARAnchorManager anchorManager;

    [Header("Placement")]
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private GameObject objectToPlace;

    private Camera arCamera;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private void Start()
    {
        arCamera = arSessionOrigin.camera;

        // Configure plane detection
        planeManager.planesChanged += OnPlanesChanged;
        planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(
                placementPose.position,
                placementPose.rotation
            );
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void PlaceObject()
    {
        var anchor = anchorManager.AddAnchor(placementPose);
        if (anchor != null)
        {
            var obj = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
            obj.transform.SetParent(anchor.transform);
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            Debug.Log($"Plane detected: {plane.trackableId}");
        }
    }
}
```

### Image Tracking

```csharp
// ARImageTracker.cs
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTracker : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject[] prefabsToPlace;

    private Dictionary<string, GameObject> _spawnedObjects = new();

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (var trackedImage in args.removed)
        {
            if (_spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out var obj))
            {
                obj.SetActive(false);
            }
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        var imageName = trackedImage.referenceImage.name;

        if (!_spawnedObjects.ContainsKey(imageName))
        {
            var prefab = GetPrefabForImage(imageName);
            if (prefab != null)
            {
                var obj = Instantiate(prefab, trackedImage.transform);
                _spawnedObjects[imageName] = obj;
            }
        }

        if (_spawnedObjects.TryGetValue(imageName, out var spawnedObj))
        {
            spawnedObj.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            spawnedObj.transform.position = trackedImage.transform.position;
            spawnedObj.transform.rotation = trackedImage.transform.rotation;
        }
    }

    private GameObject GetPrefabForImage(string imageName)
    {
        // Map image names to prefabs
        return prefabsToPlace.FirstOrDefault(p => p.name == imageName);
    }
}
```

### Face Tracking

```csharp
// ARFaceTracker.cs
using UnityEngine.XR.ARFoundation;

public class ARFaceTracker : MonoBehaviour
{
    [SerializeField] private ARFaceManager faceManager;
    [SerializeField] private GameObject facePrefab;

    private Dictionary<TrackableId, GameObject> _faceObjects = new();

    private void OnEnable()
    {
        faceManager.facesChanged += OnFacesChanged;
    }

    private void OnDisable()
    {
        faceManager.facesChanged -= OnFacesChanged;
    }

    private void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        foreach (var face in args.added)
        {
            var faceObj = Instantiate(facePrefab, face.transform);
            _faceObjects[face.trackableId] = faceObj;
        }

        foreach (var face in args.updated)
        {
            if (_faceObjects.TryGetValue(face.trackableId, out var faceObj))
            {
                UpdateFaceObject(faceObj, face);
            }
        }

        foreach (var face in args.removed)
        {
            if (_faceObjects.TryGetValue(face.trackableId, out var faceObj))
            {
                Destroy(faceObj);
                _faceObjects.Remove(face.trackableId);
            }
        }
    }

    private void UpdateFaceObject(GameObject faceObj, ARFace face)
    {
        // Update blend shapes for facial expressions
        var skinnedMeshRenderer = faceObj.GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer != null && face.blendShapes != null)
        {
            foreach (var blendShape in face.blendShapes)
            {
                var index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key.ToString());
                if (index >= 0)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(index, blendShape.Value * 100f);
                }
            }
        }
    }
}
```

---

## 🥽 VR Standalone

### Stack Tecnológico VR Standalone

| Componente | Tecnologia | Versão |
|------------|------------|--------|
| **Engine** | Unity | 2023.2+ |
| **XR Framework** | XR Interaction Toolkit | 2.5+ |
| **Meta Quest** | Meta XR All-in-One SDK | 62+ |
| **Pico** | Pico XR SDK | 2.3+ |
| **OpenXR** | OpenXR Plugin | 1.9+ |
| **Input** | XR Input System | 1.7+ |
| **Locomotion** | XR Locomotion | 2.5+ |

### VR Setup

```csharp
// VRManager.cs
using UnityEngine.XR.Interaction.Toolkit;

public class VRManager : MonoBehaviour
{
    [Header("XR Components")]
    [SerializeField] private XROrigin xrOrigin;
    [SerializeField] private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] private ActionBasedSnapTurnProvider turnProvider;
    [SerializeField] private TeleportationProvider teleportProvider;

    [Header("Controllers")]
    [SerializeField] private XRController leftController;
    [SerializeField] private XRController rightController;

    private void Start()
    {
        ConfigureLocomotion();
        ConfigureControllers();
    }

    private void ConfigureLocomotion()
    {
        // Continuous movement
        moveProvider.moveSpeed = 2.0f;
        moveProvider.enableStrafe = true;

        // Snap turn
        turnProvider.turnAmount = 45f;

        // Teleportation
        teleportProvider.system = GetComponent<XRInteractionManager>();
    }

    private void ConfigureControllers()
    {
        // Configure haptics, input actions, etc.
    }
}
```

### VR Interaction

```csharp
// VRInteractable.cs
using UnityEngine.XR.Interaction.Toolkit;

public class VRInteractable : XRGrabInteractable
{
    [Header("Interaction Settings")]
    [SerializeField] private bool usePhysics = true;
    [SerializeField] private float throwForce = 1.5f;

    private Rigidbody rb;
    private Vector3 lastPosition;
    private Vector3 velocity;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (usePhysics && rb != null)
        {
            rb.isKinematic = true;
        }

        // Haptic feedback
        if (args.interactorObject is XRBaseControllerInteractor controller)
        {
            controller.SendHapticImpulse(0.5f, 0.1f);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (usePhysics && rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = velocity * throwForce;
        }
    }

    private void Update()
    {
        if (isSelected)
        {
            velocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
        }
    }
}
```

### Hand Tracking

```csharp
// VRHandTracker.cs
using UnityEngine.XR.Hands;

public class VRHandTracker : MonoBehaviour
{
    [SerializeField] private XRHandSubsystem handSubsystem;
    [SerializeField] private GameObject handPrefab;

    private Dictionary<Handedness, GameObject> _hands = new();

    private void Start()
    {
        if (handSubsystem != null)
        {
            handSubsystem.trackingAcquired += OnTrackingAcquired;
            handSubsystem.trackingLost += OnTrackingLost;
            handSubsystem.updatedHands += OnHandsUpdated;
        }
    }

    private void OnTrackingAcquired(XRHand hand)
    {
        if (!_hands.ContainsKey(hand.handedness))
        {
            var handObj = Instantiate(handPrefab);
            _hands[hand.handedness] = handObj;
        }
    }

    private void OnTrackingLost(XRHand hand)
    {
        if (_hands.TryGetValue(hand.handedness, out var handObj))
        {
            handObj.SetActive(false);
        }
    }

    private void OnHandsUpdated(XRHandSubsystem subsystem, XRHandSubsystem.UpdateSuccessFlags flags, XRHandSubsystem.UpdateType updateType)
    {
        foreach (var hand in subsystem.hands)
        {
            if (_hands.TryGetValue(hand.handedness, out var handObj))
            {
                UpdateHandPose(handObj, hand);
            }
        }
    }

    private void UpdateHandPose(GameObject handObj, XRHand hand)
    {
        // Update hand joint positions and rotations
        foreach (var joint in hand.joints)
        {
            if (joint.TryGetPose(out var pose))
            {
                // Update corresponding bone in hand model
            }
        }
    }
}
```

### Meta Quest Store Submission

```csharp
// MetaQuestManager.cs
using Unity.XR.Oculus;

public class MetaQuestManager : MonoBehaviour
{
    [Header("Quest Settings")]
    [SerializeField] private OVRManager ovrManager;
    [SerializeField] private bool useFixedFoveatedRendering = true;
    [SerializeField] private OVRManager.FixedFoveatedRenderingLevel ffrLevel = OVRManager.FixedFoveatedRenderingLevel.High;

    private void Start()
    {
        ConfigureQuestSettings();
    }

    private void ConfigureQuestSettings()
    {
        // Performance
        OVRManager.cpuLevel = 2;
        OVRManager.gpuLevel = 2;

        // Fixed Foveated Rendering
        if (useFixedFoveatedRendering)
        {
            OVRManager.fixedFoveatedRenderingLevel = ffrLevel;
            OVRManager.useDynamicFoveatedRendering = true;
        }

        // Refresh rate (Quest 2: 90Hz, Quest 3: 120Hz)
        float[] supportedRates = OVRManager.display.displayFrequenciesAvailable;
        if (supportedRates.Length > 0)
        {
            OVRManager.display.displayFrequency = supportedRates[supportedRates.Length - 1];
        }
    }
}
```

### Pico Store Submission

```csharp
// PicoManager.cs
using Unity.XR.PXR;

public class PicoManager : MonoBehaviour
{
    [Header("Pico Settings")]
    [SerializeField] private bool enableFoveatedRendering = true;
    [SerializeField] private FoveationLevel foveationLevel = FoveationLevel.High;

    private void Start()
    {
        ConfigurePicoSettings();
    }

    private void ConfigurePicoSettings()
    {
        // Foveated Rendering
        if (enableFoveatedRendering)
        {
            PXR_Manager.EnableEyeTracking = true;
            PXR_Manager.FoveationLevel = foveationLevel;
        }

        // Refresh rate
        PXR_System.SetDisplayRefreshRate(90f);

        // Performance
        PXR_System.SetPerformanceLevels(PxrPerfSettings.CPU_LEVEL_3, PxrPerfSettings.GPU_LEVEL_3);
    }
}
```

---

## 🌐 WebXR

### Stack Tecnológico WebXR

| Componente | Tecnologia | Versão |
|------------|------------|--------|
| **Engine** | Unity | 2023.2+ |
| **WebXR** | WebXR Export | 0.18+ |
| **Build** | WebGL | 2.0 |
| **Compression** | Brotli/Gzip | - |

### WebXR Setup

```csharp
// WebXRManager.cs
#if UNITY_WEBGL && !UNITY_EDITOR
using WebXR;
#endif

public class WebXRManager : MonoBehaviour
{
    #if UNITY_WEBGL && !UNITY_EDITOR
    private WebXRState xrState = WebXRState.NORMAL;
    #endif

    [Header("WebXR Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject vrRig;

    private void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        WebXRManager.OnXRChange += OnXRChange;
        WebXRManager.OnHeadsetUpdate += OnHeadsetUpdate;
        WebXRManager.OnControllerUpdate += OnControllerUpdate;
        #endif
    }

    #if UNITY_WEBGL && !UNITY_EDITOR
    private void OnXRChange(WebXRState state, int viewsCount, Rect leftRect, Rect rightRect)
    {
        xrState = state;

        switch (state)
        {
            case WebXRState.AR:
            case WebXRState.VR:
                vrRig.SetActive(true);
                mainCamera.enabled = false;
                break;
            case WebXRState.NORMAL:
                vrRig.SetActive(false);
                mainCamera.enabled = true;
                break;
        }
    }

    private void OnHeadsetUpdate(Matrix4x4 leftProjection, Matrix4x4 rightProjection,
                                 Matrix4x4 leftView, Matrix4x4 rightView,
                                 Matrix4x4 sitStand)
    {
        // Update VR camera matrices
    }

    private void OnControllerUpdate(WebXRControllerData controllerData)
    {
        // Update controller positions and buttons
    }
    #endif
}
```

### WebGL Build Optimization

```csharp
// WebGLOptimizer.cs (Editor Script)
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class WebGLOptimizer : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.WebGL)
        {
            // Compression
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;

            // Memory
            PlayerSettings.WebGL.memorySize = 512; // MB

            // Code optimization
            PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;
            PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Master);

            // Linker
            PlayerSettings.stripEngineCode = true;
            PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.High);

            Debug.Log("WebGL build optimized");
        }
    }
}
#endif
```

### Platform Detection

```csharp
// PlatformDetector.cs
public enum XRPlatform
{
    None,
    ARMobile,      // iOS/Android AR
    MetaQuest,     // Quest 2/3/Pro
    Pico,          // Pico Neo 3/4
    WebXR,         // Browser-based XR
    PCVR           // SteamVR, Oculus PC
}

public class PlatformDetector : MonoBehaviour
{
    public static XRPlatform CurrentPlatform { get; private set; }

    private void Awake()
    {
        DetectPlatform();
    }

    private void DetectPlatform()
    {
        #if UNITY_WEBGL
        CurrentPlatform = XRPlatform.WebXR;
        #elif UNITY_ANDROID
            #if UNITY_EDITOR
            CurrentPlatform = XRPlatform.MetaQuest; // Default for testing
            #else
            if (SystemInfo.deviceModel.Contains("Quest"))
                CurrentPlatform = XRPlatform.MetaQuest;
            else if (SystemInfo.deviceModel.Contains("Pico"))
                CurrentPlatform = XRPlatform.Pico;
            else
                CurrentPlatform = XRPlatform.ARMobile;
            #endif
        #elif UNITY_IOS
        CurrentPlatform = XRPlatform.ARMobile;
        #elif UNITY_STANDALONE_WIN
        CurrentPlatform = XRPlatform.PCVR;
        #else
        CurrentPlatform = XRPlatform.None;
        #endif

        Debug.Log($"Detected platform: {CurrentPlatform}");
    }

    public static bool IsStandaloneVR()
    {
        return CurrentPlatform == XRPlatform.MetaQuest ||
               CurrentPlatform == XRPlatform.Pico ||
               CurrentPlatform == XRPlatform.PCVR;
    }

    public static bool IsARMobile()
    {
        return CurrentPlatform == XRPlatform.ARMobile;
    }

    public static bool IsWebXR()
    {
        return CurrentPlatform == XRPlatform.WebXR;
    }
}
```

---

## ⚡ Performance

### Optimization Guidelines

```csharp
// PerformanceManager.cs
public class PerformanceManager : MonoBehaviour
{
    [Header("Target Performance")]
    [SerializeField] private int targetFrameRate = 90; // Quest 2/3
    [SerializeField] private bool enableDynamicResolution = true;

    [Header("Quality Settings")]
    [SerializeField] private int maxLODLevel = 2;
    [SerializeField] private float shadowDistance = 50f;

    private void Start()
    {
        ConfigurePerformance();
    }

    private void ConfigurePerformance()
    {
        // Frame rate
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0;

        // Dynamic resolution
        if (enableDynamicResolution)
        {
            XRSettings.eyeTextureResolutionScale = 1.0f;
        }

        // Quality
        QualitySettings.maximumLODLevel = maxLODLevel;
        QualitySettings.shadowDistance = shadowDistance;
        QualitySettings.shadows = ShadowQuality.HardOnly;

        // Physics
        Physics.defaultSolverIterations = 6;
        Physics.defaultSolverVelocityIterations = 1;
    }
}
```

### Object Pooling

```csharp
// ObjectPool.cs
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;
    [SerializeField] private int maxSize = 100;

    private Queue<GameObject> _pool = new();
    private HashSet<GameObject> _active = new();

    private void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        var obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }

    public GameObject Get()
    {
        GameObject obj;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else if (_active.Count < maxSize)
        {
            obj = CreateObject();
        }
        else
        {
            return null;
        }

        obj.SetActive(true);
        _active.Add(obj);
        return obj;
    }

    public void Return(GameObject obj)
    {
        if (_active.Remove(obj))
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}
```

### LOD System

```csharp
// DynamicLOD.cs
public class DynamicLOD : MonoBehaviour
{
    [SerializeField] private LODGroup lodGroup;
    [SerializeField] private float[] lodDistances = { 10f, 25f, 50f };

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        ConfigureLOD();
    }

    private void ConfigureLOD()
    {
        var lods = new LOD[lodDistances.Length];

        for (int i = 0; i < lodDistances.Length; i++)
        {
            var renderers = GetRenderersForLOD(i);
            lods[i] = new LOD(1.0f / lodDistances[i], renderers);
        }

        lodGroup.SetLODs(lods);
        lodGroup.RecalculateBounds();
    }

    private Renderer[] GetRenderersForLOD(int level)
    {
        // Return appropriate renderers for LOD level
        return GetComponentsInChildren<Renderer>();
    }
}
```

---

## ✅ Checklist

### AR Mobile Development

- [ ] Unity 2023.2+ com AR Foundation 5.1+
- [ ] ARKit XR Plugin (iOS)
- [ ] ARCore XR Plugin (Android)
- [ ] Plane detection configurado
- [ ] Raycast para placement
- [ ] Anchor management
- [ ] Image tracking (se necessário)
- [ ] Face tracking (se necessário)
- [ ] Light estimation
- [ ] Occlusion configurado

### VR Standalone Development

- [ ] Unity 2023.2+ com XR Interaction Toolkit 2.5+
- [ ] Meta XR All-in-One SDK (Quest)
- [ ] Pico XR SDK (se necessário)
- [ ] OpenXR Plugin configurado
- [ ] Locomotion configurado (move, turn, teleport)
- [ ] Grab interactions
- [ ] UI interactions
- [ ] Hand tracking (se necessário)
- [ ] Haptic feedback
- [ ] Comfort settings (vignette, snap turn)
- [ ] Performance 90fps (Quest 2/3)
- [ ] Fixed Foveated Rendering ativo

### WebXR Development

- [ ] Unity 2023.2+ com WebXR Export 0.18+
- [ ] WebGL build target configurado
- [ ] Brotli compression ativo
- [ ] Memory size otimizado (512MB)
- [ ] Code stripping configurado
- [ ] Platform detection implementado
- [ ] Fallback para desktop/mobile
- [ ] Loading screen otimizado
- [ ] Asset bundles para streaming

### Store Submission

#### Meta Quest Store
- [ ] Meta Developer Account criado
- [ ] App ID registrado
- [ ] Entitlements configurados
- [ ] Privacy Policy URL
- [ ] Content rating completo
- [ ] Screenshots/videos (Quest)
- [ ] APK < 1GB
- [ ] Performance requirements atendidos
- [ ] Comfort rating definido

#### Pico Store
- [ ] Pico Developer Account criado
- [ ] App registrado na plataforma
- [ ] SDK integration validado
- [ ] Content compliance verificado
- [ ] APK otimizado
- [ ] Store assets preparados

### Performance

- [ ] Target 90fps mantido
- [ ] Object pooling implementado
- [ ] LOD system configurado
- [ ] Occlusion culling ativo
- [ ] Baked lighting quando possível
- [ ] Texture compression
- [ ] Mesh optimization
- [ ] Physics optimization
- [ ] Draw calls < 100
- [ ] Memory < 2GB

### Quality

- [ ] Clean Architecture implementada
- [ ] Service Locator/DI configurado
- [ ] Unit tests (>70% coverage)
- [ ] Play mode tests
- [ ] Error handling robusto
- [ ] Analytics integrado
- [ ] Crash reporting
- [ ] Performance profiling
- [ ] Platform detection implementado
- [ ] Multi-platform testing

### Cross-Platform

- [ ] Código compartilhado entre plataformas
- [ ] Platform-specific code isolado (#if directives)
- [ ] Input abstraction layer
- [ ] UI adaptável por plataforma
- [ ] Performance targets por plataforma
- [ ] Build automation configurado
- [ ] CI/CD para múltiplas plataformas

---

<div align="center">

## 🎯 DATAMETRIA Unity AR/VR Standard v1.0.0

**Desenvolvido com ❤️ pela equipe DATAMETRIA**

[![DATAMETRIA](https://img.shields.io/badge/DATAMETRIA-Unity%20Ready-blue?style=for-the-badge)](https://github.com/datametria/DATAMETRIA-standards)

**CTO**: Vander Loto | **Email**: vander.loto@datametria.io

</div>
