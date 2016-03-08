using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Loadout : MonoBehaviour 
{

    public Object ArmorPrefap;


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
        AILoadout();
        Managers.Play();
    }

    void AILoadout()
    {
        int rnd = Random.Range(1, 7);

        //LoadDoubleBurrell(
        //   if (rnd == 1 && SinglePlayer.GameDifficulty == SinglePlayer.GameDifficultyEnum.Normal)

        foreach (GameObject tank in Managers.TurnManager.tanks)
        {
            LoadDoubleBurrell(tank);
            //Color32 rnd = ModesCombo.ColorStrength.ElementAt(Random.Range(0, 4)).Key;
            //CreateArmor(tank, rnd, ModesCombo.ColorStrength[rnd]);
        }
    }




    void LoadDoubleBurrell(GameObject tank)
    {

        GameObject burrell = tank.transform.FindChild("Burrell").gameObject;
        GameObject secondBurrell = (GameObject)Instantiate(burrell, burrell.transform.position, Quaternion.identity);
        secondBurrell.tag = "SecondBurrell";
        secondBurrell.name = "Burrell2";
        secondBurrell.GetComponent<Burrell_Movement>().enabled = false;
        secondBurrell.transform.SetParent(burrell.transform.parent, true);
        secondBurrell.transform.localEulerAngles = new Vector3(0, 0, 180 - burrell.transform.eulerAngles.z);
        secondBurrell.transform.localScale = burrell.transform.localScale;
        tank.GetComponent<Tank>().BurrellCount = 2;
    }

    //--------------------------------------------------------------------


    void CreateArmor(GameObject tank,Color32 color, float strength)
    {

        Vector3 pos = tank.transform.position;
        pos.y -= 0.1f;
        pos.x -= 0.1f;
        pos.z = 0.05f;
        GameObject gm = Instantiate(ArmorPrefap, pos, Quaternion.identity) as GameObject;
        gm.transform.SetParent(Managers.TurnManager.PlayerTank.transform);
        gm.GetComponent<SpriteRenderer>().color = color;
        tank.GetComponent<Tank>().ArmorActivate = true;
        Armor arm = gm.GetComponent<Armor>();
        arm.SetStrength(strength);
        arm.Tank = tank;

    }


}
