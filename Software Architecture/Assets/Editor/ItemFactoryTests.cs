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
}
