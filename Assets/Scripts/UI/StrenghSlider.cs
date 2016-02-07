using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrenghSlider : MonoBehaviour 
{
    public static int Strengh = 0;

    public void PointerUp()
    {
        int current = (int)this.GetComponent<Slider>().value;
        if (current > 0)
        {
            if (!CheckIfWeaponsValid())
            {
                this.GetComponent<Slider>().value = 0;
                return;
            }
            else
            {
                Strengh = current;
            }

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
