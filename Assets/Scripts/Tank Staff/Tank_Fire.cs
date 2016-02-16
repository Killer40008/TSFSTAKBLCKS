using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tank_Fire : MonoBehaviour
{
    public int FireCount = 1;

    public GameObject Fire(float strength = 0, bool AI = false)
    {

        if (enabled)
        {
            if (strength == 0) strength = StrenghSlider.Strengh;

            Managers.WeaponManager.GenerateGameObject();
            IWeapon weapon = (IWeapon)WeaponsCombo.CurrentWeapon;
            if (!AI) weapon.Drag = (100 - Managers.DamageManager.GetStrength(this.transform.parent.gameObject)) / 100;

            Sprite[] sprites = Managers.SpawnManager.BombSprites;
            Object[] Explosions = Managers.SpawnManager.BombExplosions;
            weapon.Create(sprites[weapon.GameObjectSpriteIndex], Explosions[weapon.ExplosionSpriteIndex],
                strength, this.transform.parent.gameObject);

            if(AI) AIWeaponsConfig();

            weapon.Fire(this.transform.parent.gameObject);
            this.GetComponent<Burrell_Movement>().OnFire();
            this.enabled = false;
            return weapon.WeaponObj;
        }
        return null;
    }

    private void AIWeaponsConfig()
    {
        if (WeaponsCombo.CurrentWeapon is Missile)
            Missile.SelectRandomTankForAI(this.transform.parent.gameObject);
        else if (WeaponsCombo.CurrentWeapon is Airstike)
            Airstike.SelectRandomTankForAI(this.transform.parent.gameObject);

    }

    public void FireButtonClicked()
    {
        if (CheckIfWeaponsValid() && Managers.DestroyManager.Win == false)
        {
            GameObject.Find("PlayerTimer").GetComponent<Timer>().StopTimer();

            if (FireCount == 1)
            {
                Managers.TurnManager.PlayerTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().Fire();
                if (CheckIfHasSecondBurrell())
                    FireCount = 2;
                
            }
            else
            {
                Managers.TurnManager.PlayerTank.transform.FindChild("Burrell2").GetComponent<Tank_Fire>().Fire();
                Managers.TurnManager.PlayerTank.transform.FindChild("Burrell2").GetComponent<Burrell_Movement>().enabled = false;
                Managers.TurnManager.PlayerTank.transform.FindChild("Burrell2").GetComponent<Tank_Fire>().enabled = false;
                FireCount = 1;
            }



            //decrease count
            if (Managers.WeaponManager.WeaponType != Weapons.Normal_Bomb)
                WeaponsClass.WeaponsQuantities[Managers.WeaponManager.WeaponType]--;
        }
    }

    private bool CheckIfHasSecondBurrell()
    {
        for (int i = 0; i < Managers.TurnManager.PlayerTank.transform.childCount; i++)
        {
            if (Managers.TurnManager.PlayerTank.transform.GetChild(i).tag == "SecondBurrell" && FireCount == 1)
            {
                return true;
            }
        }
        return false;
    }


    private bool CheckIfWeaponsValid()
    {
        if (WeaponsClass.WeaponsQuantities[Managers.WeaponManager.WeaponType] == 0)
        {
            StartCoroutine(Highlight());
            return false;
        }
        else return true;
    }

    private IEnumerator Highlight()
    {
        GameObject.Find("WeaponsCombo").GetComponent<Image>().color = new Color32(255, 0, 0, 20);
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("WeaponsCombo").GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("WeaponsCombo").GetComponent<Image>().color = new Color32(255, 0, 0, 20);
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("WeaponsCombo").GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("WeaponsCombo").GetComponent<Image>().color = new Color32(255, 0, 0, 20);
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("WeaponsCombo").GetComponent<Image>().color = Color.white;
    }
}
