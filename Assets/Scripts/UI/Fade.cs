using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{

    public static GameObject CreateFade()
    {
        GameObject gm = Instantiate(Managers.Me.FadePanel) as GameObject;
        gm.transform.SetParent(GameObject.Find("Canvas").transform, false);
        return gm;
    }


 
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
        transform.Find("AntiStrikePanel").transform.Find("AntiStikeSlider").GetComponent<AntiStrikeSlider>().StartSlider();
    }

    public void ShowLoadout()
    {
        transform.Find("Loadout").GetComponent<CanvasGroup>().alpha = 1;
        transform.Find("Loadout").GetComponent<CanvasGroup>().interactable = true;
        transform.Find("Loadout").GetComponent<Loadout>().StartLoadout();
    }

    public void ShowWinPanel()
    {
        transform.Find("RoundCompleted").GetComponent<CanvasGroup>().alpha = 1;
        transform.Find("RoundCompleted").GetComponent<CanvasGroup>().interactable = true;
        //
        transform.Find("RoundCompleted").GetComponent<Animator>().SetBool("Do", true);
    }



    public void DestroyFade()
    {
        Destroy(this.gameObject);
    }

    #region Background
    public static void BackGroundFadeIn()
    {
        Managers.Me.StartCoroutine(backfade(new Color32(100, 100, 100, 0)));
    }
    public static void BackGroundFadeOut()
    {
        Managers.Me.StartCoroutine(backfade(new Color32(255, 255, 255, 0)));
    }
    static IEnumerator backfade(Color32 target)
    {
        SpriteRenderer background = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        Color32 orgin = background.color;
        float t = 0;

        while (t < 1)
        {
            yield return new WaitForFixedUpdate();
            background.color = Color32.Lerp(orgin, new Color32(target.r, target.g, target.b, orgin.a), t);
            t += 0.05f;
        }
    }
    #endregion


}
