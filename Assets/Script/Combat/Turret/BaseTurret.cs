using UnityEngine;
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
}
