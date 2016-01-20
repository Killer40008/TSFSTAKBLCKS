using UnityEngine;
using System.Collections;

public class Tank_Fire : MonoBehaviour
{

    GameObject game;
    Rigidbody rigit;

    public void Fire()
    {
        if (enabled)
        {
            GameObject bomb = new GameObject();
            Normal_Bomb script = bomb.AddComponent<Normal_Bomb>();

            Sprite[] sprites = this.transform.parent.GetComponent<Tank>().BombSprites;
            Object[] Explosions = this.transform.parent.GetComponent<Tank>().BombExplosions;
            script.Create(bomb, sprites[0], Explosions[0], StrenghSlider.Strengh, this.transform.parent.gameObject);


            script.Bomb.Fire(this.transform.parent.gameObject);
            this.GetComponent<Burrell_Movement>().OnFire();

            Managers.TurnManager.SetTurnToNextTank();

        }
    }
}
