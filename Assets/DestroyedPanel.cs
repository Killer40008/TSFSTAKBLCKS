using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyedPanel : MonoBehaviour 
{

    public void End()
    {
        DestroyManager.SetScoreToRoundScene();
        SceneManager.LoadScene("Round");   
    }
    public void Skip()
    {
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().DestroyFade();
    }
}
