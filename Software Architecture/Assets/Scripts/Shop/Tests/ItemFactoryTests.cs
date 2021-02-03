using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    public class ItemFactoryTests
    {
        [Test]
        public void CheckItemListSize()
        {
            Inventory inventory = new Inventory(5, 5, 5);
            int listItemCount = inventory.GetItems().Count;
            Assert.AreEqual(15, listItemCount);
        }

        
        [Test]
        public void DisplayCorrectArmorBasePrice()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItemUnitTest(EItemRarity.LEGENDARY);
            Assert.That(armorInstance.BasePrice, Is.EqualTo(150));
        }

        [Test]
        public void CheckItemUpgradeProcess()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItem();
            EItemRarity previousRarity = armorInstance.ItemRarity;
            armorInstance.UpgradeItem();

            Assert.That(armorInstance.ItemRarity, Is.EqualTo(previousRarity + 1));
        }

        [Test]
        public void CheckItemPropertyValue()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            EItemRarity rarity = EItemRarity.LEGENDARY;
            Item armorInstance = factoryInstance.CreateItemUnitTest(rarity);

            Assert.That(armorInstance.BaseEnchantmentValue, Is.EqualTo(100));
        }
    }
}