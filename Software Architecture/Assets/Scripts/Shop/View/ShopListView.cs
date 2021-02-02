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

    private void Start()
    {
        switch (InventoryInstance)
        {
            case 0:
                PopulateItemIconView(0, ShopCreator.Instance.shopModel);
                ShopCreator.Instance.shopModel.Attach(this);
                break;
            case 1:
                PopulateItemIconView(0, ShopCreator.Instance.shopModelInventory);
                ShopCreator.Instance.shopModelInventory.Attach(this);
                break;
        }

        ShopModel.OnSelect += updateDetailsPanel;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    private void PopulateItemIconView(int index, ShopModel model)
    {
        foreach (Item item in model.inventory.GetItems())
        {
            AddItemToView(item);
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
    
    public void AcceptTransferredItem(Item item)
    {
        AddItemToView(item);
    }

    public override void UpdateObservers(ISubject pSubject)
    {
        if (pSubject.SubjectState == (int)ShopActions.PURCHASED)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.SOLD)
            updateItemList();

        if (pSubject.SubjectState == (int)ShopActions.UPGRADED)
        {
            GameObject upgradedItem = itemList[ShopCreator.Instance.shopModelInventory.GetSelectedItemIndex()];

            ListViewItemContainer itemContainer = upgradedItem.GetComponent<ListViewItemContainer>();
            itemContainer.updateItemDetailsUI();
        }

        updateMoneyPanel();
    }

    private void updateDetailsPanel(int index)
    {
        Debug.Log("Item has been upgraded! Updating details now in ShopListView.");

        if (this.gameObject == null)
            return;

        switch (InventoryInstance)
        {
            case 0:
                currentItem = ShopCreator.Instance.shopModel.GetSelectedItem();
                break;
            case 1:
                currentItem = ShopCreator.Instance.shopModelInventory.GetSelectedItem();
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
        int removedItemIndex = ShopCreator.Instance.shopModel.inventory.RemovedItemIndex;
        itemList.RemoveAt(removedItemIndex);

        Transform child = itemLayoutGroup.transform.GetChild(removedItemIndex);
        Destroy(child.gameObject);
    }
}