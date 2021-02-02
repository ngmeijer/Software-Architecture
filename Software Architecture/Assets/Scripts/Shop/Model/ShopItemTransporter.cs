using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTransporter : MonoBehaviour, IObserver
{
    [SerializeField] private ShopGridView _shopView;

    [Space(10)]
    [SerializeField] private ShopGridView _inventoryView;

    private int index;
    private Item item;
    private ShopModel usedModel;


    private void Start()
    {
        ShopCreator.Instance.shopModel.Attach(this);
        ShopCreator.Instance.inventoryModel.Attach(this);
    }

    public void UpdateObservers(ISubject pSubject)
    {
        item = pSubject.tradedItem;

        if (pSubject.SubjectState == (int) ShopActions.PURCHASED)
        {
            usedModel = ShopCreator.Instance.inventoryModel;
        }

        if (pSubject.SubjectState == (int) ShopActions.SOLD)
        {
            usedModel = ShopCreator.Instance.shopModel;
        }

        usedModel.inventory.AddItemShop(item);

        _shopView.RepopulateItemIconView();
        _inventoryView.RepopulateItemIconView();
    }
}