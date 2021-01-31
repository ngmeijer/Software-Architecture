using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    public class ItemFactoryTests
    {
        #region Armor

        [Test]
        public void DisplayCorrectArmorName()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItem();
            Assert.That(armorInstance.Name, Is.EqualTo("Silk Vest of Hallowed Hell"));
        }

        [Test]
        public void DisplayCorrectArmorSpriteName()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItem();
            Assert.That(armorInstance.IconName, Is.EqualTo("items_109"));
        }

        [Test]
        public void DisplayCorrectArmorBasePrice()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItem();
            Assert.That(armorInstance.BasePrice, Is.EqualTo(125));
        }

        [Test]
        public void DisplayCorrectArmorProtectionValue()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItem();
            Assert.That(armorInstance.BaseEnchantmentValue, Is.EqualTo(50));
        }

        [Test]
        public void DisplayCorrectArmorItemDescription()
        {
            ArmorFactory factoryInstance = new ArmorFactory();
            Item armorInstance = factoryInstance.CreateItem();
            Assert.That(armorInstance.Description, Is.EqualTo("Piece of cloth, commonly used " +
                                                              "by the lower Demons in Hell. Bloody, " +
                                                              "but offers some protection."));
        }

        #endregion

        [Test]
        public void CheckItemListSize()
        {
            Inventory inventory = new Inventory(5, 5, 5);
            int listItemCount = inventory.GetItems().Count;
            Assert.AreEqual(15, listItemCount);
        }
    }
}