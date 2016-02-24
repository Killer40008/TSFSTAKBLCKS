using UnityEngine;
using System.Collections;

public static class ScoreModule 
{

    const string DATA_KEY = "Data";

    public static PlayerData CreateNewPlayerData()
    {
        PlayerData pd = new PlayerData();

        string data = BinatySerialization.SerializeToString(pd);
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

    public static void SavePlayerData(PlayerData data)
    {
        string datastr = BinatySerialization.SerializeToString(data);
        PlayerPrefs.SetString(DATA_KEY, datastr);
    }

    public static PlayerData GetPlayerData()
    {
        if (IsPlayerDataExist())
        {
            string data = PlayerPrefs.GetString(DATA_KEY);
            PlayerData pd = BinatySerialization.DeserializeToObject(data);

            return pd;
        }
        else
            return null;
    }


    //
      
}