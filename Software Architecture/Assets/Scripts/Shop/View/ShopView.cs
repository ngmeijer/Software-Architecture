using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    #region Singleton

    public static ShopView Instance;

    public ShopModel shopModel { get; private set; }
    public  ShopController shopController { get; protected set; }

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

    public delegate void OnInputMethodSwitch(int index);
    public static OnInputMethodSwitch OnInputSwitch;

    public void Start()
    {
        shopModel = new BuyModel(2f, 16, 25); //Right now use magic values to set up the shop
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller
    }

    public void SwitchInputMethod(int index)
    {
        Destroy(shopController);

        if (index == 0)
        {
            shopController = gameObject.AddComponent<GridViewKeyboardController>().Initialize(shopModel);
        }

        if (index == 1)
        {
            shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);
        }
    }
}