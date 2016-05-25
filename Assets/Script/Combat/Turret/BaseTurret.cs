﻿using UnityEngine;
using System.Collections;

public abstract class BaseTurret : MonoBehaviour {

    public GameObject projectile;
    public DamageInfo turretDmg = new DamageInfo();

    protected float refreshRate = 0.10f;
    protected float lastAction;
    protected float cooldown = 1.0f;
    private float lastTick;
    protected float range = 150.0f;
    public int goldCost = 20;

    public float buffDuration = 0.0f;

    public float rangeMultiplier = 1.0f;
    public float cooldownMultiplier = 1.0f;

    void Start()
    {
        GameObject.Find("Player").GetComponent<Player>().ChangeBalance(-goldCost);
    }

    // Update is called once per frame
    void Update()
    {
        buffDuration -= Time.deltaTime;
        if (buffDuration < 0.0f)
        {
            rangeMultiplier = 1.0f;
            cooldownMultiplier = 1.0f;
            buffDuration = 0.0f;
        }
        if (Time.time - lastAction > (cooldown* cooldownMultiplier))
        {
            if (Time.time - lastAction > refreshRate)
            {
                lastTick = Time.time;
                Transform target = GetNearestEnemy();
                if (target != null)
                {
                    Action(target);
                }
            }
        }
    }

    private Transform GetNearestEnemy()
    {
        Collider[] allEnemys = Physics.OverlapSphere(transform.position, range* rangeMultiplier, LayerMask.GetMask("Enemy"));

        //Debug.Log(allEnemys.Length);
        if (allEnemys.Length != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemys[0].transform.position);
            for (int i = 1; i < allEnemys.Length; i++)
            {
                float newDistance = Vector3.SqrMagnitude(transform.position - allEnemys[i].transform.position);
                if (newDistance < nearestDistance)
                {
                    nearestDistance = newDistance;
                    closestIndex = i;
                }
            }
            return allEnemys[closestIndex].transform;
        }
        return null;

    }
    protected virtual void Action(Transform t)
    {
        lastAction = Time.time;
        ShootBullet(t);
    }

    protected void ShootBullet(Transform t)
    {
        Debug.DrawRay(transform.position, t.position - transform.position, Color.red, 1.5f);
        GameObject bullet = Instantiate(projectile
            , transform.position
            , Quaternion.identity) as GameObject;
        bullet.GetComponent<BaseProjectile>().Launch(this.transform, t, turretDmg);
    }
}
