using UnityEngine;
using System.Collections;

public class Normal_Bomb : MonoBehaviour
{

    GameObject Tank;

   public void Create(GameObject bombObj, Sprite sprite, Object explosion ,float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new BombData()
        {
            Damage = 20,
            BombObj = bombObj,
            SizeInital = new Vector3(0.5f, 0.5f, 0.5f),
            SizeFinal = new Vector3(0.5f, 0.5f, 0.5f),
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh * 0.1f,
        };
    }
    public BombData Bomb { get; set; }



    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject != Tank)
            Bomb.PlayerHit(other.gameObject);
        else if (other.gameObject.tag == "Terrain")
            Bomb.FloorHit(other.gameObject);
    }

    
}
