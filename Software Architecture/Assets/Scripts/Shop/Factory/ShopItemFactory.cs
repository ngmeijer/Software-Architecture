using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemFactory : ItemFactory
{
    public override Armor CreateArmor(string pIconName)
    {
        return new Armor("low armor", pIconName, 5);
    }

    public override Potion CreatePotion()
    {
        return new Potion("low potion", "Potion", 3);
    }

    public override Weapon CreateWeapon()
    {
        return new Weapon("items_99", "Weapon", 6);
    }
}
