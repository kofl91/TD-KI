using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionEvaluator  {

    private List<RatedAction> ratedActions;


    public RatedAction GetBestAction()
    {
        evaluateActions();
        return ratedActions[0];
    }

    //TODO: Make more efficient by adding turrets in a sorted way.
    private void evaluateActions()
    {
        ratedActions = new List<RatedAction>();
        ratedActions.Add(new RatedAction(Action.Nothing, evaluateNothing()));
        ratedActions.Add(new RatedAction(Action.Build, evaluateBuild()));
        ratedActions.Add(new RatedAction(Action.Destroy, evaluateDestroy()));
        ratedActions.Add(new RatedAction(Action.Send, evaluateSend()));
        ratedActions.Add(new RatedAction(Action.Upgrade, evaluateUpgrade()));
        ratedActions.Sort();
    }

    private float evaluateDestroy()
    {
        return 0.0f;
    }

    private float evaluateUpgrade()
    {
        return 0.0f;
    }

    private float evaluateSend()
    {
        return 0.0f;
    }

    private float evaluateBuild()
    {
        return 1.0f;
    }

    private float evaluateNothing()
    {
        return 0.0f;
    }
}
