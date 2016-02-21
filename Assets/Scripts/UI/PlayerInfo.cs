using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour 
{

    public static void DrawPlayerInfoInUI()
    {
        GameObject.Find("PlayersInfo").transform.Find("PlayerOne").Find("Text").GetComponent<Text>().text = Managers.TurnManager.PlayerTank
            .GetComponent<Tank>().PlayerName;

    }


}
