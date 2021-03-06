﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class connects a grid view for buy state of the shop to a controller to manipulate the BuyModel via a shopController
/// interface, it contains specific methods to setup and update a grid view, with the data from a BuyModel. If you want to display
/// information outside of the BuyModel, for example, the money amount from the player's inventory, then you need to either keep a
/// reference to all the related models, or make this class an observer/event subscriber of the related models.
/// </summary>
public class ShopGridView : ShopView
{
    [SerializeField] protected GridLayoutGroup itemLayoutGroup;

    private void Awake()
    {
        viewConfig =
            Resources.Load<ViewConfig>("ViewConfig"); //Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        setupItemIconView(); //Setup the grid view's properties

        Inventory.OnMoneyChanged += updateMoneyPanel;

        switch (InventoryInstance)
        {
            case 0:
                ShopCreator.Instance.shopModel.Attach(this);
                usedModel = ShopCreator.Instance.shopModel;
                break;
            case 1:
                ShopCreator.Instance.inventoryModel.Attach(this);
                usedModel = ShopCreator.Instance.inventoryModel;
                break;
        }
    }

    private void Start()
    {
        updateMoneyPanel();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SetupItemIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Setup the grid view according to the ViewConfig object's requirements, right now it just sets the constraint mode and column count,
    //you can make cosmetic adjustments to the GridLayoutGroup by adding more configurations to ViewConfig and use them adjusting properties
    //like cellSize, spacing, padding, etc.
    private void setupItemIconView()
    {
        itemLayoutGroup.constraint =
            GridLayoutGroup.Constraint.Flexible; //Set the constraint mode of the GridLayoutGroup
        itemLayoutGroup.constraintCount =
            viewConfig.gridViewColumnCount; //Set the column count according to the ViewConfig object
        itemLayoutGroup.cellSize = new Vector2(75, 75);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RepopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the grid view and repopulates it with new icons (updates the visible icons)
    public override void RepopulateItemIconView()
    {
        clearIconView();
        populateItemIconView();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    protected override void populateItemIconView()
    {
        foreach (Item item in usedModel.inventory.GetItems())
        {
            addItemToView(item);
        }
    }

    public int GetGridItemListCount()
    {
        return _itemList.Count;
    }

//------------------------------------------------------------------------------------------------------------------------
    //                                                  ClearIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Removes all existing icons in the grid view
    protected override void clearIconView()
    {
        _itemList.Clear();
        Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allIcons)
        {
            if (child != itemLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  AddItemToView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds a new item container to the view, each view can have its way of displaying items
    protected override void addItemToView(Item item)
    {
        GameObject newItemInstance = GameObject.Instantiate(itemPrefab);
        newItemInstance.transform.SetParent(itemLayoutGroup.transform);
        newItemInstance.transform.localScale = Vector3.one;//The scale would automatically change in Unity so we set it back to Vector3.one.

        GridViewItemContainer itemContainer = newItemInstance.GetComponent<GridViewItemContainer>();
        Debug.Assert(itemContainer != null);
        itemContainer.Initialize(item);

        _itemList.Add(newItemInstance);
    }

    public override void UpdateObservers(ISubject pSubject)
    {
        if (pSubject.SubjectState == (int) ShopActions.PURCHASED)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.SOLD)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.UPGRADED)
        {
            GameObject itemInstance = _itemList[ShopCreator.Instance.inventoryModel.GetSelectedItemIndex()];

            GridViewItemContainer itemContainer = itemInstance.GetComponent<GridViewItemContainer>();
            itemContainer.UpdateItemDetailsUI();
        }

        updateMoneyPanel();
    }


    protected override void updateItemList()
    {
        int removedItemIndex = usedModel.inventory.RemovedItemIndex;
        _itemList.RemoveAt(removedItemIndex);
        Transform child = itemLayoutGroup.transform.GetChild(removedItemIndex);
        Destroy(child.gameObject);
    }
}