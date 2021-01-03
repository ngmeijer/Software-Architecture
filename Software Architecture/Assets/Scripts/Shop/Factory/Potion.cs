using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    private string _name;
    private string _description;
    private string _iconName;
    private int _price;
    private string _itemType = "Potion";

    public E_ItemRarity _itemRarity;
    public string PotionEffect { get; set; }
    public int PotionEffectAmount { get; set; }

    private int[] upgradedEffectValues = new int[5]
    {
        1,
        3,
        5,
        7,
        10
    };

    public Potion(string pName, string pIconName, int pPrice)
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
    public override string Description
    {
        get { return _description; }
        set { _description = value; }
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

    public override E_ItemRarity ItemRarity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //public override E_ItemRarity ItemRarity
    //{
    //    get { return _itemRarity; }
    //    set { _itemRarity = value; }
    //}

    public int newEffectValue(E_ItemRarity newTier)
    {
        return upgradedEffectValues[(int)newTier];
    }

    public override void generateItemDetails()
    {
        throw new NotImplementedException();
    }
}