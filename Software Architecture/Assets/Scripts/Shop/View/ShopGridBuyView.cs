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
public class ShopGridBuyView : ShopBuyView, ISubsciber
{
    //SUBSCRIBER CLASS!
    [SerializeField]
    private GridLayoutGroup itemLayoutGroup; //Links to a GridLayoutGroup in the Unity scene

    [SerializeField]
    private GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();
    private List<GameObject> tempItemList = new List<GameObject>();

    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private Button upgradeButton;

    [SerializeField]
    private Button sellButton;

    [SerializeField]
    private TextMeshProUGUI instructionText;

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

        shopModel.Subscribe(this);
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
        //For some reason, "downcasting" from Item (base class) to, for example, Armor, will throw an InvalidCastException. 
        //However again, for some reason (while the error is still being thrown), 
        //the sorting mechanic does work for Armor, but not for Weapon & Potion?

        //UPDATE: trying to downcast is indeed the problem, when using GetItems for the abstract class Item, 
        //it won't throw the error (but doesn't sort).

        //UPDATE 2: Fixed both the error and sorting problem, but I'm not sure about whether or not the solution is clean enough.

        switch (index)
        {
            case 0:
                foreach (Item item in shopModel.inventory.GetItems())
                {
                    item.ItemIndex = shopModel.inventory.GetItems().IndexOf(item);
                    AddItemToView(item);
                }
                break;

            case 1:
                foreach (Item weapon in shopModel.inventory.GetItems())
                {
                    //ANY possible better way to check what the itemType is, 
                    //besides converting to string for better readability? 
                    //Feel like this isn't very good code.

                    //As tried before, using a foreach with any inherited child of abstract Item gives an InvalidCastException 
                    if (weapon.ItemType == "Weapon")
                        AddItemToView(weapon);
                }
                break;

            case 2:
                foreach (Item armor in shopModel.inventory.GetItems())
                {
                    if (armor.ItemType == "Armor")
                        AddItemToView(armor);
                }
                break;

            case 3:
                foreach (Item potion in shopModel.inventory.GetItems())
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

    public static void DestroyIcon()
    {

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
                    shopController.ConfirmSelectedItem();
                }
            );

        upgradeButton.onClick.AddListener(
            delegate
                {
                    shopController.ConfirmSelectedItem();
                }
            );

        sellButton.onClick.AddListener(
            delegate
                {
                    shopController.ConfirmSelectedItem();
                }
            );
    }

    private void Update()
    {
        //Switch between mouse and keyboard controllers
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (shopController is MouseController)
            {
                SwitchToKeyboardControl();
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (shopController is GridViewKeyboardController)
            {
                SwitchToMouseControl();
            }
        }

        //Let the current controller handle input
        //shopController.HandleInput();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected override void SwitchToKeyboardControl()
    {
        shopController = gameObject.AddComponent<GridViewKeyboardController>().Initialize(shopModel);//Create and add a keyboard controller
        instructionText.text = "The current control mode is: Keyboard Control, WASD to select item, press K to buy. Press left mouse button to switch to Mouse Control.";
        buyButton.gameObject.SetActive(false);//Hide the buy button because we only use keyboard
        upgradeButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToMouseControl()
    //------------------------------------------------------------------------------------------------------------------------ 
    protected override void SwitchToMouseControl()
    {
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Create and add a mouse controller
        instructionText.text = "The current control mode is: Mouse Control, press 'K' to switch to Keyboard Control.";
        buyButton.gameObject.SetActive(true);//Show the buy button for the mouse controller
        upgradeButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(true);
    }

    public void Update(ShopModel model)
    {
        Debug.Log($"Subscriber has been notified. Size of subscriber list: {shopModel.SubscriberList.Count}");
    }
}
