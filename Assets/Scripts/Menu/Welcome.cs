using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Welcome : MonoBehaviour 
{
    public void Start()
    {
        if (ScoreModule.IsPlayerDataExist())
        {
            MenuBase.Show(this, "Menu");

        }
        else
        {
            MenuBase.Show(this, "Welcome");
        }

    }

    public void OnNextButtonClicked()
    {

        string textboxText = GameObject.Find("Welcome").transform.Find("InputField").GetComponent<InputField>().text;

        if (textboxText.Length > 9) {
            GameObject.Find("Welcome").transform.Find("InputField").GetComponent<InputField>().text = "Must be less than 9 letters";
            return;
        }

        if (textboxText.Trim() != "")
        {
            PlayerData dt = ScoreModule.CreateNewPlayerData();
            dt.PlayerName = textboxText;
            dt.PlayerWeapons = new System.Collections.Generic.Dictionary<Weapons, int>();
            dt.PlayerModes = new System.Collections.Generic.Dictionary<ModesClass.Modes, int>();
            dt.PlayerRank = 1;
            dt.PlayerMoney = 0;


            ScoreModule.SavePlayerData(dt);
            MenuBase.HideAndShow(this, "Welcome", "Menu");
        }

    }

}
