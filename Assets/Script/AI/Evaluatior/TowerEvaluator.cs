using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEvaluator 
{
    private List<TowerStructure> allTower;

    private List<RatedTower> ratedTower;

    private Spawner spawner;

    private DamageInfo nextResistance;

    //TODO: Make more efficient by adding turrets in a sorted way.
    private void evaluateAllTower()
    {
        spawner = (Spawner)GameObject.FindObjectOfType<Spawner>();
        BaseEnemy enemy = spawner.GetNextEnemy();
        nextResistance = enemy.GetResistance();
        ratedTower = new List<RatedTower>();
        foreach (TowerStructure tower in allTower)
        {
            ratedTower.Add(new RatedTower(tower, evaluateTower(tower)));
            
        }
        ratedTower.Sort();
    }

    // Rates one Tower
    // TODO: Make the rating more dynamic. Also calculate what dmg is already there and what dmg is needed for the next wave.
    private float evaluateTower(TowerStructure t)
    {
        return t.dmg.calcAbsoluteDmg(nextResistance) * t.attackspeed / t.cost;
    }

    public RatedTower GetBestTower()
    {
        evaluateAllTower();
        return ratedTower[0];   
    }

    public void SetTowerList(List<TowerStructure> list)
    {
        allTower = list;
    }
}
