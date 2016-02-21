using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayer : MonoBehaviour 
{
    public enum GameDifficultyEnum { Easy, Normal, Hard }
    public static GameDifficultyEnum GameDifficulty { get; set; }

    public void OnSigleButtonClick()
    {
        MenuBase.HideAndShow(this, "Menu", "Difficulty");
    }

    public void OnEasyClick()
    {
        GameDifficulty = GameDifficultyEnum.Easy;
        StartTheGame();

        StartCoroutine(MenuBase.FadeHide(GameObject.Find("Difficulty").GetComponent<CanvasGroup>()));
    }

    public void OnNormalClick()
    {
        GameDifficulty = GameDifficultyEnum.Normal;
        StartTheGame();

        StartCoroutine(MenuBase.FadeHide(GameObject.Find("Difficulty").GetComponent<CanvasGroup>()));
    }

    public void OnHardClick()
    {
        GameDifficulty = GameDifficultyEnum.Hard;
        StartTheGame();

        StartCoroutine(MenuBase.FadeHide(GameObject.Find("Difficulty").GetComponent<CanvasGroup>()));
    }



    void StartTheGame()
    {
        GameObject.Find("Loading").GetComponent<CanvasGroup>().alpha = 1;
        SceneManager.LoadSceneAsync ("Game");
    }




}
