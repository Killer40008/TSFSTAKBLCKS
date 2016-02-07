using UnityEngine;
using System.Collections;

public class Tank_Fire : MonoBehaviour
{


    public void Fire(float strength = 0, bool AI = false)
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
        }
    }

    private void AIWeaponsConfig()
    {
        if (WeaponsCombo.CurrentWeapon is Missile)
            Missile.SelectRandomTankForAI(this.transform.parent.gameObject);

    }

    public void FireButtonClicked()
    {
        GameObject.Find("PlayerTimer").GetComponent<Timer>().StopTimer();
        Managers.TurnManager.PlayerTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().Fire();


        //decrease count
        if (Managers.WeaponManager.WeaponType != Weapons.Normal_Bomb)
            WeaponsClass.WeaponsQuantities[Managers.WeaponManager.WeaponType]--;
    }

}
