using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///This class holds the model of our Shop. It contains an inventory, a price modifier, and an index to select the items.
///In its current setup, view and controller need to get data via polling. Advisable is, to apply observer pattern or
///set up an event system for better integration with View and Controller.
/// </summary>

public abstract class ShopModel : ISubject
{
    public Inventory inventory { get; } // Getter of the inventory, the views might need this to set up the display.
    public bool ListHasDecreasedSize { get; set; }
    public bool ListHasItemUpgraded { get; set; }

    protected float priceModifier; //Modifies the item's price based on its base price
    protected int selectedItemIndex = 0; //selected item index

    public delegate void OnItemClicked(int index);
    public static event OnItemClicked OnClick;

    public bool removeItem;


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  shopModel()
    //------------------------------------------------------------------------------------------------------------------------        
    public ShopModel(float pPriceModifier, int pWeaponCount, int pArmorCount, int pPotionCount)
    {
        inventory = new Inventory(pWeaponCount, pArmorCount, pPotionCount);

        priceModifier = pPriceModifier;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns the selected item
    public Item GetSelectedItem()
    {
        int index = GetSelectedItemIndex();
        if (index >= 0 && index < inventory.GetItemCount())
        {
            return inventory.GetItemByIndex(index);
        }
        return null;
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
    public void SelectItem(Item item)
    {
        if (item != null)
        {
            int index = inventory.GetItems().IndexOf(item);
            if (index >= 0)
            {
                selectedItemIndex = index;
                OnClick(index);
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
    public abstract void ConfirmTransactionSelectedItem(ShopActions action);

    public abstract void Attach(IObserver pObserver);

    public abstract void Detach(IObserver pObserver);

    public abstract void NotifyObservers();
}