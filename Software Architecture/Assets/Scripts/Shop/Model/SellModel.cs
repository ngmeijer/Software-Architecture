using System.Collections.Generic;
using UnityEngine;

public class SellModel : ShopModel
{
    private List<IObserver> _observerList = new List<IObserver>();

    public int MainState { get; set; } = 0;

    private Item _currentItem;

    public SellModel(float pPriceModifier, int pWeaponCount, int pArmorCount, int pPotionCount) : base(pPriceModifier, pWeaponCount, pArmorCount, pPotionCount)
    {
        _observerList = new List<IObserver>();
        NotifyObservers();
    }

    public override void ConfirmTransactionSelectedItem(ShopActions action)
    {
        _currentItem = inventory.GetItemByIndex(GetSelectedItemIndex());

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
        bool isMaxLevel = _currentItem.CheckItemLevel();

        if (ShopCreator.MoneyCount >= _currentItem.BasePrice)
        {
            if (!isMaxLevel)
            {
                inventory.UpdateMoneyCountAfterTransaction(_currentItem, ShopActions.UPGRADED);
                _currentItem.UpgradeItem();
                ListHasItemUpgraded = true;
            }
        }
    }

    private void SellItem()
    {
        inventory.RemoveItemByIndex(GetSelectedItemIndex());
        inventory.UpdateMoneyCountAfterTransaction(_currentItem, ShopActions.SOLD);
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