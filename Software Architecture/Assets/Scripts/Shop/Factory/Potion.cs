using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    private string _name;
    private string _iconName;
    private int _price;
    private int _itemType = 2;
    private E_ItemRarity _itemRarity;

    private string _potionEffect;
    private int _potionEffectAmount;

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

    public string PotionEffect
    {
        get { return _potionEffect; }
        set { _potionEffect = value; }
    }

    public int PotionEffectAmount
    {
        get { return _potionEffectAmount; }
        set { _potionEffectAmount = value; }
    }

    public int newEffectValue(E_ItemRarity newTier)
    {
        return upgradedEffectValues[(int)newTier];
    }
}