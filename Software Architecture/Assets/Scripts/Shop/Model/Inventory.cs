using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines a basic inventory
/// </summary>
public class Inventory
{
    public int Money { get; private set; }//Getter for the money, the views need it to display the amount of money.
    private List<Item> _itemList = new List<Item>(); //Items in the inventory

    public delegate void OnMoneyBalanceChanged();
    public static event OnMoneyBalanceChanged OnMoneyChanged;

    private int _removedItemIndex = 0;

    //Set up the inventory with item count and money
    public Inventory(int pItemCount, int pMoney)
    {
        PopulateInventory(pItemCount);
        Money = pMoney;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns a list with all current items in the shop.
    public List<Item> GetItems()
    {
        return new List<Item>(_itemList); //Returns a copy of the list, so the original is kept intact, 
                                          //however this is shallow copy of the original list, so changes in 
                                          //the original list will likely influence the copy, apply 
                                          //creational patterns like prototype to fix this. 
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItemCount()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns the number of items
    public int GetItemCount()
    {
        return _itemList.Count;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to get an item, specified by index, returns null if unsuccessful. Depends on how you set up your shop, it might be
    //a good idea to return a copy of the original item.
    public Item GetItemByIndex(int index)
    {
        if (index >= 0 && index < _itemList.Count)
        {
            Debug.Log($"index in GetItemByIndex -- {index} -- ");
            return _itemList[index];
        }
        return null;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 AddItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds an item to the inventory's item list.
    public void AddItem(Item item)
    {
        _itemList.Add(item);//In your setup, what would happen if you add an item that's already existed in the list?
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 RemoveItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to remove an item, fails silently.
    public void Remove(Item item)
    {
        if (_itemList.Contains(item))
        {
            _itemList.Remove(item);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 RemoveItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    public void RemoveItemByIndex(int index)
    {
        if (index >= 0 && index < _itemList.Count)
        {
            _itemList.RemoveAt(index);
            _removedItemIndex = index;
            foreach (Item item in _itemList)
            {
                if (item.ItemIndex >= index)
                {
                    item.ItemIndex--;
                }
            }
        }
    }

    public int GetRemovedItemIndex()
    {
        return _removedItemIndex;
    }

    public void UpdateInventoryMoneyCountAfterPurchase(int index)
    {
        Item itemReference = GetItemByIndex(index);

        Debug.Log($"index of purchased item is -- { index } --.\n" +
                  $"Name of selected item is -- {itemReference.Name}. \n" +
                  $"Base Price of selected item is -- {itemReference.BasePrice} Gold.");

        Money -= itemReference.BasePrice;
        if (OnMoneyChanged != null)
            OnMoneyChanged();
    }


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateInventory()
    //------------------------------------------------------------------------------------------------------------------------
    private void PopulateInventory(int itemCount)
    {
        WeaponFactory weaponFactory = new WeaponFactory();
        ArmorFactory armorFactory = new ArmorFactory();
        PotionFactory potionFactory = new PotionFactory();

        for (int index = 0; index < 6; index++)
        {
            Item weapon = weaponFactory.CreateItem();
            _itemList.Add(weapon);
        }

        for (int index = 0; index < 5; index++)
        {
            Item armor = armorFactory.CreateItem();
            _itemList.Add(armor);
        }

        for (int index = 0; index < 4; index++)
        {
            Item potion = potionFactory.CreateItem();
            _itemList.Add(potion);
        }

        int itemInstanceIndex = 0;

        foreach (Item item in _itemList)
        {
            item.ItemIndex = itemInstanceIndex;
            itemInstanceIndex++;
        }
    }
}
