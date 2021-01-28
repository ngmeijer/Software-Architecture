﻿using System.Collections.Generic;
using UnityEngine;

public class SellModel : ShopModel
{
    private List<IObserver> _observerList = new List<IObserver>();

    public int MainState { get; set; } = 0;

    public SellModel(float pPriceModifier, int pWeaponCount, int pArmorCount, int pPotionCount) : base(pPriceModifier, pWeaponCount, pArmorCount, pPotionCount)
    {
        _observerList = new List<IObserver>();
        NotifyObservers();
    }

    public override void ConfirmTransactionSelectedItem(ShopActions action)
    {
        if (action == ShopActions.SOLD)
        {
            SellItem();
        }

        if (action == ShopActions.UPGRADED)
        {
            UpgradeItem();
        }

        NotifyObservers();
    }

    private void UpgradeItem()
    {
        Item item = inventory.GetItemByIndex(GetSelectedItemIndex());
        bool isMaxLevel = item.CheckItemLevel();

        if (!isMaxLevel)
        {
            inventory.UpdateMoneyCountAfterInventoryTransaction(GetSelectedItemIndex(), ShopActions.UPGRADED);
            item.UpgradeItem();
            ListHasItemUpgraded = true;
        }
    }

    private void SellItem()
    {
        inventory.RemoveItemByIndex(GetSelectedItemIndex());
        inventory.UpdateMoneyCountAfterInventoryTransaction(GetSelectedItemIndex(), ShopActions.SOLD);
        ListHasDecreasedSize = true;
    }

    public override void Attach(IObserver pObserver)
    {
        this._observerList.Add(pObserver);
        Debug.Log("SellModel subject: Attached an Observer.");
    }

    public override void Detach(IObserver pObserver)
    {
        this._observerList.Remove(pObserver);
        Debug.Log("SellModel subject: Detached an Observer.");
    }

    public override void NotifyObservers()
    {
        foreach (IObserver observer in _observerList)
        {
            observer.UpdateObservers(this);
        }

        ListHasDecreasedSize = false;

        Debug.Log("SellModel subject: Notifying Observers...");
    }
}