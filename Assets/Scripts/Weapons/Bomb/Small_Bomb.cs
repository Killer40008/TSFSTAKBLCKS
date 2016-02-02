﻿using UnityEngine;
using System.Collections;

public class Small_Bomb : MonoBehaviour, IWeapon
{
    GameObject Tank;

    public void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 40,
            Strength = 30,
           BombObj = this.gameObject,Drag = this.Drag,
            SizeInital = new Vector3(0.7f, 0.7f, 0.7f),
            SizeFinal = new Vector3(0.9f, 0.9f, 0.9f),
            ExplosionSize = new Vector3(0.5f, 0.5f, 0.5f),
            IntialPeriod = 0.5f,
            SpriteColor = Color.black,
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
        get { return 0; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);
    }
}
