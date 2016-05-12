using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponsCount : MonoBehaviour {

    public Weapons weapon;
    private int lastUpdateCount;

    void Start()
    {
        WeaponsClass.InitiallizeWeaponsQuantities();
    }
	
	void Update ()
    {
        int currentCount = WeaponsClass.WeaponsQuantities[weapon];
        if ( currentCount != lastUpdateCount)
        {
            transform.GetChild(0).GetComponent<Text>().text = currentCount.ToString();

            lastUpdateCount = currentCount;
        }
	}
}
