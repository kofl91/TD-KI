﻿using UnityEngine;
using System.Collections;

public class GoldTower : BaseTurret {

    protected int dmg = 1;

    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        owner.Gold += 1;
    }

    // Use this for initialization
    GoldTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.normal = dmg;
        range = 2.5f * range;
        cooldown = 1.0f * cooldown;
        goldCost = 100;
    }


    public override int getCost()
    {
        return goldCost;
    }
}
