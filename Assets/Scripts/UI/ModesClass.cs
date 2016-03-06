using UnityEngine;
using System.Collections;

public class ModesClass : MonoBehaviour 
{
    public enum Modes
    {
        Double_Damage,
        Double_Burrell,
        Mini_Armor,
        Normal_Armor,
        Strong_Armor,
        Super_Armor,
        HealthPlus,
        StrengthPlus,
        OilPlus,
        AntiStrike,
    }

    public static System.Collections.Generic.Dictionary<Modes, int> ModesQuantities;



    public static System.Collections.Generic.Dictionary<Modes, int> InitiallizeModesQuantities(bool setDefault = false)
    {
        ModesQuantities = new System.Collections.Generic.Dictionary<Modes, int>();

        if (ScoreModule.GetPlayerData() != null &&  setDefault == false)
        {
            ModesQuantities = ScoreModule.GetPlayerData().PlayerModes;
        }
        else
        {
            ModesQuantities.Clear();
            ModesQuantities.Add(Modes.AntiStrike, 3);
            ModesQuantities.Add(Modes.Double_Burrell, 0);
            ModesQuantities.Add(Modes.Double_Damage, 0);
            ModesQuantities.Add(Modes.HealthPlus, 5);
            ModesQuantities.Add(Modes.Mini_Armor, 0);
            ModesQuantities.Add(Modes.Normal_Armor, 0);
            ModesQuantities.Add(Modes.OilPlus, 1);
            ModesQuantities.Add(Modes.StrengthPlus, 5);
            ModesQuantities.Add(Modes.Strong_Armor, 0);
            ModesQuantities.Add(Modes.Super_Armor, 0);
        }

        return ModesQuantities;
    }

    public static void SubtractModeQuantitie(Modes  mode)
    {
        ModesQuantities[mode]--;
        PlayerData dt = ScoreModule.GetPlayerData();
        if (dt != null)
        {
            dt.PlayerModes = ModesQuantities;
            ScoreModule.SavePlayerData(dt);
        }
    }
}
