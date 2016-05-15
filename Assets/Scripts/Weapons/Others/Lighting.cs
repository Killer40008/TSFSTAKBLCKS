using UnityEngine;
using System.Collections;
using System.Linq;

public class Lighting : MonoBehaviour, IWeapon
{
    public const int COST = 11000;

    public const float Damage = 200;
    public  const float Strength = 200;
    GameObject Tank;

    public void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = Damage,
            Strength = Strength,
            BombObj = this.gameObject,
            Sprite = null,
            SoruceTank = tank

        };
    }

    public WeaponData Bomb { get; set; }



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
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction,bool forward = true)
    { }

    public void Fire(GameObject tank)
    {
        StartCoroutine(StrikeLighting(tank));
    }
    IEnumerator StrikeLighting(GameObject tank)
    {
        Fade.BackGroundFadeIn();
        yield return new WaitForSeconds(1.5f);
        GameObject[] attackingTanks = Managers.TurnManager.tanks.Where(t => t != tank && t.activeSelf == true).ToArray();


        //show Lighting
        for (int i = 0; i < attackingTanks.Length; i++)
        {
            Vector3 pos = attackingTanks[i].transform.position;
            pos.y -= 0.6f;
            pos.z = -7;
            Instantiate(Managers.SpawnManager.LightingPrefap, pos, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.6f);


        for (int i = 0; i < attackingTanks.Length; i++)
        {

            GameObject armor = FindChildByLayer(attackingTanks[i].transform, LayerMask.NameToLayer("Armor"));
            if (armor != null)
                armor.GetComponent<Armor>().OnLightingEnter(tank, attackingTanks[i], this.gameObject);
            else
                Bomb.PlayerHitLighting(attackingTanks[i], Damage);
        }


        yield return new WaitForSeconds(1);
        Managers.TurnManager.StartCoroutine(Bomb.CheckForNextStep());

        Fade.BackGroundFadeOut();
        Destroy(this.gameObject);
    }

    public GameObject FindChildByLayer(Transform parent, int layer)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).gameObject.layer == layer)
                return parent.GetChild(i).gameObject;
        }
        return null;
    }
}
