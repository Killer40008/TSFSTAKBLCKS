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
    Lighting,
    Molotove
}

public interface IWeapon 
{
    float Drag { get; set; }
    int ExplosionSpriteIndex { get;}
    int GameObjectSpriteIndex { get; }
    GameObject WeaponObj { get; set; }
    WeaponData Bomb { get; set; }
    void Fire(GameObject tank);
    void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction, bool forward = true);
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
    public GameObject SoruceTank { get; set; }
    public static GameObject LastPlayerCollide { get; private set; }



    public void PlayerHit(GameObject hit)
    {

        if (!BombObjectDestroyed)
        {
         

            if (AntiStrikeSlider.allow)
                AntiStrikeSlider.DeActive();

            Managers.DamageManager.SubstractHealth(hit, Damage);
            Managers.DamageManager.SubstractStrength(hit, Strength);
            bool destroyed = Managers.DestroyManager.CheckAndDestroy(hit);

            //set money
            if (hit != SoruceTank)
            {
                if (destroyed)
                    Managers.PlayerInfos.AddMoneyToPlayer(SoruceTank, 500);
                else
                    Managers.PlayerInfos.AddMoneyToPlayer(SoruceTank, 200);
            }

            //
            LastPlayerCollide = hit.gameObject;
            PlayExplosionEffect();

            //
            if (!(BombObj.GetComponent<IWeapon>() is Lighting))
            {
                BombObjectDestroyed = true;
            }
        }
    }


    public void FloorHit(GameObject bomb, bool armorCall = false)
    {
        if (!BombObjectDestroyed)
        {

            //Set damage to nearby tanks
            if (RadiusOfExplosion > 0 && armorCall == false)
            {
                GameObject sphareTrigger = new GameObject();
                sphareTrigger.transform.position = bomb.transform.position;
                SphereCollider collider = sphareTrigger.AddComponent<SphereCollider>();
                collider.radius = RadiusOfExplosion;
                SphareTriggerHit st = sphareTrigger.AddComponent<SphareTriggerHit>();
                st.Weapon = this;
                st.transform.position = BombObj.transform.position;
                st.CollisionPosisiton = BombObj.transform.position;
                collider.isTrigger = true;
            }




            //
            if (!(BombObj.GetComponent<IWeapon>() is Lighting))
            {
                MonoBehaviour.Destroy(BombObj);
                BombObjectDestroyed = true;
            }

            PlayExplosionEffect();
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
        rigit.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rigit.mass = Mass;
        rigit.drag = Drag;
        if (!DontUseAngularDrag) rigit.angularDrag = direction == 1 ? 1.7f : 1.3f;
        rigit.useGravity = true;
        rigit.AddForce(BombObj.transform.right * FireSpeed, ForceMode.VelocityChange);
        if (!DontUseAngularDrag) rigit.AddTorque(new Vector3(0, 0, direction));


        //set audio
        //AudioSource source = BombObj.transform.gameObject.AddComponent<AudioSource>();
        //source.clip = LunchClip;
        //source.Play();

    }

    public enum Direction{Up=1,Down=-1}
    public static int ShellCount = 1;
    public void FireCluster(GameObject mainBomb, float strength, Direction yDirection = Direction.Up, bool forward = true)
    {
        //set sprites and size , position , collider


        BombObj.transform.position = new Vector3(mainBomb.transform.position.x, mainBomb.transform.position.y + 1, 0);
        BombObj.transform.gameObject.AddComponent<SpriteRenderer>().sprite = Sprite;
        BombObj.transform.gameObject.GetComponent<SpriteRenderer>().color = SpriteColor;
        BombObj.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        BombObj.transform.localScale = SizeInital;
        var colider = BombObj.AddComponent<SphereCollider>();
        if (!DontUseStandardRigitRadius) colider.radius = 0.1f;
        Managers.Me.StartCoroutine(InitalPeriodEnd());

        //set force and position 
        float direction = Mathf.Sign(mainBomb.transform.position.x - SoruceTank.transform.position.x);
        direction = forward == true ? direction : -direction;
        Rigidbody rigit = BombObj.transform.gameObject.AddComponent<Rigidbody>();
        rigit.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rigit.mass = Mass;
        rigit.useGravity = true;
        if (yDirection == Direction.Up)
            rigit.AddForce(new Vector3(direction * Random.Range(0.1f, 2.1f), Random.Range(2 * (int)yDirection, 6 * (int)yDirection), 0), ForceMode.VelocityChange);
        else
            rigit.AddForce(new Vector3(direction * ShellCount++ * 0.5f, 0, 0), ForceMode.VelocityChange);

    }

    public void DropAirstrike(GameObject plane)
    {

        BombObj.transform.position = plane.transform.position;
        BombObj.transform.gameObject.AddComponent<SpriteRenderer>().sprite = Sprite;
        BombObj.transform.gameObject.GetComponent<SpriteRenderer>().color = SpriteColor;
        BombObj.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        BombObj.transform.localScale = SizeInital;
        var colider = BombObj.AddComponent<SphereCollider>();
        if (!DontUseStandardRigitRadius) colider.radius = 0.1f;
        Managers.Me.StartCoroutine(InitalPeriodEnd());

        //set force and position 
        Rigidbody rigit = BombObj.transform.gameObject.AddComponent<Rigidbody>();
        rigit.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        rigit.mass = Mass;
        rigit.useGravity = true;
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

        if (BombObj != null)
        {
            if (!(WeaponsCombo.CurrentWeapon is Missile) && !(WeaponsCombo.CurrentWeapon is Airstike))
            {
                MonoBehaviour.Destroy(BombObj.GetComponent<SphereCollider>());
                BombObj.AddComponent<SphereCollider>();
            }
        }
    }

    public GameObject PlayExplosionEffect(bool Destroy = true, Vector3? position = null)
    {
        GameObject explosion = null;
        if (!(BombObj.GetComponent<IWeapon>() is Lighting))
        {
            Vector3 instantiatePos = position == null ? BombObj.transform.position : (Vector3)position;
            explosion = (GameObject)MonoBehaviour.Instantiate(ExplosionPrefap, instantiatePos, Quaternion.identity);
            explosion.GetComponent<SpriteRenderer>().sortingOrder = 3;
            explosion.transform.localScale = ExplosionSize;
            DestroyWhenFinished dwf = explosion.AddComponent<DestroyWhenFinished>();
            DestroyWhenFinished.AllowNextTurn = true;
            dwf.tankSource = SoruceTank;

            if (BombObj.GetComponent<IWeapon>() is Molotove && Destroy)
                Managers.WeaponManager.StartCoroutine(DestroyBurnObj(explosion));

            MonoBehaviour.Destroy(BombObj);
        }
        Managers.TurnManager.StartCoroutine(CheckForNextStep());
        return explosion;
    }
    IEnumerator DestroyBurnObj(GameObject burnObj)
    {
        yield return new WaitForSeconds(1f);
        MonoBehaviour.Destroy(burnObj);
    }

    public GameObject PlayAntiStrikeCollisionEffect()
    {
        
        GameObject explosion = (GameObject)MonoBehaviour.Instantiate(Managers.SpawnManager.BombCollisionEffect, BombObj.transform.position, Quaternion.identity);
        explosion.transform.localScale = ExplosionSize;
        DestroyWhenFinished DS = explosion.AddComponent<DestroyWhenFinished>();
        DestroyWhenFinished.AllowNextTurn = true;
        AnimationClip clip = explosion.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;
        AnimationEvent ev = new AnimationEvent() { functionName = "DestroyAnimationFinished", time = clip.length, intParameter = 0 };
        clip.AddEvent(ev);

        if (TurnCorotine != null)
            Managers.TurnManager.StopCoroutine(TurnCorotine);

        MonoBehaviour.Destroy(BombObj);
        Managers.TurnManager.StartCoroutine(CheckForNextStep());

        TurnCorotine = CheckForNextStep();

        return explosion;
    }

    public static IEnumerator TurnCorotine = null;
    public static volatile bool AllowNextTurn;
    IEnumerator CheckForNextStep()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.7f);
        if (GameObject.FindGameObjectsWithTag("Bomb").Count() != 0) { yield break; }
        Managers.TurnManager.StopAllCoroutines();
   
            GameObject tankSource = this.SoruceTank;
            if (tankSource != null)
            {
                Tank tf = tankSource.GetComponent<Tank>();
                if (tf.BurrellCount == 1 && AllowNextTurn) //for double burrell
                {

                    Managers.TurnManager.SetTurnToNextTank();
                    AllowNextTurn = false;


                    if (tankSource != Managers.TurnManager.PlayerTank && tf.DoubleBurrell)
                    {
                        tankSource.GetComponent<Tank>().BurrellCount = 2;
                    }
                }
                else if (tf.BurrellCount == 2 && Managers.TurnManager.CurrentTank == Managers.TurnManager.PlayerTank)
                {
                    if (!CheckForDamageValue(tankSource)) yield break;


                    //double burrell
                    if (Managers.WeaponManager.lastButton != null)
                        Managers.WeaponManager.OnWeaponsSelected(Managers.WeaponManager.lastButton);

                    GameObject.Find("PlayerTimer").GetComponent<Timer>().StartTimer();

                    tankSource.transform.FindChild("Burrell2").GetComponent<Burrell_Movement>().enabled = true;
                    tankSource.transform.FindChild("Burrell2").GetComponent<Tank_Fire>().enabled = true;
                    tankSource.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().enabled = false;
                    NotifyMessage.ShowMessage("Second Burrell Activated!", 2);
                    GameObject.Find("Canvas").transform.FindChild("HUD").FindChild("DisabledPanel").GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                else if (tf.BurrellCount == 2 && Managers.TurnManager.CurrentTank != Managers.TurnManager.PlayerTank)
                {

                    if (!CheckForDamageValue(tankSource)) yield break;

                    tankSource.GetComponent<Tank>().BurrellCount = 1;
                    tankSource.GetComponent<Tank_AI>().LastTankHit = null;

                    if (Managers.TurnManager.tanks.Count(t => t.activeSelf == true) > 1)
                    {
                        Managers.TurnManager.CurrentTank.GetComponent<Tank_AI>().AimBurrellToRandomTank(
                            Managers.TurnManager.tanks.Where(t => t != Managers.TurnManager.CurrentTank).ToArray());
                    }
                }

            
        }
    }

            
    bool CheckForDamageValue(GameObject tankSource)
    {
        if (Managers.DamageManager.GetHealth(tankSource) <= 0)
        {
            
                Managers.TurnManager.SetTurnToNextTank();
            
    
        }
        return true;
    }


    public void OnCollide(GameObject fireTank, GameObject other)
    {
        if (!BombObjectDestroyed)
        {
            if (other.tag == "Player")
            {
                PlayerHit(other);
                SetAlTankHit(fireTank, other);
            }
            else if (other.tag == "Terrain" || other.tag == "Pistons" || other.tag == "ForestFloor")
            {
                FloorHit(other);
                SetAlTankHit(fireTank, null);
            }
            else if (other.tag == "Snow")
            {
                MonoBehaviour.Destroy(other);
                FloorHit(other);
                SetAlTankHit(fireTank, null);
            }
            else if (other.tag == "Bomb")
            {
                //set score
                Managers.PlayerInfos.AddMoneyToPlayer(fireTank, 50);

                //play effect
                PlayAntiStrikeCollisionEffect();
                SetAlTankHit(fireTank, null);
            }
        }
    }

    void SetAlTankHit(GameObject fireTank, GameObject hit)
    {
        if (fireTank.GetComponent<Tank_AI>().IsAlTank)
            fireTank.GetComponent<Tank_AI>().LastTankHit = hit;

    }

}
