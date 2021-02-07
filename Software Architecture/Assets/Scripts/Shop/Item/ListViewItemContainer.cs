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
public class ListViewItemContainer : ViewItemContainer
{
    public override Item Item { get; set; } //Public getter for the pItem, required by IItemContainer interface.

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public override void Initialize(Item pItem)
    {
        Item = pItem;
        UpdateItemDetailsUI();
        ShopModel.OnSelect += HandlePanelForSelectedItem;
    }

    public override void UpdateItemDetailsUI()
    {
        itemNameText.text = Item.Name;
        itemPriceText.text = Item.BasePrice.ToString();
        itemTypeText.text = Item.ItemType;
        itemRarityText.text = Item.ItemRarity.ToString();

        Sprite sprite = iconAtlas.GetSprite(Item.IconName);
        Item.ItemSprite = sprite;

        icon.sprite = Item.ItemSprite;
    }

    public override void HandlePanelForSelectedItem(int index)
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

    private void OnDestroy()
    {
        ShopModel.OnSelect -= HandlePanelForSelectedItem;
    }
}