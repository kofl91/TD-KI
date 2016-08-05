using UnityEngine;
using System.Collections;

// Ein Projektil das Flächenschaden verursacht
public class Rocket : BaseProjectile {

    // Die Reichweite der Explosion
    float range = 5.0f;

    // Der Aufprall und somit das Zünden der Explosion
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
                }
            }
        }
        Destroy(gameObject);
    }
}
