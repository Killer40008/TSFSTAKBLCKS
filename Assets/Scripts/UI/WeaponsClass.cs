﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponsClass  : MonoBehaviour
{
    public Weapons weapon;
    public static Dictionary<Weapons, int> WeaponsQuantities = new Dictionary<Weapons, int>();

    public static void InitiallizeWeaponsQuantities()
    {
        WeaponsQuantities.Add(Weapons.Normal_Bomb, 1);
        WeaponsQuantities.Add(Weapons.Cluster_Bomb, 5);
        WeaponsQuantities.Add(Weapons.Small_Bomb, 5);
        WeaponsQuantities.Add(Weapons.Large_Bomb, 5);
        WeaponsQuantities.Add(Weapons.Mega_Bomb, 5);
        WeaponsQuantities.Add(Weapons.Mega_Shell, 0);
        WeaponsQuantities.Add(Weapons.Moving_Bomb, 0);
        WeaponsQuantities.Add(Weapons.Nuclear_bomb, 0);
        WeaponsQuantities.Add(Weapons.Shell, 0);
        WeaponsQuantities.Add(Weapons.Airstike, 0);
        WeaponsQuantities.Add(Weapons.Laser, 0);
        WeaponsQuantities.Add(Weapons.Magnit_Bomb, 0);
    }
}
