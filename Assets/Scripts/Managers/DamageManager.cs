using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{


    public void CalculatePlayerHealthInUI()
    {
        float health = GetHealth(Managers.TurnManager.PlayerTank);
        GameObject.Find("PlayerHealthProgress").transform.FindChild("Health").GetComponent<Image>().fillAmount = health / 100;
        Transform indicator = GameObject.Find("PlayerHealthProgress").transform.FindChild("Health").FindChild("Indicator").transform;

        float xIndicator = (12f * ((100 - health) / 10));
        xIndicator = health > 50 ? 59 - xIndicator : 62f - xIndicator;

        indicator.localPosition = new Vector3(xIndicator, indicator.localPosition.y, indicator.localPosition.z);
    }

    public void CalculatePlayerStrenghInUI()
    {
        float strengh = GetStrength(Managers.TurnManager.PlayerTank);
        GameObject.Find("PlayerStrengthProgress").transform.FindChild("Strengh").GetComponent<Image>().fillAmount = strengh / 100;
        Transform indicator = GameObject.Find("PlayerStrengthProgress").transform.FindChild("Strengh").FindChild("Indicator").transform;

        float xIndicator = (12f * ((100 - strengh) / 10));
        xIndicator = strengh > 50 ? 59 - xIndicator : 62f - xIndicator;

        indicator.localPosition = new Vector3(xIndicator, indicator.localPosition.y, indicator.localPosition.z);
    }


    //get & set

    public float GetHealth(GameObject tank)
    {
        return tank.GetComponent<Tank>().Health;
    }
    public void SubstractHealth(GameObject tank, float damage)
    {
        if (GetHealth(tank) - damage > 0)
        {
            if (Managers.TurnManager.CurrentTank.GetComponent<Tank>().DoubleDamage)
                tank.GetComponent<Tank>().Health -= (damage * 2);
            else
                tank.GetComponent<Tank>().Health -= (damage);
        }
        else
            tank.GetComponent<Tank>().Health = 0;
    }

    public void AddHealth(GameObject tank, float newHealth)
    {
        if (GetHealth(tank) < 100)
            tank.GetComponent<Tank>().Health += newHealth;
    }
    //


    public float GetStrength(GameObject tank)
    {
        return tank.GetComponent<Tank>().Strength;
    }
    public void SubstractStrength(GameObject tank, float newStrength)
    {
        if (GetStrength(tank) - newStrength > 0)
            tank.GetComponent<Tank>().Strength -= newStrength;
        else
            tank.GetComponent<Tank>().Strength = 0;

        tank.GetComponent<Tank>().CalculateHealthAboveTank();
    }
    public void AddStrength(GameObject tank, float newStrength)
    {
        if (GetStrength(tank) < 100)
            tank.GetComponent<Tank>().Strength += newStrength;
    }


    //
    public void SetHealth(GameObject tank, float newHealth)
    {
        if (newHealth > 0)
            tank.GetComponent<Tank>().Health = newHealth;
        else
            tank.GetComponent<Tank>().Health = 0;
    }
    public void SetStrength(GameObject tank, float newStrength)
    {
        if (newStrength > 0)
            tank.GetComponent<Tank>().Strength = newStrength;
        else
            tank.GetComponent<Tank>().Strength = 0;
    }
}
