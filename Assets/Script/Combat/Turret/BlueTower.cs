using UnityEngine;
using System.Collections;

public class BlueTower : BaseTurret
{
    // Use this for initialization
    BlueTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.water = 3;
    }
}
