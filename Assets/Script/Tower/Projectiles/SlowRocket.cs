using UnityEngine;

// Ein Projektil das Flächenschaden macht und dabei verlangsamt
public class SlowRocket : BaseProjectile {

    // Der Radius der Explosion
    float range = 5.0f;

    // Die Dauer der Verlangsamung
    //float debuffduration = 2.0f;
    // Die Effektivität der Verlangsamung
    float slow = 0.1f;

   

    // Der Aufprall und somit das Zünden der Explosion
    override protected void ReachTarget()
    {
        Vector3 posi = new Vector3(transform.position.x, 1.0f, transform.position.z);
        GameObject explosionInstance = Instantiate(explosion, posi, Quaternion.identity) as GameObject;
        explosionInstance.transform.SetParent(transform);
        Destroy(explosionInstance, 0.5f);
        if (Target)
        {
            Collider[] allEnemys = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));

            if (allEnemys.Length != 0)
            {
                for (int i = 0; i < allEnemys.Length; i++)
                {
                    allEnemys[i].gameObject.SendMessage("OnDamage", Damage);
                    allEnemys[i].GetComponent<NavMeshAgent>().velocity *= slow;
                }
            }
        }
        Destroy(gameObject);
    }
}
