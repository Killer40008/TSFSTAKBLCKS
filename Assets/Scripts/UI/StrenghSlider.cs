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

        Strengh = current;



    }

 
}
