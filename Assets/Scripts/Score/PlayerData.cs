using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData
{
    public string PlayerName { get; set; }
    public int PlayerRank { get; set; }
    public int PlayerMoney { get; set; }
    public int PlayerPoints { get; set; }
    public Dictionary<Weapons, int> PlayerWeapons { get; set; }
    public Dictionary<ModesClass.Modes, int> PlayerModes { get; set; }
}
