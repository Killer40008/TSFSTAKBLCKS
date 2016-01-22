using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurnManager : MonoBehaviour 
{
    public static GameObject PlayerTank;
    public Object SelectorArrow;
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

        TankEnabled(Selector, firstTime);
        ShowArrowAboveTank(Selector);

        Selector++;
    }

    private void ShowArrowAboveTank(int index)
    {
        Vector3 tPos = tanks[index].transform.position;
        GameObject arr = (GameObject)Instantiate(SelectorArrow, new Vector3(tPos.x, tPos.y + 3f, 0), Quaternion.identity);
        arr.transform.SetParent(tanks[index].transform);
    }

  
    public void TankEnabled(int index,bool firstTime = false)
    {
        GameObject[] tanksToDisabled =   tanks.Where(t => t.GetComponent<Tank>().CanDisabled == true).ToArray();
        for (int i = 0; i < tanks.Length; i++)
        {
            //active tank by index
            if (i == index)
            {
                var burrell = tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Burrell_Movement>();
                TankFileAngleSlider.me.Value = burrell.rotatoinAngleZ / burrell.Speed;
                tanksToDisabled[i].GetComponent<Tank>().Active(true);
                tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = false;
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = true;
                PlayerTank = tanksToDisabled[i];
            }
            //disblead others
            else
            {
                tanksToDisabled[i].GetComponent<Tank>().Active(false);
                if (!firstTime) tanksToDisabled[i].GetComponent<Rigidbody>().isKinematic = true;
                tanksToDisabled[i].transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = false;
            }
        }

        
    }

}
