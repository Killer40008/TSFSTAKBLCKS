using UnityEngine;
using System.Collections;

public class BuyWeapon : MonoBehaviour
{
    public Weapons weapon;

    public void OnClick()
    {
        int playerMoney = ScoreModule.GetPlayerData().PlayerMoney;
        int cost = 0;
        switch (weapon)
        {
            case Weapons.Airstike:
                cost = Airstike.COST;
                break;
            case Weapons.Auto_Missile:
                cost = Missile.COST;
                break;
            case Weapons.Cluster_Bomb:
                cost = Cluster_Bomb.COST;
                break;
            case Weapons.Large_Bomb:
                cost = Large_Bomb.COST;
                break;
            case Weapons.Lighting:
                cost = Lighting.COST;
                break;
            case Weapons.Mega_Bomb:
                cost = Mega_Bomb.COST;
                break;
            case Weapons.Mega_Shell:
                cost = MegaShell_bomb.COST;
                break;
            case Weapons.Molotove:
                cost = Molotove.COST;
                break;
            case Weapons.Moving_Bomb:
                cost = Mega_Bomb.COST;
                break;
            case Weapons.Nuclear_bomb:
                cost = NeclearBomb.COST;
                break;
            case Weapons.Shell:
                cost = Shell_bomb.COST;
                break;
            case Weapons.Small_Bomb:
                cost = Small_Bomb.COST;
                break;
        }

        if (cost <= playerMoney)
        {
            WeaponsClass.WeaponsQuantities[weapon]++;

            PlayerData pd = ScoreModule.GetPlayerData();
            pd.PlayerWeapons = WeaponsClass.WeaponsQuantities;
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
