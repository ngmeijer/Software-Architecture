using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : IItemFactory
{
    public Item CreateItem()
    {
        EItemRarity rarity = ReturnRarity();
        Weapon weaponInstance = new Weapon(rarity);

        return weaponInstance;
    }

    public EItemRarity ReturnRarity()
    {
        EItemRarity rarity = (EItemRarity)Random.Range((float)EItemRarity.COMMON, (float)EItemRarity.LEGENDARY + 1);
        return rarity;
    }

    public void UpgradeItem(Item item)
    {
        throw new System.NotImplementedException();
    }
}
