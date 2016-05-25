using UnityEngine;
using System.Collections;

public class BlueTower : BaseTurret
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
    void Start()
    {
        turretDmg = new DamageInfo();
        turretDmg.amount = dmg;

        goldCost = 40;
    }
}
