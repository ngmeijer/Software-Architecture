using System;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

/// <summary>
/// BuyModel is one of the Subjects/Publishers for the Observer behavioral pattern.
/// </summary>
public class BuyModel : ShopModel, ISubject
{
    private List<IObserver> _observerList = new List<IObserver>();

    public int MainState { get; set; } = 0;


    public BuyModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {
        _observerList = new List<IObserver>();
    }

    //------------------------------------------------------------------------------------------------------------------------

    //                                                 ConfirmSelectedItem()

    //------------------------------------------------------------------------------------------------------------------------        

    //Currently it just removes the selected item from the shop's inventory, rewrite this function and don't forget the unit test.

    public override void ConfirmSelectedItem()
    {
        Debug.Log("removing item.");
        inventory.RemoveItemByIndex(selectedItemIndex);
        NotifyObservers();
    }

    public void Attach(IObserver pObserver)
    {
        this._observerList.Add(pObserver);
        Debug.Log("Subject: Attached an Observer.");
    }

    public void Detach(IObserver pObserver)
    {
        this._observerList.Remove(pObserver);
        Debug.Log("Subject: Detached an Observer.");
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in _observerList)
        {
            observer.UpdateObservers(this);
        }

        Debug.Log("Subject: Notifying Observers...");
    }
}