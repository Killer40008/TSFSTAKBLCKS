using UnityEngine;
using System.Collections;
using System.Linq;

public class TurnManager : MonoBehaviour 
{
     GameObject[] tanks;
     int Selector = 0;

     public void Begin()
     {
         tanks = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => Random.Range(0, 100)).ToArray();
         SetTurnToNextTank(true);
     }


    public void SetTurnToNextTank(bool firstTime = false)
    {
        if (Selector == Managers.TanksCount)
            Selector = 0;

        TankEnabled(Selector, true, firstTime);

        Selector++;
    }
    public void TankEnabled(int index,bool enabled, bool firstTime = false)
    {
        GameObject[] tanksToDisabled =   tanks.Where(t => t.GetComponent<Tank>().CanDisabled == true).ToArray();
        for (int i = 0; i < tanks.Length; i++)
        {
            if (i == index)
            {
                tanksToDisabled[i].GetComponent<Tank>().Active(true);
                tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = false;
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = true;
            }
            else
            {
                tanksToDisabled[i].GetComponent<Tank>().Active(false);
                if (!firstTime) tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = true;
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = false;
            }
        }
    }
}
