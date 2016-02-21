using UnityEngine;
using System.Collections;

public class XmlSerialization 
{
    public static string SerializeToString(PlayerData data)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream();


        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(data.GetType());
        x.Serialize(ms, data);

        using (System.IO.StreamReader reader = new System.IO.StreamReader(ms))
        {
            string str = reader.ReadToEnd();
            reader.Close();
            ms.Close();
            ms.Dispose();

            return str;
        }
    }


    public static PlayerData DeserializeToObject(string data)
    {
        System.IO.StringReader reader = new System.IO.StringReader(data);

        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(data.GetType());
        PlayerData pd = (PlayerData)x.Deserialize(reader);


        return pd;
    }

}
