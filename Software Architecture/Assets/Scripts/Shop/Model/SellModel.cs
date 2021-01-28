using System.Collections.Generic;
using UnityEngine;

public class SellModel : ShopModel
{
    private List<IObserver> _observerList = new List<IObserver>();

    public int MainState { get; set; } = 0;

    public SellModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount)
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
        Item item = inventory.GetItemByIndex(selectedItemIndex);
        bool isMaxLevel = item.CheckItemLevel();

        if (!isMaxLevel)
        {
            inventory.UpdateMoneyCountAfterInventoryTransaction(selectedItemIndex, ShopActions.UPGRADED);
            item.UpgradeItem();
            ListHasItemUpgraded = true;
        }
    }

    private void SellItem()
    {
        ListHasDecreasedSize = true;
        inventory.RemoveItemByIndex(selectedItemIndex);
        inventory.UpdateMoneyCountAfterInventoryTransaction(selectedItemIndex, ShopActions.SOLD);
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