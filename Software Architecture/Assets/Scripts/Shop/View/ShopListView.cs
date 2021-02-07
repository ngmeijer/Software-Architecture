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
public class ShopListView : ShopView
{
    //Wanted to use LayoutGroup so I can relocate it to ShopView but as it seems, VerticalLayoutGroup does not inherit from LayoutGroup -,- 
    [SerializeField] protected VerticalLayoutGroup itemLayoutGroup;

    //Panel components on the right side of the screen.
    [Header("Details panel")]
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] private TextMeshProUGUI _itemType;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    [SerializeField] private TextMeshProUGUI _itemRarity;
    [SerializeField] private Image _itemIcon;

    //Equal to the currently selected item in models.
    [Space] private Item _currentItem = null;

    private void Awake()
    {
        //Switch between the correct ShopModels. Is this an ShopView (0), or an InventoryView (1)?
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

        ShopModel.OnSelect += updateDetailsPanel;
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

        ListViewItemContainer itemContainer = newItemInstance.GetComponent<ListViewItemContainer>();
        Debug.Assert(itemContainer != null);
        itemContainer.Initialize(item);

        _itemList.Add(newItemInstance);
    }

    public override void UpdateObservers(ISubject pSubject)
    {
        //Separated the SOLD & PURCHASED actions, should a feature implementation in the future apply for one but not for the other.
        if (pSubject.SubjectState == (int)ShopActions.PURCHASED)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.SOLD)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.UPGRADED)
        {
            GameObject upgradedItem = _itemList[ShopCreator.Instance.inventoryModel.GetSelectedItemIndex()];

            ListViewItemContainer itemContainer = upgradedItem.GetComponent<ListViewItemContainer>();
            itemContainer.UpdateItemDetailsUI();
        }

        //Whatever happens, always update the money counter.
        updateMoneyPanel();
    }

    private void updateDetailsPanel(int index)
    {
        if (this.gameObject == null)
            return;

        switch (InventoryInstance)
        {
            case 0:
                _currentItem = ShopCreator.Instance.shopModel.GetSelectedItem();
                break;
            case 1:
                _currentItem = ShopCreator.Instance.inventoryModel.GetSelectedItem();
                break;
        }

        _itemName.text = _currentItem.Name;
        _itemDescription.text = _currentItem.Description;
        _itemType.text = _currentItem.ItemType;
        _itemPrice.text = _currentItem.BasePrice.ToString();
        _itemRarity.text = _currentItem.ItemRarity.ToString();
        _itemIcon.sprite = _currentItem.ItemSprite;
    }

    protected override void updateItemList()
    {
        int removedItemIndex = usedModel.inventory.RemovedItemIndex;
        _itemList.RemoveAt(removedItemIndex);

        Transform child = itemLayoutGroup.transform.GetChild(removedItemIndex);
        Destroy(child.gameObject);
    }
}