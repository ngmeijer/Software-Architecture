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
public class ListViewItemContainer : ViewItemContainer, IItemContainer
{
    public override Item Item { get; set; } //Public getter for the pItem, required by IItemContainer interface.

    //Link to the highlight image (set in prefab)
    public override GameObject highLight { get; set; }

    //Link to the infomation panel (set in prefab)
    public override GameObject infoPanel { get; set; }

    public override Image icon { get; set; }
    public override TextMeshProUGUI itemNameText { get; set; }
    public override TextMeshProUGUI itemTypeText { get; set; }
    public override TextMeshProUGUI itemRarityText { get; set; }
    public override TextMeshProUGUI itemPriceText { get; set; }
    public override TextMeshProUGUI itemDescriptionText { get; set; }
    public override TextMeshProUGUI itemPropertyText { get; set; }

    //Link to the atlas of all the pItem icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    public override SpriteAtlas iconAtlas { get; set; }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item pItem)
    {
        //Stores the pItem
        this.Item = pItem;

        // Clones the first Sprite in the icon atlas that matches the iconName and uses it as the sprite of the icon image.
        Sprite sprite = iconAtlas.GetSprite(pItem.IconName);

        if (sprite != null)
        {
            icon.sprite = sprite;
        }

        updateItemDetailsUI();

        ShopModel.OnClick += handlePanelForSelectedItem;
    }

    private void updateItemDetailsUI()
    {
        itemNameText.text = Item.Name;
        itemDescriptionText.text = Item.Description;
        itemTypeText.text = Item.ItemType;
        itemPriceText.text = Item.BasePrice.ToString();
        itemPropertyText.text = Item.BaseEnchantmentText + Item.BaseEnchantmentValue;
        itemRarityText.text = Item.ItemRarity.ToString();
    }

    public void handlePanelForSelectedItem(int index)
    {
        if (this.gameObject == null)
            return;

        Debug.Log("handling focus.");

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
