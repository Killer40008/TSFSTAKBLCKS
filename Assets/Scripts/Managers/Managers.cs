using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Managers : MonoBehaviour 
{
    public static readonly int TanksStaringCount = 4;
    public static TerrianManager TerrianManager;
    public static SpawnManager SpawnManager;
    public static TurnManager TurnManager;
    public static Cload_Movement Cloud;
    public static DamageManager DamageManager;
    public static ScoreManager ScoreManager;
    public static DestroyManager DestroyManager;
    public static WeaponsCombo WeaponManager;
    public static MapManager MapsManager;

    void Start()
    {
        TerrianManager = transform.FindChild("TerrainManager").GetComponent<TerrianManager>();
        SpawnManager = transform.FindChild("SpawnManager").GetComponent<SpawnManager>();
        TurnManager = transform.FindChild("TurnManager").GetComponent<TurnManager>();
        DamageManager = transform.FindChild("DamageAndScoreManager").GetComponent<DamageManager>();
        ScoreManager = transform.FindChild("DamageAndScoreManager").GetComponent<ScoreManager>();
        DestroyManager = transform.FindChild("DestroyManager").GetComponent<DestroyManager>();
        WeaponManager = transform.FindChild("WeaponsManager").GetComponent<WeaponsCombo>();
        MapsManager = transform.FindChild("MapManager").GetComponent<MapManager>();

        MapsManager.StartMap(MapManager.Maps.Forest);
        SpawnManager.Spawn(TanksStaringCount, "", 1, 2);
        TurnManager.Begin();
        Wind.StartWind();
        WeaponsClass.InitiallizeWeaponsQuantities();

        GameObject.Find("Cloud").GetComponent<Cload_Movement>().Begin();
        GameObject.Find("Cloud (1)").GetComponent<Cload_Movement>().Begin();
        GameObject.Find("Cloud (2)").GetComponent<Cload_Movement>().Begin();
    }
}
