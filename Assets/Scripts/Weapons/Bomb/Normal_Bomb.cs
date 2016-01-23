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
            FireSpeed = fireStrengh,
        };
    }

    public BombData Bomb { get; set; }



    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject != Tank)
        {
            Bomb.PlayerHit(other.gameObject);
            SetAlTankHit(other.gameObject);
        }
        else if (other.gameObject.tag == "Terrain")
        {
            Bomb.FloorHit(other.gameObject);
            SetAlTankHit(null);
        }
    }
    void SetAlTankHit(GameObject hit)
    {
        if (Tank.GetComponent<Tank_AI>().IsAlTank)
            Tank.GetComponent<Tank_AI>().LastTankHit = hit;

    }

    void LateUpdate()
    {
        //destroy bomb when it's leaves the screen and set turn to the next tank
        if (-(SpawnManager.CameraWidth / 2) >= this.transform.position.x ||
            (SpawnManager.CameraWidth / 2) <= this.transform.position.x)
        {
            if (!Bomb.BombObjectDestroyed)
            {
                SetAlTankHit(null);
                Destroy(this.gameObject);
                Managers.TurnManager.SetTurnToNextTank();
            }
        }
    }

    
}
