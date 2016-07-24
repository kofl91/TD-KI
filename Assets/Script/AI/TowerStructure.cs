﻿using UnityEngine;
using System.Collections;

public class TowerStructure {

    public DamageInfo dmg;

    public float attackspeed;

    public int cost;

    public GameObject prefab;

    public TowerStructure(DamageInfo turretDmg, float v, int goldCost, GameObject p)
    {
        this.dmg = turretDmg;
        this.attackspeed = v;
        this.cost = goldCost;
        this.prefab = p;
    }
}
