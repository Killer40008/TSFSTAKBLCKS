using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loadout : MonoBehaviour 
{
    Text countText;
    GameObject fade;

    public void StartLoadout()
    {
        countText = GameObject.Find("CountDownRound").GetComponent<Text>();
        if (ModesClass.ModesQuantities[ModesClass.Modes.AntiStrike] <= 0)
            transform.Find("AntiStikeButton").GetComponent<Button>().interactable = false;

        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        int count = 9;

        while (count > -1)
        {
            yield return new WaitForSeconds(1);
            countText.text = (count).ToString();
            count--;
        }

        GameObject.Find("Fade").GetComponent<Fade>().DestroyFade();
        Managers.Play();
    }
}
