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
    public abstract Sprite ItemSprite { get; set; }

    public int ItemIndex = 0;

    protected int[] _prices = new int[5]
    {
        10,
        25,
        50,
        100,
        150
    };

    public abstract EItemRarity ItemRarity { get; set; }

    public abstract void GenerateItemDetails();

    protected string TakeElementFromArray(string[,] array, int index)
    {
        string value = "unassigned";
        float randomIndex = Random.Range(0, array.GetLength(1) - 1);
        value = array[index, (int)randomIndex];
        return value;
    }

    public bool CheckItemLevel()
    {
        bool isMaxLevel;
        isMaxLevel = ItemRarity.Equals(EItemRarity.LEGENDARY);

        return isMaxLevel;
    }

    public void UpgradeItem()
    {
        ItemRarity++;
        GenerateItemDetails();
    }
}