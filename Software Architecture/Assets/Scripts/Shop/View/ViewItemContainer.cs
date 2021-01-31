using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public abstract class ViewItemContainer : MonoBehaviour, IItemContainer
{
    //Link to the highlight image (set in prefab)
    [SerializeField] protected GameObject highLight;

    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI itemNameText;
    [SerializeField] protected TextMeshProUGUI itemTypeText;
    [SerializeField] protected TextMeshProUGUI itemRarityText;
    [SerializeField] protected TextMeshProUGUI itemPriceText;

    //Link to the atlas of all the item icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    [SerializeField] protected SpriteAtlas iconAtlas;

    //link to the original item (set in Initialize)
    public abstract Item Item { get; set; }

    public abstract void Initialize(Item pItem);

    public abstract void updateItemDetailsUI();
    public abstract void handlePanelForSelectedItem(int index);
}