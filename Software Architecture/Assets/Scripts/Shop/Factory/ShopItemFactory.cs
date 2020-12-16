using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemFactory : ItemFactory
{
    public override Armor CreateArmor()
    {
        return new Armor("low armor", "Armor", 5);
    }

    public override Potion CreatePotion()
    {
        return new Potion("low potion", "Potion", 3);
    }

    public override Weapon CreateWeapon()
    {
        return new Weapon("low weapon", "Weapon", 6);
    }
}
