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
    public int[] _protectionValues = new int[5]
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
            "Forged and worn by the Dark Elves of the North, the Faero. This armor is infused with black magic, making it nearly impenetrable.",
            "..."
        },

        //Legendary
        {
            "'Rip and tear, until it is done. For it is he that they fear, not man or his armies. They fear the mark of the Beast.",
            "..."
        }
    };

    private readonly string[,] _itemIconNames = new string[5, 2]
    {
        {"112", "113"},
        {"121", "122"},
        {"103", "105"},
        {"108", "119"},
        {"107", "109"}
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
        //Select correct array for the used attribute, and pass rarity.
        Name = TakeElementFromArray(_itemNameArrays, (int)_itemRarity);
        IconName = "items_" + TakeElementFromArray(_itemIconNames, (int)_itemRarity);
        Description = TakeElementFromArray(_itemDescriptionArrays, 0);
        BaseEnchantmentValue = _protectionValues[(int)_itemRarity];

        //Select price based on rarity.
        BasePrice = _prices[(int)_itemRarity];
    }
}
