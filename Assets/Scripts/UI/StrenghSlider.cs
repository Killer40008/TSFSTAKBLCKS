using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrenghSlider : MonoBehaviour 
{
    public static int Strengh = 0;

    public void PointerUp()
    {
        Strengh = (int)this.GetComponent<Slider>().value;
       TurnManager.PlayerTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().Fire();
       this.GetComponent<Slider>().value = 0;
    }
}
