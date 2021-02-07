using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemRarity
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY
};

public interface IItemFactory
{
    Item CreateItem();
    EItemRarity ReturnRarity();
}