using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.UI;
using GooglePlayGames.BasicApi;

public class ConnectionTest : MonoBehaviour, RealTimeMultiplayerListener
{
   
    // Use this for initialization
    void Start()
    {
        PlayGamesPlatform.Activate();


        Social.localUser.Authenticate(Successed);
    }

    void Successed(bool success)
    {
        if (success)
        {
            GameObject.Find("egewgeg").GetComponent<Text>().text = "Connected";
            const int MinOpponents = 1, MaxOpponents = 3;
            const int GameVariant = 0;
            PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents,
                        GameVariant, this);

        }
        else
            GameObject.Find("egewgeg").GetComponent<Text>().text = "Not Connected";
    }

    public void OnRoomSetupProgress(float percent)
    {
        GameObject.Find("egewgeg").GetComponent<Text>().text = percent.ToString();
    }

    public void OnRoomConnected(bool success)
    {
        
    }

    public void OnLeftRoom()
    {
        
    }

    public void OnParticipantLeft(Participant participant)
    {
        
    }

    public void OnPeersConnected(string[] participantIds)
    {
        GameObject.Find("egewgeg").GetComponent<Text>().text = "peered";
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        
    }
}
