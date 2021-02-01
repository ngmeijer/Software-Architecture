using UnityEngine;

public class Weapon : Item
{
    private string _name;
    private string _description;
    private string _iconName;
    private int _price;

    private string _itemType = "Weapon";
    private EItemRarity _itemRarity;

    private Sprite _itemSprite;

    private int _damage;
    private int[] _damageValues = new int[5]
    {
        10,
        30,
        50,
        70,
        100
    };
    private string _damageTextValue = "Damage: ";

    private readonly string[,] _itemNameArrays = new string[5, 2]
    {
        //Common
        {
            "Recruit's Sculptor",
            "Grasscutter"
        },
        
        //Uncommon
        {
            "Captain's Iron Broadsword",
            "Venomshank"
        },

        //Rare
        {
            "Nethersbane",
            "Fury, Reaver of the Talon"
        },

        //Epic
        {
            "The Void's Poison",
            "Blade of the Void"
        },

        //Legendary
        {
            "Dawnbreaker",
            "Edge of Death"
        }
    };

    private readonly string[,] _itemDescriptionArrays = new string[5, 2]
    {
        //Common
        {
            "Regular sword used by recruits. Mass-produced and nothing special, but does the job.",
            ""
        },
        
        //Uncommon
        {
            "",
            ""
        },

        //Rare
        {
            "",
            ""
        },

        //Epic
        {
            "It is said that those who are struck by this blade have no hope of surviving. Their physical body, that is. Their soul will be trapped forever in the Void.",
            ""
        },

        //Legendary
        {
            "Wielded by the Dwarven King, during the Battle of The Black Mountain. No enemy is worthy enough to withstand its power.'",
            "Forged in the depths of Hell by the Khan Maykr herself, it contains a part of the very core of all Nine Circles."
        }
    };

    private readonly string[,] _itemIconNames = new string[5, 2]
    {
        {"73", "74"},
        {"75", "76"},
        {"77", "80"},
        {"79", "82"},
        {"85", "83"}
    };

    public Weapon(EItemRarity pItemRarity)
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
        get { return _damage; }
        set { _damage = value; }
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
        Description = TakeElementFromArray(_itemDescriptionArrays, (int)_itemRarity);
        BaseEnchantmentValue = _damageValues[(int)_itemRarity];
        BasePrice = _prices[(int)_itemRarity];
    }
}
