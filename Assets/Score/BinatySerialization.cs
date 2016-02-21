using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class BinatySerialization 
{
    public static string SerializeToString(PlayerData data)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream();


        BinaryFormatter x = new BinaryFormatter();
        x.Serialize(ms, data);


        string str = System.Convert.ToBase64String(ms.ToArray());

        ms.Close();
        ms.Dispose();

        return str;

    }


    public static PlayerData DeserializeToObject(string data)
    {
        System.IO.MemoryStream reader = new System.IO.MemoryStream();
        byte[] orginData = System.Convert.FromBase64String(data);
        reader.Write(orginData,0,orginData.Length);
        reader.Seek(0, System.IO.SeekOrigin.Begin);

        BinaryFormatter x = new BinaryFormatter();
        PlayerData pd = (PlayerData)x.Deserialize(reader);

        reader.Close();
        reader.Dispose();
        return pd;
    }

}
