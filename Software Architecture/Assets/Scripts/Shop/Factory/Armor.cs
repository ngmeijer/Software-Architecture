using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

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

    private readonly string[,] itemNameArrays = new string[5, 2]
    {
        //Common
        {
            "Primal Robes of Blood",
            "Silk Vest of Hallowed Hell"
        },
        
        //Uncommon
        {
            "Guardian's Embroided Vestment",
            "Demonic Tunic of Terrors"
        },

        //Rare
        {
            "Wretched Breastplate of Valor",
            "Storm-Forged Mail Vest"
        },

        //Epic
        {
            "Greatplate of the Volcano",
            "Adamantite Armor of Blind Punishment"
        },

        //Legendary
        {
            "Batteplate of Timeless Kings",
            "Mithril Armor of Imminent Hope"
        }
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
        Random r = new Random();
        switch (_itemRarity)
        {
            case E_ItemRarity.COMMON:
                IconName = "items_103";
                Name = itemNameArrays[0, r.Next(itemNameArrays.GetLength(1))];
                BasePrice = 10;
                break;
            case E_ItemRarity.UNCOMMON:
                IconName = "items_104";
                Name = itemNameArrays[1, r.Next(itemNameArrays.GetLength(1))];
                BasePrice = 25;
                break;
            case E_ItemRarity.RARE:
                IconName = "items_105";
                Name = itemNameArrays[2,r.Next(itemNameArrays.GetLength(1))];
                BasePrice = 50;
                break;
            case E_ItemRarity.EPIC:
                IconName = "items_106";
                Name = itemNameArrays[3, r.Next(itemNameArrays.GetLength(1))];
                BasePrice = 80;
                break;
            case E_ItemRarity.LEGENDARY:
                IconName = "items_107";
                Name = itemNameArrays[4, r.Next(itemNameArrays.GetLength(1))];
                BasePrice = 125;
                break;
        }
    }

    public int NewProtectionValue(E_ItemRarity newTier)
    {
        return upgradedProtectionValues[(int)newTier];
    }
}
