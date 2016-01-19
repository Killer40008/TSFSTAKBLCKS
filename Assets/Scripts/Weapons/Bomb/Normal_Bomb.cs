using UnityEngine;
using System.Collections;

public class Normal_Bomb
{

    Rigidbody rigit;

   public Normal_Bomb(GameObject bombObj,Sprite sprite, float lunchSpeed, GameObject tank)
    {
        rigit = bombObj.GetComponent<Rigidbody>();
        Bomb = new BombData()
        {
            Damage = 20,
            BombObj = bombObj,
            SizeInital = new Vector3(0.5f, 0.5f, 0.5f),
            SizeFinal = new Vector3(0.5f, 0.5f, 0.5f),
            Sprite = sprite,
            FireSpeed = lunchSpeed,
            Mass = 1,
        };
    }
    public BombData Bomb { get; set; }


    
}
