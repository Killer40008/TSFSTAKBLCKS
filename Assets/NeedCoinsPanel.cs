using UnityEngine;
using System.Collections;

public class NeedCoinsPanel : MonoBehaviour 
{
    public void OnBuyCoins()
    {
        BuyButton.HidePanel(this.gameObject);
        BuyButton.ShowPanel(GameObject.Find("Buy"));
    }
    public void OnCancel()
    {
        BuyButton.HidePanel(this.gameObject);
        BuyButton.HidePanel(GameObject.Find("Fade"));
    }


}
