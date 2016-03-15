using UnityEngine;
using System.Collections;
using System.Linq;

public class Tank_AI : MonoBehaviour 
{
    public Object AlPrefab;
    //records
    public GameObject LastTankHit = null;
    public float LastTankHitAngle;
    public bool IsAlTank = false;


    // I know this code is Unreadable ^_^

    public void AimBurrellToRandomTank(GameObject[] tanks)
    {
        GameObject virtulThisTank = CreateVirtualTank(this.transform.position);
        GameObject virtulOtherTank = null, otherTank = null;

        if (LastTankHit == null || LastTankHit.activeSelf == false)
        {
            otherTank = tanks.Where(t => t.activeSelf == true).ToArray().OrderBy(x => Random.Range(0, 100)).FirstOrDefault();
            virtulOtherTank = CreateVirtualTank(otherTank.transform.position);
        }
        else
        {
            otherTank = LastTankHit;
            virtulOtherTank = CreateVirtualTank(LastTankHit.transform.position);
        }

        float deltaX = virtulOtherTank.transform.position.x - virtulThisTank.transform.position.x;
        float deltaY = this.transform.position.y - otherTank.transform.position.y;


        float gravity = Mathf.Abs(Physics.gravity.y);
        float angle = 0;
        if (LastTankHit == null)
        {
            angle = 90 + (Random.Range(45, 90) * -Mathf.Sign(deltaX));
            LastTankHitAngle = angle;
        }
        { 
            angle = LastTankHitAngle; 
        }


        float strength = 0;
        if (angle > 90)
            strength = Mathf.Sqrt((Mathf.Abs(deltaX) * gravity) / Mathf.Sin(DegreeToRadian((180 - angle) * 2)));
        else
            strength = Mathf.Sqrt((Mathf.Abs(deltaX) * gravity) / Mathf.Sin(DegreeToRadian(angle * 2)));

        strength -= 0.4f + (deltaY * 0.8f);




        //for double Burrell
        if (GetComponent<Tank>().BurrellCount == 1)
        {
            this.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().enabled = true;
            this.transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = true;

            StartCoroutine(this.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().
                MoveToAngleAndFire(Quaternion.Euler(0, 0, angle), strength));
        }
        else
        {
            this.transform.FindChild("Burrell2").GetComponent<Burrell_Movement>().enabled = true;
            this.transform.FindChild("Burrell2").GetComponent<Tank_Fire>().enabled = true;

            StartCoroutine(this.transform.FindChild("Burrell2").GetComponent<Burrell_Movement>().
                MoveToAngleAndFire(Quaternion.Euler(0, 0, angle), strength));
        }

        Destroy(virtulOtherTank);
        Destroy(virtulThisTank);
    }

    public GameObject CreateVirtualTank(Vector3 pos)
    {
        return (GameObject)Instantiate(AlPrefab, pos, Quaternion.identity);
    }


    private static float DegreeToRadian(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }

}
