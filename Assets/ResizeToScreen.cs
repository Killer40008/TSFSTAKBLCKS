using UnityEngine;
using System.Collections;

public class ResizeToScreen : MonoBehaviour 
{

	// Use this for initialization
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale =new  Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x - 0.2f;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
    }
}
