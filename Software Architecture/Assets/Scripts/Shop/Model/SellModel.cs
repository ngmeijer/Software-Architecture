using System.Collections.Generic;
using UnityEngine;

public class SellModel : ShopModel
{
    private List<IObserver> _observerList = new List<IObserver>();

    public SellModel(float pPriceModifier, int pWeaponCount, int pArmorCount, int pPotionCount) : base(pPriceModifier, pWeaponCount, pArmorCount, pPotionCount)
    {
        _observerList = new List<IObserver>();
        NotifyObservers();
    }

    public override void ConfirmTransactionSelectedItem(ShopActions action)
    {
        tradedItem = inventory.GetItemByIndex(GetSelectedItemIndex());

        switch (action)
        {
            case ShopActions.SOLD:
                SellItem();
                break;
            case ShopActions.UPGRADED:
                UpgradeItem();
                break;
        }
        NotifyObservers();
    }

    private void UpgradeItem()
    {
        bool isMaxLevel = tradedItem.CheckItemLevel();

        if (ShopCreator.MoneyCount >= tradedItem.BasePrice)
        {
            if (!isMaxLevel)
            {
                SubjectState = (int)ShopActions.UPGRADED;
                inventory.UpdateMoneyCountAfterTransaction(tradedItem, (ShopActions)SubjectState);
                tradedItem.UpgradeItem();
            }
        }
    }

    private void SellItem()
    {
        SubjectState = (int)ShopActions.SOLD;
        inventory.RemoveItemByIndex(GetSelectedItemIndex());
        inventory.UpdateMoneyCountAfterTransaction(tradedItem, (ShopActions)SubjectState);
    }

    public override void Attach(IObserver pObserver)
    {
        this._observerList.Add(pObserver);
    }

    public override void Detach(IObserver pObserver)
    {
        this._observerList.Remove(pObserver);
    }

    public override void NotifyObservers()
    {
        foreach (IObserver observer in _observerList)
        {
            observer.UpdateObservers(this);
        }

        SubjectState = (int)ShopActions.DEFAULT;
    }
}