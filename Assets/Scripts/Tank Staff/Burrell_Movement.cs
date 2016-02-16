using UnityEngine;
using System.Collections;

public class Burrell_Movement : MonoBehaviour
{
    public float Speed = 2;
    public float rotatoinAngleZ = 0;
   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enabled && !this.transform.parent.GetComponent<Tank_AI>(). IsAlTank)
        {
            rotatoinAngleZ = TankFileAngleSlider.Angle * Speed;
            rotatoinAngleZ = Mathf.Clamp(rotatoinAngleZ, 0F, 180F); // clamp angle between 0 to 180

            if (this.tag == "SecondBurrell")
                rotatoinAngleZ = 180 - rotatoinAngleZ;
            

            this.transform.localEulerAngles = new Vector3(0, 0, rotatoinAngleZ);
            this.transform.parent.GetComponent<Tank>().BurrellPosition = this.transform.FindChild("Fire").transform.position;
            this.transform.parent.GetComponent<Tank>().BurrellRotation = this.transform.eulerAngles;

        }
    }

    public IEnumerator MoveToAngleAndFire(Quaternion angle, float strength)
    {
        if (enabled && this.transform.parent.GetComponent<Tank_AI>().IsAlTank)
        {
            while (Mathf.Abs(this.transform.rotation.eulerAngles.z - angle.eulerAngles.z) >= 0.5f)
            {
                yield return new WaitForFixedUpdate();
                this.transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 2);
                if (this.transform.localEulerAngles.z >= 180 || this.transform.localEulerAngles.z <= 0) break;
            }

            this.transform.parent.GetComponent<Tank>().BurrellPosition = this.transform.FindChild("Fire").transform.position;
            this.transform.parent.GetComponent<Tank>().BurrellRotation = this.transform.eulerAngles;
            this.transform.GetComponent<Tank_Fire>().Fire(strength, true);
        }
    }

    public IEnumerator MoveToAngle(Quaternion angle, bool UseSmooth = true)
    {
        this.enabled = false;
        if (UseSmooth)
        {
            while (Mathf.Abs(this.transform.rotation.eulerAngles.z - angle.eulerAngles.z) >= 0.5f)
            {
                yield return new WaitForFixedUpdate();
                this.transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 2);
                if (this.transform.localEulerAngles.z >= 180 || this.transform.localEulerAngles.z <= 0) break;
            }
        }
        else
            this.transform.eulerAngles = angle.eulerAngles;

        this.transform.parent.GetComponent<Tank>().BurrellPosition = this.transform.FindChild("Fire").transform.position;
        this.transform.parent.GetComponent<Tank>().BurrellRotation = this.transform.eulerAngles;

    }


    public void OnFire()
    {
        this.transform.FindChild("tank-01_up").GetComponent<Animator>().enabled = true;
        this.transform.FindChild("tank-01_up").GetComponent<Animator>().Play(0);
    }

  
}
