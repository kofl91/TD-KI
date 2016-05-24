using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;
using System;

public class AIController : UnitController
{
    bool IsRunning;
    IBlackBox box;
    private int mapsixeX = Map.Instance.mapSizeX;
    private int mapsixeY = Map.Instance.mapSizeY;

    public Player myPlayer;


    public override void Activate(IBlackBox box)
    {
        myPlayer = GameManager.Instance.firstPlayer;
        myPlayer.RemoveTurrets();
        this.box = box;
        IsRunning = true;
        // Create Player

        // Tell the Game who the Player is
        myPlayer.turretsplaced = 0;
        myPlayer.failedActions = 0;
        myPlayer.enemyKilled = 0;
        myPlayer.Gold = 100;
        myPlayer.Life = 20;
    }

    public override float GetFitness()
    {
        float fitnes = myPlayer.enemyKilled + myPlayer.turretsplaced ;
        if (fitnes > 0)
            return fitnes;
        else
            return 0;
    }

    public override void Stop()
    {
        IsRunning = false;
        Debug.Log("Test!!!");
        // Remove all Turrets of Player
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ISignalArray inputArr = box.InputSignalArray;
        //inputArr[0] = frontSensor;
        int index = 0;
        for (int i= 0; i<mapsixeX; i++)
        {
            for (int j = 0; j < mapsixeY; j++)
            {
                if (myPlayer.freeTiles[i, j])
                    inputArr[index] = 1.0f;
                else
                    inputArr[index] = 0.0f;
                index++;
            }
        }
        box.Activate();

        ISignalArray outputArr = box.OutputSignalArray;

        index = 0;
        int bestindex = 0;
        int bestx = 0;
        int besty = 0;
        for (int i = 0; i < mapsixeX; i++)
        {
            for (int j = 0; j < mapsixeY; j++)
            {
                if (outputArr[index] > outputArr[bestindex])
                {
                    System.Random rnd = new System.Random();
                    
                    if (myPlayer.freeTiles[i,j])
                    {
                        bestindex = index;
                        besty = j;
                        bestx = i;
                    }
                }

                index++;
            }
        }

        int x = bestx;
        int y = besty;
        myPlayer.CreateTurretUnit(x, y, PrefabContainer.Instance.turrets[0]);

        /*
                int x = (int)(outputArr[0] * mapsixeX);
                int y = (int)(outputArr[1] * mapsixeY);
                
        myPlayer.CreateTurretUnit(x, y, PrefabContainer.Instance.turrets[0]);
        */
    }
}

