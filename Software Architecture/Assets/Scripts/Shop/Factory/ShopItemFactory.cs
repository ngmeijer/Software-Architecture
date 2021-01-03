using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemFactory : ItemFactory
{
    private string[] armorIconNames = new string[5]
    {
        "items_103",
        "items_104",
        "items_105",
        "items_106",
        "items_107",
    };

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

    public override Armor CreateArmor()
    {
        Item.E_ItemRarity itemRarity = (Item.E_ItemRarity)Random.Range((float)Item.E_ItemRarity.COMMON, (float)Item.E_ItemRarity.LEGENDARY);
        Armor armor = new Armor(itemRarity);

        Debug.Log($"Item instance  has Rarity {itemRarity}");

        return armor;
        //return new Armor("Armor display name", armorIconNames[index], 5);
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
        //weapon.Damage += weapon.newDamageValue(weapon.ItemRarity += 1);
        //weapon.AttackSpeed += weapon.newAttackSpeedValue(weapon.ItemRarity += 1);
        //weapon.ItemRarity += 1;

        return weapon;
    }
}
