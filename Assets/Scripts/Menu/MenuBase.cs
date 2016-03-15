using UnityEngine;
using System.Collections;

public class MenuBase : MonoBehaviour {

    public static void BringToFront(GameObject panel)
    {
        panel.transform.SetParent(null, false);
        panel.transform.SetParent(GameObject.Find("Canvas").transform, false);
        panel.transform.localPosition = Vector3.zero;
    }

    public static IEnumerator FadeHide(CanvasGroup canvasGroup)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            yield return new WaitForEndOfFrame();
            alpha = Mathf.Lerp(alpha, 0, 0.5f * 0.5f);
            canvasGroup.alpha = alpha;
        }
    }
    public static IEnumerator FadeShow(CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(0.5f);
        float alpha = 0;
        while (alpha < 1)
        {
            yield return new WaitForEndOfFrame();
            alpha = Mathf.Lerp(alpha, 1, 0.5f * 0.1f);
            canvasGroup.alpha = alpha;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }


    //
    public static void HideAndShow(MonoBehaviour sender ,string panelHide, string panelShow)
    {

        //hide menu
        GameObject.Find(panelHide).GetComponent<CanvasGroup>().interactable = false;
        sender.StartCoroutine(MenuBase.FadeHide(GameObject.Find(panelHide).GetComponent<CanvasGroup>()));


        //show
        GameObject diffObj = GameObject.Find(panelShow);
        diffObj.GetComponent<CanvasGroup>().interactable = true;
        sender.StartCoroutine(MenuBase.FadeShow(diffObj.GetComponent<CanvasGroup>()));
        MenuBase.BringToFront(diffObj);
    }

    public static void HideAndShowInGame(MonoBehaviour sender, string panelHide, string panelShow)
    {

        //hide menu
        GameObject.Find(panelHide).GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find(panelHide).GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find(panelHide).GetComponent<CanvasGroup>().alpha = 0;


        //show
        GameObject diffObj = GameObject.Find(panelShow);
        diffObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        diffObj.GetComponent<CanvasGroup>().interactable = true;
        diffObj.GetComponent<CanvasGroup>().alpha = 1;
    }

    public static void Show(MonoBehaviour sender, string panelShow)
    {

    
        //show
        GameObject diffObj = GameObject.Find(panelShow);
        diffObj.GetComponent<CanvasGroup>().interactable = true;
        sender.StartCoroutine(MenuBase.FadeShow(diffObj.GetComponent<CanvasGroup>()));
        MenuBase.BringToFront(diffObj);
    }
}
