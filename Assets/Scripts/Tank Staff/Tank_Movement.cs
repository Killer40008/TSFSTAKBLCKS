using UnityEngine;
using System.Collections;

public class Tank_Movement : MonoBehaviour 
{

    bool IsGrounded = false;
    public float Speed = 0.03f;



    void FixedUpdate()
    {
        if (enabled)
        {
            //get input from user
            float translation = Input.GetAxis("Horizontal") * Speed;

            ////get tanks around player
            //RaycastHit rInfo, lInfo; 
            //Physics.Raycast(transform.position, Vector3.right ,out rInfo);
            //Physics.Raycast(transform.position, Vector3.left ,out lInfo);

            if (translation != 0 && IsGrounded)
            {
                //if (rInfo.g.
                this.transform.Translate(translation, 0, 0);
            }
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
        if (enabled)
        {
            if (theCollision.gameObject.tag == "Terrain")
            {
                IsGrounded = true;
            }
            else if (theCollision.gameObject.tag == "Player")
            {
                theCollision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
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
