using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{

    public void StartTimer()
    {
        StartCoroutine(StartTimerCoroutine());
    }

    IEnumerator StartTimerCoroutine()
    {
        float counter = 1.0f;
        Image img = GetComponent<Image>();
        Text text = transform.FindChild("Image").FindChild("Text").GetComponent<Text>();
        img.color = Color.white;
        text.text = "20";
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.2f);
            counter -= 0.01f;
            img.fillAmount = counter;
            int val = ((int)(counter * 20));
            text.text = val.ToString();
            if (val < 5) StartCoroutine(HighLight());
        }
        Managers.TurnManager.SetTurnToNextTank();
    }

    IEnumerator HighLight()
    {
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(0.5f);
            GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }


}
