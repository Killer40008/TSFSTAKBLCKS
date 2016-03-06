using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AntiStrikeSlider : MonoBehaviour 
{
    Slider slider;
    public static bool allow;
    enum AdditionMode { Increase, Decrease }
    AdditionMode mode = AdditionMode.Increase;
    const float VALUE = 2f;

    void Start()
    {
        allow = true;
        slider = GetComponent<Slider>();
        StartCoroutine(Timeout(10));
    }
	
	// Update is called once per frame
    void Update()
    {
        if (allow)
        {
            if (mode == AdditionMode.Increase)
                slider.value += VALUE;
            else
                slider.value -= VALUE;

            if (slider.value == 100)
                mode = AdditionMode.Decrease;
            else if (slider.value == 0)
                mode = AdditionMode.Increase;

        }
    }

    public void OnButtonClick(GameObject indicator)
    {
        allow = false;
        StopAllCoroutines();

        if (slider.value > 38 && slider.value < 62)
        {
            indicator.GetComponent<Image>().color = Color.green;
            if (SphareAntistrike.Me != null)
                SphareAntistrike.Me.Strike();
        }
        else
        {
            indicator.GetComponent<Image>().color = Color.red;
        }

        ModesClass.SubtractModeQuantitie(ModesClass.Modes.AntiStrike);
        DeActive();
    }

    public void OnCancelClick()
    {
        allow = false;
        StopAllCoroutines();
        StartCoroutine(DestoryPeriod(1));
        SphareAntistrike.Me.Triggered = false;
    }

    static IEnumerator DestoryPeriod(float period)
    {
        yield return new WaitForSeconds(period * Time.timeScale);
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().FadeOut();
        Managers.ResetTimescale();
        yield return new WaitForSeconds(0.5f * Time.timeScale);
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>().DestroyFade();
    }


    public static void DeActive()
    {
        allow = false;
        Managers.Me.StartCoroutine(DestoryPeriod(1));

        if (SphareAntistrike.Me != null)
            Destroy(SphareAntistrike.Me.gameObject);


        if (ModesClass.ModesQuantities[ModesClass.Modes.AntiStrike] > 0)
            GameObject.Find("UIButtons").transform.FindChild("Antistrike").GetComponent<Button>().interactable = true;
        else
            GameObject.Find("UIButtons").transform.FindChild("Antistrike").GetComponent<Button>().interactable = false;

        Color32 color = GameObject.Find("UIButtons").transform.FindChild("Antistrike").GetComponent<Image>().color;
        color.a = 200;
        GameObject.Find("UIButtons").transform.FindChild("Antistrike").GetComponent<Image>().color = color;

    }

    IEnumerator Timeout(float second)
    {
        yield return new WaitForSeconds(second * Time.timeScale);
        DeActive();

    }
}
