using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BaseEnemy : NetworkBehaviour
{

    public float maxlife = 1f;
    public float life = 1f;
    public int bounty = 1;
    private bool gaveBounty = false;

    public GameObject target;

    [SyncVar]
    public Vector3 target2;

    [SyncVar]
    public int enemyPlayerID;


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
        healthbar = GetComponentInChildren<Healthbar>();
    }
    void Update()
    {
        if (target2 != Vector3.zero)
        {
            agent.SetDestination(target2);
        }
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

                enemy = GameObject.FindObjectsOfType<PlayerController>()[enemyPlayerID-1];
                enemy.Gold += bounty;
                gaveBounty = true;
            }
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Vector3 t)
    {
        target2 = t;
    }
}
