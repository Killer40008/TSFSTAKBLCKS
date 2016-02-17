using UnityEngine;
using System.Collections;

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

            float bombCount = GameObject.FindGameObjectsWithTag("Bomb").Length;
            if (bombCount == 0 && tankSource != null)
            {
                Tank_Fire tf = tankSource.transform.FindChild("Burrell").GetComponent<Tank_Fire>();
                if (tf.FireCount == 1 || Managers.DamageManager.GetHealth(Managers.TurnManager.CurrentTank) <= 0) //for double burrell
                {
                    tf.FireCount = 1;
                    if (AllowNextTurn)
                        Managers.TurnManager.SetTurnToNextTank();

                    AllowNextTurn = false;
                }
                else if (tf.FireCount == 2)
                {
                    //double burrell
                    if (Managers.WeaponManager.lastButton != null)
                        Managers.WeaponManager.OnWeaponsSelected(Managers.WeaponManager.lastButton);

                    GameObject.Find("PlayerTimer").GetComponent<Timer>().StartTimer();

                    tankSource.transform.FindChild("Burrell2").GetComponent<Burrell_Movement>().enabled = true;
                    tankSource.transform.FindChild("Burrell2").GetComponent<Tank_Fire>().enabled = true;
                    tankSource.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().enabled = false;
                    NotifyMessage.ShowMessage("Second Burrell Activated!", 2);
                }
                _localDestroyed = true;
            }
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
