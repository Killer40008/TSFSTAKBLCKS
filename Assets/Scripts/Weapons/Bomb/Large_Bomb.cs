using UnityEngine;
using System.Collections;

public class Large_Bomb : MonoBehaviour , IWeapon
{
    GameObject Tank;

    public void Create(Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 50,
            Strength = 25,
            BombObj = this.gameObject,
            Drag = this.Drag,
            SizeInital = new Vector3(0.04715929f, 0.04715929f, 0.04715929f),
            SizeFinal = new Vector3(0.09457242f, 0.09457242f, 0.09457242f),
            ExplosionSize = new Vector3(0.9f, 0.9f, 0.9f),
            RadiusOfExplosion = 2f,
            IntialPeriod = 0.5f,
            SpriteColor = new Color32(191, 0, 0, 255),
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
        get { return 0; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 6; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction)
    {
        Bomb.FireCluster(mainBomb, strength,direction);
    }
}
