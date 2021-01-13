using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    public ShopModel shopModel { get; private set; }
    public ShopController shopController { get; protected set; } //Controller in MVC pattern

    public void Awake()
    {
        shopModel = new BuyModel(2f, 16, 25); //Right now use magic values to set up the shop
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller
    }
}