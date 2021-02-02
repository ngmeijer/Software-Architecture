﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTransporter : MonoBehaviour, IObserver
{
    [SerializeField] private ShopGridView _shopGridView;
    [SerializeField] private ShopListView _shopListView;

    [Space(10)]
    [SerializeField] private ShopGridView _inventoryGridView;
    [SerializeField] private ShopListView _inventoryListView;

    private void Start()
    {
        ShopCreator.Instance.shopModel.Attach(this);
        ShopCreator.Instance.shopModelInventory.Attach(this);
    }

    public void UpdateObservers(ISubject pSubject)
    {
        if (pSubject.SubjectState == (int)ShopActions.PURCHASED)
        {
            int index = ShopCreator.Instance.shopModel.inventory.RemovedItemIndex;

            Item item = pSubject.tradedItem;

            ShopCreator.Instance.shopModelInventory.inventory.AddItemShop(item);
            _inventoryGridView.AcceptTransferredItem(item);
            _inventoryListView.AcceptTransferredItem(item);
        }
    }
}