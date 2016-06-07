using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour
{

    public float maxlife = 1f;
    public float life = 1f;
    public int bounty = 1;
    private bool gaveBounty = false;
    public GameObject target;

    public PlayerController enemy;

    private NavMeshAgent agent;

    private Healthbar healthbar;

    protected DamageInfo dmgfactor = new DamageInfo();

    public BaseEnemy()
    {
        dmgfactor.setNeutralResistance();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target)
            agent.SetDestination(target.transform.position);
        healthbar = GetComponentInChildren<Healthbar>();
    }

    protected void ReachedGoal()
    {
        Destroy(gameObject);
    }

    public void OnDamage(DamageInfo damage)
    {
        life -= damage.calcAbsoluteDmg(dmgfactor);
        if (healthbar)
            healthbar.SetHealthVisual(life / maxlife);
        if (life <= 0)
        {
            if (!gaveBounty)
            {
                enemy.Gold += bounty;
                gaveBounty = true;
            }
            Destroy(this.gameObject);
        }
    }
}
