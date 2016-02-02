using UnityEngine;
using System.Collections;

public class StoreMenu : MonoBehaviour
{

    public void OnStoreButtonClick()
    {
        //hide menu
        GameObject.Find("Menu").GetComponent<CanvasGroup>().interactable = false;
        StartCoroutine(MenuBase.FadeHide(GameObject.Find("Menu").GetComponent<CanvasGroup>()));


        //show store
        GameObject diffObj = GameObject.Find("Store");
        diffObj.GetComponent<CanvasGroup>().interactable = true;
        StartCoroutine(MenuBase.FadeShow(diffObj.GetComponent<CanvasGroup>()));
        MenuBase.BringToFront(diffObj);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&  GameObject.Find("Store").GetComponent<CanvasGroup>().interactable)
        {
            //hide store
            GameObject.Find("Store").GetComponent<CanvasGroup>().interactable = false;
            StartCoroutine(MenuBase.FadeHide(GameObject.Find("Store").GetComponent<CanvasGroup>()));


            //show main
            GameObject diffObj = GameObject.Find("Menu");
            diffObj.GetComponent<CanvasGroup>().interactable = true;
            StartCoroutine(MenuBase.FadeShow(diffObj.GetComponent<CanvasGroup>()));
            MenuBase.BringToFront(diffObj);
        }
    }
}
