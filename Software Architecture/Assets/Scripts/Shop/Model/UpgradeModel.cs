using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModel : ShopModel
{
    private ShopItemFactory _factory;
    public override int State { get; set; } = 0;

    public UpgradeModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {

    }

    public override void ConfirmSelectedItem()
    {
        upgradeSelectedItem();
    }

    private void upgradeSelectedItem()
    {
        Item currentItem = inventory.GetItemByIndex(selectedItemIndex);
        Debug.Log("upgrading current item");
        switch (currentItem.ItemType)
        {
            case "Weapon":
                _factory.UpgradeWeapon((Weapon)currentItem);
                break;
            case "Armor":
                _factory.UpgradeArmor((Armor) currentItem);
                break;
            case "Potion":
                _factory.UpgradePotion((Potion)currentItem);
                //inventory.Money -= currentItem.BasePrice;
                break;
        }

    }
}