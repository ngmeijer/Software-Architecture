﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory
{
    public abstract Weapon CreateWeapon();

    public abstract Armor CreateArmor();

    public abstract Potion CreatePotion();
}