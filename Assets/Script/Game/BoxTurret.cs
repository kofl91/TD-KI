using UnityEngine;
using System.Collections;
using System;

public class BoxTurret : BaseTurret {

    protected int dmg= 3;

    protected override void Action(Transform t)
    {
        Debug.Log("Shooting at " + t);
        lastAction = Time.time;
        Debug.DrawRay(transform.position, t.position - transform.position, Color.red, 1.5f);
        GameObject bullet = Instantiate(projectile
            , transform.position
            , Quaternion.identity) as GameObject;
        bullet.GetComponent<BaseProjectile>().Launch(this.transform, t, turretDmg);
        
       // t.SendMessage("OnDamage", turretDmg);
    }

    // Use this for initialization
    void Start () {
        turretDmg = new DamageInfo();
        turretDmg.amount = dmg;
    }
}
