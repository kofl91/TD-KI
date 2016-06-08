using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class BaseTurret : MonoBehaviour, IBelongsToPlayer {

    public GameObject projectile;
    public DamageInfo turretDmg = new DamageInfo();

    protected float refreshRate = 0.10f;
    protected float lastAction;
    protected float cooldown = 1.0f;
    private float lastTick;
    protected float range = 10.0f;
    protected int goldCost;

    public float buffDuration = 0.0f;

    public float rangeMultiplier = 1.0f;
    public float cooldownMultiplier = 1.0f;

    protected PlayerController owner;

    private RotatesTowardsTarget rtt;
    void Start()
    {
        //GameObject.Find("Player").GetComponent<Player>().ChangeBalance(-goldCost);
        rtt = GetComponentInChildren<RotatesTowardsTarget>();
        
    }
    
    public abstract int getCost();


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
                if (rtt)
                    rtt.target = target;
                if (target != null)
                {
                    Action(target);
                }
            }
        }
    }

    private Transform GetNearestEnemy()
    {
        Collider[] allCollider = Physics.OverlapSphere(transform.position, range* rangeMultiplier, LayerMask.GetMask("Enemy"));
        //Debug.Log(allEnemys.Length);

        // Filter for the right Enemys
        List<Collider> allEnemys = new List<Collider>();
        for (int i = 0; i < allCollider.Length; i++)
        {
            BaseEnemy enemy = allCollider[i].GetComponent<BaseEnemy>();
            if (enemy.enemy == owner)
            {
                allEnemys.Add(allCollider[i]);
            }
        }

        if (allEnemys.Count != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemys[0].transform.position);
            for (int i = 1; i < allEnemys.Count; i++)
            {
                float newDistance = Vector3.SqrMagnitude(transform.position - allEnemys[i].transform.position);
                
                if ((newDistance < nearestDistance))
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
        GameObject bullet = Instantiate(projectile) as GameObject;
        Transform start;
        if (rtt)
            start = rtt.transform;
        else
            start = transform;
        bullet.GetComponent<BaseProjectile>().Launch(start, t, turretDmg);
    }

    public void SetPlayer(PlayerController player)
    {
        owner = player;
    }

    public PlayerController GetPlayer()
    {
        return owner;
    }
}
