using UnityEngine;
using System.Collections;

public class Tank_Movement : MonoBehaviour 
{

    bool IsGrounded = false;
    public float Speed = 0.03f;
    Rigidbody rigit;

    void Start()
    {
        rigit = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (enabled)
        {
            //get input from user
            float translation = Input.GetAxis("Horizontal") * Speed;
            if (translation != 0 && IsGrounded)
            {
                //if (rInfo.g.
                this.transform.Translate(translation, 0, 0);
            }

            //apply tank gravity
            rigit.AddForce(Physics.gravity * 0.3f, ForceMode.VelocityChange);

            ClampPosition();
        }
    }

    private void ClampPosition()
    {
        float xClamped = Mathf.Clamp(this.transform.position.x, -8.184701f, 8.184701f);

        this.transform.position = new Vector3(xClamped, this.transform.position.y, this.transform.position.z);
            
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "Terrain")
        {
            IsGrounded = true;
        }
        else if (theCollision.gameObject.tag == "Player")
        {
            if (enabled)
                 theCollision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    //void OnCollisionStay(Collision theCollision)
    //{
    //    if (theCollision.gameObject.tag == "Terrain")
    //    {
    //        IsGrounded = true;
    //    }
    //}

    //void OnCollisionExit(Collision theCollision)
    //{
    //    if (theCollision.gameObject.tag == "Terrain")
    //    {
    //        IsGrounded = false;
    //    }
    //}


}
