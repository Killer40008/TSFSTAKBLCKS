using UnityEngine;
using System.Collections;

public class DestroyExplosionEffect : MonoBehaviour
{


    public void EffectAnimationFinished(int value)
    {
        Destroy(this.gameObject);
    }
}
