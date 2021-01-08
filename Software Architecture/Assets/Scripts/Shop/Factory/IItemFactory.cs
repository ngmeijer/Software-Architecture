using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemRarity
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY
};


public interface IItemFactory
{
    Item CreateItem();
    EItemRarity ReturnRarity();

    void UpgradeItem(Item item);

    //Item CreateWeapon();
    //Item CreateArmor();
    //Item CreatePotion();

    //On first glance this seems like repeated code. It's not, though. 
    //For example, Weapon has a _damage attribute, that Armor obviously does not have,  eliminating the chance for reusing a general function.
    //public abstract Weapon UpgradeWeapon(Weapon weapon);
    //public abstract Armor UpgradeArmor(Armor armor);
    //public abstract Potion UpgradePotion(Potion potion);
}