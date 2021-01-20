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
    public Item Item { get; private set; }

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

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item item)
    {
        //Stores the item
        this.Item = item;

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
        itemNameText.text = Item.Name;
        itemDescriptionText.text = Item.Description;
        itemTypeText.text = Item.ItemType;
        itemPriceText.text = Item.BasePrice.ToString();
        itemPropertyText.text = Item.BaseEnchantmentText + Item.BaseEnchantmentValue;
        itemRarityText.text = Item.ItemRarity.ToString();
    }

    public void handlePanelForSelectedItem(int index)
    {
        Debug.Log($"Item index is {(Item.ItemIndex)}");
        if (this.gameObject == null)
            return;

        if (index == Item.ItemIndex)
        {
            Debug.Log($"Attempting to select item with index {(index)}. Correct index is {(this.Item.ItemIndex)}");

            highLight.SetActive(true);
            infoPanel.SetActive(true);

            return;
        }

        highLight.SetActive(false);
        infoPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        ShopModel.OnClick -= handlePanelForSelectedItem;
    }
}