using UnityEngine;
using System.Collections.Generic;


// Basisklasse für alle Türme
public class BaseTower : MonoBehaviour {

    // Das Projektil, welches der Turm abfeuert
    public GameObject projectile;

    #region Turmattribute
    // Der Schaden, den der Turm als Grundwert hat
    protected DamageInfo baseDmg = new DamageInfo();
    // Der Schaden, den der Turm zufügt
    public DamageInfo turretDmg = new DamageInfo();
    // Die Reichweite
    public float range = 10.0f;
    // Die Schussfrequenz
    public float cooldown = 1.0f;
    // Die Baukosten
    public int buildCost = 10;
    // Die Upgradekosten
    protected int upgradeCost = 5;
    // Die Upgradstuffe
    protected int level = 1;
    #endregion

    #region CooldownBerechnung
    // Zeitvariablen für das kontinuierliche abfeuern von Projektilen
    protected float refreshRate = 0.10f;
    protected float lastAction;
    private float lastTick;
    #endregion

    // Variablen für Buffs und Debuffs
    protected float buffDuration = 0.0f;
    protected float rangeMultiplier = 1.0f;
    protected float cooldownMultiplier = 1.0f;

    // Die Spielerzugehörigkeit
    protected PlayerController owner;

    // Eine Referenz auf ein GameObject, dass sich zum Ziel drehen soll
    private RotatesTowardsTarget rtt;


    public BaseTower()
    {
        baseDmg.Set(turretDmg.normal/2,turretDmg.fire / 2, turretDmg.water / 2, turretDmg.nature / 2);
        upgradeCost = buildCost / 2;
    }

    #region Unity
    // Initialisierung
    void Start()
    {
        rtt = GetComponentInChildren<RotatesTowardsTarget>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Aktualisierung temporärer Buffs
        buffDuration -= Time.deltaTime;
        if (buffDuration < 0.0f)
        {
            rangeMultiplier = 1.0f;
            cooldownMultiplier = 1.0f;
            buffDuration = 0.0f;
        }

        // Abfeuern der Projektile
        if (Time.time - lastAction > (cooldown* cooldownMultiplier))
        {
            if (Time.time - lastAction > refreshRate)
            {
                lastTick = Time.time;
                Transform target = GetNearestEnemy();
                if (rtt)
                    rtt.target = target;
                if (target != null)
                {
                    Action(target);
                }
            }
        }
    }
    #endregion

    #region Zielfindung
    // Zielen auf den Gegner der am nächsten am Turm ist
    private Transform GetNearestEnemy()
    {
        // Alle Objekte die in Reichweite sind und als Enemy markiert sind
        Collider[] allCollider = Physics.OverlapSphere(transform.position, range * rangeMultiplier, LayerMask.GetMask("Enemy"));

        // In Liste einfügen
        List<Collider> allEnemys = new List<Collider>();
        allEnemys.AddRange(allCollider);

        // Durchsuche alle Gegner und gib den mit der geringsten Distanz zurück
        if (allEnemys.Count != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemys[0].transform.position);
            for (int i = 1; i < allEnemys.Count; i++)
            {
                float newDistance = Vector3.SqrMagnitude(transform.position - allEnemys[i].transform.position);

                if ((newDistance < nearestDistance))
                {
                    nearestDistance = newDistance;
                    closestIndex = i;
                }
            }
            return allEnemys[closestIndex].transform;
        }
        return null;
    }

    // Zielen auf den Gegner der am meisten Leben hat
    private Transform GetStrongestEnemy()
    {
        // Alle Objekte die in Reichweite sind und als Enemy markiert sind
        Collider[] allCollider = Physics.OverlapSphere(transform.position, range * rangeMultiplier, LayerMask.GetMask("Enemy"));

        // In Liste einfügen
        List<Collider> allEnemys = new List<Collider>();
        allEnemys.AddRange(allCollider);

        // Durchsuche alle Gegner und gib den mit der meisten aktuellen HP zurück
        if (allEnemys.Count != 0)
        {
            int closestIndex = 0;
            float highestHP = allEnemys[0].GetComponent<BaseEnemy>().life;
            for (int i = 1; i < allEnemys.Count; i++)
            {
                float newHP = allEnemys[i].GetComponent<BaseEnemy>().life;
                if ((newHP > highestHP))
                {
                    highestHP = newHP;
                    closestIndex = i;
                }
            }
            return allEnemys[closestIndex].transform;
        }
        return null;
    }

    // Zielen auf den Gegner der am wenigsten Leben hat
    private Transform GetWeakestEnemy()
    {
        // Alle Objekte die in Reichweite sind und als Enemy markiert sind
        Collider[] allCollider = Physics.OverlapSphere(transform.position, range * rangeMultiplier, LayerMask.GetMask("Enemy"));

        // In Liste einfügen
        List<Collider> allEnemys = new List<Collider>();
        allEnemys.AddRange(allCollider);

        // Durchsuche alle Gegner und gib den mit der wenigsten aktuellen HP zurück
        if (allEnemys.Count != 0)
        {
            int closestIndex = 0;
            float lowestHP = allEnemys[0].GetComponent<BaseEnemy>().life;
            for (int i = 1; i < allEnemys.Count; i++)
            {
                float newHP = allEnemys[i].GetComponent<BaseEnemy>().life;
                if ((newHP < lowestHP))
                {
                    lowestHP = newHP;
                    closestIndex = i;
                }
            }
            return allEnemys[closestIndex].transform;
        }
        return null;
    }
    #endregion

    // Die Aktion die der Turm regelmäßig ausführt. In fast allen Fällen ist dies das Abschießen einer Kugel
    protected virtual void Action(Transform t)
    {
        lastAction = Time.time;
        ShootBullet(t);
    }

    // Das Abschießen einer Kugel
    protected void ShootBullet(Transform t)
    {
        // Erstelle ein Projektil und...
        GameObject bullet = Instantiate(projectile) as GameObject;
        
        Transform start;

        // Gibt es eine Abschussrampe, welche durch die Rotation zum Ziel identifiziert wird
        if (rtt)
            start = rtt.transform; // schieße das Projektil von hier ab
        else
            start = transform;

        // Feuer es ab
        bullet.GetComponent<BaseProjectile>().Launch(start, t, turretDmg);
    }

    // Wertet den Turm auf.
    public void Upgrade()
    {
        if (level < 10)
        {
            level++;
            turretDmg.Add(baseDmg);
            range *= 1.05f;
        }
    }

    // Gibt die Turminformationen als Datenstruktur zurück.
    public TowerStructure GetTowerStructure()
    {
        return new TowerStructure(turretDmg, 1 / cooldown, buildCost, this.gameObject);
    }

   
}
