using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory
{
    public abstract Weapon CreateWeapon(int index);

    public abstract Armor CreateArmor(int index);

    public abstract Potion CreatePotion(int index);

    public abstract void UpgradeWeapon(Weapon weapon);

    public abstract void UpgradeArmor(Armor armor);

    public abstract void UpgradePotion(Potion potion);
}