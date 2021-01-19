using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

/// <summary>
/// This class is applied to a button that represents an Item in the View. It is a visual representation of the pItem
/// when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to
/// the one specified in the Item data, and it enables or disables the infoPanel to indicate if the pItem is selected
/// and display the details of the pItem.
/// </summary>
public class ListViewItemContainer : MonoBehaviour, IItemContainer
{
    public Item Item { get; private set; } //Public getter for the pItem, required by IItemContainer interface.

    //Link to the highlight image (set in prefab)
    [SerializeField] private GameObject highLight;
    [SerializeField] private GameObject infoPanel;

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    //Link to the atlas of all the pItem icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    [SerializeField] private SpriteAtlas iconAtlas;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item pItem)
    {
        //Stores the pItem
        this.Item = pItem;

        Sprite sprite = iconAtlas.GetSprite(Item.IconName);
        Item.ItemSprite = sprite;

        updateItemDetailsUI();

        ShopModel.OnClick += handlePanelForSelectedItem;
    }

    public void updateItemDetailsUI()
    {
        itemNameText.text = Item.Name;
        itemPriceText.text = Item.BasePrice.ToString();
        itemTypeText.text = Item.ItemType;
        itemRarityText.text = Item.ItemRarity.ToString();
    }

    public void handlePanelForSelectedItem(int index)
    {
        if (this.gameObject == null)
            return;

        if (index == Item.ItemIndex)
        {
            highLight.SetActive(true);
        }
        else
        {
            highLight.SetActive(false);
        }
    }
}