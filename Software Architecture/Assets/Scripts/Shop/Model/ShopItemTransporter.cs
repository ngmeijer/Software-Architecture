using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTransporter : MonoBehaviour, IObserver
{
    [SerializeField] private ShopGridView _shopView;

    [Space(10)]
    [SerializeField] private ShopGridView _inventoryView;

    private Item item;

    private void Start()
    {
        ShopCreator.Instance.shopModel.Attach(this);
        ShopCreator.Instance.inventoryModel.Attach(this);
    }

    public void UpdateObservers(ISubject pSubject)
    {
        item = pSubject.tradedItem;

        if (pSubject.SubjectState == (int)ShopActions.PURCHASED)
        {
            //ShopCreator.Instance.shopModel.inventory.RemoveItemShop(item);
            ShopCreator.Instance.inventoryModel.inventory.AddItemShop(item);
        }

        if (pSubject.SubjectState == (int)ShopActions.SOLD)
        {
            //ShopCreator.Instance.inventoryModel.inventory.RemoveItemShop(item);
            ShopCreator.Instance.shopModel.inventory.AddItemShop(item);
        }
    }
}