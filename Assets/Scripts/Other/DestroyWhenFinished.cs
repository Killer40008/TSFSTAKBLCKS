using UnityEngine;
using System.Collections;

public class DestroyWhenFinished : MonoBehaviour
{
    private bool _localDestroyed = false;

    public void ExplosionAnimationFinished(int nothing)
    {
        if (!_localDestroyed)
        {
            Destroy(this.gameObject);
            Managers.TurnManager.SetTurnToNextTank();
            _localDestroyed = true;
        }
    }

    public void SelectorAnimationFinished()
    {
        Destroy(this.transform.parent.gameObject);
    }
    public void DestroyAnimationFinished()
    {
        Destroy(this.transform.gameObject);
    }
}
