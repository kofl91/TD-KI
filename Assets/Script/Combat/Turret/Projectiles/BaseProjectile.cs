using UnityEngine;
using System.Collections;
using System;

public class BaseProjectile : MonoBehaviour
{
    public Vector3 Turret { get; set; }
    public Transform Target { get; set; }
    public Vector3 TargetLocation { set; get; }
    public DamageInfo Damage { set; get; }


    public bool IsLockedOnTarget { set; get; }
    public float TimeToTarget { set; get; }

    private bool isLaunched = false;
    private float transition = 0.0f;

    public BaseProjectile()
    {
        IsLockedOnTarget = false;
        TimeToTarget = 5.0f;
    }

    private void Update()
    {
        if (!isLaunched)
        {
            return;
        }

        transition += Time.deltaTime / TimeToTarget;

        if (transition >= 1.0f)
        {
            ReachTarget();
        }

        if (IsLockedOnTarget && Target)
        {
            TargetLocation = Target.position;
        }
        //transform.position = Vector3.MoveTowards(transform.position, TargetLocation, ProjectileSpeed * Time.deltaTime);
       
        transform.LookAt(TargetLocation);
        transform.Rotate(Vector3.up, 90);
        transform.position = Vector3.Lerp(Turret, TargetLocation, transition);

    }

    protected virtual void ReachTarget()
    {
        if (Target)
        {
            Target.SendMessage("OnDamage", Damage);
        }
        Destroy(gameObject);
    }

    public virtual void Launch(Transform turret, Transform target, DamageInfo dmg)
    {
        isLaunched = true;
        Turret = turret.position;
        Target = target;
        TargetLocation = target.position;
        IsLockedOnTarget = true;
        Damage = dmg;
        TimeToTarget = 0.5f;
    }
}
