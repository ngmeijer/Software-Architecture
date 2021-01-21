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

    //                                                 ConfirmSelectedItem()

    //------------------------------------------------------------------------------------------------------------------------        
    public override void ConfirmSelectedItem()
    {
        inventory.UpdateInventoryMoneyCountAfterTransaction(selectedItemIndex, ShopActions.PURCHASED);
        inventory.RemoveItemByIndex(selectedItemIndex);
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

        Debug.Log("Subject: Notifying Observers...");
    }
}