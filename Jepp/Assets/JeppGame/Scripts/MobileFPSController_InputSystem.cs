using UnityEngine;
using UnityEngine.InputSystem;

public class MobileFPSController_InputSystem : MonoBehaviour
{
    public CameraBobbing cb;
    [Header("Mobile Joysticks")]
    public VirtualJoystick moveJoystick;
    public VirtualJoystick lookJoystick;

    [Header("References")]
    public Camera playerCamera;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float lookSensitivity = 2f;
    public float maxLookX = 80f;
    public float minLookX = -80f;

    private CharacterController controller;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float camRotX;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (GameManager.isMenuOpen)
        {
            moveJoystick.gameObject.SetActive(false);
            lookJoystick.gameObject.SetActive(false);

            moveInput = Vector2.zero;
            lookInput = Vector2.zero;
            return;
        }
        else
        {
            moveJoystick.gameObject.SetActive(true);
            lookJoystick.gameObject.SetActive(true);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        Move(moveInput);
        RotateView(lookInput);
#else
    Move(moveJoystick.Direction());
    RotateView(lookJoystick.Direction() * 5f); // aumenta sensibilidade pra toque
#endif

        ApplyGravity();
        cb.isWalking = moveJoystick.Direction().magnitude > 0.1f;
    }
    void Move(Vector2 input)
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * speed * Time.deltaTime);
    }

    void RotateView(Vector2 input)
    {
        float mouseX = input.x * lookSensitivity;
        float mouseY = input.y * lookSensitivity;

        camRotX -= mouseY;
        camRotX = Mathf.Clamp(camRotX, minLookX, maxLookX);

        playerCamera.transform.localRotation = Quaternion.Euler(camRotX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void Close()
    {
        GameManager.isMenuOpen = false;
    }
}
