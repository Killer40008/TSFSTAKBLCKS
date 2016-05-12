using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    public  bool AllowBackButton = true;



    public void OnStoreButtonClick()
    {

        RoundManager.OpenNormalStore();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && AllowBackButton && GameObject.Find("Fade").GetComponent<CanvasGroup>().interactable)
        {
            if (SceneManager.GetActiveScene().name == "Store")
            {
                BuyButton.HidePanel(GameObject.Find("Fade"));
                BuyButton.HidePanel(GameObject.Find("Buy"));
            }
            else
                MenuBase.HideAndShowInGame(this, "Store", "Menu");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && AllowBackButton)
        {
            if (RoundManager.ShowCounter == false)
                SceneManager.LoadScene("Menu");
        }
    }


    public void OnShowModes()
    {
        //show modes buttons
        GetComponent<Animator>().SetBool("ShowWeapons", false);
        GetComponent<Animator>().SetBool("ShowModes", true);

        GameObject.Find("LeftButton").GetComponent<Button>().interactable = false;
        GameObject.Find("RightButton").GetComponent<Button>().interactable = true;
    }

    public void OnShowWeapons()
    {
        //show weapons buttons
        GetComponent<Animator>().SetBool("ShowModes", false);
        GetComponent<Animator>().SetBool("ShowWeapons", true);

        GameObject.Find("LeftButton").GetComponent<Button>().interactable = true;
        GameObject.Find("RightButton").GetComponent<Button>().interactable = false;


    }
}
