﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class AlgorithmBot : AIPlayer
{
    // TODO: Change how the grid is accessed
    public GameObject gridMakerObject;
    // The part of the Bot that decides where to place a tower
    GridEvaluator gridEvaluator;
    // The part of the Bot that decides what tower to place
    TowerEvaluator towerEvaluator;
    // The part of the Bot that decides what action to do
    ActionEvaluator actionEvaluator;
    // The grid the player can build on
    GridStructure grid;
    // A reference to the spawner to get information about the next wave
    private Spawner spawner;
    

    // This initializes the Bot.
    // Finds the spawner, player and grid. Also creates the
    // evaluators
    protected override void Init()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();
        player = GetComponentInParent<PlayerController>();
        grid = gridMakerObject.GetComponent<GridMaker>().GetGrid();
        gridEvaluator = new GridEvaluator(grid);
        towerEvaluator = new TowerEvaluator();
        towerEvaluator.SetTowerList(GetTowerStructureList());
        actionEvaluator = new ActionEvaluator();
    }

    // Makes a move. Decides what to do and where to place.
    // TODO: implement what-to-do decision
    public override void MakeMove()
    {
        if (!isInitialized)
        {
            Init();
            isInitialized = true;
        }
        RatedAction bestAction = actionEvaluator.GetBestAction(new ResourcesStructure(player.GetMoney(), player.GetLife()), spawner.GetWave());
        switch (bestAction.action)
        {
            case Action.Build:
                AIBuild();
                break;
            case Action.Destroy:
                AIDestory();
                break;
            case Action.Send:
                AISend();
                break;
            case Action.Upgrade:
                AIUpgrade();
                break;
            case Action.Nothing:
            default:
                break;
        }
    }

    protected override void AIBuild()
    {
        RatedTower bestTower = towerEvaluator.GetBestTower();
        if (player.SpendMoney(bestTower.tower.cost))
        {
            RatedPosition nextPosition = gridEvaluator.GetNextPosition();
            nextPosition.tile.obj.GetComponent<MeshRenderer>().enabled = true;
            player.BuildTower(bestTower.tower, nextPosition.tile);
        }
    }

    protected override void AIDestory()
    {
        throw new NotImplementedException();
    }

    protected override void AISend()
    {
        throw new NotImplementedException();
    }

    protected override void AIUpgrade()
    {
        throw new NotImplementedException();
    }
}
