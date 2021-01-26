using System;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

/// <summary>
/// BuyModel is one of the Subjects/Publishers for the Observer behavioral pattern.
/// </summary>
public class BuyModel : ShopModel
{
    private List<IObserver> _observerList = new List<IObserver>();

    public int MainState { get; set; } = 0;

    public BuyModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {
        _observerList = new List<IObserver>();
        NotifyObservers();
    }

    //------------------------------------------------------------------------------------------------------------------------

    //                                                 ConfirmTransactionSelectedItem()

    //------------------------------------------------------------------------------------------------------------------------        
    public override void ConfirmTransactionSelectedItem(ShopActions action)
    {
        inventory.UpdateInventoryMoneyCountAfterTransaction(selectedItemIndex, action);

        if (action == ShopActions.PURCHASED || action == ShopActions.SOLD)
        {
            Debug.Log("list has changed.");
            ListHasChanged = true;
            inventory.RemoveItemByIndexShop(selectedItemIndex);
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