using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemInstance
{
    public string itemName;
    public List<RolledModifier> rolledModifiers = new List<RolledModifier>();
}

[System.Serializable]
public class RolledModifier
{
    public StatType stat;
    public int value;
}
