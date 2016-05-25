using UnityEngine;
using System.Collections;

public class SplashTower : BaseTurret {

    protected int dmg = 1;

    // Use this for initialization
    SplashTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.normal = dmg;
        range = 1.0f * range;
        cooldown = 1.0f * cooldown;
        goldCost = 50;
    }
}
