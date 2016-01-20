using UnityEngine;
using System.Collections;

public interface IBomb
{
    void PlayerHit(GameObject hit);
    void FloorHit(GameObject hit);
    void Fire(GameObject tank);
    void InitalPeriodEnd();
}

public class BombData :  IBomb
{
    public GameObject BombObj { get; set; }
    public float Damage { get; set; }
    public float Mass { get; set; }
    public bool ExplodeWhenHitOtherPlayer { get; set; }
    public Vector3 SizeInital { get; set; }
    public Vector3 SizeFinal { get; set; }
    public Material Material { get; set; }
    public Sprite Sprite { get; set; }
    public float FireSpeed { get; set; }
    public float IntialPeriod { get; set; }
    public float RadiusOfExplosion { get; set; }
    public Object ExplosionPrefap { get; set; }
    public AudioClip FireClip { get; set; }
    public AudioClip ExplosionClip { get; set; }


    public void PlayerHit(GameObject hit)
    {
        float deltaPosition = Mathf.Abs(hit.transform.position.x - BombObj.transform.position.x);
        hit.GetComponent<Tank>().Health -= (Damage - (deltaPosition * 10));
        //
        PlayExplosionEffect();
    }


    public void FloorHit(GameObject hit)
    {
        MonoBehaviour.Destroy(BombObj);
    }

    public void Fire(GameObject tank)
    {
        //set sprites and size , position , collider
        
        BombObj.transform.position = tank.GetComponent<Tank>().BurrellPosition;
        BombObj.transform.eulerAngles = tank.GetComponent<Tank>().BurrellRotation;
        BombObj.transform.gameObject.AddComponent<SpriteRenderer>().sprite = Sprite;
        BombObj.AddComponent<SphereCollider>();
        BombObj.transform.localScale = SizeInital;

        //set force and position 
        Rigidbody rigit = BombObj.transform.gameObject.AddComponent<Rigidbody>();
        rigit.constraints = RigidbodyConstraints.FreezePositionZ;
        rigit.mass  = Mass;
        rigit.useGravity = true;
        rigit.AddForce(BombObj.transform.right * FireSpeed, ForceMode.VelocityChange);

        //set audio
        //AudioSource source = BombObj.transform.gameObject.AddComponent<AudioSource>();
        //source.clip = LunchClip;
        //source.Play();
        
    }

    public void InitalPeriodEnd()
    {
        BombObj.transform.localScale = SizeFinal;
    }

    private void PlayExplosionEffect()
    {
        GameObject explosion = (GameObject)MonoBehaviour.Instantiate(ExplosionPrefap, BombObj.transform.position, Quaternion.identity);
        explosion.AddComponent<DestroyExplosionEffect>();
        AnimationClip clip = explosion.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;
        AnimationEvent ev = new AnimationEvent() { functionName = "EffectAnimationFinished", time = clip.length, intParameter = 1 };
        clip.AddEvent(ev);
        MonoBehaviour.Destroy(BombObj);
    }

}
