using UnityEngine;
using System.Collections;

public class StoreMenu : MonoBehaviour
{

    public void OnStoreButtonClick()
    {
       
        MenuBase.HideAndShow(this, "Menu", "Store");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&  GameObject.Find("Store").GetComponent<CanvasGroup>().interactable)
        {
            MenuBase.HideAndShow(this, "Store", "Menu");

        }
    }
}
