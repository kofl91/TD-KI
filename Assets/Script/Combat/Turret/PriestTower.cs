using UnityEngine;
using System.Collections;

public class PriestTower : BaseTurret {

    protected int dmg = 1;

    new private GameObject projectile;

    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        GameManager.Instance.firstPlayer.IncreaseGold(1);
    }

    // Use this for initialization
    void Start()
    {
        turretDmg = new DamageInfo();
        turretDmg.amount = dmg;
        range = 2.5f * range;
        cooldown = 1.0f * cooldown;
        goldCost = 100;
    }
}
