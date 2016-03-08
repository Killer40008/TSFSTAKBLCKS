﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Specialized;

public class RoundScore : MonoBehaviour 
{
    public static OrderedDictionary Name_Money = new OrderedDictionary();
    public static int RoundsMoney = 0;
    private int ScoreFinished = 0;

    void Start()
    {
        //set text
        GameObject.Find("RoundNumber").GetComponent<Text>().text = RoundManager.CurrentRound.ToString();
        //
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
            CheckForWallet((string)sortedDictionary.Keys.ToArray()[i], j,(int)sortedDictionary.Values.ToArray()[i]);
        }
        yield return StartCoroutine(WaitForScoreCounters());

        yield return new WaitForSeconds(1);
        StartCoroutine(SetRealWalletValue());

        yield return new WaitForSeconds(3);
        //
        RoundManager.StartNextRound();
    }

    private IEnumerator SetRealWalletValue()
    {

        Color orgin = GameObject.Find("WalletValue").GetComponent<Text>().color;
        float t = 0;
        while (t < 1)
        {
            yield return new WaitForFixedUpdate();
            GameObject.Find("WalletValue").GetComponent<Text>().color = Color32.Lerp(orgin, new Color32(255,255,255,10), t);
            t += 0.1f;
        }

        GameObject.Find("WalletValue").GetComponent<Text>().text = ScoreModule.GetPlayerData().PlayerMoney.ToString();

        t = 0;
        while (t < 1)
        {
            yield return new WaitForFixedUpdate();
            GameObject.Find("WalletValue").GetComponent<Text>().color = Color32.Lerp(new Color32(255, 255, 255, 10), orgin, t);
            t += 0.05f;
        }


    }

    private void CheckForWallet(string currentKey, int counter, int money)
    {
        if (currentKey == ScoreModule.GetPlayerData().PlayerName)
        {
            GameObject wallet = GameObject.Find("Wallet");
            wallet.transform.localPosition = new Vector3(wallet.transform.localPosition.x, 0, wallet.transform.localPosition.z);
            wallet.transform.localPosition = new Vector3(wallet.transform.localPosition.x, wallet.transform.localPosition.y - 80 * counter, wallet.transform.localPosition.z);
            wallet.transform.Find("WalletValue").GetComponent<Text>().text = (ScoreModule.GetPlayerData().PlayerMoney - money).ToString();
        }
    }

    private IEnumerator WaitForScoreCounters()
    {
        while (ScoreFinished < 4)
            yield return new WaitForEndOfFrame();
    }

    IEnumerator PrintAsCounter(Text obj, int money)
    {
        int current = int.Parse(obj.text);
        int local = current;


        while (local < money)
        {
            yield return new WaitForSeconds(0.0005f);
            obj.text = (current+=5).ToString();
            local = current;
        }
        obj.text = money.ToString();
        ScoreFinished++;
    }
}
