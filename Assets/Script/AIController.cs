using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;
using System;

public class AIController : UnitController
{
    bool IsRunning;
    IBlackBox box;
    private int mapsixeX = 5;
    private int mapsixeY = 5;

    public Player myPlayer;


    public override void Activate(IBlackBox box)
    {
        myPlayer = GameManager.Instance.firstPlayer;
        myPlayer.RemoveTurrets();
        this.box = box;
        IsRunning = true;
        // Create Player
        
        // Tell the Game who the Player is
        myPlayer.enemyKilled = 0;
        myPlayer.Gold = 100;
        myPlayer.Life = 20;
    }

    public override float GetFitness()
    {
        
        return GameManager.Instance.firstPlayer.enemyKilled;
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
        for (int i= 0; i<6; i++)
        {
            for (int j = 0; j < 6; j++)
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
        
        int x = (int)(outputArr[0] * mapsixeX);
        int y = (int)(outputArr[1] * mapsixeY);
        myPlayer.CreateTurretUnit(x, y, PrefabContainer.Instance.turrets[0]);
    }
}

