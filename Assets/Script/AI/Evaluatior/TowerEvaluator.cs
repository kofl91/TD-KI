using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEvaluator 
{
    private List<TowerStructure> allTower;

    private List<RatedTower> ratedTower;

    //TODO: Make more efficient by adding turrets in a sorted way.
    private void evaluateAllTower()
    {
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
        DamageInfo neutralDmg = new DamageInfo();
        neutralDmg.setNeutralResistance();
        return t.dmg.calcAbsoluteDmg(neutralDmg) * t.attackspeed / t.cost;
    }

    public RatedTower GetBestTower()
    {
        evaluateAllTower();
        return ratedTower[0];   
    }

    public void SetTowerList(List<TowerStructure> l)
    {
        allTower = l;
    }
}
