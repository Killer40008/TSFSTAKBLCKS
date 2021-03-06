﻿using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class TurnManager : MonoBehaviour 
{
    public GameObject PlayerTank;
    public GameObject CurrentTank;
    public Object SelectorArrow;
    public List<GameObject> tanks;
    public int Selector = 0;
    public int PlayerIndex;

     public void Begin()
     {
         tanks.AddRange(GameObject.FindGameObjectsWithTag("Player").OrderBy(x => Random.Range(0, 100)).ToArray());
         int playerRnd = Random.Range(0, Managers.TanksStaringCount);
        // PlayerTank = tanks[playerRnd];
         PlayerTank = tanks[0];
         PlayerIndex = PlayerTank.GetComponent<Tank>().Index;

         SpawnManager.SetPropertiesToPlayerTank();
     }
    

     public void SetTurnToNextTank(bool firstTime = false)
     {
         if (tanks.Count(t => t.activeSelf == true) > 1)
         {
             //reset selector
             if (Selector >= tanks.Count)
                 Selector = 0;

             if (tanks[Selector].activeSelf)
             {
                 TankEnabled(Selector, firstTime);
                 ShowArrowAboveTank(Selector);
                 SetController(Selector);
                 CurrentTank = tanks[Selector];

                 Selector++;
             }
             else
             {
                 Selector++;
                 SetTurnToNextTank();
             }
         }
     }

  
    public void TankEnabled(int index,bool firstTime = false)
    {
        GameObject[] tanksToDisabled =   tanks.Where(t => t.GetComponent<Tank>().CanDisabled == true).ToArray();
        for (int i = 0; i < tanks.Count; i++)
        {
            if (i == index)
            {
                var burrell = tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Burrell_Movement>();
                TankFileAngleSlider.me.Value = burrell.rotatoinAngleZ / burrell.Speed;

                tanksToDisabled[i].GetComponent<Tank>().Active(true);
                tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = false;
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = true;

            }
            //disblead others
            else
            {
                tanksToDisabled[i].GetComponent<Tank>().Active(false);

                if (!firstTime && Managers.MapsManager.CurrentMap == MapManager.Maps.Volcano)
                    tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = true;
                
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = false;
            }
        }

        
    }

    private void SetController(int selector)
    {
        
        //deactive focus icon 
        #region FocusIcon
        Managers.TurnManager.tanks.ToList().ForEach(e => e.GetComponent<Tank>().Color = e.GetComponent<Tank>().OrginalColor);
        Managers.TurnManager.tanks.ToList().ForEach(e => e.GetComponent<Focus>().DeActive());
        if (Missile.highlightCoroutines.Count > 0)
        {

            Missile.highlightCoroutines.ForEach(c => Managers.Me.StopCoroutine(c));
            Missile.highlightCoroutines.Clear();
        }
        #endregion

        //set control either to Al system or to the HUD (if it's player tank)
        if (tanks[selector] == PlayerTank)
        {
            ShowHUD(true);
            Managers.ShowSliders(true);
            Managers.DamageManager.CalculatePlayerHealthInUI();
            Managers.DamageManager.CalculatePlayerStrenghInUI();
            Managers.WeaponManager.DrawWeaponInfoInUI();
            if (Managers.WeaponManager.lastButton != null)
                Managers.WeaponManager.OnWeaponsSelected(Managers.WeaponManager.lastButton);
            else
                Managers.WeaponManager.OnWeaponsSelected(GameObject.Find("WeaponsCombo").transform.FindChild("Border")
                    .FindChild("ButtonsTop").transform.FindChild("Normal_Bomb").gameObject);


            GameObject.Find("PlayerTimer").GetComponent<Timer>().StartTimer();
            
        }
        else
        {
            GameObject.Find("PlayerTimer").GetComponent<Timer>().StopTimer();
            ShowHUD(false);
            tanks[selector].GetComponent<Tank_AI>().IsAlTank = true;
            tanks[selector].GetComponent<Tank_AI>().AimBurrellToRandomTank(tanks.Where(t => t != tanks[selector]).ToArray());
        }
    }

    private void ShowArrowAboveTank(int index)
    {
        GameObject.FindGameObjectsWithTag("Arrow").ToList().ForEach(a => Destroy(a));
        Vector3 tPos = tanks[index].transform.position;
        GameObject arr = (GameObject)Instantiate(SelectorArrow, new Vector3(tPos.x, tPos.y + 3f, 0), Quaternion.identity);
        arr.transform.SetParent(tanks[index].transform);
    }



    void ShowHUD(bool show)
    {

        if (show)
        {
            GameObject.Find("Canvas").transform.FindChild("HUD").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("HUDMasterButtons").GetComponent<RectTransform>().anchoredPosition =
                new Vector3(GameObject.Find("HUDMasterButtons").GetComponent<RectTransform>().anchoredPosition.x, -85, 0);
            GameObject.Find("Canvas").transform.FindChild("HUD").FindChild("DisabledPanel").GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
        else
        {
            GameObject.Find("Canvas").transform.FindChild("HUD").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("HUDMasterButtons").GetComponent<RectTransform>().anchoredPosition =
                      new Vector3(GameObject.Find("HUDMasterButtons").GetComponent<RectTransform>().anchoredPosition.x, -22, 0);
            GameObject.Find("Canvas").transform.FindChild("HUD").FindChild("DisabledPanel").GetComponent<CanvasGroup>().blocksRaycasts = true;

        }
    }

}
