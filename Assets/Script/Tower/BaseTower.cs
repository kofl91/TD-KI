﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;


// Basisklasse für alle Türme
public class BaseTower : NetworkBehaviour
{

    // Das Projektil, welches der Turm abfeuert
    public GameObject projectile;

    #region Turmattribute
    // Der Schaden, den der Turm als Grundwert hat
    [SyncVar]
    protected DamageInfo baseDmg = new DamageInfo();
    // Der Schaden, den der Turm zufügt
    [SyncVar]
    public DamageInfo turretDmg = new DamageInfo();
    // Die Reichweite
    [SyncVar]
    public float range = 10.0f;
    // Die Schussfrequenz
    [SyncVar]
    public float cooldown = 1.0f;
    // Die Baukosten
    [SyncVar]
    public int buildCost = 10;
    // Die Upgradekosten
    [SyncVar]
    protected int upgradeCost = 100;
    // Die Upgradstuffe
    [SyncVar]
    public int level = 1;
    public int posX = 0;
    public int posY = 0;
    #endregion



    #region CooldownBerechnung
    // Zeitvariablen für das kontinuierliche abfeuern von Projektilen
    protected float refreshRate = 0.10f;
    protected float lastAction;
    #endregion

    // Variablen für Buffs und Debuffs
    protected float buffDuration = 0.0f;
    protected float rangeMultiplier = 1.0f;
    protected float cooldownMultiplier = 1.0f;

    // Die Spielerzugehörigkeit
    public PlayerController owner;

    // Eine Referenz auf ein GameObject, dass sich zum Ziel drehen soll
    private RotatesTowardsTarget rtt;

    // Ein Delegate FunktionsTyp
    public delegate Transform Del();

    // Ein Platzhalter für die Zielauswahl
    public Del GetTarget;

    // Eine Liste aller Targetfunktionen
    public List<Del> TargetingFunctions = new List<Del>();

    // Aufzählung aller Zielerfassungsmodi
    public enum TargetingModes { Close = 0, Weak = 1, Strong = 2 };

    // Der Ausgewählte Zielerfassungsmode.
    public TargetingModes TargetChooser;

    // Initialisierung
    public BaseTower()
    {
       
        upgradeCost = buildCost / 2;

        TargetingFunctions.Add(GetNearestEnemy);
        TargetingFunctions.Add(GetWeakestEnemy);
        TargetingFunctions.Add(GetStrongestEnemy);

        GetTarget = TargetingFunctions[(int)TargetChooser];
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
        if (Time.time - lastAction > (cooldown * cooldownMultiplier))
        {
            if (Time.time - lastAction > refreshRate)
            {
                Transform target = GetTarget();
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
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        bullet.transform.SetParent(transform);
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
    [Command]
    public void CmdUpgrade()
    {
        Debug.Log("Command Upgrade!");
        if (GetComponentInParent<PlayerController>().Gold >= upgradeCost)
        {
            GetComponentInParent<PlayerController>().Gold -= upgradeCost;
            if (level == 1)
            {
                baseDmg.Set(turretDmg.normal / 2, turretDmg.fire / 2, turretDmg.water / 2, turretDmg.nature / 2);
            }
            if (level < 10)
            {
                level++;
                turretDmg.Add(baseDmg);
                range *= 1.01f;
                cooldown *= 0.99f;
            }
        }
    }

    // Gibt die Turminformationen als Datenstruktur zurück.
    public TowerStructure GetTowerStructure()
    {
        return new TowerStructure(turretDmg, 1 / cooldown, range, buildCost , this.gameObject);
    }


    public DamageInfo GetDmgAtPosition(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position) < range)
        {
            return turretDmg;
        }

        DamageInfo nulldmg = new DamageInfo();
        return nulldmg;
    }

    public static DamageInfo GetAllDmgAtPosition(Vector3 pos)
    {
        BaseTower[] allTower = FindObjectsOfType<BaseTower>();
        DamageInfo retDmg = new DamageInfo();
        foreach (BaseTower t in allTower)
        {
            retDmg.Add(t.GetDmgAtPosition(pos));
        }
        return retDmg;
    }
}
