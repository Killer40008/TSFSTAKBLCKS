using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Specialized;

public class RoundScore : MonoBehaviour 
{
    public static OrderedDictionary Name_Money = new OrderedDictionary();

    void Start()
    {
        StartCoroutine(PrintScores());
    }

    private IEnumerator PrintScores()
    {
        var sortedDictionary = Name_Money.Cast<DictionaryEntry>()
                       .OrderBy(r => r.Value)
                       .ToDictionary(c => c.Key, d => d.Value);
       
        yield return new WaitForSeconds(1);
        GameObject pScore = GameObject.Find("PlayerScore");
        GameObject pName = GameObject.Find("PlayerNames");
        for (int i = 0, j = pScore.transform.childCount - 1; i < pScore.transform.childCount && j >= 0; i++, j--)
        {
            pName.transform.GetChild(j).GetComponent<Text>().text = (string)sortedDictionary.Keys.ToArray()[i];
            StartCoroutine(PrintAsCounter(pScore.transform.GetChild(j).GetComponent<Text>(), (int)sortedDictionary.Values.ToArray()[i]));
        }
    }

    IEnumerator PrintAsCounter(Text obj, int money)
    {
        int local = 0;


        while (local < money)
        {
            yield return new WaitForSeconds(0.0005f);
            int current = int.Parse(obj.text);
            obj.text = (current+=5).ToString();
            local = current;
        }
        obj.text = money.ToString();
    }
}
