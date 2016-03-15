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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb" && other.GetComponent<IWeapon>().Bomb.SoruceTank != Tank)
        {
            int dMultiply = other.GetComponent<IWeapon>().Bomb.SoruceTank.GetComponent<Tank>().DoubleDamage == true ? 2 : 1;
            float damage = (other.gameObject.GetComponent<IWeapon>().Bomb.Damage) * dMultiply;
            currentStrength -=  damage;
            Color color = GetComponent<SpriteRenderer>().color;
            if (currentStrength <= 0)
            {
                color.a = 0;
                other.gameObject.GetComponent<IWeapon>().Bomb.FloorHit(other.gameObject);
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
                other.gameObject.GetComponent<IWeapon>().Bomb.FloorHit(other.gameObject);
            }

            GetComponent<SpriteRenderer>().color = color;   
        }
    }

    public void OnLightingEnter()
    {

        float damage = Lighting.Damage;
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
