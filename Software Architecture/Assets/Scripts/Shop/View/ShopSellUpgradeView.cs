﻿using UnityEngine;
using UnityEngine.UI;

public class ShopSellUpgradeView : ShopTransactionView
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;

    private void Start()
    {
        InitializeButtons();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  InitializeButtons()
    //------------------------------------------------------------------------------------------------------------------------        
    //This method adds a listener to the 'Buy' button. They are forwarded to the controller. Since this is the confirm button of
    //the buy view, it will just call the controller interface's ConfirmTransactionSelectedItem function, the controller will handle the rest.
    protected override void InitializeButtons()
    {
        upgradeButton.onClick.AddListener(
            delegate
            {
                ShopCreator.Instance.shopControllerInventory.ConfirmSelectedItem(ShopActions.UPGRADED, ShopCreator.Instance.shopModelInventory);
            }
        );

        sellButton.onClick.AddListener(
            delegate
            {
                ShopCreator.Instance.shopControllerInventory.ConfirmSelectedItem(ShopActions.SOLD, ShopCreator.Instance.shopModelInventory);
            }
        );
    }

    private void OnDisable()
    {
        upgradeButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
    }
}