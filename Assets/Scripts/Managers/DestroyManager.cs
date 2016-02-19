﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class DestroyManager : MonoBehaviour
{

    public Object DestroyEffect1;
    public bool Win = false;

    public bool CheckAndDestroy(GameObject tank)
    {
       float health = Managers.DamageManager.GetHealth(tank);
       if (health <= 0)
       {
           //destroy
           DestroyEffect(tank);
           tank.SetActive(false);

           //check if there we have a winner
           CheckForWinner();
           return true;
       }
       return false;
    }

    private void DestroyEffect(GameObject tank)
    {
        Instantiate(DestroyEffect1, tank.transform.position, Quaternion.identity);
    }

    private void CheckForWinner()
    {
        if (Managers.TurnManager.tanks.Count(t => t.activeSelf == true) == 1)
        {
            //winner
            Managers.Me.StopAllCoroutines();
            foreach (GameObject gm in GameObject.FindGameObjectsWithTag("Bomb"))
                Destroy(gm);

            Fade fp = Fade.CreateFade().GetComponent<Fade>();
            fp.FadeIn();
            fp.ShowWinPanel();
            
            Win = true;
        }
    }
}
