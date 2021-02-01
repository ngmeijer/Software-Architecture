using UnityEngine;

public class Potion : Item
{
    private string _name;
    private string _description;
    private string _iconName;
    private int _price;

    private string _itemType = "Potion";
    private EItemRarity _itemRarity;

    private Sprite _itemSprite;

    private int _heal;
    private int[] _healValues = new int[5]
    {
        10,
        30,
        50,
        70,
        100
    };

    private string _damageTextValue = "Heal: ";

    private readonly string[,] _itemNameArrays = new string[5, 2]
    {
        //Common
        {
            "Flask of the Mountain",
            "Flask of Ancient Secrets"
        },
        
        //Uncommon
        {
            "Draught of sleep Inducement",
            "Phial of Blank Minds"
        },

        //Rare
        {
            "Philter of Enhanced Senses",
            "Elixer of the Senses"
        },

        //Epic
        {
            "Phial of Vitality",
            "Potion of Immortality"
        },

        //Legendary
        {
            "Flask of Firepower",
            "Flask of Brute Force"
        }
    };
    private readonly string[,] _itemDescriptionArrays = new string[5, 2]
    {
        //Common
        {
            "You'll get really high and think you are ",
            ""
        },
        
        //Uncommon
        {
            "",
            ""
        },

        //Rare
        {
            "Gives you a short moment of clarity. Use when you feel unfocused.",
            ""
        },

        //Epic
        {
            "",
            ""
        },

        //Legendary
        {
            "'",
            ""
        }
    };

    private readonly string[,] _itemIconNames = new string[5, 2]
    {
        {"130", "131"},
        {"285", "286"},
        {"135", "136"},
        {"143", "144"},
        {"140", "141"}
    };

    public Potion(EItemRarity pItemRarity)
    {
        _itemRarity = pItemRarity;

        GenerateItemDetails();
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

    public override string BaseEnchantmentText
    {
        get { return _damageTextValue; }
        set { _damageTextValue = value; }
    }

    public override int BaseEnchantmentValue
    {
        get { return _heal; }
        set { _heal = value; }
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

    public override void GenerateItemDetails()
    {
        Name = TakeElementFromArray(_itemNameArrays, (int)_itemRarity);
        IconName = "items_" + TakeElementFromArray(_itemIconNames, (int)_itemRarity);
        Description = TakeElementFromArray(_itemDescriptionArrays, 0);
        BaseEnchantmentValue = _healValues[(int)_itemRarity];
        BasePrice = _prices[(int)_itemRarity];
    }
}