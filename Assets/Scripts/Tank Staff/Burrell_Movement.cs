using UnityEngine;
using System.Collections;

public class Burrell_Movement : MonoBehaviour
{
    public float Speed = 2;
    float rotatoinAngleZ = 0;
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (enabled)
        {
            rotatoinAngleZ += Input.GetAxis("Vertical") * Speed;
            rotatoinAngleZ = Mathf.Clamp(rotatoinAngleZ, 0F, 180F); // clamp angle between 0 to 180

            this.transform.localEulerAngles = new Vector3(0, 0, rotatoinAngleZ);
            this.transform.parent.GetComponent<Tank>().BurrellPosition = this.transform.FindChild("Fire").transform.position;
            this.transform.parent.GetComponent<Tank>().BurrellRotation = this.transform.eulerAngles;

        }
    }

    public void OnFire()
    {
        this.transform.FindChild("tank-01_up").GetComponent<Animator>().enabled = true;
        this.transform.FindChild("tank-01_up").GetComponent<Animator>().Play(0);
    }

  
}
