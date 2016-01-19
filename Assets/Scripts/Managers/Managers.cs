using UnityEngine;
using System.Collections;

public class Managers : MonoBehaviour 
{
    public static int TanksCount = 4;
    public static TerrianManager TerrianManager;
    public static SpawnManager SpawnManager;
    public static TurnManager TurnManager;
    public static Cload_Movement Cloud;

    void Start()
    {
        TerrianManager = transform.FindChild("TerrainManager").GetComponent<TerrianManager>();
        SpawnManager = transform.FindChild("SpawnManager").GetComponent<SpawnManager>();
        TurnManager = transform.FindChild("TurnManager").GetComponent<TurnManager>();
        Cloud = GameObject.Find("Cloud").GetComponent<Cload_Movement>();

        TerrianManager.Create();
        SpawnManager.Spawn(TanksCount, "", 1, 2);
        TurnManager.Begin();
        Wind.StartWind();
        Cloud.Begin();
    }
}
