using UnityEngine;
using System.Collections;

public class BuyButton : MonoBehaviour 
{
    public void OnClick()
    {
        GameObject fade = GameObject.Find("Fade");
        ShowPanel(fade);
        ShowPanel(GameObject.Find("Buy"));

    }


    public static void ShowPanel(GameObject panel)
    {
        panel.GetComponent<CanvasGroup>().alpha = 1;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        panel.GetComponent<CanvasGroup>().interactable = true;
    }


    public static void HidePanel(GameObject panel)
    {
        panel.GetComponent<CanvasGroup>().alpha = 0;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        panel.GetComponent<CanvasGroup>().interactable = false;
    }


    //------------------------------------



    public void OnPackage_1_Click()
    {
       var b = GameObject.Find("Buy").GetComponent<Billing>();
       b.BuyProductID("coins_1");
    }
    public void OnPackage_2_Click()
    {
        var b = GameObject.Find("Buy").GetComponent<Billing>();
        b.BuyProductID("coins_2");
    }
    public void OnPackage_3_Click()
    {
        var b = GameObject.Find("Buy").GetComponent<Billing>();
        b.BuyProductID("coins_3");
    }
    public void OnPackage_4_Click()
    {
        var b = GameObject.Find("Buy").GetComponent<Billing>();
        b.BuyProductID("coins_4");
    }
    public void OnPackage_5_Click()
    {
        var b = GameObject.Find("Buy").GetComponent<Billing>();
        b.BuyProductID("coins_5");
    }
    public  void OnPackage_6_Click()
    {
        var b = GameObject.Find("Buy").GetComponent<Billing>();
        b.BuyProductID("coins_6");
    }
}
