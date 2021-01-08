using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public abstract class ViewItemContainer : MonoBehaviour
{
    //Link to the highlight image (set in prefab)
    public abstract GameObject highLight { get; set; }

    //Link to the infomation panel (set in prefab)
    public abstract GameObject infoPanel { get; set; }

    public abstract Image icon { get; set; }
    public abstract TextMeshProUGUI itemNameText { get; set; }
    public abstract TextMeshProUGUI itemTypeText { get; set; }
    public abstract TextMeshProUGUI itemRarityText { get; set; }
    public abstract TextMeshProUGUI itemPriceText { get; set; }
    public abstract TextMeshProUGUI itemDescriptionText { get; set; }
    public abstract TextMeshProUGUI itemPropertyText { get; set; }

    //Link to the atlas of all the item icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    public abstract SpriteAtlas iconAtlas { get; set; }

    //link to the original item (set in Initialize)
    public abstract Item Item { get; set; }

    public abstract void Initialize(Item pItem);
    public abstract void updateItemDetailsUI();
    public abstract void handlePanelForSelectedItem(int index);
}