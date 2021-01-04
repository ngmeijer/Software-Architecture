using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines a basic inventory
/// </summary>
public class Inventory
{
    public int Money { get; }//Getter for the money, the views need it to display the amount of money.
    private List<Item> itemList = new List<Item>(); //Items in the inventory

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
        return new List<Item>(itemList); //Returns a copy of the list, so the original is kept intact, 
                                         //however this is shallow copy of the original list, so changes in 
                                         //the original list will likely influence the copy, apply 
                                         //creational patterns like prototype to fix this. 

    }

    public List<Weapon> GetWeapons()
    {
        List<Weapon> localList = new List<Weapon>();
        foreach (Weapon weapon in itemList)
        {
            localList.Add(weapon);
        }
        return localList;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItemCount()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns the number of items
    public int GetItemCount()
    {
        return itemList.Count;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to get an item, specified by index, returns null if unsuccessful. Depends on how you set up your shop, it might be
    //a good idea to return a copy of the original item.
    public Item GetItemByIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            return itemList[index];
        }
        else
        {
            return null;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 AddItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds an item to the inventory's item list.
    public void AddItem(Item item)
    {
        itemList.Add(item);//In your setup, what would happen if you add an item that's already existed in the list?
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 RemoveItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to remove an item, fails silently.
    public void Remove(Item item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 RemoveItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to remove an item, specified by index, fails silently.
    public void RemoveItemByIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            itemList.RemoveAt(index);
        }
    }


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateInventory()
    //------------------------------------------------------------------------------------------------------------------------
    //This is obviously not how you should generate items. For this assignment you need either an abstract item factory, or
    //make this into a factory method.
    //DONE

    private void PopulateInventory(int itemCount)
    {
        ShopItemFactory factory = new ShopItemFactory();

        //Working on converting this to a reusable method. See generateItem();.

        //Order of for-loops determines what object type shows correctly?
        //FIXED

        for (int index = 0; index < 6; index++)
        {
            Weapon weapon = factory.CreateWeapon();
            itemList.Add(weapon);
        }

        for (int index = 0; index < 5; index++)
        {
            Armor armor = factory.CreateArmor();
            itemList.Add(armor);
        }

        for (int index = 0; index < 4; index++)
        {
            Potion potion = factory.CreatePotion();
            itemList.Add(potion);
        }
    }
    //Think of other necessary functions for the inventory based on your design of the shop. Don't forget to unit test all the functions.


    //------------------------------------------------------------------------------------------------------------------------
    //WIP!
    //------------------------------------------------------------------------------------------------------------------------
    private void generateItem(ShopItemFactory factory, Item itemInstance, int itemCount)
    {
        int itemIndex = 0;

        for (int index = 0; index < itemCount; index++)
        {
            itemInstance = factory.CreateArmor();
            itemIndex++;

            itemList.Add(itemInstance);
        }
    }
}
