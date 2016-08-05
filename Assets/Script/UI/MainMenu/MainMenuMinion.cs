using UnityEngine;
using System.Collections;

public class MainMenuMinion : MonoBehaviour {


    public float maxlife = 4f;
    public float life = 3f;
    public GameObject target;

    public PlayerController enemy;

    private NavMeshAgent agent;

    private Healthbar healthbar;

    protected DamageInfo dmgfactor = new DamageInfo();

    public MainMenuMinion()
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
            Destroy(this.gameObject);
        }
    }

    public DamageInfo GetResistance()
    {
        return dmgfactor;
    }

    void Update()
    {
        //Debug.Log(agent.remainingDistance);
        if(agent.remainingDistance < 5 && agent.remainingDistance != 0)
        {
            Destroy(gameObject);
        }
    }
}
