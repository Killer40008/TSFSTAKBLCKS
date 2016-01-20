using UnityEngine;
using System.Collections;
using System.Linq;

public class Tank : MonoBehaviour
{
    public Color32 Color 
    {
        get { return GetComponent<SpriteRenderer>().color; }
        set
        {
            GetComponent<SpriteRenderer>().color = value;
            transform.FindChild("Burrell").FindChild("tank-01_up").GetComponent<SpriteRenderer>().color = value;
        }
    }
    public float Health; //100.0 to 0.0
    public float Power; //0 to 100;
    public bool CanDisabled;
    public Vector3 BurrellPosition;
    public Vector3 BurrellRotation;
    public Sprite[] BombSprites;
    public Object[] BombExplosions;


    //player
    public string PlayerName;
    public int PlayerRank;
    public int PlayerScore;

    public void Active(bool active)
    {
        GetComponent<Tank_Movement>().enabled = active;
        transform.FindChild("Burrell").GetComponent<Burrell_Movement>().enabled = active;

    }
}


class ColorRandom
{
    static ArrayList listOFColor = new ArrayList();
    static Color32[] orderedColors;
    static ColorRandom()
    {
        listOFColor.Add(new Color32(0, 202, 255,255));
        listOFColor.Add(new Color32(255, 0, 0, 255));
        listOFColor.Add(new Color32(241, 202, 0, 255));
        listOFColor.Add(new Color32(9, 255, 0, 255));
        listOFColor.Add(new Color32(0, 44, 255, 255));
        listOFColor.Add(new Color32(218, 0, 255, 255));
        orderedColors = listOFColor.Cast<Color32>().OrderBy(x => Random.Range(0, 100)).ToArray();
    }

    public static Color GetRandomColors(int counter)
    {

        return orderedColors[counter];
  
    }
}
