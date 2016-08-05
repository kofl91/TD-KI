using UnityEngine;
using System.Collections;

public class GreenTower : BaseTower
{
    protected int dmg = 3;

    protected override void Action(Transform t)
    {
        //Debug.Log("Shooting at " + t);
        lastAction = Time.time;
        ShootBullet(t);
        // t.SendMessage("OnDamage", turretDmg);
    }

    // Use this for initialization
    GreenTower()
    {
        buildCost = 40;
        turretDmg = new DamageInfo();
        turretDmg.nature = dmg;
    }
}
