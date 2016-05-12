using UnityEngine;
using System.Collections;

public class Cluster_Bomb : MonoBehaviour, IWeapon
{

    public const int COST = 200;
    GameObject Tank;

    public void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 30,
            Strength = 5,
           BombObj = this.gameObject,Drag = this.Drag,
            SizeInital = new Vector3(0.7f, 0.7f, 0.7f),
            SizeFinal = new Vector3(0.7f, 0.7f, 0.7f),
            ExplosionSize = new Vector3(0.4f, 0.4f, 0.4f),
            RadiusOfExplosion = 0f,
            IntialPeriod = 0.5f,
            SpriteColor = Color.black,
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
        if (other.gameObject.tag != "Bomb")
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject wObj = new GameObject() { layer = 9 };
                wObj.tag = "Bomb";
                wObj.transform.position = new Vector3(Bomb.BombObj.transform.position.x, Bomb.BombObj.transform.position.y + 1);
                WeaponsCombo.CurrentWeapon = wObj.AddComponent<Normal_Bomb>();
                WeaponsCombo.CurrentWeapon.Create(Bomb.Sprite, Bomb.ExplosionPrefap, Bomb.Strength, Bomb.SoruceTank);
                WeaponsCombo.CurrentWeapon.Bomb.RadiusOfExplosion = 1.3f;
                WeaponsCombo.CurrentWeapon.FireCluster(Bomb.BombObj, 10, WeaponData.Direction.Up);
            }
            for (int i = 0; i < 2; i++)
            {
                GameObject wObj = new GameObject() { layer = 9 };
                wObj.tag = "Bomb";
                wObj.transform.position = new Vector3(Bomb.BombObj.transform.position.x, Bomb.BombObj.transform.position.y + 1);
                WeaponsCombo.CurrentWeapon = wObj.AddComponent<Normal_Bomb>();
                WeaponsCombo.CurrentWeapon.Create(Bomb.Sprite, Bomb.ExplosionPrefap, Bomb.Strength, Bomb.SoruceTank);
                WeaponsCombo.CurrentWeapon.Bomb.RadiusOfExplosion = 1.3f;
                WeaponsCombo.CurrentWeapon.FireCluster(Bomb.BombObj, 10, WeaponData.Direction.Up, false);
            }
        }

        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("AntiBomb"))
        {
            Bomb.OnCollide(Tank, other.gameObject);
        }
        else
        {
            Bomb.OnCollide(Tank, other.gameObject);
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
        get { return 0; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction, bool forward  =true)
    {
        Bomb.FireCluster(mainBomb, strength,direction, forward);
    }

}
