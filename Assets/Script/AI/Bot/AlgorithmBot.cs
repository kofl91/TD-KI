using UnityEngine;
using System;
using System.Collections.Generic;

public class AlgorithmBot : AIPlayer
{
    public GameObject gridMaker;

    // The part of the Bot that decides where to place a tower
    GridEvaluator gridEvaluator;
    // The part of the Bot that decides what tower to place
    TowerEvaluator towerEvaluator;
    // The part of the Bot that decides what action to do
    ActionEvaluator actionEvaluator;

    EnemyEvaluator enemyEvaluator;
    // A reference to the spawner to get information about the next wave
    public Spawner spawner;
    

    // This initializes the Bot.
    // Finds the spawner, player and grid. Also creates the
    // evaluators
    public override void Init()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();
        player = GetComponentInParent<PlayerController>();
        if (!enemy)
        {
            PlayerController[] allplayer = FindObjectsOfType<PlayerController>();
            foreach (PlayerController pl in allplayer)
            {
                if (!pl.Equals(player))
                {
                    enemy = pl;
                }
            }
        }
        gridEvaluator = new GridEvaluator(gridMaker.GetComponent<GridMaker>().GetGrid());
        towerEvaluator = new TowerEvaluator();
        towerEvaluator.SetTowerList(GetTowerStructureList());
        actionEvaluator = new ActionEvaluator();
        enemyEvaluator = new EnemyEvaluator(GetEnemyStructureList());
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
        Action bestAction = actionEvaluator.GetBestAction(GetTowerFromPlayer(player), GetTowerFromPlayer(enemy),spawner.GetNextEnemy());
        Debug.Log(bestAction);
        switch (bestAction)
        {
            case Action.BuildOrUpgrade:
                AIBuild();
                break;
            case Action.Destroy:
                AIDestroy();
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
            //nextPosition.tile.obj.GetComponent<MeshRenderer>().enabled = true;
            player.BuildTower(bestTower.tower, nextPosition.tile);
        }
    }

    protected override void AIDestroy()
    {
        throw new NotImplementedException();
    }

    protected override void AISend()
    {
        player.SendEnemys(enemyEvaluator.GetBestEnemy(estimateDamage(GetTowerFromPlayer(enemy))));
    }

    protected override void AIUpgrade()
    {
        throw new NotImplementedException();
    }

    internal override void Reset()
    {
        gridEvaluator = new GridEvaluator(gridMaker.GetComponent<GridMaker>().GetGrid());
    }

    private List<TowerStructure> GetTowerFromPlayer(PlayerController player)
    {
        BaseTower[] allTower = player.GetComponentsInChildren<BaseTower>();
        List<TowerStructure> towerList = new List<TowerStructure>();
        foreach (BaseTower t in allTower)
        {
            towerList.Add(t.GetTowerStructure());
        }
        return towerList;
    }

    private DamageInfo estimateDamage(List<TowerStructure> towerList)
    {
        DamageInfo retVal = new DamageInfo();
        foreach (TowerStructure t in towerList)
        {
            retVal.Add(t.dmg.Multiply(t.attackspeed));
        }
        return retVal;
    }
}
