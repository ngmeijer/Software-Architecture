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
public class ShopListView : MonoBehaviour, IObserver
{
    //SUBSCRIBER CLASS!
    [SerializeField] private VerticalLayoutGroup itemLayoutGroup; //Links to a VerticalLayoutGroup in the Unity scene

    [SerializeField] private GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField] private List<GameObject> itemList = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI moneyText;

    private ViewConfig viewConfig; //To set up the grid view, we need to know how many columns the grid view has, in the current setup,
                                   //this information can be found in a ViewConfig scriptable object, which serves as a configuration file for
                                   //views.

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemRarity;
    [SerializeField] private Image itemIcon;

    private void Start()
    {
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        PopulateItemIconView(0); //Display all items

        ShopView.Instance.shopModel.Attach(this);

        ShopModel.OnClick += updateDetailsPanel;
        Inventory.OnMoneyChanged += updateMoneyPanel;
        updateMoneyPanel();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RepopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the grid view and repopulates it with new icons (updates the visible icons)
    public void RepopulateItemIconView(int index)
    {
        ClearIconView();
        PopulateItemIconView(index);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    private void PopulateItemIconView(int index)
    {
        foreach (Item item in ShopView.Instance.shopModel.inventory.GetItems())
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

    public void UpdateObservers(ISubject pSubject)
    {
        if (pSubject.ListHasChanged)
            updateItemList();

        updateMoneyPanel();
    }

    private void updateDetailsPanel(int index)
    {
        if (this.gameObject == null)
            return;

        Item currentItem = ShopView.Instance.shopModel.GetSelectedItem();

        itemName.text = currentItem.Name;
        itemDescription.text = currentItem.Description;
        itemType.text = currentItem.ItemType;
        itemPrice.text = currentItem.BasePrice.ToString();
        itemRarity.text = currentItem.ItemRarity.ToString();
        itemIcon.sprite = currentItem.ItemSprite;
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
        moneyText.text = ShopView.Instance.shopModel.inventory.Money.ToString();
    }
}