using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory
{
    public abstract Weapon CreateWeapon();
    public abstract Armor CreateArmor();
    public abstract Potion CreatePotion();

    //On first glance this seems like repeated code. It's not, though. 
    //For example, Weapon has a _damage attribute, that Armor obviously does not have,  eliminating the chance for reusing a general function.
    public abstract Weapon UpgradeWeapon(Weapon weapon);
    public abstract Armor UpgradeArmor(Armor armor);
    public abstract Potion UpgradePotion(Potion potion);
}