using UnityEngine;
using System.Collections;

public class SplashTower : BaseTurret {

    protected int dmg = 1;

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
        range = 1.0f * range;
        cooldown = 1.0f * cooldown;
        goldCost = 50;
    }
}
