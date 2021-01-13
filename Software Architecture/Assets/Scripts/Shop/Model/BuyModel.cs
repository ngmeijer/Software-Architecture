using System;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

/// <summary>
/// This is a concrete, empty model for the buy state of the shop for you to implement
/// </summary>
public class BuyModel : ShopModel
{
    //PUBLISHER CLASS!
    public override List<ISubsciber> SubscriberList { get; set; }

    public override int MainState { get; set; }


    public BuyModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {
        SubscriberList = new List<ISubsciber>();
    }

    //------------------------------------------------------------------------------------------------------------------------

    //                                                 ConfirmSelectedItem()

    //------------------------------------------------------------------------------------------------------------------------        

    //Currently it just removes the selected item from the shop's inventory, rewrite this function and don't forget the unit test.

    public override void ConfirmSelectedItem()
    {
        Debug.Log("removing item.");
        inventory.RemoveItemByIndex(selectedItemIndex);
        NotifySubscribers();
    }

    public override void MainBussinessLogic()
    {
        //MainState = NewState;
        NotifySubscribers();
    }

    public override void NotifySubscribers()
    {
        foreach (ISubsciber s in SubscriberList)
        {
            s.UpdateSubscribers(this);
        }
    }

    public override void Subscribe(ISubsciber subsciber)
    {
        SubscriberList.Add(subsciber);
        NotifySubscribers();
    }

    public override void Unsubscribe(ISubsciber subscriber)
    {
        SubscriberList.Remove(subscriber);
    }
}