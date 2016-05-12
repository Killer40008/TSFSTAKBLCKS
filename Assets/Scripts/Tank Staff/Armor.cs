using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{
    public GameObject Tank;
    public float ArmorStrength;
    float currentStrength;

    void Start()
    {
        if (Tank.activeSelf)
        {
            this.transform.eulerAngles = this.transform.parent.eulerAngles;
        }
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = Mathf.Clamp(currentStrength / ArmorStrength, 0, 0.4243f);
        GetComponent<SpriteRenderer>().color = color;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb" && other.GetComponent<IWeapon>().Bomb.SoruceTank != Tank)
        {
            //setmoney
            Managers.PlayerInfos.AddMoneyToPlayer(other.GetComponent<IWeapon>().Bomb.SoruceTank, 100);
            //
            int dMultiply = other.GetComponent<IWeapon>().Bomb.SoruceTank.GetComponent<Tank>().DoubleDamage == true ? 2 : 1;
            float damage = (other.gameObject.GetComponent<IWeapon>().Bomb.Damage) * dMultiply;
            currentStrength -=  damage;
            #region ReduceDamageIFShell
            if (WeaponsCombo.CurrentWeapon is MegaShell_bomb || WeaponsCombo.CurrentWeapon is Shell_bomb)
            {
                damage /= 4;
            }
            #endregion

            Color color = GetComponent<SpriteRenderer>().color;
            if (currentStrength <= 0)
            {
                color.a = 0;
                other.gameObject.GetComponent<IWeapon>().Bomb.FloorHit(other.gameObject, true);
                Managers.DamageManager.SubstractHealth(Tank, Mathf.Abs(currentStrength));
                Managers.DamageManager.SubstractStrength(Tank, Mathf.Abs(currentStrength));

                bool destroyed = Managers.DestroyManager.CheckAndDestroy(Tank);

                //set money
                if (Tank != other.gameObject.GetComponent<IWeapon>().Bomb.SoruceTank)
                {
                    if (destroyed)
                        Managers.PlayerInfos.AddMoneyToPlayer(other.gameObject.GetComponent<IWeapon>().Bomb.SoruceTank, 500);
                    else
                        Managers.PlayerInfos.AddMoneyToPlayer(other.gameObject.GetComponent<IWeapon>().Bomb.SoruceTank, 50);
                }

                if (Tank.activeSelf)
                {
                    StartCoroutine(DeactiveArmor());
                }
            }
            else
            {
                color.a = Mathf.Clamp(currentStrength / ArmorStrength, 0, 0.4243f);
                GetComponent<SpriteRenderer>().color = color;
                other.gameObject.GetComponent<IWeapon>().Bomb.FloorHit(other.gameObject, true);
            }

            GetComponent<SpriteRenderer>().color = color;   
        }
    }


    public void OnTriggerHit(float damage)
    {
        if (WeaponsCombo.CurrentWeapon is MegaShell_bomb || WeaponsCombo.CurrentWeapon is Shell_bomb)
        {
            damage /= 5;
        }

        currentStrength -= damage;
        Color color = GetComponent<SpriteRenderer>().color;
        if (currentStrength <= 0)
        {
            color.a = 0;
            Managers.DamageManager.SubstractHealth(Tank, Mathf.Abs(currentStrength));
            Managers.DamageManager.SubstractStrength(Tank, Mathf.Abs(currentStrength));
            Managers.DestroyManager.CheckAndDestroy(Tank);



            if (Tank.activeSelf)
            {
                StartCoroutine(DeactiveArmor());
            }
        }
        else
        {
            color.a = Mathf.Clamp(currentStrength / ArmorStrength, 0, 0.4243f);
            GetComponent<SpriteRenderer>().color = color;
        }

        GetComponent<SpriteRenderer>().color = color;
    }
    

    public void OnLightingEnter(GameObject tank)
    {
        int dMultiply = tank.GetComponent<Tank>().DoubleDamage == true ? 2 : 1;

        float damage = Lighting.Damage * dMultiply;
        currentStrength -= damage;
        Color color = GetComponent<SpriteRenderer>().color;
        if (currentStrength <= 0)
        {
            color.a = 0;
            Managers.DamageManager.SubstractHealth(Tank, Mathf.Abs(currentStrength));
            Managers.DamageManager.SubstractStrength(Tank, Mathf.Abs(currentStrength));
            Managers.DestroyManager.CheckAndDestroy(Tank);

            if (Tank.activeSelf)
            {
                StartCoroutine(DeactiveArmor());
            }
        }
        else
        {
            color.a = currentStrength / ArmorStrength;
        }

        GetComponent<SpriteRenderer>().color = color;
       
    }




    IEnumerator DeactiveArmor()
    {
        yield return new WaitForSeconds(2);
        if (Tank.activeSelf)
        {
            Tank.GetComponent<Tank>().ArmorActivate = false;
            Destroy(this.gameObject);
        }

        if (Tank == Managers.TurnManager.PlayerTank)
            Managers.ModesManager.OnNoneSelected();

    }



    public void SetStrength(float strength)
    {
        ArmorStrength = currentStrength = strength;
    }
}
