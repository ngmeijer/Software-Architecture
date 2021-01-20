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

    [SerializeField] private Button buyButton;

    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private TextMeshProUGUI instructionText;

    private ViewConfig viewConfig; //To set up the grid view, we need to know how many columns the grid view has, in the current setup,
                                   //this information can be found in a ViewConfig scriptable object, which serves as a configuration file for
                                   //views.

    private void Start()
    {
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        SetupItemIconView(); //Setup the grid view's properties
        PopulateItemIconView(0); //Display all items
        InitializeButtons(); //Connect the buttons to the controller

        Inventory.OnMoneyChanged += updateMoneyPanel;
        ShopView.Instance.shopModel.Attach(this);
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
        itemLayoutGroup.cellSize = new Vector2(50, 50);
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
        switch (index)
        {
            case 0:
                foreach (Item item in ShopView.Instance.shopModel.inventory.GetItems())
                {
                    AddItemToView(item);
                }
                break;

            case 1:
                foreach (Item weapon in ShopView.Instance.shopModel.inventory.GetItems())
                {
                    if (weapon.ItemType == "Weapon")
                        AddItemToView(weapon);
                }
                break;

            case 2:
                foreach (Item armor in ShopView.Instance.shopModel.inventory.GetItems())
                {
                    if (armor.ItemType == "Armor")
                        AddItemToView(armor);
                }
                break;

            case 3:
                foreach (Item potion in ShopView.Instance.shopModel.inventory.GetItems())
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

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  InitializeButtons()
    //------------------------------------------------------------------------------------------------------------------------        
    //This method adds a listener to the 'Buy' button. They are forwarded to the controller. Since this is the confirm button of
    //the buy view, it will just call the controller interface's ConfirmSelectedItem function, the controller will handle the rest.
    private void InitializeButtons()
    {
        buyButton.onClick.AddListener(
            delegate
                {
                    ShopView.Instance.shopController.ConfirmSelectedItem();
                }
            );
    }

    private void Update()
    {
        //Switch between mouse and keyboard controllers
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (ShopView.Instance.shopController is MouseController)
            {
                SwitchToKeyboardControl();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (ShopView.Instance.shopController is GridViewKeyboardController)
            {
                SwitchToMouseControl();
            }
        }

        //Let the current controller handle input
        ShopView.Instance.shopController.HandleInput();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected void SwitchToKeyboardControl()
    {
        instructionText.text = "The current control mode is: Keyboard Control, WASD to select item, press K to buy. Press left mouse button to switch to Mouse Control.";
        buyButton.gameObject.SetActive(false);
        ShopView.OnInputSwitch(0);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToMouseControl()
    //------------------------------------------------------------------------------------------------------------------------ 
    protected void SwitchToMouseControl()
    {
        instructionText.text = "The current control mode is: Mouse Control, press 'K' to switch to Keyboard Control.";
        buyButton.gameObject.SetActive(true);
        ShopView.OnInputSwitch(1);
    }

    public void UpdateObservers(ISubject subject)
    {
        updateItemList();
        updateMoneyPanel();
    }

    private void updateItemList()
    {
        int removedItemIndex = ShopView.Instance.shopModel.inventory.GetRemovedItemIndex();
        itemList.RemoveAt(removedItemIndex);

        Transform child = itemLayoutGroup.transform.GetChild(removedItemIndex);
        GridViewItemContainer container = child.GetComponent<GridViewItemContainer>();
        Destroy(child.gameObject);

        Debug.Log($"Should have removed item with index {removedItemIndex}.");
    }

    private void updateMoneyPanel()
    {
        moneyText.text = ShopView.Instance.shopModel.inventory.Money.ToString();
    }
}