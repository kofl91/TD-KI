using UnityEngine;
using System.Collections;

public class StrongShotTower : BaseTurret {

    protected int dmg = 20;

    protected override void Action(Transform t)
    {
        //Debug.Log("Shooting at " + t);
        lastAction = Time.time;
        ShootBullet(t);

        // t.SendMessage("OnDamage", turretDmg);
    }

    // Use this for initialization
    void Start()
    {
        turretDmg = new DamageInfo();
        turretDmg.amount = dmg;
        range = 2.5f * range;
        cooldown = 4.5f * cooldown;
        goldCost = 70;
    }
}
