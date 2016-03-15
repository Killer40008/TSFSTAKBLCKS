using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour 
{
    public void OnOptionsButtonClick()
    {

        MenuBase.HideAndShow(this, "Menu", "Option");


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Option").GetComponent<CanvasGroup>().interactable)
        {
            Debug.Log(SceneManager.GetActiveScene().name);

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                MenuBase.HideAndShow(this, "Option", "Menu");

                string name = GameObject.Find("InputField").GetComponent<InputField>().text;

                PlayerData dt = ScoreModule.GetPlayerData();
                dt.PlayerName = name;
                ScoreModule.SavePlayerData(dt);
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                string name = GameObject.Find("NameInputField").GetComponent<InputField>().text;
                Managers.TurnManager.PlayerTank.GetComponent<Tank>().PlayerName = name;
                PlayerData dt = ScoreModule.GetPlayerData();
                dt.PlayerName = name;
                ScoreModule.SavePlayerData(dt);
                Managers.PlayerInfos.DrawPlayerInfoInUI_SinglePlayer();

                MenuBase.HideAndShowInGame(this, "Option", "Menu");


            }

        }
    }
}
