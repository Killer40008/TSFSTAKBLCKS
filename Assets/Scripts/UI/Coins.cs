using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coins : MonoBehaviour 
{
    private int lastUpdateCoins;

	
	
	// Update is called once per frame
	void Update ()
    {
       int current = ScoreModule.GetPlayerData().PlayerMoney;
       if (current != lastUpdateCoins)
       {
           GetComponent<Text>().text = current.ToString();

           lastUpdateCoins = current;
       }
	}
}
