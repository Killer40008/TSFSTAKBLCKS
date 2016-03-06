using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Managers : MonoBehaviour 
{
    public Object FadePanel;
    public static readonly int TanksStaringCount = 4;
    public static TerrianManager TerrianManager;
    public static SpawnManager SpawnManager;
    public static TurnManager TurnManager;
    public static Cload_Movement Cloud;
    public static DamageManager DamageManager;
    public static ScoreManager ScoreManager;
    public static DestroyManager DestroyManager;
    public static WeaponsCombo WeaponManager;
    public static ModesCombo ModesManager;
    public static MapManager MapsManager;
    public static PlayerInfo PlayerInfos;
    public static Managers Me;

    void Start()
    {

        Me = this;
        TerrianManager = transform.FindChild("TerrainManager").GetComponent<TerrianManager>();
        SpawnManager = transform.FindChild("SpawnManager").GetComponent<SpawnManager>();
        TurnManager = transform.FindChild("TurnManager").GetComponent<TurnManager>();
        DamageManager = transform.FindChild("DamageAndScoreManager").GetComponent<DamageManager>();
        ScoreManager = transform.FindChild("DamageAndScoreManager").GetComponent<ScoreManager>();
        DestroyManager = transform.FindChild("DestroyManager").GetComponent<DestroyManager>();
        WeaponManager = transform.FindChild("WeaponsManager").GetComponent<WeaponsCombo>();
        ModesManager = transform.FindChild("ModesManager").GetComponent<ModesCombo>();
        MapsManager = transform.FindChild("MapManager").GetComponent<MapManager>();
        PlayerInfos = SpawnManager.GetComponent<PlayerInfo>();

        MapsManager.StartMap((MapManager.Maps) RoundManager.RandomMap);
        SpawnManager.Spawn(TanksStaringCount);
        TurnManager.Begin();
        Wind.StartWind();
        WeaponsClass.InitiallizeWeaponsQuantities();
        ModesClass.InitiallizeModesQuantities();



        PlayerInfos.DrawPlayerInfoInUI_SinglePlayer();



        //begin clouds
        GameObject.Find("Cloud").GetComponent<Cload_Movement>().Begin();
        GameObject.Find("Cloud (1)").GetComponent<Cload_Movement>().Begin();
        GameObject.Find("Cloud (2)").GetComponent<Cload_Movement>().Begin();

        //
        GameObject.Find("UIButtons").GetComponent<UIButtonClick>().InitilizeButtons();
    }


    public static void ShowSliders(bool active)
    {
        GameObject.Find("StrenghSlider").GetComponent<CanvasGroup>().alpha = System.Convert.ToInt32(active);
        GameObject.Find("StrenghSlider").GetComponent<CanvasGroup>().interactable = active;
        GameObject.Find("AngleSlider").GetComponent<CanvasGroup>().alpha = System.Convert.ToInt32(active);
        GameObject.Find("AngleSlider").GetComponent<CanvasGroup>().interactable = active;

        GameObject.Find("RightMovement").GetComponent<CanvasGroup>().alpha = System.Convert.ToInt32(active);
        GameObject.Find("RightMovement").GetComponent<CanvasGroup>().interactable = active;
        GameObject.Find("LeftMovement").GetComponent<CanvasGroup>().alpha = System.Convert.ToInt32(active);
        GameObject.Find("LeftMovement").GetComponent<CanvasGroup>().interactable = active;
    }

    public static void ResetTimescale()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public static void SlowDownTimescale(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
