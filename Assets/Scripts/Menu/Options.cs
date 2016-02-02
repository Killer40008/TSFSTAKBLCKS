using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour 
{
    public void OnOptionsButtonClick()
    {
        //hide menu
        GameObject.Find("Menu").GetComponent<CanvasGroup>().interactable = false;
        StartCoroutine(MenuBase.FadeHide(GameObject.Find("Menu").GetComponent<CanvasGroup>()));


        //show diffcult
        GameObject diffObj = GameObject.Find("Option");
        diffObj.GetComponent<CanvasGroup>().interactable = true;
        StartCoroutine(MenuBase.FadeShow(diffObj.GetComponent<CanvasGroup>()));
        MenuBase.BringToFront(diffObj);

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Option").GetComponent<CanvasGroup>().interactable)
        {
            //hide store
            GameObject.Find("Option").GetComponent<CanvasGroup>().interactable = false;
            StartCoroutine(MenuBase.FadeHide(GameObject.Find("Option").GetComponent<CanvasGroup>()));


            //show main
            GameObject diffObj = GameObject.Find("Menu");
            diffObj.GetComponent<CanvasGroup>().interactable = true;
            StartCoroutine(MenuBase.FadeShow(diffObj.GetComponent<CanvasGroup>()));
            MenuBase.BringToFront(diffObj);
        }
    }
}
