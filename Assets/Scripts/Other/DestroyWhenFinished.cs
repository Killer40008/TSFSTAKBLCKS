using UnityEngine;
using System.Collections;
using System.Linq;

public class DestroyWhenFinished : MonoBehaviour
{
    private bool _localDestroyed = false;
    public GameObject tankSource;
    public bool AllowNextTurn = true;

    public void ExplosionAnimationFinished(int destroy)
    {
        if (!_localDestroyed)
        {
            if (destroy == 1) Destroy(this.gameObject);

            float bombCount = GameObject.FindGameObjectsWithTag("Bomb").Length;
            if (bombCount == 0 && tankSource != null)
            {
                Tank tf = tankSource.GetComponent<Tank>();
                if (tf.BurrellCount == 1 || Managers.DamageManager.GetHealth(Managers.TurnManager.CurrentTank) <= 0) //for double burrell
                {
                    if (AllowNextTurn)
                    {
                        Managers.TurnManager.SetTurnToNextTank();
                        if (Managers.TurnManager.CurrentTank != Managers.TurnManager.PlayerTank)
                        {
                            tf.BurrellCount = 2;
                        }
                        AllowNextTurn = false;
                    }
                }
                else if (tf.BurrellCount == 2 && Managers.TurnManager.CurrentTank == Managers.TurnManager.PlayerTank)
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
                else if (tf.BurrellCount == 2)
                {
                    tf.BurrellCount = 1;

                    Managers.TurnManager.CurrentTank.transform.FindChild("Burrell2").GetComponent<Burrell_Movement>().enabled = true;
                    Managers.TurnManager.CurrentTank.transform.FindChild("Burrell2").GetComponent<Tank_Fire>().enabled = true;
                    Managers.TurnManager.CurrentTank.transform.FindChild("Burrell").GetComponent<Burrell_Movement>().enabled = false;
                    tankSource.GetComponent<Tank_AI>().LastTankHit = null;

                    Managers.TurnManager.CurrentTank.GetComponent<Tank_AI>().AimBurrellToRandomTank(
                        Managers.TurnManager.tanks.Where(t => t != Managers.TurnManager.CurrentTank).ToArray());

                }


                GameObject.Find("Canvas").transform.FindChild("HUD").FindChild("DisabledPanel").GetComponent<CanvasGroup>().blocksRaycasts = false;
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
