using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Welcome : MonoBehaviour 
{
    public void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (ScoreModule.IsPlayerDataExist())
        {
            MenuBase.Show(this, "Menu");
        }
        else
        {
            MenuBase.Show(this, "Welcome");
        }

        var l = GameObject.FindGameObjectsWithTag("RM").ToList();
        l.RemoveAt(0);
        l.ForEach(r => Destroy(r));
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
            dt.PlayerWeapons = WeaponsClass.InitiallizeWeaponsQuantities(true);
            dt.PlayerModes = ModesClass.InitiallizeModesQuantities(true);
            dt.PlayerRank = 1;
            dt.PlayerMoney = 0;
            dt.PlayerPoints = 0;

            ScoreModule.SavePlayerData(dt);
            MenuBase.HideAndShow(this, "Welcome", "Menu");
        }

    }

}
