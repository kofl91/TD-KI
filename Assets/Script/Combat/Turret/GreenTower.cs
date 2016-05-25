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
        turretDmg = new DamageInfo();
<<<<<<< HEAD
        turretDmg.amount = dmg;
        goldCost = 40;
=======
        turretDmg.nature = dmg;
>>>>>>> 0c2aeff776256dbd722cafe29698893ae8c50e67
    }
}
