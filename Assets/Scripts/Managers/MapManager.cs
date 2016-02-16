using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
public class MapManager : MonoBehaviour
{
    public enum Maps { Volcano = 0, Forest = 1, Ice = 2 }
    public Maps CurrentMap;
    //
    public Sprite Background_Volcano;
    public Sprite Background_Forest;
    public Sprite Background_Ice;


    public void StartMap(Maps map)
    {
        CurrentMap = map;
        switch (map)
        {
            case Maps.Volcano:
                GameObject.Find("BackGround").GetComponent<SpriteRenderer>().sprite = Background_Volcano;
                Managers.TerrianManager.Create();
                break;
            case Maps.Forest:
                GameObject.Find("BackGround").GetComponent<SpriteRenderer>().sprite = Background_Forest;
                transform.FindChild("Forest").gameObject.SetActive(true);
                float[] xPos = GetChilds(transform.FindChild("Forest"), "Pistons").Select(g => g.transform.position.x).ToArray();
                for (int i = 0; i < xPos.Length; i++)
                    xPos[i] -= 0.15f;
                Managers.SpawnManager.CustomPositionX.AddRange(xPos);
                break;
            case Maps.Ice:
                GameObject.Find("BackGround").GetComponent<SpriteRenderer>().sprite = Background_Ice;
                transform.FindChild("Ice").gameObject.SetActive(true);
                break;
        }

        GameObject.Find("BackGround").GetComponent<ResizeToScreen>().Resize();
    }


    private GameObject[] GetChilds(Transform parent,string tag)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.tag == tag)
                list.Add(parent.GetChild(i).gameObject);
        }
        return list.ToArray();
    }
}
