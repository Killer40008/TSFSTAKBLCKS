using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TankFileAngleSlider : MonoBehaviour
{
    public static float Angle = 0;
    public static TankFileAngleSlider me;

    void Start()
    {
        me = transform.GetComponent<TankFileAngleSlider>();
    }
    public void PointerUp()
    {
        Angle = this.GetComponent<Slider>().value;
    }

    public float Value { set { this.GetComponent<Slider>().value = value; } }
}
