using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemFactory : ItemFactory
{

    private string[] weaponIconNames = new string[6]
    {
        "items_73",
        "items_74",
        "items_75",
        "items_76",
        "items_77",
        "items_78"
    };

    private string[] potionIconNames = new string[4]
    {
        "items_285",
        "items_286",
        "items_287",
        "items_288",
    };

    private List<Item.E_ItemRarity> existingRarities = new List<Item.E_ItemRarity>();

    private static Item.E_ItemRarity generateRarity()
    {
        Item.E_ItemRarity rarity = (Item.E_ItemRarity)Random.Range((float)Item.E_ItemRarity.COMMON, (float)Item.E_ItemRarity.LEGENDARY + 1);
        return rarity;
    }

    public override Armor CreateArmor()
    {
        Item.E_ItemRarity rarity = generateRarity();
        Armor armor = new Armor(rarity);

        return armor;
    }

    public override Potion CreatePotion(int index)
    {
        return new Potion("Potion display name", potionIconNames[index], 3);
    }

    public override Weapon CreateWeapon(int index)
    {
        return new Weapon("Weapon display name", weaponIconNames[index], 6);
    }

    public override Armor UpgradeArmor(Armor armor)
    {
        //armor.Protection += armor.NewProtectionValue(armor.ItemRarity += 1);
        //armor.ItemRarity += 1;

        return armor;
    }

    public override Potion UpgradePotion(Potion potion)
    {
        //potion.PotionEffectAmount += potion.newEffectValue(potion.ItemRarity += 1);
        //potion.ItemRarity += 1;

        return potion;
    }

    public override Weapon UpgradeWeapon(Weapon weapon)
    {
        //weapon._damage += weapon.newDamageValue(weapon.ItemRarity += 1);
        //weapon.AttackSpeed += weapon.newAttackSpeedValue(weapon.ItemRarity += 1);
        //weapon.ItemRarity += 1;

        return weapon;
    }
}
