using UnityEngine;

public abstract class Item
{
    public abstract string Name { get; set; }
    public abstract string Description { get; set; }
    public abstract string ItemType { get; set; }
    public abstract string IconName { get; set; }
    public abstract int BasePrice { get; set; }

    public abstract string BaseEnchantmentText { get; set; }
    public abstract int BaseEnchantmentValue { get; set; }

    public int ItemIndex = 0;

    public abstract EItemRarity ItemRarity { get; set; }

    public abstract void GenerateItemDetails();

    public abstract Sprite ItemSprite { get; set; }
}