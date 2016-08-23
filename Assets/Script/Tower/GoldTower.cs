using UnityEngine;
using System.Collections;

public class GoldTower : BaseTower {

    protected int dmg = 1;

    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        if (!owner)
        {
            owner = GetComponentInParent<PlayerController>();
            return;
        }
        owner.Gold += 1;
    }

    // Use this for initialization
    GoldTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.normal = dmg;
        range = 2.5f * range;
        cooldown = 1.0f * cooldown;
        buildCost = 100;
    }
}
