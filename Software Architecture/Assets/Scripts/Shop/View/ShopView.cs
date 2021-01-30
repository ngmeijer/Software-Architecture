using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ShopView : MonoBehaviour, IObserver
{
    [SerializeField] protected  GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField] protected List<GameObject> itemList = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI moneyText;
    protected ViewConfig viewConfig;

    [SerializeField] protected int InventoryInstance = 0;

    private void Start()
    {
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);

        Inventory.OnMoneyChanged += updateMoneyPanel;

        updateMoneyPanel();
    }

    public abstract void UpdateObservers(ISubject pSubject);

    protected void updateMoneyPanel()
    {
        moneyText.text = ShopCreator.MoneyCount.ToString();
    }
}