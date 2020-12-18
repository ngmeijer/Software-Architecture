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

    public override Armor CreateArmor(int index)
    {
        return new Armor("Armor display name", armorIconNames[index], 5);
    }

    public override Potion CreatePotion(int index)
    {
        return new Potion("Potion display name", potionIconNames[index], 3);
    }

    public override Weapon CreateWeapon(int index)
    {
        return new Weapon("Weapon display name", weaponIconNames[index], 6);
    }

    public override void UpgradeArmor(Armor armor)
    {
        armor.Protection += armor.NewProtectionValue(armor.ItemRarity += 1);
        armor.ItemRarity += 1;
    }

    public override void UpgradePotion(Potion potion)
    {
        potion.PotionEffectAmount += potion.newEffectValue(potion.ItemRarity += 1);
        potion.ItemRarity += 1;
    }

    public override void UpgradeWeapon(Weapon weapon)
    {
        weapon.Damage += weapon.newDamageValue(weapon.ItemRarity += 1);
        weapon.AttackSpeed += weapon.newAttackSpeedValue(weapon.ItemRarity += 1);
        weapon.ItemRarity += 1;
    }
}
