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
public class GridViewItemContainer : MonoBehaviour, IItemContainer
{
    public Item Item => item;//Public getter for the item, required by IItemContainer interface.

    //Link to the highlight image (set in prefab)
    [SerializeField] private GameObject highLight;

    //Link to the infomation panel (set in prefab)
    [SerializeField] private GameObject infoPanel;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemPropertyText;

    //Link to the atlas of all the item icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    [SerializeField] private SpriteAtlas iconAtlas;

    //link to the original item (set in Initialize)
    private Item item;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item item)
    {
        //Stores the item
        this.item = item;

        // Clones the first Sprite in the icon atlas that matches the iconName and uses it as the sprite of the icon image.
        Sprite sprite = iconAtlas.GetSprite(item.IconName);

        if (sprite != null)
        {
            icon.sprite = sprite;
        }

        updateItemDetailsUI();

        ShopModel.OnClick += handlePanelForSelectedItem;
    }

    private void updateItemDetailsUI()
    {
        itemNameText.text = item.Name;
        itemDescriptionText.text = item.Description;
        itemTypeText.text = item.ItemType;
        itemPriceText.text = item.BasePrice.ToString();
        itemPropertyText.text = item.BaseEnchantmentText + item.BaseEnchantmentValue;
        itemRarityText.text = item.ItemRarity.ToString();
    }

    public void handlePanelForSelectedItem(int index)
    {
        if (this.gameObject == null)
            return;

        Debug.Log("handling focus.");

        if (index == item.ItemIndex)
        {
            highLight.SetActive(true);
            infoPanel.SetActive(true);
        }
        else
        {
            highLight.SetActive(false);
            infoPanel.SetActive(false);
        }
    }


}
