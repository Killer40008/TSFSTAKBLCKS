using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButtons : MonoBehaviour 
{

    public void OnResumeClick() 
    {
        HUDMasterButtons.FadeObj.GetComponent<Fade>().DestroyFade();
        Managers.ResumeGame();
    }
    public void OnOptionsClick()
    {
        MenuBase.HideAndShowInGame(this, "Menu", "Option");
        GameObject.Find("NameInputField").GetComponent<InputField>().text = ScoreModule.GetPlayerData().PlayerName;
    }
    public void OnStoreClick()
    {

    }
    public void OnBackClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
