using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

/// <summary>
/// This class is applied to a button that represents an Item in the View. It is a visual representation of the item
/// when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to
/// the one specified in the Item data, and it enables or disables the infoPanel to indicate if the item is selected
/// and display the details of the item.
/// </summary>
public class GridViewItemContainer : ViewItemContainer
{
    public override Item Item { get; set; }

    //Link to the infomation panel (set in prefab)
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPropertyText;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public override void Initialize(Item pItem)
    {
        Item = pItem;
        updateItemDetailsUI();
        ShopModel.OnSelect += handlePanelForSelectedItem;
    }

    public override void updateItemDetailsUI()
    {
        itemNameText.text = Item.Name;
        itemDescriptionText.text = Item.Description;
        itemTypeText.text = Item.ItemType;
        itemPriceText.text = Item.BasePrice.ToString();
        itemPropertyText.text = Item.BaseEnchantmentText + Item.BaseEnchantmentValue;
        itemRarityText.text = Item.ItemRarity.ToString();

        // Clones the first Sprite in the icon atlas that matches the iconName and uses it as the sprite of the icon image.
        Sprite sprite = iconAtlas.GetSprite(Item.IconName);
        if (sprite != null)
        {
            icon.sprite = sprite;
            Item.ItemSprite = sprite;
        }
    }

    public override void handlePanelForSelectedItem(int index)
    {
        if (this.gameObject == null)
            return;

        if (index == Item.ItemIndex)
        {
            highLight.SetActive(true);
            infoPanel.SetActive(true);

            return;
        }

        highLight.SetActive(false);
        infoPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        ShopModel.OnSelect -= handlePanelForSelectedItem;
    }
}