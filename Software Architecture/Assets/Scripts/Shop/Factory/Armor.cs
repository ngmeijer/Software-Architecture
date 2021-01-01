using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    private string _name;
    private string _iconName;
    private int _price;
    private string _itemType = "Armor";
    private E_ItemRarity _itemRarity;

    private int _protection;
    private int[] upgradedProtectionValues = new int[5]
    {
        1,
        3,
        5,
        7,
        10
    };

    private string[] ItemNameArray = new string[5]
    {
        "Bad Armor",
        "Meh Armor",
        "Decent Armor",
        "Great Armor",
        "OP Armor"
    };

    public Armor(E_ItemRarity pItemRarity)
    {
        _itemRarity = pItemRarity;
        //_name = pName;
        //_iconName = pIconName;
        //_price = pPrice;

        generateItemDetails();
    }

    public override string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public override string ItemType
    {
        get { return _itemType; }
        set { _itemType = value; }
    }

    public override string IconName
    {
        get { return _iconName; }
        set { _iconName = value; }
    }

    public override int BasePrice
    {
        get { return _price; }
        set { _price = value; }
    }

    public override E_ItemRarity ItemRarity
    {
        get { return _itemRarity; }
        set { _itemRarity = value; }
    }

    public int Protection
    {
        get { return _protection; }
        set { _protection = value; }
    }

    public override void generateItemDetails()
    {
        switch (_itemRarity) 
        {
            case E_ItemRarity.COMMON:
                IconName = "items_103";
                Name = ItemNameArray[UnityEngine.Random.Range(0, ItemNameArray.Length)];
                break;
            case E_ItemRarity.UNCOMMON:
                IconName = "items_104";
                Name = ItemNameArray[UnityEngine.Random.Range(0, ItemNameArray.Length)];
                break;
            case E_ItemRarity.RARE:
                IconName = "items_105";
                Name = ItemNameArray[UnityEngine.Random.Range(0, ItemNameArray.Length)];
                break;
            case E_ItemRarity.EPIC:
                IconName = "items_106";
                Name = ItemNameArray[UnityEngine.Random.Range(0, ItemNameArray.Length)];
                break;
            case E_ItemRarity.LEGENDARY:
                IconName = "items_107";
                Name = ItemNameArray[UnityEngine.Random.Range(0, ItemNameArray.Length)];
                break;
        }
    }

    public int NewProtectionValue(E_ItemRarity newTier)
    {
        return upgradedProtectionValues[(int)newTier];
    }
}
