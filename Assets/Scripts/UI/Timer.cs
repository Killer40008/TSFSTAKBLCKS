using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{


    const int TIME = 30;



    Color orginal;
    Image img;

    public void StartTimer()
    {
        StartCoroutine(StartTimerCoroutine());
        img = GameObject.Find("progress").GetComponent<Image>();
        orginal = img.color;
    }

    IEnumerator StartTimerCoroutine()
    {
        float counter = 1.0f;


        while (counter > 0)
        {
            yield return new WaitForSeconds(TIME / 100.0f);
            counter -= 0.01f;
            img.fillAmount = counter;
            int val = ((int)(counter * TIME));

            if (val == 5) StartCoroutine(HighLight(img, orginal));
        }
        StopCoroutine(HighLight(img, orginal));
        img.color = orginal;

        TimerFinished();
    }

    IEnumerator HighLight(Image img, Color orginal)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            img.color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(1f);
            img.color = orginal;
        }
    }

    public void StopTimer()
    {
        img.color = orginal;
        StopAllCoroutines();
    }

    void TimerFinished()
    {
        Managers.TurnManager.SetTurnToNextTank();
        Managers.WeaponManager.WeaponConfigWhenComboOpened();
    }
}
