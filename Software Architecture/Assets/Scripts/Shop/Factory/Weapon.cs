using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    private string _name;
    private string _iconName;
    private int _price;
    private string _itemType = "Weapon";

    public int Damage { get; set; }
    public E_ItemRarity _itemRarity { get; set; } = 0;
    public int AttackSpeed { get; set; }

    private int[] upgradedDamageValues = new int[5]
    {
        1,
        3,
        5,
        7,
        10
    };

    private int[] upgradedAttackSpeedValues = new int[5]
    {
        1,
        3,
        5,
        7,
        10
    };

    public Weapon(string pName, string pIconName, int pPrice)
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


    public int newDamageValue(E_ItemRarity newTier)
    {
        return upgradedDamageValues[(int)newTier];
    }

    public int newAttackSpeedValue(E_ItemRarity newTier)
    {
        return upgradedAttackSpeedValues[(int)newTier];
    }

    public override void generateItemDetails()
    {
        //eehhhh
        _itemRarity = (E_ItemRarity)UnityEngine.Random.Range((float)E_ItemRarity.COMMON, (float)E_ItemRarity.LEGENDARY);
    }
}
