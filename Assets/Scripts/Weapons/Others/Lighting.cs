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

        //show Lighting
        foreach (GameObject cTank in Managers.TurnManager.tanks.Where(t => t != tank && t.activeSelf == true).ToArray())
        {
            Vector3 pos = cTank.transform.position;
            pos.y -= 0.6f;
            pos.z = -7;
            Instantiate(Managers.SpawnManager.LightingPrefap, pos, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.6f);

        //destroy
        bool nextTurn = true;
        foreach (GameObject cTank in Managers.TurnManager.tanks.Where(t => t != tank && t.activeSelf == true).ToArray())
        {
            yield return new WaitForEndOfFrame();
            bool destroyed = false;
            GameObject armor = null;
            if ((armor = FindChildByLayer(cTank.transform, LayerMask.NameToLayer("Armor"))) != null)
                armor.GetComponent<Armor>().OnLightingEnter(tank);
            else
            {
                Managers.PlayerInfos.AddMoneyToPlayer(tank, 200);
                Managers.DamageManager.SubstractHealth(cTank, Damage);
                Managers.DamageManager.SubstractStrength(cTank, Strength);
                destroyed = Managers.DestroyManager.CheckAndDestroy(cTank);
            }

            //-----------------

            if (!destroyed && nextTurn)
            {
                Managers.TurnManager.SetTurnToNextTank();
                nextTurn = false;
            }
        }

        yield return new WaitForSeconds(1);
    

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
