using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Tank_Movement : MonoBehaviour, IPointerDownHandler
{

    bool IsGrounded = false;
    public float Speed = 0.03f;
    Rigidbody rigit;

    void Start()
    {
        rigit = GetComponent<Rigidbody>();
    }

    public void OnPointerDown(PointerEventData data)
    {
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
                this.transform.Translate(translation, 0, 0);
            }

            //apply tank gravity
            rigit.AddForce(Physics.gravity * 0.3f, ForceMode.VelocityChange);

            ClampPosition();
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
                this.transform.Translate(translation, 0, 0);
            }

            //apply tank gravity
            rigit.AddForce(Physics.gravity * 0.3f, ForceMode.VelocityChange);

            ClampPosition();
        }
    }

    private void ClampPosition()
    {
        float width = this.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float xClamped = Mathf.Clamp(this.transform.position.x, (-SpawnManager.CameraWidth / 2) + width, (SpawnManager.CameraWidth / 2) - width);

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
