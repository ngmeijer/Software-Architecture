using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCreator : MonoBehaviour
{
    #region Singleton

    public static ShopCreator Instance;

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
    private ShopController _shopTracker;

    [Header("Shop")]
    [SerializeField] private float _priceModifier = 2f;
    [Range(0, 50)] [SerializeField] private int _weaponCountShop;
    [Range(0, 50)] [SerializeField] private int _armorCountShop;
    [Range(0, 50)] [SerializeField] private int _potionCountShop;

    [Header("Inventory")]
    [SerializeField] private float _priceModifierInventory = 2f;
    [Range(0, 50)] [SerializeField] private int _weaponCountInventory;
    [Range(0, 50)] [SerializeField] private int _armorCountInventory;
    [Range(0, 50)] [SerializeField] private int _potionCountInventory;

    [Space]
    [SerializeField] private int _money = 250;

    [Space]
    [SerializeField] private TextMeshProUGUI instructionText;

    public int CurrentActiveShop = 0;

    public static int MoneyCount;

    public void Start()
    {
        MoneyCount = _money;

        shopModel = new BuyModel(_priceModifier, _weaponCountShop, _armorCountShop, _potionCountShop);
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller

        shopModelInventory = new SellModel(_priceModifierInventory, _weaponCountInventory, _armorCountInventory, _potionCountInventory);
        shopControllerInventory = gameObject.AddComponent<MouseController>().Initialize(shopModelInventory);

        SwitchActiveShop(0);
    }

    private void Update()
    {
        //Switch between mouse and keyboard controllers
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (_shopTracker is MouseController)
            {
                SwitchToKeyboardControl();
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (_shopTracker is GridViewKeyboardController)
            {
                SwitchToMouseControl();
            }
        }

        //Let the current controller handle input
        _shopTracker.HandleInput();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected void SwitchToKeyboardControl()
    {
        instructionText.text = "Controls: WASD to navigate. \nShop: 1 to buy. Inventory: 2, 3 to upgrade & sell." +
                               "\nLeft Mouse button to switch to MouseControl";
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
        instructionText.text = "Controls: Mouse Control. \nPress 'K' to switch to Keyboard Control.";
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);
        shopControllerInventory = gameObject.AddComponent<MouseController>().Initialize(shopModelInventory);

        GridViewKeyboardController[] controllersFound = FindObjectsOfType<GridViewKeyboardController>();
        foreach (GridViewKeyboardController controller in controllersFound)
        {
            Destroy(controller);
        }
    }

    public static int CalculateBalance(int change)
    {
        MoneyCount += change;

        return MoneyCount;
    }

    public void SwitchActiveShop(int index)
    {
        CurrentActiveShop = index;

        switch (CurrentActiveShop)
        {
            case 0:
                _shopTracker = shopController;
                break;
            case 1:
                _shopTracker = shopControllerInventory;
                break;
        }
    }
}