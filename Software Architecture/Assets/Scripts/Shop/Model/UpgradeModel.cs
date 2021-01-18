using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModel : ShopModel
{
    private WeaponFactory weaponFactory;
    private ArmorFactory armorFactory;
    private PotionFactory potionFactory;

    public UpgradeModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {
        weaponFactory = new WeaponFactory();
        armorFactory = new ArmorFactory();
        potionFactory = new PotionFactory();
    }

    public override void ConfirmSelectedItem()
    {
        upgradeSelectedItem();
    }

    private void upgradeSelectedItem()
    {
        Item currentItem = inventory.GetItemByIndex(selectedItemIndex);
        Debug.Log("upgrading current item");
    }
}