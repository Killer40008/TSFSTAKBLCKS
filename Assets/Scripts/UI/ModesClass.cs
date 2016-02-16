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

    public static System.Collections.Generic.Dictionary<Modes, int> ModesQuantities = new System.Collections.Generic.Dictionary<Modes, int>();



    public static void InitiallizeWeaponsQuantities()
    {
        ModesQuantities.Add(Modes.AntiStrike, 3);
        ModesQuantities.Add(Modes.Double_Burrell, 1);
        ModesQuantities.Add(Modes.Double_Damage, 1);
        ModesQuantities.Add(Modes.HealthPlus, 1);
        ModesQuantities.Add(Modes.Mini_Armor, 1);
        ModesQuantities.Add(Modes.Normal_Armor, 1);
        ModesQuantities.Add(Modes.OilPlus, 1);
        ModesQuantities.Add(Modes.StrengthPlus, 1);
        ModesQuantities.Add(Modes.Strong_Armor, 1);
        ModesQuantities.Add(Modes.Super_Armor, 1);
    }
}
