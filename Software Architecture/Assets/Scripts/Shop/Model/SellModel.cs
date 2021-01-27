using System.Collections.Generic;
using UnityEngine;

public class SellModel : ShopModel
{
    private List<IObserver> _observerList = new List<IObserver>();

    public int MainState { get; set; } = 0;

    public SellModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {
        _observerList = new List<IObserver>();
        NotifyObservers();
    }

    public override void ConfirmTransactionSelectedItem(ShopActions action)
    {
        if (action == ShopActions.SOLD)
        {
            Debug.Log("list has changed.");
            ListHasChanged = true;
            inventory.RemoveItemByIndexShop(selectedItemIndex);
            inventory.UpdateMoneyCountAfterInventoryTransaction(selectedItemIndex,ShopActions.SOLD);
        }

        if (action == ShopActions.UPGRADED)
        {
            inventory.UpdateMoneyCountAfterInventoryTransaction(selectedItemIndex, ShopActions.UPGRADED);
            //
        }

        NotifyObservers();
    }

    public override void Attach(IObserver pObserver)
    {
        this._observerList.Add(pObserver);
        Debug.Log("Subject: Attached an Observer.");
    }

    public override void Detach(IObserver pObserver)
    {
        this._observerList.Remove(pObserver);
        Debug.Log("Subject: Detached an Observer.");
    }

    public override void NotifyObservers()
    {
        foreach (IObserver observer in _observerList)
        {
            observer.UpdateObservers(this);
        }

        ListHasChanged = false;

        Debug.Log("Subject: Notifying Observers...");
    }
}