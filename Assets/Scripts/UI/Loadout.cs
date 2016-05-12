using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Loadout : MonoBehaviour 
{

    public Object ArmorPrefap;


    Text countText;

    public void StartLoadout()
    {
        countText = GameObject.Find("CountDownRound").GetComponent<Text>();
        if (ModesClass.ModesQuantities[ModesClass.Modes.AntiStrike] <= 0)
            transform.Find("AntiStikeButton").GetComponent<Button>().interactable = false;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
   
        int count = 4;
        countText.text = (count +1).ToString();

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
        int rndMode;


        foreach (GameObject tank in Managers.TurnManager.tanks.Where(t => t != Managers.TurnManager.PlayerTank).ToArray())
        {

            if (SinglePlayer.GameDifficulty == SinglePlayer.GameDifficultyEnum.Easy)
            {
                rndMode = Random.Range(-1, 1);
                if (rndMode == 0)
                {
                    Color32 rnd = ModesCombo.ColorStrength.ElementAt(0).Key;
                    CreateArmor(tank, rnd, ModesCombo.ColorStrength[rnd]);
                }
            }
            else if (SinglePlayer.GameDifficulty == SinglePlayer.GameDifficultyEnum.Normal)
            {
                rndMode = Random.Range(-1, 3);

                if (rndMode == 0)
                {
                    Color32 rnd = ModesCombo.ColorStrength.ElementAt(1).Key;
                    CreateArmor(tank, rnd, ModesCombo.ColorStrength[rnd]);
                }
                else if (rndMode == 1)
                {
                    Color32 rnd = ModesCombo.ColorStrength.ElementAt(2).Key;
                    CreateArmor(tank, rnd, ModesCombo.ColorStrength[rnd]);
                }
                else if (rndMode == 2)
                {
                    LoadDoubleDamage(tank);
                }
            }
            else if (SinglePlayer.GameDifficulty == SinglePlayer.GameDifficultyEnum.Hard)
            {
                rndMode = Random.Range(0, 3);

                if (rndMode == 0)
                {
                    Color32 rnd = ModesCombo.ColorStrength.ElementAt(Random.Range(0,4)).Key;
                    CreateArmor(tank, rnd, ModesCombo.ColorStrength[rnd]);
                }
                else if (rndMode == 1)
                {
                    LoadDoubleBurrell(tank);
                }
                else if (rndMode == 2)
                {
                    LoadDoubleDamage(tank);
                    tank.transform.FindChild("Canvas").FindChild("doubleDamage").GetComponent<CanvasGroup>().alpha = 1;
                }
            }
        }
    }
    




    void LoadDoubleDamage(GameObject tank)
    {

        tank.GetComponent<Tank>().DoubleDamage = true;

    }


    //----------------------------------------------------------------------------



        void LoadDoubleBurrell(GameObject tank)
    {

        GameObject burrell = tank.transform.FindChild("Burrell").gameObject;
        GameObject secondBurrell = (GameObject)Instantiate(burrell, burrell.transform.position, Quaternion.identity);
        secondBurrell.tag = "SecondBurrell";
        secondBurrell.name = "Burrell2";
        secondBurrell.GetComponent<Burrell_Movement>().enabled = false;
        secondBurrell.transform.SetParent(burrell.transform.parent, true);
        secondBurrell.transform.localEulerAngles = new Vector3(0, 0, 170);
        secondBurrell.transform.localScale = burrell.transform.localScale;
        tank.GetComponent<Tank>().BurrellCount = 2;
        tank.GetComponent<Tank>().DoubleBurrell = true;
    }

    //--------------------------------------------------------------------



        void CreateArmor(GameObject tank, Color32 color, float strength)
        {

            Vector3 pos = tank.transform.position;
            pos.y -= 0.1f;
            pos.x -= 0.1f;
            pos.z = 0.05f;
            GameObject gm = Instantiate(ArmorPrefap, pos, Quaternion.identity) as GameObject;
            gm.transform.SetParent(tank.transform);
            gm.GetComponent<SpriteRenderer>().color = color;
            tank.GetComponent<Tank>().ArmorActivate = true;
            Armor arm = gm.GetComponent<Armor>();
            arm.SetStrength(strength);
            arm.Tank = tank;
        }


}
