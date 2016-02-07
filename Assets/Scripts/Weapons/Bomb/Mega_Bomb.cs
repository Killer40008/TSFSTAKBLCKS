using UnityEngine;
using System.Collections;

public class Mega_Bomb : MonoBehaviour, IWeapon
{
    GameObject Tank;

    public void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 80,
            Strength = 80,
           BombObj = this.gameObject,Drag = this.Drag,
            SizeInital = new Vector3(0.1523757f, 0.1523757f, 0.1523757f),
            SizeFinal = new Vector3(0.25f, 0.25f, 0.25f),
            ExplosionSize = new Vector3(1.5f, 1.5f, 1.5f),
            IntialPeriod = 0.5f,
            RadiusOfExplosion = 1.6f,
            SpriteColor = Color.white,
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh,
        };
        
    }

    public WeaponData Bomb { get; set; }


    public void OnCollisionEnter(Collision other)
    {
        Bomb.OnCollide(Tank, other);

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



    public int ExplosionSpriteIndex
    {
        get { return 1; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 2; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
}
