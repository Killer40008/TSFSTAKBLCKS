using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Tank_Fire : MonoBehaviour
{


    public GameObject Fire(float strength = 0, bool AI = false)
    {

        if (enabled)
        {
            if (strength == 0) strength = StrenghSlider.Strengh;

            if (AI) AIWeaponsConfig();


            WeaponData.AllowNextTurn = true;
            Managers.WeaponManager.GenerateGameObject();
            IWeapon weapon = (IWeapon)WeaponsCombo.CurrentWeapon;
            if (!AI) weapon.Drag = (100 - Managers.DamageManager.GetStrength(this.transform.parent.gameObject)) / 100;

            Sprite[] sprites = Managers.SpawnManager.BombSprites;
            Object[] Explosions = Managers.SpawnManager.BombExplosions;
            weapon.Create(sprites[weapon.GameObjectSpriteIndex], Explosions[weapon.ExplosionSpriteIndex],
                strength, this.transform.parent.gameObject);

            weapon.Fire(this.transform.parent.gameObject);
            this.GetComponent<Burrell_Movement>().OnFire();
            this.enabled = false;
            return weapon.WeaponObj;
        }
        return null;
    }

    private bool IsPlayerConfigured()
    {


        if (Managers.WeaponManager.WeaponType == Weapons.Auto_Missile || Managers.WeaponManager.WeaponType == Weapons.Airstike)
        {
            if (Managers.TurnManager.tanks.Where(t => t.GetComponent<Focus>().TankSelected != null).FirstOrDefault() == null)
            {
                NotifyMessage.ShowMessage("Please Select Tank", 3);
                return false;
            }
        }
        return true;
    }

    private void AIWeaponsConfig()
    {
        //select random weapon
        int weaponsCount = System.Enum.GetNames(typeof(Weapons)).Length;


        switch (SinglePlayer.GameDifficulty)
        {
            case SinglePlayer.GameDifficultyEnum.Easy:
                Managers.WeaponManager.WeaponType = (Weapons)Random.Range(0, 7);
                break;
            case SinglePlayer.GameDifficultyEnum.Normal:
                Managers.WeaponManager.WeaponType = (Weapons)Random.Range(0, 10);
                break;
            case SinglePlayer.GameDifficultyEnum.Hard:
                Managers.WeaponManager.WeaponType = (Weapons)Random.Range(0, weaponsCount);
                break;
        }



        if (Managers.WeaponManager.WeaponType == Weapons.Auto_Missile)
            Missile.SelectRandomTankForAI(this.transform.parent.gameObject);
        else if (Managers.WeaponManager.WeaponType == Weapons.Airstike)
            Airstike.SelectRandomTankForAI(this.transform.parent.gameObject);



    }

    public void FireButtonClicked()
    {
        if (CheckIfWeaponsValid() && Managers.DestroyManager.Win == false)
        {

            if (!IsPlayerConfigured()) return;


            GameObject.Find("PlayerTimer").GetComponent<Timer>().StopTimer();
            GameObject.Find("Canvas").transform.FindChild("HUD").FindChild("DisabledPanel").GetComponent<CanvasGroup>().blocksRaycasts = true;


            if (Managers.TurnManager.CurrentTank.GetComponent<Tank>().BurrellCount == 1)
            {
                Managers.TurnManager.PlayerTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().Fire();
                if (Managers.TurnManager.PlayerTank.GetComponent<Tank>().DoubleBurrell)
                    Managers.TurnManager.CurrentTank.GetComponent<Tank>().BurrellCount = 2;

            }
            else
            {
                Transform burrell2 = Managers.TurnManager.PlayerTank.transform.FindChild("Burrell2");
                burrell2.GetComponent<Tank_Fire>().Fire();
                burrell2.GetComponent<Burrell_Movement>().enabled = false;
                burrell2.GetComponent<Tank_Fire>().enabled = false;
                Managers.TurnManager.CurrentTank.GetComponent<Tank>().BurrellCount = 1;
            }



            //decrease count
            WeaponsClass.SubtractWeaponQuantitie(Managers.WeaponManager.WeaponType);
        }
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
