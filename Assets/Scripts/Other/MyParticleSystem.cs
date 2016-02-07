using UnityEngine;
using System.Collections;

public class MyParticleSystem : MonoBehaviour 
{

    void Start()
    {
        StartCoroutine(Draw());
    }
    public void Stop()
    {
        StopAllCoroutines();
    }


    IEnumerator Draw()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.0f, 1.5f));
            GameObject snow = transform.FindChild("sprite").gameObject;

            float sRnd = Random.Range(0.6f, 0.9f);
            float xRnd = Random.Range(-SpawnManager.CameraWidth, SpawnManager.CameraWidth);
            GameObject gm = (GameObject)Instantiate(snow, new Vector3(xRnd, 9.2f, 0), Quaternion.identity);
            gm.transform.localScale = new Vector3(sRnd, sRnd, sRnd);
            StartCoroutine(Dispose(gm));
        }
    }

    IEnumerator Dispose(GameObject gameobj)
    {
        yield return new WaitForSeconds(60);
        Destroy(gameobj);
    }
}
