using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    private string _name;
    private string _iconName;
    private int _price;
    private int _itemType = 1;
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

    public Armor(string pName, string pIconName, int pPrice)
    {
        _name = pName;
        _iconName = pIconName;
        _price = pPrice;
    }

    public override string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public override int ItemType
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

    public E_ItemRarity ItemRarity
    {
        get { return _itemRarity; }
        set { _itemRarity = value; }
    }

    public int Protection
    {
        get { return _protection; }
        set { _protection = value; }
    }

    public int NewProtectionValue(E_ItemRarity newTier)
    {
        return upgradedProtectionValues[(int)newTier];
    }
}
