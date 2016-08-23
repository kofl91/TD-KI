using UnityEngine;
using System.Collections;

public class TowerStructure {

    public DamageInfo dmg;

    // Attackspeed in Attacks per Second
    public float attackspeed;

    public int cost;

    public GameObject prefab;
    public float range;

    public TowerStructure(DamageInfo turretDmg, float v, float r, int buildCost, GameObject p)
    {
        this.dmg = turretDmg;
        this.attackspeed = v;
        this.cost = buildCost;
        this.range = r;
        this.prefab = p;
    }
}
