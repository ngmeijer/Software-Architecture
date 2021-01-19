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

    public override void Attach(IObserver pObserver)
    {
        throw new System.NotImplementedException();
    }

    public override void ConfirmSelectedItem()
    {
        upgradeSelectedItem();
    }

    public override void Detach(IObserver pObserver)
    {
        throw new System.NotImplementedException();
    }

    public override void NotifyObservers()
    {
        throw new System.NotImplementedException();
    }

    private void upgradeSelectedItem()
    {
        Item currentItem = inventory.GetItemByIndex(selectedItemIndex);
        Debug.Log("upgrading current item");
    }
}