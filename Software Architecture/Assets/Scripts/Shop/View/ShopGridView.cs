using System;
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
public class ShopGridView : MonoBehaviour, IObserver
{
    //SUBSCRIBER CLASS!

    [SerializeField] private GridLayoutGroup itemLayoutGroup; //Links to a GridLayoutGroup in the Unity scene

    [SerializeField] private GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField] private List<GameObject> itemList = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private TextMeshProUGUI instructionText;

    private ViewConfig viewConfig;

    [SerializeField] private int InventoryInstance = 0;

    private void Start()
    {
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        SetupItemIconView(); //Setup the grid view's properties

        Inventory.OnMoneyChanged += updateMoneyPanel;
        switch (InventoryInstance)
        {
            case 0:
                PopulateItemIconView(0, ShopView.Instance.shopModel); ShopView.Instance.shopModel.Attach(this);
                break;
            case 1:
                PopulateItemIconView(0, ShopView.Instance.shopModelInventory);
                ShopView.Instance.shopModelInventory.Attach(this);
                break;
        }
        updateMoneyPanel();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SetupItemIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Setup the grid view according to the ViewConfig object's requirements, right now it just sets the constraint mode and column count,
    //you can make cosmetic adjustments to the GridLayoutGroup by adding more configurations to ViewConfig and use them adjusting properties
    //like cellSize, spacing, padding, etc.
    private void SetupItemIconView()
    {
        itemLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;//Set the constraint mode of the GridLayoutGroup
        itemLayoutGroup.constraintCount = viewConfig.gridViewColumnCount; //Set the column count according to the ViewConfig object
        itemLayoutGroup.cellSize = new Vector2(75, 75);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RepopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the grid view and repopulates it with new icons (updates the visible icons)
    public void RepopulateItemIconView(int index)
    {
        ClearIconView();
        //PopulateItemIconView(index);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    private void PopulateItemIconView(int index, ShopModel model)
    {
        switch (index)
        {
            case 0:
                foreach (Item item in model.inventory.GetItems())
                {
                    AddItemToView(item);
                }
                break;

            case 1:
                foreach (Item weapon in model.inventory.GetItems())
                {
                    if (weapon.ItemType == "Weapon")
                        AddItemToView(weapon);
                }
                break;

            case 2:
                foreach (Item armor in model.inventory.GetItems())
                {
                    if (armor.ItemType == "Armor")
                        AddItemToView(armor);
                }
                break;

            case 3:
                foreach (Item potion in model.inventory.GetItems())
                {
                    if (potion.ItemType == "Potion")
                        AddItemToView(potion);
                }
                break;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ClearIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Removes all existing icons in the grid view
    private void ClearIconView()
    {
        itemList.Clear();
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
    private void AddItemToView(Item item)
    {
        GameObject newItemInstance = GameObject.Instantiate(itemPrefab);
        newItemInstance.transform.SetParent(itemLayoutGroup.transform);
        newItemInstance.transform.localScale = Vector3.one;//The scale would automatically change in Unity so we set it back to Vector3.one.

        GridViewItemContainer itemContainer = newItemInstance.GetComponent<GridViewItemContainer>();
        Debug.Assert(itemContainer != null);
        itemContainer.Initialize(item);

        itemList.Add(newItemInstance);
    }

    public void UpdateObservers(ISubject pSubject)
    {
        Debug.Log($"Has item upgraded? {pSubject.ListHasItemUpgraded}");

        if (pSubject.ListHasDecreasedSize)
            updateItemList();

        if (pSubject.ListHasItemUpgraded)
        {
            Item item = ShopView.Instance.shopModelInventory.GetSelectedItem();

            GameObject upgradedItem = itemList[ShopView.Instance.shopModelInventory.GetSelectedItemIndex()];

            GridViewItemContainer itemContainer = upgradedItem.GetComponent<GridViewItemContainer>();
            itemContainer.updateItemDetailsUI();
        }

        updateMoneyPanel();
    }


    private void updateItemList()
    {
        int removedItemIndex = ShopView.Instance.shopModel.inventory.GetRemovedItemIndex();
        itemList.RemoveAt(removedItemIndex);

        Transform child = itemLayoutGroup.transform.GetChild(removedItemIndex);
        Destroy(child.gameObject);
    }


    private void updateMoneyPanel()
    {
        moneyText.text = ShopView.MoneyCount.ToString();
    }
}