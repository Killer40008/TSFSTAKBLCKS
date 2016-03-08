using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class DestroyManager : MonoBehaviour
{

    public Object DestroyEffect1;
    public bool Win = false;

    public bool CheckAndDestroy(GameObject tank)
    {
       float health = Managers.DamageManager.GetHealth(tank);
       if (health <= 0)
       {
           //destroy
           DestroyEffect(tank);
           tank.SetActive(false);

           //check if there we have a winner
           CheckForWinner();
           return true;
       }
       return false;
    }

    private void DestroyEffect(GameObject tank)
    {
        Instantiate(DestroyEffect1, tank.transform.position, Quaternion.identity);
    }

    private void CheckForWinner()
    {
        if (Managers.TurnManager.tanks.Count(t => t.activeSelf == true) == 1)
        {
            //winner
            Managers.Me.StopAllCoroutines();
            foreach (GameObject gm in GameObject.FindGameObjectsWithTag("Bomb"))
                Destroy(gm);

            //set score
            SetScoreToRoundScene();
            RoundScore.RoundsMoney += Managers.PlayerInfos.GetPlayerMoney();

            //update playerscore
            PlayerData dt = ScoreModule.GetPlayerData();
            dt.PlayerMoney += Managers.TurnManager.PlayerTank.GetComponent<Tank>().PlayerMoney;
            ScoreModule.SavePlayerData(dt);

            //show win panel
            Fade fp = Fade.CreateFade().GetComponent<Fade>();
            fp.FadeIn();
            fp.ShowWinPanel();
            StartCoroutine(ShowRoundScene());
            
            Win = true;
        }
    }


    void SetScoreToRoundScene()
    {
        foreach (GameObject gm in Managers.TurnManager.tanks)
        {
            string name = gm.GetComponent<Tank>().PlayerName;
            int money = gm.GetComponent<Tank>().PlayerMoney;

            if (RoundManager.CurrentRound == 1)
            {
                RoundScore.Name_Money.Add(name, money);
            }
            else
            {
                RoundScore.Name_Money[name] = (int)RoundScore.Name_Money[name] + money;
            }
        }

    }

    IEnumerator ShowRoundScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("Round");    
    }

}
