using UnityEngine;
using System.Collections;

public class SlowRocket : BaseProjectile {

    float range = 100.0f;

    float debuffduration = 2.0f;
    float slow = 0.5f;

    override protected void ReachTarget()
    {

        if (Target)
        {
            Collider[] allEnemys = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));

            if (allEnemys.Length != 0)
            {
                for (int i = 0; i < allEnemys.Length; i++)
                {
                    allEnemys[i].gameObject.SendMessage("OnDamage", Damage);
                    // TODO
                    // Implement a Slow!
                }
            }
        }

        Destroy(gameObject);
    }
}
