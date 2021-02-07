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
            //Add the item to the opposing view (Inventory, because it was bought)
            ShopCreator.Instance.inventoryModel.inventory.AddItemShop(item);
        }

        if (pSubject.SubjectState == (int)ShopActions.SOLD)
        {
            //Add the item to the opposing view (Shop, because it was sold)
            ShopCreator.Instance.shopModel.inventory.AddItemShop(item);
        }
    }
}