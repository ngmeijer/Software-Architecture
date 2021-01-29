﻿using System.Collections;
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

    public static int MoneyCount;

    public void Start()
    {
        MoneyCount = _money;

        shopModel = new BuyModel(_priceModifier, _weaponCountShop, _armorCountShop, _potionCountShop);
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller

        shopModelInventory = new SellModel(_priceModifierInventory, _weaponCountInventory, _armorCountInventory, _potionCountInventory);
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
        instructionText.text = "Controls: WASD to navigate. 1, 2, 3 to buy, upgrade & sell." +
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
}