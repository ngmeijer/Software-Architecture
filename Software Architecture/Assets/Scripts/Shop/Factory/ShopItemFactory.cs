using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemFactory : ItemFactory
{
    private string[] armorIconNames = new string[5]
    {
        "items_103",
        "items_104",
        "items_105",
        "items_106",
        "items_107",
    };

    private string[] weaponIconNames = new string[6]
    {
        "items_73",
        "items_74",
        "items_75",
        "items_76",
        "items_77",
        "items_78"

    };

    private string[] potionIconNames = new string[4]
    {
        "items_285",
        "items_286",
        "items_287",
        "items_288",
    };

    public override Armor CreateArmor(int index)
    {
        return new Armor("low armor", armorIconNames[index], 5);
    }

    public override Potion CreatePotion(int index)
    {
        return new Potion("low potion", potionIconNames[index], 3);
    }

    public override Weapon CreateWeapon(int index)
    {
        return new Weapon("low weapon", weaponIconNames[index], 6);
    }
}
