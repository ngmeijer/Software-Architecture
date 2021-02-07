using System;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

/// <summary>
/// BuyModel is one of the Subjects/Publishers for the Observer behavioral pattern.
/// </summary>
public class BuyModel : ShopModel
{
    private readonly List<IObserver> _observerList = new List<IObserver>();

    public BuyModel(float pPriceModifier, int pWeaponCount, int pArmorCount, int pPotionCount) : base(pPriceModifier, pWeaponCount, pArmorCount, pPotionCount)
    {
        NotifyObservers();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 ConfirmTransactionSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------
    //Should we want the BuyModel to perform any other actions that involve buying an item, we can easily extend 
    public override void ConfirmTransactionSelectedItem(ShopActions pAction)
    {
        Item item = inventory.GetItemByIndex(GetSelectedItemIndex());

        if (pAction == ShopActions.PURCHASED)
        {
            if (ShopCreator.MoneyCount >= item.BasePrice)
            {
                tradedItem = item;
                inventory.UpdateMoneyCountAfterTransaction(item, pAction);
                inventory.RemoveItemByIndex(GetSelectedItemIndex());
                SubjectState = (int) pAction;

                NotifyObservers();

                return;
            }

            //If-statement failed, which means the money balance is not sufficient & throw ArgumentException.
            //For future iterations, we can implement a way to display that to the user. 
            throw new ArgumentException("Money balance is not sufficient enough to purchase this item.");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Observer Pattern
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

        //Reset State.
        SubjectState = (int)ShopActions.DEFAULT;
    }
    //------------------------------------------------------------------------------------------------------------------------
}