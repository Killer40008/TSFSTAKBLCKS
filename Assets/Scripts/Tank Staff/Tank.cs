using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{

    #region Color
    public Color OrginalColor { get; set; }
    public Color32 Color 
    {
        get { return GetComponent<SpriteRenderer>().color; }
        set
        {
            GetComponent<SpriteRenderer>().color = value;
            transform.FindChild("Burrell").FindChild("tank-01_up").GetComponent<SpriteRenderer>().color = value;
        }
    }
    #endregion

    public int Index;
    public float Health; //100.0 to 0.0
    public float Strength; //100 to 0.0;
    public float Oil;
    public bool CanDisabled;
    public bool ArmorActivate;
    public bool DoubleDamage;
    public Vector3 BurrellPosition;
    public Vector3 BurrellRotation;
    public int BurrellCount = 1;
    public bool DoubleBurrell;
    public bool IsAI;

    //player
    public string PlayerName;
    public int PlayerRank;
    public int PlayerMoney;



    #region Functions

    public void Active(bool active)
    {
        GetComponent<Tank_Movement>().enabled = active;
        transform.FindChild("Burrell").GetComponent<Burrell_Movement>().enabled = active;

    }

    public void CalculateHealthAboveTank()
    {
        float health = Managers.DamageManager.GetHealth(this.gameObject);
        Transform obj = transform.FindChild("Canvas").transform.FindChild("health").Find("Health");
        obj.GetComponent<Image>().fillAmount = health / 100;
        if (health <= 70 && health > 30)
        {
            obj.GetComponent<Image>().color = new Color(255, 255, 0);
        }
        else if (health <= 30)
        {
            obj.GetComponent<Image>().color = new Color(255, 0, 0);
        }
        else
        {
            obj.GetComponent<Image>().color = new Color(0, 255, 0);

        }
        if (this.gameObject.activeSelf)
        StartCoroutine(ShowHealth());
    }

    public IEnumerator ShowHealth()
    {
        float t = 0;
        while (t < 1)
        {
            transform.Find("Canvas").transform.FindChild("health").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 0.6f, t);
            t += 0.1f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2);
        t = 0;
        while (t <= 1)
        {
            transform.Find("Canvas").transform.FindChild("health").GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0.6f, 0, t);
            t += 0.1f;
            yield return new WaitForEndOfFrame();
        }
        transform.Find("Canvas").transform.FindChild("health").GetComponent<CanvasGroup>().alpha = 0;
    }

   
    #endregion
}


class ColorRandom
{
    static ArrayList listOFColor = new ArrayList();
    static Color32[] orderedColors;
    static ColorRandom()
    {
        listOFColor.Add(new Color32(0, 202, 255,255));
        listOFColor.Add(new Color32(255, 140, 140, 255));
        listOFColor.Add(new Color32(241, 202, 0, 255));
        listOFColor.Add(new Color32(9, 255, 0, 255));
        listOFColor.Add(new Color32(238, 141, 255, 255));
        orderedColors = listOFColor.Cast<Color32>().OrderBy(x => Random.Range(0, 100)).ToArray();
    }

    public static Color GetRandomColors(int counter)
    {

        return orderedColors[counter];
  
    }
}
