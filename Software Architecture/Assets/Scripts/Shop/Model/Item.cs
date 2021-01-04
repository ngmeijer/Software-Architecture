using UnityEngine;

public abstract class Item : ScriptableObject
{
    public abstract string Name { get; set; }
    public abstract string Description { get; set; }
    public abstract string ItemType { get; set; }
    public abstract string IconName { get; set; }
    public abstract int BasePrice { get; set; }

    public abstract string BaseEnchantmentText { get; set; }
    public abstract int BaseEnchantmentValue { get; set; }

    public int ItemIndex = 0;

    public enum E_ItemRarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }

    public abstract  E_ItemRarity ItemRarity { get; set; }

    public abstract void generateItemDetails();
}