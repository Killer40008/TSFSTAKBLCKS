using UnityEngine;
using System.Collections;

public enum Weapons
{
    Normal_Bomb,
    Cluster_Bomb,
    Small_Bomb,
    Large_Bomb,
    Mega_Bomb,
    Moving_Bomb,
    Nuclear_bomb,
    Magnit_Bomb,
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


    public void PlayerHit(GameObject hit)
    {

        if (!BombObjectDestroyed)
        {
            float deltaPosition = Mathf.Abs(hit.transform.position.x - BombObj.transform.position.x);
            Managers.DamageManager.SubstractHealth(hit, Damage);
            Managers.DamageManager.SubstractStrength(hit, Strength);
            Managers.DestroyManager.CheckAndDestroy(hit);
            //
            PlayExplosionEffect();
            BombObjectDestroyed = true;
        }
    }


    public void FloorHit(GameObject hit)
    {
        if (!BombObjectDestroyed)
        {
            MonoBehaviour.Destroy(BombObj);
            Managers.TurnManager.SetTurnToNextTank();
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
        BombObj.transform.localScale = SizeInital;
        BombObj.AddComponent<SphereCollider>().radius = 0.1f;

        //set force and position 
        Rigidbody rigit = BombObj.transform.gameObject.AddComponent<Rigidbody>();
        rigit.constraints = RigidbodyConstraints.FreezePositionZ;
        rigit.mass = Mass;
        rigit.drag = Drag;
        rigit.useGravity = true;
        rigit.AddForce(BombObj.transform.right * FireSpeed, ForceMode.VelocityChange);

        //set audio
        //AudioSource source = BombObj.transform.gameObject.AddComponent<AudioSource>();
        //source.clip = LunchClip;
        //source.Play();


    }

    public IEnumerator InitalPeriodEnd()
    {
        yield return new WaitForSeconds(IntialPeriod);
        BombObj.transform.localScale = SizeFinal;
    }

    private void PlayExplosionEffect()
    {
        GameObject explosion = (GameObject)MonoBehaviour.Instantiate(ExplosionPrefap, BombObj.transform.position, Quaternion.identity);
        explosion.transform.localScale = ExplosionSize;
        explosion.AddComponent<DestroyWhenFinished>();
        AnimationClip clip = explosion.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;
        AnimationEvent ev = new AnimationEvent() { functionName = "ExplosionAnimationFinished", time = clip.length, intParameter = 0 };
        clip.AddEvent(ev);
        MonoBehaviour.Destroy(BombObj);
    }

}
