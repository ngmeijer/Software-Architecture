using System;
using System.Collections.Generic;

/// <summary>
///This class holds the model of our Shop. It contains an inventory, a price modifier, and an index to select the items.
///In its current setup, view and controller need to get data via polling. Advisable is, to apply observer pattern or
///set up an event system for better integration with View and Controller.
/// </summary>

public abstract class ShopModel
{
    public Inventory inventory { get; } // Getter of the inventory, the views might need this to set up the display.
    protected float priceModifier; //Modifies the item's price based on its base price
    protected int selectedItemIndex = 0; //selected item index

    public delegate void OnItemClicked(int index);
    public static event OnItemClicked onClick;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ShopModel()
    //------------------------------------------------------------------------------------------------------------------------        
    public ShopModel(float pPriceModifier, int pItemCount, int pMoney)
    {
        inventory = new Inventory(pItemCount, pMoney);

        priceModifier = pPriceModifier;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns the selected item
    public Item GetSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < inventory.GetItemCount())
        {
            return inventory.GetItemByIndex(selectedItemIndex);
        }
        else
        {
            return null;
        }
    }

    private void ClickItem(Item item)
    {
        if (onClick != null)
        {
            //Debug.Log("Clicked item! Delegate works.");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to select the item, specified by 'index', fails silently
    public void SelectItemByIndex(int index)
    {
        if (index >= 0 && index < inventory.GetItemCount())
        {
            selectedItemIndex = index;
        }
    }


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItem(Item item)
    //------------------------------------------------------------------------------------------------------------------------
    //Attempts to select the given item, fails silently
    //Question: I don't see why this would supposedly fail? Item parameter is not null, and index will always be at least 0.
    public void SelectItem(Item item)
    {
        if (item != null)
        {
            int index = inventory.GetItems().IndexOf(item);
            if (index >= 0)
            {
                selectedItemIndex = index;
                onClick(index);
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetSelectedItemIndex()
    //------------------------------------------------------------------------------------------------------------------------
    //returns the index of the current selected item
    public int GetSelectedItemIndex() => selectedItemIndex;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Confirm()
    //------------------------------------------------------------------------------------------------------------------------        
    //Concrete classes to implement 
    public abstract void ConfirmSelectedItem();
}

