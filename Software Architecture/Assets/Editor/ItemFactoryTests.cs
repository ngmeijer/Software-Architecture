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
        Assert.That(weaponInstance.Name, Is.EqualTo("Weapon display name"));
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
        ShopItemFactory factory = new ShopItemFactory();
        Armor armorInstance = factory.CreateArmor();
        Assert.That(armorInstance.Name, Is.EqualTo("Armor display name"));
    }

    [Test]
    public void DisplayCorrectArmorBasePrice()
    {
        ShopItemFactory factory = new ShopItemFactory();
        Armor armorInstance = factory.CreateArmor();
        Assert.That(armorInstance.BasePrice, Is.EqualTo(5));
    }

    [Test]
    public void CheckItemListSize()
    {
        Inventory inventory = new Inventory(15, 500);
        int listItemCount = inventory.GetItems().Count;
        Assert.AreEqual(15, listItemCount);
    }

    [Test] 
    public void TestItemSelect()
    {
        
    }
}
