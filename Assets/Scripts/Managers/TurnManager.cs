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
         SetTurnToNextTank();
     }


    public void SetTurnToNextTank()
    {
        if (Selector == Managers.TanksCount)
            Selector = 0;

        TankEnabled(Selector, true);

        Selector++;
    }
    public void TankEnabled(int index,bool enabled)
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
                tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = true;
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = false;
            }
        }
    }
}
