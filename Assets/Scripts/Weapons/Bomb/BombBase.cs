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
    public bool BombObjectDestroyed { private set; get; }

    public void PlayerHit(GameObject hit)
    {

        if (!BombObjectDestroyed)
        {
            float deltaPosition = Mathf.Abs(hit.transform.position.x - BombObj.transform.position.x);
            hit.GetComponent<Tank>().Health -= (Damage - (deltaPosition * 10));
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
        explosion.AddComponent<DestroyWhenFinished>();
        AnimationClip clip = explosion.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;
        AnimationEvent ev = new AnimationEvent() { functionName = "ExplosionAnimationFinished", time = clip.length, intParameter = 0 };
        clip.AddEvent(ev);
        MonoBehaviour.Destroy(BombObj);
    }

}
