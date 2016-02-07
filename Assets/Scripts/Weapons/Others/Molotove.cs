using UnityEngine;
using System.Collections;

public class Molotove : MonoBehaviour, IWeapon
{

    GameObject Tank;

    public void Create(Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 5,
            Strength = 5,
            BombObj = this.gameObject,
            Drag = this.Drag,
            SizeInital = new Vector3(0.1523757f, 0.1523757f, 0.1523757f),
            SizeFinal = new Vector3(0.3612028f, 0.3612028f, 0.3612028f),
            ExplosionSize = new Vector3(1f, 1f, 1f),
            IntialPeriod = 0.5f,
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
        if (other.gameObject.tag == "Terrain" || other.gameObject.tag == "Pistons" || other.gameObject.tag == "ForestFloor")
        {
            Bomb.OnCollide(Tank, other);
        }
        else if (other.gameObject.tag == "Player")
        {
            GameObject burnObj = Bomb.PlayExplosionEffect(false);
            Destroy(Bomb.BombObj);
           Managers.Me. StartCoroutine(SubtractHealth(other.gameObject, burnObj));
        }

    }

    IEnumerator SubtractHealth(GameObject tank, GameObject burnObject)
    {
        while (Managers.DamageManager.GetHealth(tank) > 0)
        {
            yield return new WaitForSeconds(1);
            Managers.DamageManager.SubstractHealth(tank, Bomb.Damage);
            Managers.DamageManager.SubstractStrength(tank, Bomb.Strength);
            Managers.DamageManager.CalculatePlayerHealthInUI();
            Managers.DamageManager.CalculatePlayerStrenghInUI();
        }
        Managers.DestroyManager.CheckAndDestroy(tank);
        Destroy(burnObject);
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
        get { return 3; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 5; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
}
