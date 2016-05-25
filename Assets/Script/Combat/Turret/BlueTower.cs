using UnityEngine;
using System.Collections;

public class BlueTower : BaseTurret
{
    // Use this for initialization
    BlueTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.amount = dmg;

        goldCost = 40;
        turretDmg.water = 3;
        turretDmg.water = 3;
    }
}
