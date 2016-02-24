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
        
        if (other.tag == "Player" && other.GetComponent<Tank>().ArmorActivate == false && !triggeredList.Contains(other.gameObject))
        {
            float direction = Mathf.Sign(CollisionPosisiton.x - other.transform.position.x);
            float deltaPosition = Vector3.Distance(CollisionPosisiton, other.transform.position);
            Debug.Log(deltaPosition);
            Managers.DamageManager.SubstractHealth(other.gameObject, Weapon.Damage / (int)deltaPosition);
            Managers.DamageManager.SubstractStrength(other.gameObject, Weapon.Strength / (int)deltaPosition);
            bool desroyed = Managers.DestroyManager.CheckAndDestroy(other.gameObject);

            //set score
                if (desroyed)
                    Managers.PlayerInfos.AddMoneyToPlayer(Weapon.SoruceTank, 500);
                else
                    Managers.PlayerInfos.AddMoneyToPlayer(Weapon.SoruceTank, 80);


            triggeredList.Add(other.gameObject);
        }

        
    }

    IEnumerator DistroyTrigger()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
