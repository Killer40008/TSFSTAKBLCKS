using UnityEngine;
using System.Collections;

public static class ScoreModule 
{

    const string DATA_KEY = "Data";

    public static PlayerData CreateNewPlayerData()
    {
        PlayerData pd = new PlayerData();

        string data = XmlSerialization.SerializeToString(pd);
        PlayerPrefs.SetString(DATA_KEY, data);
        return pd;
    }

    public static bool IsPlayerDataExist()
    {
        if (PlayerPrefs.HasKey(DATA_KEY))
            return true;
        else 
           return false;
    }

    public static PlayerData LoadPlayerData()
    {
        if (IsPlayerDataExist())
        {
            string data = PlayerPrefs.GetString(DATA_KEY);
            PlayerData pd = XmlSerialization.DeserializeToObject(data);

            return pd;
        }
        else
            return null;
    }


    //

}