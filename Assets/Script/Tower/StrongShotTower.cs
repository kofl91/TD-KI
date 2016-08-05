using UnityEngine;
using System.Collections;
using System;

public class StrongShotTower : BaseTower {

    protected int dmg = 20;

    // Use this for initialization
    StrongShotTower()
    {
        turretDmg.normal = dmg;
        range = 2.5f * range;
        cooldown = 4.5f * cooldown;
        buildCost = 80;
    }

}
