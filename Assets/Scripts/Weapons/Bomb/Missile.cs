using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Missile : MonoBehaviour, IWeapon
{
    public const int COST = 5000;
    GameObject Tank;
    public static List<IEnumerator> highlightCoroutines = new List<IEnumerator>();



    public void Create(Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 100,
            Strength = 100,
            BombObj = this.gameObject,
            Drag = this.Drag,
            SizeInital = new Vector3(0.1523757f, 0.1523757f, 0.1523757f),
            SizeFinal = new Vector3(0.5581225f, 0.5581225f, 0.5581225f),
            ExplosionSize = new Vector3(0.7f, 0.7f, 0.7f),
            IntialPeriod = 0.5f,
            SpriteColor = Color.white,
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh,
            DontUseAngularDrag = true,
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
    {
        //destroy bomb when it's leaves the screen and set turn to the next tank
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
        get { return 0; }
    }

    public int GameObjectSpriteIndex
    {
        get { return 4; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }


    public void Fire(GameObject tank)
    {

        StartCoroutine(FireMagnit(tank));
    }
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction,bool forward = true)
    {
        Bomb.FireCluster(mainBomb, strength,direction);
    }

    private IEnumerator FireMagnit(GameObject tank)
    {
        yield return StartCoroutine(tank.transform.FindChild("Burrell").GetComponent<Burrell_Movement>()
               .MoveToAngle(Quaternion.Euler(0, 0, 90)));

        Bomb.Drag = 0;
        Bomb.FireSpeed = 8.3f;
        Bomb.Fire(tank);

        GameObject gm = Instantiate(Managers.SpawnManager.RocketEffect, Bomb.BombObj.transform.position, Quaternion.identity) as GameObject;
        gm.transform.position = new Vector3(gm.transform.position.x, gm.transform.position.y-0.2f, -6);
        gm.transform.SetParent(Bomb.BombObj.transform);

        StartCoroutine(MoveToTarget());

    }

    private IEnumerator MoveToTarget()
    {
        const float targetY = 3.24f;
        GameObject target = Managers.TurnManager.tanks.Where(t => t.GetComponent<Focus>().TankSelected != null).FirstOrDefault();

        while (Bomb.BombObj.transform.position.y < targetY) { yield return new WaitForFixedUpdate(); }

        Vector3 orginalRot = Bomb.BombObj.transform.eulerAngles;
        float direction = Mathf.Sign(target.transform.position.x - Bomb.BombObj.transform.position.x);
        float angle = (direction == 1 ? -45 : 180+45);

        float time = 0f;
        while (time < 1)
        {
            yield return new WaitForFixedUpdate();
            Bomb.BombObj.transform.eulerAngles = Vector3.Lerp(orginalRot, new Vector3(0, 0, angle), time * 2f);

            time += 0.015f;
        }

        Vector3 orginalPos = Bomb.BombObj.transform.position;
        Vector3 targetPos = target.transform.position;

        time = 0;
        while (time < 1)
        {
            yield return new WaitForFixedUpdate();
            Bomb.BombObj.transform.position = Vector3.Lerp(orginalPos, targetPos, time);

            time += 0.02f * Time.timeScale;
        }

    }
}
