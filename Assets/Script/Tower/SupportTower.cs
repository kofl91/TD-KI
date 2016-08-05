using UnityEngine;
using System.Collections;
using System;

public class SupportTower : BaseTower {


    float rangeBuff = 1.5f;
    float buffduration = 3.0f;
    float speedBuff = 0.5f;


    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        Collider[] allTower = Physics.OverlapSphere(transform.position, range * rangeMultiplier, LayerMask.GetMask("Tower"));
        for (int i = 0; i < allTower.Length; i++)
        {
            //allTower[i].GetComponent<BaseTower>().buffDuration = buffduration;
            //allTower[i].GetComponent<BaseTower>().rangeMultiplier = rangeBuff;
            //allTower[i].GetComponent<BaseTower>().cooldownMultiplier = speedBuff;
        }
    }

    // Use this for initialization
    SupportTower()
    {
        range = 30f * range;
        cooldown = 2.0f * cooldown;
        buildCost = 100;
    }
}
