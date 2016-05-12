using UnityEngine;
using System.Collections;

public class ResizeToScreen : MonoBehaviour 
{
    public bool ResizeHeight = true;
    public bool ResizeWhenStart = false;

    void Start()
    {
        if (ResizeWhenStart == true)
            Resize();
    }

	// Use this for initialization
    public void Resize()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        float heightScale = transform.localScale.y;
        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x - 0.2f;
        float height = sr.sprite.bounds.size.y - 0.2f;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        if (ResizeHeight)
            transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
        else
            transform.localScale = new Vector3(worldScreenWidth / width, heightScale);


    }
}
