[System.Serializable]
public class CharacterSaveData
{
    public string characterName;
    public string className;

    public int maxHP;
    public int currentHP;
    public int attack;
    public int magic;
    public int armor;
    public int resistance;
    public int speed;
    public int armorPen;
    public int magicPen;
    public int crit;

    public int GetFinalStat(StatType stat)
    {
        int baseValue = stat switch
        {
            StatType.MaxHP => maxHP,
            StatType.Attack => attack,
            StatType.Magic => magic,
            StatType.Armor => armor,
            StatType.Resistance => resistance,
            StatType.Speed => speed,
            StatType.Crit => crit,
            StatType.ArmorPen => armorPen,
            StatType.MagicPen => magicPen,
            _ => 0
        };
        return baseValue + GetBonus(stat);
    }

    public int level;
    public int currentXP;
    public int xpToNextLevel;

    public ItemInstance equippedItem;

    public bool usesMagic;
    public int xpReward;

    public int GetBonus(StatType stat)
    {
        if (equippedItem == null) return 0;

        int total = 0;
        foreach (RolledModifier mod in equippedItem.rolledModifiers)
        {
            if (mod.stat == stat)
            {
                total += mod.value;
            }
        }
        return total;
    }




}
