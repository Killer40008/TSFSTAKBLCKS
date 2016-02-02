using UnityEngine;
using System.Collections;

public class MenuBase : MonoBehaviour {

    public static void BringToFront(GameObject panel)
    {
        panel.transform.SetParent(null, false);
        panel.transform.SetParent(GameObject.Find("Canvas").transform, false);
        panel.transform.localPosition = Vector3.zero;
    }

    public static IEnumerator FadeHide(CanvasGroup canvasGroup)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            yield return new WaitForEndOfFrame();
            alpha = Mathf.Lerp(alpha, 0, 0.5f * 0.5f);
            canvasGroup.alpha = alpha;
        }
    }
    public static IEnumerator FadeShow(CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(0.5f);
        float alpha = 0;
        while (alpha < 1)
        {
            yield return new WaitForEndOfFrame();
            alpha = Mathf.Lerp(alpha, 1, 0.5f * 0.1f);
            canvasGroup.alpha = alpha;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
