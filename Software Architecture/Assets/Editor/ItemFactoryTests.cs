using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ItemFactoryTests
{
    [Test]
    public void DisplayCorrectWeaponName()
    {
        ShopItemFactory factory = new ShopItemFactory();
        Weapon weaponInstance = factory.CreateWeapon();
        Assert.That(weaponInstance.Name, Is.EqualTo("low weapon"));
    }

    [Test]
    public void DisplayCorrectWeaponBasePrice()
    {
        ShopItemFactory factory = new ShopItemFactory();
        Weapon weaponInstance = factory.CreateWeapon();
        Assert.That(weaponInstance.BasePrice, Is.EqualTo(6));
    }


    [Test]
    public void DisplayCorrectArmorName()
    {
        //ShopItemFactory factory = new ShopItemFactory();
        //Armor armorInstance = factory.CreateArmor();
        //Assert.That(armorInstance.Name, Is.EqualTo("low armor"));
    }

    [Test]
    public void DisplayCorrectArmorBasePrice()
    {
        //ShopItemFactory factory = new ShopItemFactory();
        //Armor armorInstance = factory.CreateArmor();
        //Assert.That(armorInstance.BasePrice, Is.EqualTo(5));
    }
}
