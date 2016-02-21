using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour 
{
    public void OnOptionsButtonClick()
    {

        MenuBase.HideAndShow(this, "Menu", "Option");


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Option").GetComponent<CanvasGroup>().interactable)
        {

            MenuBase.HideAndShow(this, "Option", "Menu");

        }
    }
}
