using UnityEngine;
using System.Collections;

public class SupportTower : BaseTurret {


    float rangeBuff = 1.5f;
    float buffduration = 3.0f;
    float speedBuff = 0.5f;


    protected override void Action(Transform t)
    {
        lastAction = Time.time;
        Collider[] allTower = Physics.OverlapSphere(transform.position, range * rangeMultiplier, LayerMask.GetMask("Tower"));
        for (int i = 0; i < allTower.Length; i++)
        {
            allTower[i].GetComponent<BaseTurret>().buffDuration = buffduration;
            allTower[i].GetComponent<BaseTurret>().rangeMultiplier = rangeBuff;
            allTower[i].GetComponent<BaseTurret>().cooldownMultiplier = speedBuff;
        }
    }

    // Use this for initialization
    void Start()
    {
        range = 30f * range;
        cooldown = 2.0f * cooldown;
        goldCost = 100;
    }
}
