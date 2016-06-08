using UnityEngine;
using System.Collections;
using System;

public class StrongShotTower : BaseTurret {

    protected int dmg = 20;

    // Use this for initialization
    StrongShotTower()
    {
        turretDmg.normal = dmg;
        range = 2.5f * range;
        cooldown = 4.5f * cooldown;
        goldCost = 80;
    }

    public override int getCost()
    {
        return 80;
    }
}
