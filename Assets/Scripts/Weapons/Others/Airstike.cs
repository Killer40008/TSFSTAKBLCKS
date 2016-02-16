using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Airstike : MonoBehaviour, IWeapon
{
    public static List<IEnumerator> highlightCoroutines = new List<IEnumerator>();
    GameObject Tank;

    public void Create( Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 150,
            Strength = 100,
           BombObj = this.gameObject,Drag = this.Drag,
            SizeInital = new Vector3(0.1523757f, 0.1523757f, 0.1523757f),
            SizeFinal = new Vector3(0.7389345f, 0.7389345f, 0.7389345f),
            ExplosionSize = new Vector3(1.8f, 1.8f, 1.8f),
            IntialPeriod = 0.5f,
            SpriteColor = Color.white,
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh,
            SoruceTank = tank

        };
        
    }

    public WeaponData Bomb { get; set; }


    public static void SelectRandomTankForAI(GameObject tank)
    {
        Focus f = Managers.TurnManager.tanks.Where(t => t.activeSelf == true && t != tank).ToArray()
              .OrderBy(x => Random.Range(0, 100))
              .FirstOrDefault().GetComponent<Focus>();
        f.Active();
        f.OnMouseDown();
    }


    public static void Selected()
    {
        Managers.ShowSliders(false);
        Managers.TurnManager.tanks.ToList().ForEach(e =>
        {
            if (e != Managers.TurnManager.PlayerTank && e.activeSelf == true)
            {
                e.GetComponent<Focus>().Active();
                highlightCoroutines.Add(Highlight(e.transform));
                Managers.Me.StartCoroutine(highlightCoroutines[highlightCoroutines.Count - 1]);
            }
        });
    }


    public static IEnumerator Highlight(Transform trns)
    {
        Tank sprite = trns.GetComponent<Tank>();
        Color orginal = sprite.Color;
        sprite.OrginalColor = orginal;
        float t = 0.05f;

        while (true)
        {
            while (t < 1)
            {
                sprite.Color = Color.Lerp(sprite.Color, Color.white, t);
                t += 0.05f;
                yield return new WaitForEndOfFrame();
            }
            while (t > 0)
            {
                sprite.Color = Color.Lerp(orginal, Color.white, t);
                t -= 0.05f;
                yield return new WaitForEndOfFrame();
            }

        }

    }



    public void OnCollisionEnter(Collision other)
    {
        Managers.TurnManager.tanks.ToList().ForEach(e => e.GetComponent<Focus>().DeActive());

        Bomb.OnCollide(Tank, other);
        if (Missile.highlightCoroutines.Count > 0)
        {
            Missile.highlightCoroutines.ForEach(c => Managers.Me.StopCoroutine(c));
            Missile.highlightCoroutines.Clear();
        }

        Managers.TurnManager.tanks.ToList().ForEach(t => t.GetComponent<Tank>().Color = t.GetComponent<Tank>().OrginalColor);
    }
    void SetAlTankHit(GameObject hit)
    {
        if (Tank.GetComponent<Tank_AI>().IsAlTank)
            Tank.GetComponent<Tank_AI>().LastTankHit = hit;

    }

    void LateUpdate()
    {        //destroy bomb when it's leaves the screen and set turn to the next tank
        if (-(SpawnManager.CameraWidth / 2) >= this.transform.position.x + 1 ||
            (SpawnManager.CameraWidth / 2) <= this.transform.position.x - 1)
        {
            if (!Bomb.BombObjectDestroyed)
            {
                SetAlTankHit(null);
                Bomb.PlayExplosionEffect();
            }
        }
    }



    public int ExplosionSpriteIndex
    {
        get { return 1; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 7; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {

        StartCoroutine(FireAirstrike(tank));
    }


    private IEnumerator FireAirstrike(GameObject tank)
    {
      GameObject plane = (GameObject) Instantiate(Managers.SpawnManager.AirstrikePrefap);
        GameObject target = Managers.TurnManager.tanks.Where(t => t.GetComponent<Focus>().TankSelected != null).FirstOrDefault();

        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (plane.transform.position.x >= target.transform.position.x)
            {
                Bomb.DropAirstrike(plane);
                break;
            }
        }
    }


    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction)
    {
        Bomb.FireCluster(mainBomb, strength,direction);
    }
}
