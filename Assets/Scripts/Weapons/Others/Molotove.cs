using UnityEngine;
using System.Collections;

public class Molotove : MonoBehaviour, IWeapon
{
    public const int COST = 3000;
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
            SoruceTank = tank

        };

    }

    public WeaponData Bomb { get; set; }


    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {

            Vector3 position = other.transform.position;
            position.y += 0.3f;
            GameObject burnObj = Bomb.PlayExplosionEffect(false, position);
            burnObj.GetComponent<SpriteRenderer>().sortingOrder = 100;
            Managers.Me.StartCoroutine(SubtractHealth(other.gameObject, burnObj));
            if (Managers.MapsManager.CurrentMap == MapManager.Maps.Forest)
            {
                burnObj.transform.SetParent(other.gameObject.transform);
            }

        }
        else
        {
            Bomb.OnCollide(Tank, other.gameObject);
        }
    }



    IEnumerator SubtractHealth(GameObject tank, GameObject burnObject)
    {
        while (Managers.DamageManager.GetHealth(tank) > 0)
        {
            if (burnObject == null || tank == null || Mathf.Abs(tank.transform.position.x - burnObject.transform.position.x) > 1) break; 
            yield return new WaitForSeconds(1);
            Managers.DamageManager.SubstractHealth(tank, Bomb.Damage);
            Managers.DamageManager.SubstractStrength(tank, Bomb.Strength);
            Managers.DamageManager.CalculatePlayerHealthInUI();
            Managers.DamageManager.CalculatePlayerStrenghInUI();
        }
        Managers.DestroyManager.CheckAndDestroy(tank);

        yield return new WaitForSeconds(3);
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
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction,bool forward = true)
    {
        Bomb.FireCluster(mainBomb, strength,direction);
    }
}
