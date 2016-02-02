using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class WeaponsCombo : MonoBehaviour 
{
    public static IWeapon CurrentWeapon;
    public Weapons WeaponType;

    public void OnWeaponsComboOpen()
    {
        GameObject.Find("WeaponsCombo").transform.FindChild("Border").gameObject.SetActive(true);

    }

    public void OnWeaponsSelected(GameObject button)
    {
        WeaponType = button.GetComponent<WeaponsClass>().weapon;

        GameObject.Find("WeaponsCombo").transform.FindChild("Text").GetComponent<Text>().text
            = WeaponType.ToString().Replace("_", " ");

        if (WeaponType != Weapons.Normal_Bomb)
            GameObject.Find("WeaponsCombo").transform.FindChild("Count").GetComponent<Text>().text
                = WeaponsClass.WeaponsQuantities[WeaponType].ToString();
        else
            GameObject.Find("WeaponsCombo").transform.FindChild("Count").GetComponent<Text>().text = "∞";


        GameObject.Find("WeaponsCombo").transform.FindChild("Border").gameObject.SetActive(false);
    }

    public void DrawWeaponInfoInUI()
    {

        if (WeaponType != Weapons.Normal_Bomb)
            GameObject.Find("WeaponsCombo").transform.FindChild("Count").GetComponent<Text>().text
                = WeaponsClass.WeaponsQuantities[WeaponType].ToString();
      

    }

    public void GenerateGameObject()
    {
        switch (WeaponType)
        {
            case Weapons.Airstike: CurrentWeapon = new GameObject().AddComponent<Airstike>(); break;
            case Weapons.Cluster_Bomb: CurrentWeapon = new GameObject().AddComponent<Cluster_Bomb>(); break;
            case Weapons.Large_Bomb: CurrentWeapon = new GameObject().AddComponent<Large_Bomb>(); break;
            case Weapons.Laser: CurrentWeapon = new GameObject().AddComponent<Laser>(); break;
            case Weapons.Magnit_Bomb: CurrentWeapon = new GameObject().AddComponent<MagintBomb>(); break;
            case Weapons.Mega_Bomb: CurrentWeapon = new GameObject().AddComponent<Mega_Bomb>(); break;
            case Weapons.Mega_Shell: CurrentWeapon = new GameObject().AddComponent<MegaShell_bomb>(); ; break;
            case Weapons.Normal_Bomb: CurrentWeapon = new GameObject().AddComponent<Normal_Bomb>(); break;
            case Weapons.Nuclear_bomb: CurrentWeapon = new GameObject().AddComponent<NeclearBomb>(); break;
            case Weapons.Shell: CurrentWeapon = new GameObject().AddComponent<Shell_bomb>(); break;
            case Weapons.Moving_Bomb: CurrentWeapon = new GameObject().AddComponent<Slided_Bomb>(); ; break;
            case Weapons.Small_Bomb: CurrentWeapon = new GameObject().AddComponent<Small_Bomb>(); break;
        }
    }
}
