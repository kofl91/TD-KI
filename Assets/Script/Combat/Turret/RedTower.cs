using UnityEngine;
using System.Collections;

public class RedTower : BaseTurret
{
    // Use this for initialization
    RedTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.fire = 3;
    }
}
