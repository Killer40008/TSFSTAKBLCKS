using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour 
{
    public Sprite RankSprite;
    public Sprite AISprite;

    public void DrawPlayerInfoInUI_SinglePlayer()
    {
        //player
        Tank tData = Managers.TurnManager.PlayerTank.GetComponent<Tank>();
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerOne").transform, tData.PlayerName, tData.PlayerRank, false);

        //computers
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerTwo").transform, "Comp 1", 0, true);
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerThree").transform, "Comp 2", 0, true);
        SetNameAndRank(GameObject.Find("PlayersInfo").transform.Find("PlayerFour").transform, "Comp 3", 0, true);

    }


    private void SetNameAndRank(Transform objParent, string name, int rank, bool isAI)
    {
        objParent.Find("Text").GetComponent<Text>().text = name;

        if (isAI)
        {
            objParent.Find("Image").GetComponent<Image>().sprite = AISprite;
            objParent.Find("Image").transform.localScale = new Vector3(0.171137f, 0.4498451f, 0.1711369f);
            objParent.Find("Image").Find("Text").GetComponent<Text>().text = "";
        }
        else
        {
            objParent.Find("Image").GetComponent<Image>().sprite = RankSprite;
            ConfigRankText(objParent.Find("Image").Find("Text").gameObject, rank);
            ConfigRankImage(objParent.Find("Image").gameObject, rank);
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

    private  void ConfigRankImage(GameObject imgObject, int rank)
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
