using System;
using System.Collections.Generic;
using UnityEngine;

public enum ShopActions
{
    DEFAULT,
    PURCHASED,
    UPGRADED,
    SOLD,
}

/// <summary>
///This class holds the model of our Shop. It contains an inventory, a price modifier, and an index to select the items.
///In its current setup, view and controller need to get data via polling. Advisable is, to apply observer pattern or
///set up an event system for better integration with View and Controller.
/// </summary>

public abstract class ShopModel : ISubject
{
    public Inventory inventory { get; } // Getter of the inventory, the views might need this to set up the display.
    public int SubjectState { get; set; } //Connected to ShopActions, to communicate with Observers without having any specific knowledge of them, only their existence.
    public Item tradedItem { get; set; } //Storage for the selected item, so that observers can get the details of the item.

    protected float priceModifier; //Modifies the item's price based on its base price
    protected int selectedItemIndex = 0; //selected item index

    //Notifies subscribers of event when Item is clicked.
    public delegate void OnItemSelected(int index);
    public static event OnItemSelected OnSelect;

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
    //Only used by keyboard control
    public void SelectItemByIndex(int index)
    {
        if (index >= 0 && index < inventory.GetItemCount())
        {
            selectedItemIndex = index;
            OnSelect(index);
        }
        else
        {
            throw new ArgumentOutOfRangeException($"{nameof(index)} must be positive and within the Inventory list range.");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItem(Item item)
    //------------------------------------------------------------------------------------------------------------------------
    //Only used by mouse control
    public void SelectItem(Item item)
    {
        if (item != null)
        {
            int index = inventory.GetItems().IndexOf(item);
            if (index >= 0)
            {
                selectedItemIndex = index;
                OnSelect(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{nameof(index)} must be positive and within the Inventory list range.");
            }
        }
        else
        {
            throw new NullReferenceException(
                $"{nameof(item)} is null. Use a valid Item of type Weapon, Armor or Potion as parameter.");
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
    public abstract void ConfirmTransactionSelectedItem(ShopActions pAction);

    public abstract void Attach(IObserver pObserver);

    public abstract void Detach(IObserver pObserver);

    public abstract void NotifyObservers();
}