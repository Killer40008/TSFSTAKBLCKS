using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphareTriggerHit : MonoBehaviour 
{
    public WeaponData Weapon { get; set; }
    public Vector3 CollisionPosisiton { get; set; }
    public List<GameObject> triggeredList = new List<GameObject>();

    void Start()
    {
        StartCoroutine(DistroyTrigger());
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && !triggeredList.Contains(other.gameObject))
        {
            float deltaPosition = Mathf.Abs(CollisionPosisiton.x - other.transform.position.x);
            Managers.DamageManager.SubstractHealth(other.gameObject, Weapon.Damage / deltaPosition);
            Managers.DamageManager.SubstractStrength(other.gameObject, Weapon.Strength / deltaPosition);
            Managers.DestroyManager.CheckAndDestroy(other.gameObject);
            triggeredList.Add(other.gameObject);
        }

        
    }

    IEnumerator DistroyTrigger()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
