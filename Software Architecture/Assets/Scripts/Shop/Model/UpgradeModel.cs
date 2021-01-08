using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModel : ShopModel
{
    private WeaponFactory weaponFactory;
    private ArmorFactory armorFactory;
    private PotionFactory potionFactory;

    public UpgradeModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {
        weaponFactory = new WeaponFactory();
        armorFactory = new ArmorFactory();
        potionFactory = new PotionFactory();
    }

    public override List<ISubsciber> SubscriberList { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int MainState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void ConfirmSelectedItem()
    {
        upgradeSelectedItem();
    }

    public override void MainBussinessLogic()
    {
        throw new System.NotImplementedException();
    }

    public override void NotifySubscribers()
    {
        throw new System.NotImplementedException();
    }

    public override void Subscribe(ISubsciber subsciber)
    {
        throw new System.NotImplementedException();
    }

    public override void Unsubscribe(ISubsciber subscriber)
    {
        throw new System.NotImplementedException();
    }

    private void upgradeSelectedItem()
    {
        Item currentItem = inventory.GetItemByIndex(selectedItemIndex);
        Debug.Log("upgrading current item");
        //switch (currentItem.ItemType)
        //{
        //    case "Weapon":
        //        weaponFactory.UpgradeWeapon((Weapon) currentItem);
        //        break;
        //    case "Armor":
        //        armorFactory.UpgradeArmor((Armor) currentItem);
        //        break;
        //    case "Potion":
        //        potionFactory.UpgradePotion((Potion) currentItem);
        //        //inventory.Money -= currentItem.BasePrice;
        //        break;
        //}
    }
}