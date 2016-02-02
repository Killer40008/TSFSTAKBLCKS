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
    public void SubstractHealth(GameObject tank, float newHealth)
    {
        tank.GetComponent<Tank>().Health -= newHealth;
    }

    public float GetStrength(GameObject tank)
    {
        return tank.GetComponent<Tank>().Strength;
    }
    public void SubstractStrength(GameObject tank, float newStrength)
    {
        tank.GetComponent<Tank>().Strength -= newStrength;
    } 
}
