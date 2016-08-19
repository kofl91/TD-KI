using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionEvaluator
{

    private List<RatedAction> ratedActions;

    private bool letthrough = true;

    // TODO: Find appropriate values
    public const float HP_DMG_FACTOR = 1.0f;

    public const float SEND_MINION_BORDER = 1.0f;

    public const float BUILD_GOLD_BORDER = 1.0f;

    public Action GetBestAction(ResourcesStructure res, int currentwave)
    {
        // TODO: Fill variables with values
        float myEstimatedDamage = 0.0f;
        float opponentEstimatedDamage = 0.0f;
        float estimatedWaveHP = 0.0f;
        if (myEstimatedDamage<estimatedWaveHP * HP_DMG_FACTOR)
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
}
