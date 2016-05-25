using UnityEngine;
using System.Collections;

public class IceTower : BaseTurret {

    protected int dmg = 0;

    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        ShootBullet(t);
    }

    // Use this for initialization
    IceTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.normal = dmg;
    }

    public override int getCost()
    {
        return 50;
    }
}
