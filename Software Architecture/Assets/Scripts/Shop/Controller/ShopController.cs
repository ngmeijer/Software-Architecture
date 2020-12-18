using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines the methods to be called by views to control a ShopModel. You can make concrete
/// controllers like a mouse controller, keyboard controller, gamepad controller, etc from this interface.
/// </summary>
public abstract class ShopController : MonoBehaviour
{ 
    public ShopModel Model => model;//Public getter for the model
    protected ShopModel model; //Ties this controller to a ShopModel

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
    //Called when a certain item is selected
    public void SelectItem(Item item)
    {
        if (item != null)
        {
            Debug.Log($"2a.1. Select item");
        }
        else
        {
            Debug.Log("2a.2. Item is null.");
        }

        model.SelectItem(item);

        Debug.Log($"2b. Index of item: {model.GetSelectedItemIndex()}");
        Debug.Log($"2c. Index of item with used function: {model.inventory.GetItems().IndexOf(item)}");
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Select an item by its index
    public void SelectItemByIndex(int index)
    {
        model.SelectItemByIndex(index);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ConfirmSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Tells the model to confirm the current selected item
    public void ConfirmSelectedItem()
    {
        model.ConfirmSelectedItem();
    }
}
