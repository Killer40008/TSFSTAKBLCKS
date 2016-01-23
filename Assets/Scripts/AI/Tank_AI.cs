using UnityEngine;
using System.Collections;

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

        if (LastTankHit == null)
        {
            otherTank = tanks[Random.Range(0, tanks.Length)];
            virtulOtherTank = CreateVirtualTank(otherTank.transform.position);
        }
        else
        {
            otherTank = LastTankHit;
            virtulOtherTank = CreateVirtualTank(LastTankHit.transform.position);
        }

        float deltaX = virtulOtherTank.transform.position.x - virtulThisTank.transform.position.x;
        float deltaY = this.transform.position.y - otherTank.transform.position.y;
        if (Mathf.Sign(this.transform.position.y - otherTank.transform.position.y) == -1)
            deltaY = otherTank.transform.position.y - this.transform.position.y;

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

        strength -= 0.4f - (deltaY * 0.3f);

        StartCoroutine(this.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().
            MoveToAngleAndFire(Quaternion.Euler(0, 0, angle), strength));

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
