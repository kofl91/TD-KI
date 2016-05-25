using UnityEngine;
using System.Collections;
using System;

public class BoxTurret : BaseTurret {

    protected int dmg = 1;
   

    protected override void Action(Transform t)
    {
        //Debug.Log("Shooting at " + t);
        lastAction = Time.time;
        ShootBullet(t);
       // t.SendMessage("OnDamage", turretDmg);
    }

    // Use this for initialization
    void Start () {
        turretDmg = new DamageInfo();
        turretDmg.normal = dmg;
    }
}
