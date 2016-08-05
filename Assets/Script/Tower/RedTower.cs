using UnityEngine;
using System.Collections;
using System;

public class RedTower : BaseTower
{ 
    // Use this for initialization
    RedTower()
    {
        buildCost = 40;
        turretDmg = new DamageInfo();
        turretDmg.fire = 3;
    }
}
