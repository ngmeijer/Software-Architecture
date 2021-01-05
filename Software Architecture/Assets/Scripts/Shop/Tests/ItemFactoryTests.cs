using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ItemFactoryTests
{
    #region Armor

    [Test]
    public void DisplayCorrectArmorName()
    {
        ShopItemFactory factoryInstance = new ShopItemFactory();
        Armor armorInstance = factoryInstance.CreateArmorUT(Item.E_ItemRarity.COMMON);
        Assert.That(armorInstance.Name, Is.EqualTo("Silk Vest of Hallowed Hell"));
    }

    [Test]
    public void DisplayCorrectArmorSpriteName()
    {
        ShopItemFactory factoryInstance = new ShopItemFactory();
        Armor armorInstance = factoryInstance.CreateArmorUT(Item.E_ItemRarity.EPIC);
        Assert.That(armorInstance.IconName, Is.EqualTo("items_109"));
    }

    [Test]
    public void DisplayCorrectArmorBasePrice()
    {
        ShopItemFactory factoryInstance = new ShopItemFactory();
        Armor armorInstance = factoryInstance.CreateArmorUT(Item.E_ItemRarity.LEGENDARY);
        Assert.That(armorInstance.BasePrice, Is.EqualTo(125));
    }

    [Test]
    public void DisplayCorrectArmorProtectionValue()
    {
        ShopItemFactory factoryInstance = new ShopItemFactory();
        Armor armorInstance = factoryInstance.CreateArmorUT(Item.E_ItemRarity.RARE);
        Assert.That(armorInstance.BaseEnchantmentValue, Is.EqualTo(50));
    }

    [Test]
    public void DisplayCorrectArmorItemDescription()
    {
        ShopItemFactory factoryInstance = new ShopItemFactory();
        Armor armorInstance = factoryInstance.CreateArmorUT(Item.E_ItemRarity.COMMON);
        Assert.That(armorInstance.Description, Is.EqualTo("Piece of cloth, commonly used " +
                                                          "by the lower Demons in Hell. Bloody, " +
                                                          "but offers some protection."));
    }

    #endregion

    [Test]
    public void CheckItemListSize()
    {
        Inventory inventory = new Inventory(15, 500);
        int listItemCount = inventory.GetItems().Count;
        Assert.AreEqual(15, listItemCount);
    }
}
