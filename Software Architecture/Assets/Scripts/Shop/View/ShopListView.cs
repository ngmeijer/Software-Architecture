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
    [SerializeField] protected VerticalLayoutGroup itemLayoutGroup;

    [Header("Details panel")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemRarity;
    [SerializeField] private Image itemIcon;

    [Space]
    private Item currentItem = null;

    [SerializeField] private List<GameObject> itemList = new List<GameObject>();


    private void Awake()
    {
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
    public void RepopulateItemIconView()
    {
        ClearIconView();
        PopulateItemIconView();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    private void PopulateItemIconView()
    {
        foreach (Item item in usedModel.inventory.GetItems())
        {
            AddItemToView(item);
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

        ListViewItemContainer itemContainer = newItemInstance.GetComponent<ListViewItemContainer>();
        Debug.Assert(itemContainer != null);
        itemContainer.Initialize(item);

        itemList.Add(newItemInstance);
    }

    public override void UpdateObservers(ISubject pSubject)
    {
        if (pSubject.SubjectState == (int)ShopActions.PURCHASED)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.SOLD)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.UPGRADED)
        {
            GameObject upgradedItem = itemList[ShopCreator.Instance.inventoryModel.GetSelectedItemIndex()];

            ListViewItemContainer itemContainer = upgradedItem.GetComponent<ListViewItemContainer>();
            itemContainer.updateItemDetailsUI();
        }

        updateMoneyPanel();
    }

    private void updateDetailsPanel(int index)
    {
        if (this.gameObject == null)
            return;

        switch (InventoryInstance)
        {
            case 0:
                currentItem = ShopCreator.Instance.shopModel.GetSelectedItem();
                break;
            case 1:
                currentItem = ShopCreator.Instance.inventoryModel.GetSelectedItem();
                break;
        }

        itemName.text = currentItem.Name;
        itemDescription.text = currentItem.Description;
        itemType.text = currentItem.ItemType;
        itemPrice.text = currentItem.BasePrice.ToString();
        itemRarity.text = currentItem.ItemRarity.ToString();
        itemIcon.sprite = currentItem.ItemSprite;
    }

    private void updateItemList()
    {
        int removedItemIndex = usedModel.inventory.RemovedItemIndex;
        itemList.RemoveAt(removedItemIndex);

        Transform child = itemLayoutGroup.transform.GetChild(removedItemIndex);
        Destroy(child.gameObject);
    }

    private void OnEnable()
    {
        if (usedModel != null)
        {
            RepopulateItemIconView();
        }
    }
}