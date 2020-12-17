using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory
{
    public abstract Weapon CreateWeapon(int index);

    public abstract Armor CreateArmor(int index);

    public abstract Potion CreatePotion(int index);
}