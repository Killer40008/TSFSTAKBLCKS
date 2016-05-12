using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModesCount : MonoBehaviour {


    public ModesClass.Modes mode;
    private int lastUpdateCount;

    void Start()
    {
        ModesClass.InitiallizeModesQuantities();
    }

    void Update()
    {
        int currentCount = ModesClass.ModesQuantities[mode];
        if (currentCount != lastUpdateCount)
        {
            transform.GetChild(0).GetComponent<Text>().text = currentCount.ToString();

            lastUpdateCount = currentCount;
        }
    }
}
