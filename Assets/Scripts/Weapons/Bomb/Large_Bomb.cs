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
            SizeInital = new Vector3(0.06799684f, 0.06799684f, 0.06799684f),
            SizeFinal = new Vector3(0.15f, 0.15f, 0.15f),
            ExplosionSize = new Vector3(1f, 1f, 1f),
            IntialPeriod = 0.5f,
            SpriteColor = new Color32(191, 0, 0, 255),
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh,
        };
        StartCoroutine(Bomb.InitalPeriodEnd());
    }

    public WeaponData Bomb { get; set; }



    bool isCollide = false;
    public void OnCollisionEnter(Collision other)
    {
        if (!isCollide)
        {
            if (other.gameObject.tag == "Player")
            {
                Bomb.PlayerHit(other.gameObject);
                SetAlTankHit(other.gameObject);
            }
            else if (other.gameObject.tag == "Terrain" || other.gameObject.tag == "Pistons" || other.gameObject.tag == "ForestFloor")
            {
                Bomb.FloorHit(other.gameObject);
                SetAlTankHit(null);
            }
            isCollide = true;
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



    public int ExplosionSpriteIndex
    {
        get { return 0; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 1; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
}
