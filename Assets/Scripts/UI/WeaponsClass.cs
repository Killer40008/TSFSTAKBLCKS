using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponsClass : MonoBehaviour
{
    public Weapons weapon;
    public static Dictionary<Weapons, int> WeaponsQuantities;



    public static Dictionary<Weapons, int> InitiallizeWeaponsQuantities(bool setDefault = false)
    {
        WeaponsQuantities = new Dictionary<Weapons, int>();

        if (ScoreModule.GetPlayerData() != null && setDefault == false)
        {
            WeaponsQuantities = ScoreModule.GetPlayerData().PlayerWeapons;
        }
        else
        {
            WeaponsQuantities.Clear();
            WeaponsQuantities.Add(Weapons.Normal_Bomb, 1);
            WeaponsQuantities.Add(Weapons.Cluster_Bomb, 10);
            WeaponsQuantities.Add(Weapons.Small_Bomb, 0);
            WeaponsQuantities.Add(Weapons.Large_Bomb, 0);
            WeaponsQuantities.Add(Weapons.Mega_Bomb, 0);
            WeaponsQuantities.Add(Weapons.Mega_Shell, 0);
            WeaponsQuantities.Add(Weapons.Moving_Bomb, 0);
            WeaponsQuantities.Add(Weapons.Nuclear_bomb, 0);
            WeaponsQuantities.Add(Weapons.Shell, 0);
            WeaponsQuantities.Add(Weapons.Airstike, 0);
            WeaponsQuantities.Add(Weapons.Lighting, 0);
            WeaponsQuantities.Add(Weapons.Auto_Missile, 0);
            WeaponsQuantities.Add(Weapons.Molotove, 0);
        }

        return WeaponsQuantities;
    }

    public static void SubtractWeaponQuantitie(Weapons weapon)
    {
        if (weapon != Weapons.Normal_Bomb)
        {
            WeaponsQuantities[weapon]--;
            PlayerData dt = ScoreModule.GetPlayerData();
            if (dt != null)
            {
                dt.PlayerWeapons = WeaponsQuantities;
                ScoreModule.SavePlayerData(dt);
            }
        }
    }
}
