using UnityEngine;
using System.Collections;
using System.Linq;

public class SphareAntistrike : MonoBehaviour 
{

    public GameObject MyTank;
    public static SphareAntistrike Me = null;
    GameObject bomb;
    public bool Triggered;




    void OnTriggerEnter(Collider other)
    {
        if (Triggered == false && Managers.TurnManager.CurrentTank != MyTank && other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Me = this;
            bomb = other.gameObject;

            Vector3 futurePosition = other.transform.position;
            futurePosition += (other.GetComponent<Rigidbody>().velocity.normalized * (other.transform.position - MyTank.transform.position).magnitude);
            futurePosition.y -= 0.5f;
            Debug.Log("futurePos: " + futurePosition);


            if (MyTank.transform.FindChild("Bounds").GetComponent<BoxCollider>().bounds.Contains(futurePosition) 
                || other.gameObject.GetComponent<Missile>() != null)
            {
                Triggered = true;
                Managers.WeaponManager.WeaponType = Weapons.Normal_Bomb;
                Managers.SlowDownTimescale(other.gameObject.GetComponent<Missile>() != null ? 0.01f : 0.035f);
                //show fade
                GameObject gm = Fade.CreateFade();
                gm.GetComponent<Fade>().ShowAntistrike();
            }
        }
    }

    public void Strike()
    {
        if (bomb != null)
        {
            Vector3 distane = bomb.transform.position - MyTank.transform.position;
            float angle = Mathf.Atan2(distane.y, distane.x) * Mathf.Rad2Deg;

            Managers.WeaponManager.WeaponType = Weapons.Normal_Bomb;
            StartCoroutine(MyTank.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().MoveToAngle(Quaternion.Euler(0, 0, angle), false));
            MyTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = true;
            GameObject myantiBomb = MyTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().Fire(10);
            myantiBomb.layer = LayerMask.NameToLayer("AntiBomb");
            Managers.Me.StartCoroutine(MoveToTarget(myantiBomb, bomb));
            MyTank.transform.FindChild("Burrell").GetComponent<Tank_Fire>().enabled = false;
        }
    }


    private IEnumerator MoveToTarget(GameObject antiBomb, GameObject target)
    {
        float time = 0f;
        Vector3 orginalPos = antiBomb.transform.position;

        time = 0;
        while (time < 1)
        {

            if (antiBomb == null) break;
            yield return new WaitForFixedUpdate();

            if (antiBomb == null) break;
            antiBomb.transform.position = Vector3.Lerp(orginalPos, target.transform.position, time);
            time += 0.02f;
        }

        if (this != null) Destroy(this.gameObject);
        Managers.TurnManager.SetTurnToNextTank();
    }



}
