﻿using UnityEngine;
using System.Collections;
using System.Linq;

public enum Weapons
{
    Normal_Bomb,
    Cluster_Bomb,
    Small_Bomb,
    Large_Bomb,
    Mega_Bomb,
    Moving_Bomb,
    Nuclear_bomb,
    Auto_Missile,
    Airstike,
    Shell,
    Mega_Shell,
    Laser,
    Molotove
}

public interface IWeapon 
{
    float Drag { get; set; }
    int ExplosionSpriteIndex { get;}
    int GameObjectSpriteIndex { get; }
    GameObject WeaponObj { get; set; }
    void Fire(GameObject tank);
    void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank);
}

public class WeaponData 
{
    public GameObject BombObj { get; set; }
    public float Damage { get; set; }
    public float Strength { get; set; }
    public float Mass { get; set; }
    public bool ExplodeWhenHitOtherPlayer { get; set; }
    public Color32 SpriteColor { get; set; }
    public Vector3 SizeInital { get; set; }
    public Vector3 SizeFinal { get; set; }
    public Vector3 ExplosionSize { get; set; }
    public float Drag { get; set; }
    public Material Material { get; set; }
    public Sprite Sprite { get; set; }
    public float FireSpeed { get; set; }
    public float IntialPeriod { get; set; }
    public float RadiusOfExplosion { get; set; }
    public Object ExplosionPrefap { get; set; }
    public AudioClip FireClip { get; set; }
    public AudioClip ExplosionClip { get; set; }
    public bool BombObjectDestroyed { private set; get; }
    public bool DontUseStandardRigitRadius { get; set; }
    public bool DontUseAngularDrag { get; set; }


    public void PlayerHit(GameObject hit)
    {

        if (!BombObjectDestroyed)
        {
            Managers.DamageManager.SubstractHealth(hit, Damage);
            Managers.DamageManager.SubstractStrength(hit, Strength);
            Managers.DestroyManager.CheckAndDestroy(hit);
            //
            PlayExplosionEffect();
            BombObjectDestroyed = true;
        }
    }


    public void FloorHit(GameObject bomb)
    {
        if (!BombObjectDestroyed)
        {
            //Set damage to nearby tanks
            if (RadiusOfExplosion > 0)
            {
                GameObject sphareTrigger = new GameObject();
                sphareTrigger.transform.position = bomb.transform.position;
                SphereCollider collider = sphareTrigger.AddComponent<SphereCollider>();
                collider.radius = RadiusOfExplosion;
                SphareTriggerHit st = sphareTrigger.AddComponent<SphareTriggerHit>();
                st.Weapon = this;
                st.transform.position = BombObj.transform.position;
                collider.isTrigger = true;
            }




            //
            MonoBehaviour.Destroy(BombObj);
            PlayExplosionEffect();
            BombObjectDestroyed = true;
        }
    }

    public void Fire(GameObject tank)
    {
        //set sprites and size , position , collider

        BombObj.transform.position = tank.GetComponent<Tank>().BurrellPosition;
        BombObj.transform.eulerAngles = tank.GetComponent<Tank>().BurrellRotation;
        BombObj.transform.gameObject.AddComponent<SpriteRenderer>().sprite = Sprite;
        BombObj.transform.gameObject.GetComponent<SpriteRenderer>().color = SpriteColor;
        BombObj.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        BombObj.transform.localScale = SizeInital;
        var colider = BombObj.AddComponent<SphereCollider>();
        if (!DontUseStandardRigitRadius) colider.radius = 0.1f;
        Managers.Me.StartCoroutine(InitalPeriodEnd());

        //set force and position 
        float direction = Mathf.Sign(BombObj.transform.right.x);
        Rigidbody rigit = BombObj.transform.gameObject.AddComponent<Rigidbody>();
        rigit.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY ;
        rigit.mass = Mass;
        rigit.drag = Drag;
        if(!DontUseAngularDrag) rigit.angularDrag = direction == 1 ? 1.7f : 1.3f;
        rigit.useGravity = true;
        rigit.AddForce(BombObj.transform.right * FireSpeed, ForceMode.VelocityChange);
        if(!DontUseAngularDrag) rigit.AddTorque(new Vector3(0, 0, direction));
     
        //set audio
        //AudioSource source = BombObj.transform.gameObject.AddComponent<AudioSource>();
        //source.clip = LunchClip;
        //source.Play();


    }

    public IEnumerator InitalPeriodEnd()
    {
        yield return new WaitForSeconds(IntialPeriod);

        Transform child = null;
        Vector3 cPos = Vector3.zero;
        if (BombObj != null && BombObj.transform.childCount == 1)
        {
            child = BombObj.transform.GetChild(0);
            cPos = child.transform.localPosition;
            cPos.z = -6;
        }

        while (BombObj != null && BombObj.transform.localScale.x < SizeFinal.x)
        {
            yield return new WaitForEndOfFrame();
            if (BombObj == null) { yield break; }
            BombObj.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);

            if (child != null)
            {
                child.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                child.transform.localPosition = cPos;
            }
        }

    }

    public GameObject PlayExplosionEffect(bool Destroy = true)
    {
        GameObject explosion = (GameObject)MonoBehaviour.Instantiate(ExplosionPrefap, BombObj.transform.position, Quaternion.identity);
        explosion.transform.localScale = ExplosionSize;
        explosion.AddComponent<DestroyWhenFinished>();
        AnimationClip clip = explosion.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;
        AnimationEvent ev = new AnimationEvent() { functionName = "ExplosionAnimationFinished", time = clip.length, intParameter = System.Convert.ToInt32(Destroy) };
        clip.AddEvent(ev);
        if (Destroy) MonoBehaviour.Destroy(BombObj);
        return explosion;
    }

    public void OnCollide(GameObject fireTank, Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHit(other.gameObject);
            SetAlTankHit(fireTank, other.gameObject);
        }
        else if (other.gameObject.tag == "Terrain" || other.gameObject.tag == "Pistons" || other.gameObject.tag == "ForestFloor")
        {
            FloorHit(other.gameObject);
            SetAlTankHit(fireTank, null);
        }
        else if (other.gameObject.tag == "Snow")
        {
            MonoBehaviour.Destroy(other.gameObject);
            FloorHit(other.gameObject);
            SetAlTankHit(fireTank, null);
        }
    }

    void SetAlTankHit(GameObject fireTank, GameObject hit)
    {
        if (fireTank.GetComponent<Tank_AI>().IsAlTank)
            fireTank.GetComponent<Tank_AI>().LastTankHit = hit;

    }
}
