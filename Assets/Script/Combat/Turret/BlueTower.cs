using UnityEngine;
using System.Collections;
using System;

public class BlueTower : BaseTurret
{
    // Use this for initialization
    BlueTower()
    {
        goldCost = 40;
        turretDmg.water = 3;
        turretDmg.water = 3;
    }
    
    public override int getCost()
    {
        return 40;
    }
}
