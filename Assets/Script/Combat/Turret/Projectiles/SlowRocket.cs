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
            Target.gameObject.SendMessage("OnDamage", Damage);
            Collider[] allEnemys = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));

            if (allEnemys.Length != 0)
            {
                for (int i = 1; i < allEnemys.Length; i++)
                {
                    allEnemys[i].gameObject.SendMessage("OnDamage", Damage);
                    allEnemys[i].GetComponent<Enemy>().speedmodifier = slow;
                    allEnemys[i].GetComponent<Enemy>().debuffDuration = debuffduration;
                }
            }
        }

        Destroy(gameObject);
    }
}
