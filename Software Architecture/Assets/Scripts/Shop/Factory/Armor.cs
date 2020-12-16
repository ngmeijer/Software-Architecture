using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    private string _name;
    private string _iconName;
    private int _price;

    public Armor(string pName, string pIconName, int pPrice)
    {
        Debug.Log($"Created{pName}");

        _name = pName;
        _iconName = pIconName;
        _price = pPrice;
    }

    public override string Name
    {
        get { return _name; }
        set { _name = value; }
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
