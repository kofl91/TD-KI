using UnityEngine;
using System.Collections;

public class GoldTower : BaseTurret {

    protected int dmg = 1;

    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        GameManager.Instance.firstPlayer.ChangeBalance(1);
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
