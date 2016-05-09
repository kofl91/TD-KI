using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseTurret : MonoBehaviour {

    #region Attributes
   

    public DamageInfo turretDmg = new DamageInfo();
    public int armorPenetration;
    // Firerate
    protected float cooldown = 1.0f;
    protected float range = 5.0f;

    public GameObject projectile;

    public int cost;

    // Timing
    protected float refreshRate = 0.10f;
    protected float lastAction;
    private float lastTick;

    public List<Upgrade> upgrades;

    #endregion

    #region Methods
    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAction > cooldown)
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
        Collider[] allEnemys = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));

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
        //Debug.Log("Shooting");
    }

    #endregion
}
