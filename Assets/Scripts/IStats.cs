using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    public IStats ModifyStats(List<Stat> Stats);
}

public struct Stat
{
    public StatType StatType;
    public int Amount;
}

public enum StatType
{
    Health,
    Strength,
    Endurance,
    Dexterity,
}
