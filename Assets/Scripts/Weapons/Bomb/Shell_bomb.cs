using UnityEngine;
using System.Collections;

public class Shell_bomb : MonoBehaviour, IWeapon {

    public const int COST = 6000;
    GameObject Tank;

    public void Create(Sprite sprite, Object explosion, float fireStrengh, GameObject tank)
    {
        Tank = tank;
        Bomb = new WeaponData()
        {
            Damage = 200,
            Strength = 100,
            BombObj = this.gameObject,
            Drag = this.Drag,
            SizeInital = new Vector3(0.7f, 0.7f, 0.7f),
            SizeFinal = new Vector3(0.9f, 0.9f, 0.9f),
            ExplosionSize = new Vector3(0.4f, 0.4f, 0.4f),
            IntialPeriod = 0.5f,
            SpriteColor = Color.red,
            Sprite = sprite,
            ExplosionPrefap = explosion,
            Mass = 0.5f,
            FireSpeed = fireStrengh,
            SoruceTank = tank
        };

    }

    public WeaponData Bomb { get; set; }




    public void OnCollisionEnter(Collision other)
    {

        Bomb.OnCollide(Tank, other.gameObject);

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
        get { return 0; }
    }

    public GameObject WeaponObj { get; set; }
    public float Drag { get; set; }

    public void Fire(GameObject tank)
    {
        Bomb.Fire(tank);

        StartCoroutine(VelocityChanged(Bomb.BombObj.GetComponent<Rigidbody>()));

    }
    public void FireCluster(GameObject mainBomb, float strength, WeaponData.Direction direction,bool forward = true)
    {
        Bomb.FireCluster(mainBomb, strength, direction);
    }

    private IEnumerator VelocityChanged(Rigidbody rigit)
    {
        while (rigit != null)
        {
            yield return new WaitForFixedUpdate();
            if (rigit.velocity.normalized.y <= 0)
            {
                WeaponData.ShellCount = 0;
                for (int i = 0; i < 12; i++)
                {
                    GameObject wObj = new GameObject() { layer = 9 };
                    wObj.tag = "Bomb";
                    WeaponsCombo.CurrentWeapon = wObj.AddComponent<Small_Bomb>();
                    Sprite[] sprites = Managers.SpawnManager.BombSprites;
                    Object[] Explosions = Managers.SpawnManager.BombExplosions;
                    WeaponsCombo.CurrentWeapon.Create(sprites[0], Explosions[0], Bomb.Strength, Bomb.SoruceTank);
                    WeaponsCombo.CurrentWeapon.FireCluster(Bomb.BombObj, 10, WeaponData.Direction.Down);
                }
                Destroy(this.gameObject);
                yield break;
            }
        }
    }
}
