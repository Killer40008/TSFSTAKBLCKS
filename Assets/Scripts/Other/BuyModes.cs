using UnityEngine;
using System.Collections;

public class BuyModes : MonoBehaviour 
{

    public ModesClass.Modes mode;

    public void OnClick()
    {
        int playerMoney = ScoreModule.GetPlayerData().PlayerMoney;
        int cost = 0;
        switch (mode)
        {
            case ModesClass.Modes.AntiStrike:
                cost = 1000;
                break;
            case ModesClass.Modes.Double_Burrell:
                cost = 2000;
                break;
            case ModesClass.Modes.Double_Damage:
                cost = 2500;
                break;
            case ModesClass.Modes.Mini_Armor:
                cost = 2500;
                break;
            case ModesClass.Modes.Normal_Armor:
                cost = 3500;
                break;
            case ModesClass.Modes.Strong_Armor:
                cost = 7000;
                break;
            case ModesClass.Modes.Super_Armor:
                cost = 11000;
                break;
            case ModesClass.Modes.HealthPlus:
                cost = 500;
                break;
            case ModesClass.Modes.StrengthPlus:
                cost = 500;
                break;
        }

        if (cost <= playerMoney)
        {
            ModesClass.ModesQuantities[mode]++;

            PlayerData pd = ScoreModule.GetPlayerData();
            pd.PlayerModes = ModesClass.ModesQuantities;
            pd.PlayerMoney -= cost;

            ScoreModule.SavePlayerData(pd);
        }
        else
        {
            BuyButton.ShowPanel(GameObject.Find("Fade"));
            BuyButton.ShowPanel(GameObject.Find("NeedCoin"));
        }

    }


}
