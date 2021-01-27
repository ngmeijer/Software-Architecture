using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    #region Singleton

    public static ShopView Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of ShopView found.");
            return;
        }

        Instance = this;
    }

    #endregion

    public ShopModel shopModel { get; private set; }
    public ShopController shopController { get; protected set; }

    public ShopModel shopModelInventory { get; private set; }
    public ShopController shopControllerInventory { get; protected set; }

    [Header("Shop")]
    [SerializeField] private float _priceModifier = 2f;
    [SerializeField] private int _itemCount = 16;
    [SerializeField] private int _money = 250;

    [Header("Inventory")] 
    [SerializeField] private float _priceModifierInventory = 2f;

    [SerializeField] private int _itemCountInventory = 8;
    [SerializeField] private int _moneyInventory = 0;

    [Space]
    [SerializeField] private TextMeshProUGUI instructionText;

    public void Start()
    {
        shopModel = new BuyModel(_priceModifier, _itemCount, _money); 
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller

        shopModelInventory = new SellModel(_priceModifier, _itemCount, _money);
        shopControllerInventory = gameObject.AddComponent<MouseController>().Initialize(shopModelInventory);
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

            if (shopControllerInventory is MouseController)
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

            if (shopControllerInventory is GridViewKeyboardController)
            {
                SwitchToMouseControl();
            }
        }

        //Let the current controller handle input
        shopController.HandleInput();
        shopControllerInventory.HandleInput();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected void SwitchToKeyboardControl()
    {
        instructionText.text = "The current control mode is: Keyboard Control, WASD to select item, press K to buy. Press left mouse button to switch to Mouse Control.";
        //buyButton.gameObject.SetActive(false);//Show the buy button for the mouse controller
        shopController = gameObject.AddComponent<GridViewKeyboardController>().Initialize(shopModel);
        shopControllerInventory = gameObject.AddComponent<GridViewKeyboardController>().Initialize(shopModelInventory);

        MouseController[] controllersFound = FindObjectsOfType<MouseController>();
        foreach (MouseController controller in controllersFound)
        {
            Destroy(controller);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToMouseControl()
    //------------------------------------------------------------------------------------------------------------------------ 
    protected void SwitchToMouseControl()
    {
        instructionText.text = "The current control mode is: Mouse Control, press 'K' to switch to Keyboard Control.";
        //buyButton.gameObject.SetActive(true);//Show the buy button for the mouse controller
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);
        shopControllerInventory = gameObject.AddComponent<MouseController>().Initialize(shopModelInventory);

        GridViewKeyboardController[] controllersFound = FindObjectsOfType<GridViewKeyboardController>();
        foreach (GridViewKeyboardController controller in controllersFound)
        {
            Destroy(controller);
        }
    }
}