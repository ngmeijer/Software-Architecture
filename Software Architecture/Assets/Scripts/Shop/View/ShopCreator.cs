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

        MoneyCount = _money;

        shopModel = new BuyModel(_priceModifier, _weaponCountShop, _armorCountShop, _potionCountShop);
        inventoryModel = new SellModel(_priceModifierInventory, _weaponCountInventory, _armorCountInventory, _potionCountInventory);

        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller
        inventoryController = gameObject.AddComponent<MouseController>().Initialize(inventoryModel);
    }

    #endregion

    public ShopModel shopModel { get; private set; }
    public ShopController shopController { get; protected set; }

    public ShopModel inventoryModel { get; private set; }
    public ShopController inventoryController { get; protected set; }
    private ShopController _shopTracker;
    private ShopModel _modelTracker;

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

    private void Start()
    {
        SetActiveShop(0);
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

        else if (Input.GetMouseButtonUp(1))
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
        instructionText.text = "Controls: WASD to navigate. \nShop: 1 to buy. Inventory: 2, 3 to upgrade & sell." + "\nRight Mouse button to switch to MouseControl";

        _shopTracker = gameObject.AddComponent<GridViewKeyboardController>().Initialize(_modelTracker);

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

        _shopTracker = gameObject.AddComponent<MouseController>().Initialize(_modelTracker);

        GridViewKeyboardController[] controllersFound = GetComponents<GridViewKeyboardController>();
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

    public void SetActiveShop(int index)
    {
        CurrentActiveShop = index;

        switch (CurrentActiveShop)
        {
            case 0:
                _shopTracker = shopController;
                _modelTracker = shopModel;
                break;
            case 1:
                _shopTracker = inventoryController;
                _modelTracker = inventoryModel;
                break;
        }
    }
}