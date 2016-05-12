using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class PlayerInfoRoundPanel : MonoBehaviour 
{

    public Sprite RankSprite;
    public Sprite AISprite;

    void Start()
    {


        var sortedDictionary = RoundScore.Name_Money.Cast<DictionaryEntry>()
           .OrderByDescending(x => x.Value)
           .ToDictionary(c => c.Key, d => d.Value);

        GameObject pRank = GameObject.Find("PlayerRanks");
        for (int i = 0; i < pRank.transform.childCount; i++)
        {
            var key = (string)sortedDictionary.Keys.ToArray()[i];
            if (key == ScoreModule.GetPlayerData().PlayerName)
            {
                SetNameAndRank(pRank.transform.GetChild(i), ScoreModule.GetPlayerData().PlayerName, ScoreModule.GetPlayerData().PlayerRank, false);
            }
            else
            {
                SetNameAndRank(pRank.transform.GetChild(i), "Comp 1", 0, true);
                SetNameAndRank(pRank.transform.GetChild(i), "Comp 2", 0, true);
                SetNameAndRank(pRank.transform.GetChild(i), "Comp 3", 0, true);
            }
        }
    }


    public void DrawPlayerInfoInUI_SinglePlayer()
    {
        //player
        Tank tData = Managers.TurnManager.PlayerTank.GetComponent<Tank>();
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerOne").transform, tData.PlayerName, tData.PlayerRank, false);

        //computers
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerTwo").transform, "Comp 1", 0, true);
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerThree").transform, "Comp 2", 0, true);
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerFour").transform, "Comp 3", 0, true);
        for (int i = 0; i < Managers.TurnManager.tanks.Count; i++)
        {
            if (Managers.TurnManager.tanks[i] != Managers.TurnManager.PlayerTank)
                Managers.TurnManager.tanks[i].GetComponent<Tank>().PlayerName = "Comp " + (i + 1);
        }
    }

    public void AddMoneyToPlayer(GameObject tank, int money)
    {
        int total = tank.GetComponent<Tank>().PlayerMoney += money;
        if (tank == Managers.TurnManager.PlayerTank)
        {
            GameObject.Find("HUDMasterButtons").transform.Find("ScorePanel").transform.Find("Text").GetComponent<Text>().text = total.ToString();
        }
    }


    public void SetPlayerMoneyText(int money)
    {
        GameObject.Find("HUDMasterButtons").transform.Find("ScorePanel").transform.Find("Text").GetComponent<Text>().text = money.ToString();
    }
    public int GetPlayerMoney()
    {
        return int.Parse(GameObject.Find("HUDMasterButtons").transform.Find("ScorePanel").transform.Find("Text").GetComponent<Text>().text);
    }


    private void SetNameAndRank(Transform objParent, string name, int rank, bool isAI)
    {
        //objParent.Find("Text").GetComponent<Text>().text = name;

        if (isAI)
        {
            objParent.GetComponent<Image>().sprite = AISprite;
            objParent.transform.localScale = new Vector3(0.6f, 0.8f, 0.1711369f);
            objParent.Find("Text").GetComponent<Text>().text = "";
        }
        else
        {
            objParent.GetComponent<Image>().sprite = RankSprite;
            ConfigRankText(objParent.Find("Text").gameObject, rank);
            ConfigRankImage(objParent.gameObject, rank);
        }
    }

    private void ConfigRankText(GameObject gameObject, int rank)
    {
        if (rank < 10)
        {
            gameObject.GetComponent<Text>().text = rank.ToString();
        }
        else if (rank < 20)
        {
            gameObject.GetComponent<Text>().text = Mathf.Abs(10 - rank).ToString();
        }
    }

    private void ConfigRankImage(GameObject imgObject, int rank)
    {
        if (rank < 10)
        {
            imgObject.GetComponent<Image>().color = new Color32(221, 221, 221, 255);
        }
        else if (rank < 20)
        {
            imgObject.GetComponent<Image>().color = new Color32(255, 248, 0, 255);
        }
    }
}
