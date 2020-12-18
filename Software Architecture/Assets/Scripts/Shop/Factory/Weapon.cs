using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    private string _name;
    private string _iconName;
    private int _price;
    private int _itemType = 0;

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
}
