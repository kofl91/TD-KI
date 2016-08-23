using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionEvaluator
{

    private List<RatedAction> ratedActions;

    // TODO: Find appropriate values
    public  float HP_DMG_FACTOR = 5.0f;

    public  float SEND_MINION_BORDER = 1.0f;

    public  float BUILD_GOLD_BORDER = 1.0f;




    public Action GetBestAction(ResourcesStructure res, int currentwave)
    {
        return Action.Nothing;
    }

    internal Action GetBestAction(List<TowerStructure> ownTower, List<TowerStructure> enemyTower, BaseEnemy baseEnemy)
    {
        // TODO: Fill variables with values
        float myEstimatedDamage = estimateDamage(ownTower,baseEnemy);
        float opponentEstimatedDamage = estimateDamage(enemyTower, baseEnemy);
        float estimatedWaveHP = baseEnemy.life;
        if (myEstimatedDamage < estimatedWaveHP * HP_DMG_FACTOR)
        {
            return Action.BuildOrUpgrade;
        }
        if (opponentEstimatedDamage < (estimatedWaveHP * HP_DMG_FACTOR + SEND_MINION_BORDER))
        {
            return Action.Send;
        }
        if (myEstimatedDamage < estimatedWaveHP * HP_DMG_FACTOR)
        {
            return Action.BuildGoldTower;
        }
        return Action.Nothing;
    }

    private float estimateDamage(List<TowerStructure> list1, BaseEnemy enemy)
    {
        float retVal = 0.0f;
        foreach(TowerStructure t in list1)
        {
            retVal += t.dmg.calcAbsoluteDmg(enemy.resistance) * t.attackspeed * t.range / enemy.speed;
        }
        return retVal;
    }
}
