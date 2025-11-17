using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionName = "Interagir";

    private Material originalMat;
    public Material highlightMat;
    private MeshRenderer rend;

    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        if (rend != null)
            originalMat = rend.material;
    }

    public void Highlight(bool state)
    {
        if (rend == null || highlightMat == null) return;
        rend.material = state ? highlightMat : originalMat;
    }

    // AGORA É VIRTUAL
    public virtual void Interact()
    {
        Debug.Log("Interagiu com " + interactionName);
    }
}
