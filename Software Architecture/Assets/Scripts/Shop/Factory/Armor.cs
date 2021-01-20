using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

public class Armor : Item
{
    #region Private fields

    private string _name;
    private string _description;
    private string _iconName;
    private int _price;

    private string _itemType = "Armor";
    private EItemRarity _itemRarity;

    private Sprite _itemSprite;

    private int _protection;
    private int[] _protectionValues = new int[5]
    {
        10,
        30,
        50,
        70,
        100
    };
    private string _protectionTextValue = "Protection: ";

    private readonly string[,] _itemNameArrays = new string[5, 2]
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
    private readonly string[,] _itemDescriptionArrays = new string[5, 2]
    {
        //Common
        {
            "Piece of cloth, commonly used by the lower Demons in Hell. Bloody, but offers some protection.",
            "Piece of cloth, commonly used by the lower Demons in Hell. Bloody, but offers some protection."
        },
        
        //Uncommon
        {
            "Armor produced for foot soldiers of Heaven, who marched against the demonic creations of the Faero.",
            "..."
        },

        //Rare
        {
            "...",
            "..."
        },

        //Epic
        {
            "Created and worn by the Dark Elves of the North, the Faero. This armor is infused with magic, making it nearly impenetrable.",
            "..."
        },

        //Legendary
        {
            "'Rip and tear, until it is done. For it is he that they fear, not man or his armies. They fear the mark of the Beast.",
            "..."
        }
    };

    #endregion

    public Armor(EItemRarity pItemRarity)
    {
        _itemRarity = pItemRarity;

        GenerateItemDetails();
    }

    #region Getters & Setters

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
    public override string BaseEnchantmentText
    {
        get { return _protectionTextValue; }
        set { _protectionTextValue = value; }
    }
    public override int BaseEnchantmentValue
    {
        get { return _protection; }
        set { _protection = value; }
    }
    public override EItemRarity ItemRarity
    {
        get { return _itemRarity; }
        set { _itemRarity = value; }
    }

    public override Sprite ItemSprite
    {
        get { return _itemSprite; }
        set { _itemSprite = value; }
    }


    #endregion

    public override void GenerateItemDetails()
    {
        Random r = new Random();

        switch (_itemRarity)
        {
            case EItemRarity.COMMON:
                //load from external
                IconName = "items_112";
                Name = _itemNameArrays[0, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[0, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _protectionValues[0];
                BasePrice = 10;
                break;

            case EItemRarity.UNCOMMON:
                IconName = "items_108";
                Name = _itemNameArrays[1, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[1, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _protectionValues[1];
                BasePrice = 25;
                break;

            case EItemRarity.RARE:
                IconName = "items_106";
                Name = _itemNameArrays[2, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[2, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _protectionValues[2];
                BasePrice = 50;
                break;

            case EItemRarity.EPIC:
                IconName = "items_109";
                Name = _itemNameArrays[3, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[3, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _protectionValues[3];
                BasePrice = 80;
                break;

            case EItemRarity.LEGENDARY:
                IconName = "items_107";
                Name = _itemNameArrays[4, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[4, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _protectionValues[4];
                BasePrice = 125;
                break;
        }
    }

    public int NewProtectionValue(EItemRarity newTier)
    {
        return _protectionValues[(int)newTier];
    }
}
