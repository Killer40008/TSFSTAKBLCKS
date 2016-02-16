using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{

    public void FadeIn()
    {
        GetComponent<Animator>().SetBool("Fade", true);

    }

    public void FadeOut()
    {
        GetComponent<Animator>().SetBool("Fade", false);
    }

    public void ShowAntistrike()
    {
        transform.Find("AntiStrikePanel").GetComponent<CanvasGroup>().alpha = 1;
        transform.Find("AntiStrikePanel").GetComponent<CanvasGroup>().interactable = true;
    }



    public void DestroyFade()
    {
        Destroy(this.gameObject);
    }
}
