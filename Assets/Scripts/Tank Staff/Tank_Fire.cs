using UnityEngine;
using System.Collections;

public class Tank_Fire : MonoBehaviour
{

    GameObject game;
    Rigidbody rigit;

    void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown("space"))
            {
                Sprite[] sprites = this.transform.parent.GetComponent<Tank>().BombSprites;
                Normal_Bomb nb = new Normal_Bomb(new GameObject(), sprites[0], 500, this.transform.parent.gameObject);
                nb.Bomb.Fire(this.transform.parent.gameObject);

                Managers.TurnManager.SetTurnToNextTank();
            }
        }
    }
}
