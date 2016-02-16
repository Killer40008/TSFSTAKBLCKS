using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{

    public float shake = 0;
    float shakeAmount = 0.2f;
    float decreaseFactor = 1.0f;
    Vector3 OrigianlPos;

    void Start()
    {
        OrigianlPos = this.transform.localPosition;
    }

    void Update()
    {
        if (shake > 0)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -10);
            shake -= Time.deltaTime * decreaseFactor;
            GameObject.Find("BackGround").GetComponent<ResizeToScreen>().Resize();
        }
        else
        {
            this.transform.localPosition = OrigianlPos;
            shake = 0.0f;
        }
    }
}
