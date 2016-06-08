using UnityEngine;
using System.Collections;

public class Rocket : BaseProjectile {

    float range = 100.0f;

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

                }
            }
        }

        
        Destroy(gameObject);
    }
}
