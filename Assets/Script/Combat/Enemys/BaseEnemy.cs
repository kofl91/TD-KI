using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour
{

    public float life = 1f;
    public int bounty = 1;

    public GameObject target;

    protected DamageInfo dmgfactor = new DamageInfo();

    public BaseEnemy()
    {
        dmgfactor.setNeutralResistance();
    }

    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

    protected void ReachedGoal()
    {
        Destroy(gameObject);
    }

    public void OnDamage(DamageInfo damage)
    {
        life -= damage.calcAbsoluteDmg(dmgfactor);
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
