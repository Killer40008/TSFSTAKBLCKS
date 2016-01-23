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
         int playerIndex = Random.Range(0, Managers.TanksCount);
         PlayerTank = tanks[playerIndex];
         Debug.Log(playerIndex);
         SetTurnToNextTank(true);
     }


     public void SetTurnToNextTank(bool firstTime = false)
     {
         if (Selector == Managers.TanksCount)
             Selector = 0;

         TankEnabled(Selector, firstTime);
         ShowArrowAboveTank(Selector);
         SetController(Selector);

         Selector++;
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

    private void SetController(int selector)
    {
        //set control either to Al system or to the HUD (if it's player tank)
        if (tanks[selector] == PlayerTank)
        {
            ShowHUD(true);
        }
        else
        {
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
            GameObject.Find("Canvas").GetComponent<CanvasGroup>().alpha = 1;
        else
            GameObject.Find("Canvas").GetComponent<CanvasGroup>().alpha = 0;
    }

}
