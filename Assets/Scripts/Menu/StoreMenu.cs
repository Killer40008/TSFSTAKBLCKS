using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            if (SceneManager.GetActiveScene().name == "Menu")
                MenuBase.HideAndShow(this, "Store", "Menu");
            else
                MenuBase.HideAndShowInGame(this, "Store", "Menu");
        }
    }
}
