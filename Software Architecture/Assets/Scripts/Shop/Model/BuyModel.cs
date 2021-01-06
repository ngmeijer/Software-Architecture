using System;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

/// <summary>
/// This is a concrete, empty model for the buy state of the shop for you to implement
/// </summary>
public class BuyModel : ShopModel, ISubject
{
    public override int State { get; set; } = 0;

    private List<IObserver> _observers = new List<IObserver>();

    public BuyModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {

    }

    //------------------------------------------------------------------------------------------------------------------------

    //                                                 ConfirmSelectedItem()

    //------------------------------------------------------------------------------------------------------------------------        

    //Currently it just removes the selected item from the shop's inventory, rewrite this function and don't forget the unit test.

    public override void ConfirmSelectedItem()
    {
        Debug.Log("removing item.");
        inventory.RemoveItemByIndex(selectedItemIndex);
    }

    public void Attach(IObserver observer)
    {
        this._observers.Add(observer);
        Debug.Log("Subject (BuyModel): Attached an observer.");
    }

    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
        Debug.Log("Subject (BuyModel): Detached an observer.");
    }

    public void Notify()
    {
        Debug.Log("Subject (BuyModel): Notifying observers...");

        foreach (IObserver observer in _observers)
        {
            observer.UpdateFromSubject(this);
        }
    }
    
    public void TestFunction()
    {
        Debug.Log("\nSubject: I'm doing something important.");
        this.State = new System.Random().Next(0, 10);

        Debug.Log("Subject: My state has just changed to: " + this.State);
        this.Notify();
    }
}