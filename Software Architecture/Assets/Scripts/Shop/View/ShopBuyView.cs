using UnityEngine;
using UnityEngine.UI;

public class ShopBuyView : ShopTransactionView
{
    [SerializeField] private Button buyButton;

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
        buyButton.onClick.AddListener(
            delegate
            {
                ShopView.Instance.shopController.ConfirmSelectedItem(ShopActions.PURCHASED);
            }
        );
    }
}