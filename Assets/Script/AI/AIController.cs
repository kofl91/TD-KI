using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;
using System;

public class AIController : UnitController
{
    bool IsRunning;
    IBlackBox box;
    int mapsixeX;
    int mapsixeY;
    int towerCount = 8;
    bool[,] canPlaceHere;   

    public PlayerController myPlayer;

    public override void Activate(IBlackBox box)
    {
        this.box = box;
        IsRunning = true;
        myPlayer = GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<PlayerController>();
        mapsixeX = myPlayer.getMapSizeX();
        mapsixeY = myPlayer.getMapSizeY();
        canPlaceHere = myPlayer.getPlaceAbleArea();
        myPlayer.removeAllTower();
        myPlayer.Gold = 100;
        myPlayer.Life = 20;
        myPlayer.towerPlaced = 0;
    }

    public override float GetFitness()
    {
        float fitnes = myPlayer.towerPlaced + myPlayer.Life;
        if (fitnes >= 0)
            return fitnes;
        else
            return 0;
    }

    public override void Stop()
    {
        IsRunning = false;
    }

    public float boolToFloat(bool input)
    {
        if (input)
            return 1.0f;
        return 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ISignalArray inputArr = box.InputSignalArray;
        for (int i = 0; i < mapsixeX; i++)
        {
            for (int j = 0; j < mapsixeY; j++)
            {
                inputArr[i + j * mapsixeX] = boolToFloat(canPlaceHere[i, j]) ;
            }
        }
        box.Activate();
        ISignalArray outputArr = box.OutputSignalArray;
        int x = (int)(outputArr[0] * (mapsixeX-1));
        int y = (int)(outputArr[1] * (mapsixeY-1));
        int towerID = (int) outputArr[2] * (towerCount-1);
        myPlayer.ChooseTower(towerID);    
        //myPlayer.CmdCreateTurretUnit(x, y);
    }
}

