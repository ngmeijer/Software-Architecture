using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTransporter : MonoBehaviour, IObserver
{
    [SerializeField] private ShopGridView _shopGridView;
    [SerializeField] private ShopListView _shopListView;

    [Space(10)]
    [SerializeField] private ShopGridView _inventoryGridView;
    [SerializeField] private ShopListView _inventoryListView;

    private int index;
    private Item item;

    private void Start()
    {
        ShopCreator.Instance.shopModel.Attach(this);
        ShopCreator.Instance.shopModelInventory.Attach(this);
    }

    public void UpdateObservers(ISubject pSubject)
    {
        item = pSubject.tradedItem;

        if (pSubject.SubjectState == (int)ShopActions.PURCHASED)
        {
            index = ShopCreator.Instance.shopModel.inventory.RemovedItemIndex;

            ShopCreator.Instance.shopModelInventory.inventory.AddItemShop(item);
            _inventoryGridView.AcceptTransferredItem(item);
            _inventoryListView.AcceptTransferredItem(item);
        }

        if (pSubject.SubjectState == (int) ShopActions.SOLD)
        {
            index = ShopCreator.Instance.shopModelInventory.inventory.RemovedItemIndex;

            ShopCreator.Instance.shopModel.inventory.AddItemShop(item);
            _shopGridView.AcceptTransferredItem(item);
            _shopListView.AcceptTransferredItem(item);
        }
    }
}