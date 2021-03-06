﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines the methods to be called by views to control a shopModel. You can make concrete
/// controllers like a mouse controller, keyboard controller, game pad controller, etc from this interface.
/// </summary>
public abstract class ShopController : MonoBehaviour
{ 
    public ShopModel Model => model;//Public getter for the model
    protected ShopModel model; //Ties this controller to a shopModel

    public abstract void HandleInput(); //Concrete controllers override this method and handle input in different ways.

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------   
    //Used as the equivalence of a constructor since we can't use new to create a MonoBehaviour, marked as virtual so that
    //concrete controllers can add their own Initialize methods
    public virtual ShopController Initialize(ShopModel pModel)
    {
        model = pModel;

        return this;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItem()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Called when a certain item is selected. Only used by Mouse control.
    public void SelectItem(Item item)
    {
        model.SelectItem(item);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Select an item by its index. Only used by Keyboard control.
    public void SelectItemByIndex(int index)
    {
        model.SelectItemByIndex(index);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ConfirmTransactionSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Tells the model to confirm the current selected item
    public void ConfirmSelectedItem(ShopActions pAction, ShopModel pModel)
    {
        pModel.ConfirmTransactionSelectedItem(pAction);
    }
}
