using UnityEngine;
using System.Collections;

public class GreenTower : BaseTurret
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
        goldCost = 40;
        turretDmg = new DamageInfo();
        turretDmg.nature = dmg;
    }

    public override int getCost()
    {
        return goldCost;
    }
}
