using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoundManager : MonoBehaviour
{
    public static int CurrentRound = 0;
    public static int RandomMap;

    private static Queue mQueue = new Queue();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //
        int[] mArray = new int[] { 0, 1, 2 };
        mArray = mArray.OrderBy(x => Random.Range(0, 100)).ToArray();
        foreach (int map in mArray)
            mQueue.Enqueue(map);
    }

    public static bool StartNextRound()
    {
        if (CurrentRound < 3)
        {
            RandomMap = (int)mQueue.Dequeue();
            CurrentRound++;
            SceneManager.LoadSceneAsync("Game");

            return true;
        }
        else
        {
            CurrentRound = 1;
            mQueue.Clear();
            return false;
        }

    }

    public static bool ShowCounter;
    public static void OpenBetweenRoandStore()
    {
        SceneManager.LoadScene("Store");
        ShowCounter = true;
    }

    public static void OpenNormalStore()
    {
        SceneManager.LoadScene("Store");
        ShowCounter = false;
    }


}
