using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Linq;

public class WeaponsCombo : MonoBehaviour 
{
    public static IWeapon CurrentWeapon;
    public Weapons WeaponType;
    public GameObject lastButton;

    public void OnWeaponsComboOpen()
    {
        GameObject.Find("WeaponsCombo").transform.FindChild("Border").gameObject.SetActive(true);

        if (WeaponType == Weapons.Auto_Missile)
        {
            if (Missile.highlightCoroutines.Count > 0)
            {

                Missile.highlightCoroutines.ForEach(c => Managers.Me.StopCoroutine(c));
                Missile.highlightCoroutines.Clear();
            }

            Managers.ShowSliders(true);
            Managers.TurnManager.tanks.ToList().ForEach(e => e.GetComponent<Tank>().Color = e.GetComponent<Tank>().OrginalColor);
            Managers.TurnManager.tanks.Where(t => t.activeSelf == true).ToList().ForEach(e => e.GetComponent<Focus>().DeActive());
        }
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

        //call selected if magnit bomb
        if (WeaponType == Weapons.Auto_Missile) Missile.Selected();

        lastButton = button;
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
        GameObject wObj = new GameObject() { layer = 9 };
        switch (WeaponType)
        {
            case Weapons.Airstike: CurrentWeapon = wObj.AddComponent<Airstike>(); break;
            case Weapons.Cluster_Bomb: CurrentWeapon = wObj.AddComponent<Cluster_Bomb>(); break;
            case Weapons.Large_Bomb: CurrentWeapon = wObj.AddComponent<Large_Bomb>(); break;
            case Weapons.Laser: CurrentWeapon = wObj.AddComponent<Laser>(); break;
            case Weapons.Auto_Missile: CurrentWeapon = wObj.AddComponent<Missile>(); break;
            case Weapons.Mega_Bomb: CurrentWeapon = wObj.AddComponent<Mega_Bomb>(); break;
            case Weapons.Mega_Shell: CurrentWeapon = wObj.AddComponent<MegaShell_bomb>(); ; break;
            case Weapons.Normal_Bomb: CurrentWeapon = wObj.AddComponent<Normal_Bomb>(); break;
            case Weapons.Nuclear_bomb: CurrentWeapon = wObj.AddComponent<NeclearBomb>(); break;
            case Weapons.Shell: CurrentWeapon = wObj.AddComponent<Shell_bomb>(); break;
            case Weapons.Moving_Bomb: CurrentWeapon = wObj.AddComponent<Slided_Bomb>(); ; break;
            case Weapons.Small_Bomb: CurrentWeapon = wObj.AddComponent<Small_Bomb>(); break;
            case Weapons.Molotove: CurrentWeapon = wObj.AddComponent<Molotove>(); break;
        }
    }
}
