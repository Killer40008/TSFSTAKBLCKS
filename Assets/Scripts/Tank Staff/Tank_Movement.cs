using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tank_Movement : MonoBehaviour
{

    bool IsGrounded = false;
    public float Speed = 1;
    Rigidbody rigit;

    void Start()
    {
        rigit = GetComponent<Rigidbody>();
    }



    public void MoveLeft()
    {
        if (enabled)
        {
            //get input from user
            float translation = -1 * Speed;
            if (translation != 0 && IsGrounded)
            {
                //if (rInfo.g.
                this.transform.Translate(translation * Time.fixedDeltaTime, 0, 0);
            }

            //apply tank gravity
            rigit.AddForce(Physics.gravity * 0.7f, ForceMode.VelocityChange);

            ClampPosition();
            SuptractOil();
        }
    }


    public void MoveRight()
    {
        if (enabled)
        {
            //get input from user
            float translation = 1 * Speed;
            if (translation != 0 && IsGrounded)
            {
                //if (rInfo.g.
                this.transform.Translate(translation * Time.fixedDeltaTime, 0, 0);
            }

            //apply tank gravity
            rigit.AddForce(Physics.gravity * 0.3f, ForceMode.VelocityChange);

            ClampPosition();
            SuptractOil();
        }
    }

    private void ClampPosition()
    {
        float width = this.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float xClamped = Mathf.Clamp(this.transform.position.x, (-SpawnManager.CameraWidth / 2) + width, (SpawnManager.CameraWidth / 2) - width);

        this.transform.position = new Vector3(xClamped, this.transform.position.y, this.transform.position.z);
            
    }

    private void SuptractOil()
    {
       float oil = GetComponent<Tank>().Oil -= 2;
       if (oil <= 0)
       {
           GameObject.Find("RightMovement").GetComponent<Button>().interactable = false;
           GameObject.Find("LeftMovement").GetComponent<Button>().interactable = false;
           this.enabled = false;
           NotifyMessage.ShowMessage("Need More Oil", 3);
       }
       else
       {
           GameObject.Find("RightMovement").GetComponent<Button>().interactable = true;
           GameObject.Find("LeftMovement").GetComponent<Button>().interactable = true;
       }
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
