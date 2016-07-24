using UnityEngine;
using System.Collections;
using System;

public class RedTower : BaseTurret
{ 
    // Use this for initialization
    RedTower()
    {
        goldCost = 40;
        turretDmg = new DamageInfo();
        turretDmg.fire = 3;
    }

    public override int getCost()
    {
        return goldCost;
    }
}
