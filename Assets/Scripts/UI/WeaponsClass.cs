using UnityEngine;
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
        WeaponsQuantities.Add(Weapons.Mega_Shell, 5);
        WeaponsQuantities.Add(Weapons.Moving_Bomb, 5);
        WeaponsQuantities.Add(Weapons.Nuclear_bomb, 5);
        WeaponsQuantities.Add(Weapons.Shell, 5);
        WeaponsQuantities.Add(Weapons.Airstike, 5);
        WeaponsQuantities.Add(Weapons.Lighting, 5);
        WeaponsQuantities.Add(Weapons.Auto_Missile, 5);
        WeaponsQuantities.Add(Weapons.Molotove, 5);
    }
}
