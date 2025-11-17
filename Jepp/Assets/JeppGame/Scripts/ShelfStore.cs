using UnityEngine;

public class ShelfStore : Interactable
{
    public GameObject storeMenu;

    public override void Interact()
    {
        storeMenu.SetActive(true);
        GameManager.isMenuOpen = true;
    }
}