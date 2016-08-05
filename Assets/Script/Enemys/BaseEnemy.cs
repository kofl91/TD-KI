using UnityEngine;

// Basisklasse für alle Gegner
public class BaseEnemy : MonoBehaviour
{
    // Das maximale Leben
    public float maxlife = 1f;
    // Das aktuelle Leben
    public float life = 1f;
    // Gold-Belohnung nach Zerstörung
    public int bounty = 1;
    // Flag das sicherstellt, das eine Einheit nur einmal Belohnung gibt
    private bool gaveBounty = false;
    
    // Zielposition an welche der Gegner läuft
    public GameObject target;

    // Spieler der Geld erhält, wenn der Minion zerstört wird 
    public PlayerController enemy;

    // Das NavMeshAgent der für die Bewegung zuständig ist
    private NavMeshAgent agent;

    // GameObjekt, welches das Leben der Einheit anzeigt
    private Healthbar healthbar;

    // Die Resistenzen der Einheit
    public DamageInfo resistance = new DamageInfo();
 

    // Initialisierung
    void Start()
    {
        life = maxlife;
        agent = GetComponent<NavMeshAgent>();
        if (target)
            agent.SetDestination(target.transform.position);
        healthbar = GetComponentInChildren<Healthbar>();
        if (healthbar)
            healthbar.SetHealthVisual(life / maxlife);
    }

    // Funktion wird bei jedem Treffer ausgeführt
    public void OnDamage(DamageInfo damage)
    {
        life -= damage.calcAbsoluteDmg(resistance);
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

    // Gibt die Resistenz des Gegners zurück
    public DamageInfo GetResistance()
    {
        return resistance;
    }

    // Setzt das maximale Leben der Einheit
    public void SetMaxLife(float max)
    {
        maxlife = max;
        life = maxlife;
    }
}
