using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;
using System;

public class ActionUnitController : UnitController
{
    IBlackBox box;

    PlayerController myPlayer;

    NeatBot neatBot;

    public override void Activate(IBlackBox box)
    {
        this.box = box;
        myPlayer = GetComponentInParent<PlayerController>();
        neatBot = gameObject.AddComponent<NeatBot>();
        neatBot.Init();
        neatBot.SetActionNet(box);
        neatBot.isPlaying = true;
    }

    public override float GetFitness()
    {
        float retValue = myPlayer.Life * 100 + myPlayer.Gold - myPlayer.GetOppponent().Life * 100;
        if (retValue < 0)
            return 0;
        return myPlayer.Life * 100 + myPlayer.Gold - myPlayer.GetOppponent().Life * 100; 
    }

    public override void Stop()
    {
        Destroy(neatBot);
    }
}
