public abstract class Item
{
    public abstract string Name { get; set; }
    public abstract int ItemType { get; set; }
    public abstract string IconName { get; set; }
    public abstract int BasePrice { get; set; }

    public enum E_ItemRarity { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
}