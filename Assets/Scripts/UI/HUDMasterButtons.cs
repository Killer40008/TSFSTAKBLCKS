using UnityEngine;
using System.Collections;

public class HUDMasterButtons : MonoBehaviour
{
    public static GameObject FadeObj;

	// Use this for initialization
    public void PauseButtonClick()
    {
        Managers.PauseGame();
        FadeObj = Fade.CreateFade();
        FadeObj.GetComponent<Fade>().ShowPausePanel();
    }
}
