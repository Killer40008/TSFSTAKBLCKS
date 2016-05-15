using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreCounter : MonoBehaviour 
{
    const int Time = 15;
    public static bool Pause = false;

    void Start()
    {
        if (RoundManager.ShowCounter)
            Show();
    }

    public void Show()
    {
        this.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(StartCounting());
    }
    public void Hide()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        StopCoroutine(StartCounting());
        this.transform.FindChild("Text").GetComponent<Text>().text = Time.ToString();
    }
    public IEnumerator StartCounting()
    {
        Image img = transform.FindChild("Image (1)").GetComponent<Image>();
        float counter = 1.0f;


        while (counter > 0)
        {
            yield return new WaitForEndOfFrame();
            if (!Pause)
            {
                yield return new WaitForSeconds(Time / 100.0f);
                counter -= 0.01f;
                img.fillAmount = counter;
                int val = ((int)(counter * Time));
                this.transform.FindChild("Text").GetComponent<Text>().text = val.ToString();
            }
        }

        RoundManager.StartNextRound();
    }
}
