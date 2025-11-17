using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // <- ADICIONE ISSO

public class InteractionController : MonoBehaviour
{
    public Camera cam;
    public float rayDistance = 3f;
    public float sphereRadius = 1f;

    private Interactable currentInteractable;

    void Update()
    {
        HandleRaycast();
        DetectTouchOrClick();
    }

    void HandleRaycast()
    {
        if (currentInteractable != null)
            currentInteractable.Highlight(false);

        currentInteractable = null;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit, rayDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                currentInteractable.Highlight(true);
            }
        }
    }

    void DetectTouchOrClick()
    {
        // --- TOUCHSCREEN (Mobile) ---
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 pos = Touchscreen.current.primaryTouch.position.ReadValue();
            TryRaycastAtScreenPosition(pos);
            return;
        }

        // --- MOUSE (PC / Editor) ---
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 pos = Mouse.current.position.ReadValue();
            TryRaycastAtScreenPosition(pos);
        }
    }

    void TryRaycastAtScreenPosition(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Interactable i = hit.collider.GetComponent<Interactable>();
            if (i != null)
                i.Interact();
        }
    }
}