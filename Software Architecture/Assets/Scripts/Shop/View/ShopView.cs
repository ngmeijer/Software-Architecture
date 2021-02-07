using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopView : MonoBehaviour, IObserver
{
    [SerializeField] protected GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField] protected TextMeshProUGUI moneyText;
    protected ViewConfig viewConfig;

    [SerializeField] protected int InventoryInstance = 0;
    protected ShopModel usedModel;
    protected List<GameObject> _itemList = new List<GameObject>();

    private void Start()
    {
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);

        Inventory.OnMoneyChanged += updateMoneyPanel;

        updateMoneyPanel();
    }

    public abstract void UpdateObservers(ISubject pSubject);

    public abstract void RepopulateItemIconView();

    protected void updateMoneyPanel()
    {
        moneyText.text = ShopCreator.MoneyCount.ToString();
    }
    protected abstract void populateItemIconView();
    protected abstract void clearIconView();
    protected abstract void addItemToView(Item item);
    protected abstract void updateItemList();

    private void OnEnable()
    {
        if (usedModel != null)
        {
            RepopulateItemIconView();
        }
        else
        {
            Debug.Log("usedModel is null");
        }
    }
}