using UnityEngine;
using System.Collections;
using System.Linq;

public class DestroyWhenFinished : MonoBehaviour
{
    private bool _localDestroyed = false;
    public GameObject tankSource;
    public static bool AllowNextTurn = true;

    public void ExplosionAnimationFinished(int destroy)
    {
        if (!_localDestroyed)
        {
            if (destroy == 1) Destroy(this.gameObject);

                _localDestroyed = true;
            
        }
    }

    public void SelectorAnimationFinished()
    {
        Destroy(this.transform.parent.gameObject);
    }
    public void DestroyAnimationFinished(int nothing)
    {
            Destroy(this.transform.gameObject);
    }
}
