using UnityEngine;
using System.Collections;

public class NeclearBomb : MonoBehaviour, IWeapon {

    public const int COST = 4000;
    GameObject Tank;

    public void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        this.transform.eulerAngles = new Vector3(0, 0, 315);
        Bomb = new WeaponData()
        {
            Damage = 100,
            Strength = 100,
            BombObj = this.gameObject,Drag = this.Drag,
            SizeInital = new Vector3(0.1523757f, 0.1523757f, 0.1523757f),
            SizeFinal = new Vector3(0.5f, 0.5f, 0.5f),
            ExplosionSize = new Vector3(1,1,1),
            RadiusOfExplosion = 4f,
            IntialPeriod = 0.5f,
            SpriteColor = Color.white,
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh,
            SoruceTank = tank
        };
        
    }

    public WeaponData Bomb { get; set; }


    public void OnCollisionEnter(Collision other)
    {
        Bomb.OnCollide(Tank, other.gameObject);

    }
    void SetAlTankHit(GameObject hit)
    {
        if (Tank.GetComponent<Tank_AI>().IsAlTank)
            Tank.GetComponent<Tank_AI>().LastTankHit = hit;

    }


  


    void LateUpdate()
    {
        //destroy bomb when it's leaves the screen and set turn to the next tank
        if (-(SpawnManager.CameraWidth / 2) >= this.transform.position.x + 1 ||
            (SpawnManager.CameraWidth / 2) <= this.transform.position.x - 1)
        {
            if (!Bomb.BombObjectDestroyed)
            {
                SetAlTankHit(null);
                Bomb.PlayExplosionEffect();
            }
        }
    }



    public int ExplosionSpriteIndex
    {
        get { return 2; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 3; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction,bool forward = true)
    {
        Bomb.FireCluster(mainBomb, strength,direction);
    }
}
