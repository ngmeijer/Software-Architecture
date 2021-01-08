using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

public class Weapon : Item
{
    private string _name;
    private string _description;
    private string _iconName;
    private int _price;

    private string _itemType = "Weapon";
    private EItemRarity _itemRarity;

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
            "",
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
            "",
            ""
        },

        //Legendary
        {
            "",
            ""
        }
    };
    private readonly string[,] _itemDescriptionArrays = new string[5, 2]
    {
        //Common
        {
            "",
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
            "",
            ""
        },

        //Legendary
        {
            "'",
            ""
        }
    };

    public Weapon(EItemRarity pItemRarity)
    {
        _itemRarity = pItemRarity;

        generateItemDetails();
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

    public override void generateItemDetails()
    {
        Random r = new Random();

        switch (_itemRarity)
        {
            case EItemRarity.COMMON:
                IconName = "items_73";
                Name = _itemNameArrays[0, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[0, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _damageValues[0];
                BasePrice = 10;
                break;

            case EItemRarity.UNCOMMON:
                IconName = "items_74";
                Name = _itemNameArrays[1, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[1, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _damageValues[1];
                BasePrice = 25;
                break;

            case EItemRarity.RARE:
                IconName = "items_77";
                Name = _itemNameArrays[2, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[2, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _damageValues[2];
                BasePrice = 50;
                break;

            case EItemRarity.EPIC:
                IconName = "items_79";
                Name = _itemNameArrays[3, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[3, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _damageValues[3];
                BasePrice = 80;
                break;

            case EItemRarity.LEGENDARY:
                IconName = "items_84";
                Name = _itemNameArrays[4, r.Next(_itemNameArrays.GetLength(1))];
                Description = _itemDescriptionArrays[4, r.Next(_itemDescriptionArrays.GetLength(1))];
                BaseEnchantmentValue = _damageValues[4];
                BasePrice = 125;
                break;
        }
    }
}
