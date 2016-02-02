using UnityEngine;
using System.Collections;

public class Cload_Movement : MonoBehaviour
{
    const float CONSTRAIN = 17.11f;
    bool HasCreated = false;
    public Sprite[] sprites;

    public void Begin()
    {
      

        this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }


    void FixedUpdate()
    {
        if (enabled)
        {
            this.transform.Translate(-Wind.WindDirection * Wind.WindForce, 0, 0);
            //
            if (Mathf.Abs(this.transform.position.x) >= CONSTRAIN && HasCreated == false)
            {
                GameObject gm = (GameObject)Instantiate(((Object)this.gameObject), new Vector3(CONSTRAIN * Wind.WindDirection, this.transform.position.y, -1), Quaternion.identity);
                gm.name = "Cloud Clone";
                gm.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                HasCreated = true;
            }

            if (Mathf.Abs(transform.position.x) >= CONSTRAIN + 0.1f)
                Destroy(this.gameObject);
        }
    }
}
