using UnityEngine;
using System.Collections.Generic;
using System;

public class AIPlayer : MonoBehaviour
{
    // TODO: Change how the grid is accessed
    public GameObject gridMakerObject;
    // TODO: Change how the player is accessed
    public GameObject playerObject;
    // The part of the Bot that decides where to place a tower
    GridEvaluator gridEvaluator;
    // The part of the Bot that decides what tower to place
    TowerEvaluator towerEvaluator;
    // The part of the Bot that decides what action to do
    ActionEvaluator actionEvaluator;

    public IPlayer player;
    GridStructure grid;

    public bool isPlaying = false;

    void Update()
    {
        if (isPlaying)
        {
            MakeMove();
        }
    }

    // Creates a UI for Debug purpose
    // TODO: Remove
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 60, 100, 40), "Get Next Move"))
        {
            MakeMove();
        }
    }

    // Makes a move. Decides what to do and where to place.
    // TODO: implement what-to-do decision
    public void MakeMove()
    {
        if(player == null)
        {
            player = playerObject.GetComponent<IPlayer>();
        }
        if (gridEvaluator == null)
        {
            grid = gridMakerObject.GetComponent<GridMaker>().GetGrid();
            gridEvaluator = new GridEvaluator(grid);
        }
        if (towerEvaluator == null)
        {
            towerEvaluator = new TowerEvaluator();
            towerEvaluator.SetTowerList(GetTowerStructureList());
        }
        if (actionEvaluator == null)
        {
            actionEvaluator = new ActionEvaluator();
        }
        RatedAction bestAction = actionEvaluator.GetBestAction();
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

    private void AIBuild()
    {
        RatedTower bestTower = towerEvaluator.GetBestTower();
        if (player.SpendMoney(bestTower.tower.cost))
        {
            RatedPosition nextPosition = gridEvaluator.GetNextPosition();
            nextPosition.tile.obj.GetComponent<MeshRenderer>().enabled = true;
            player.BuildTower(bestTower.tower, nextPosition.tile);
        }
    }

    private void AIDestory()
    {
        throw new NotImplementedException();
    }

    private void AISend()
    {
        throw new NotImplementedException();
    }

    private void AIUpgrade()
    {
        throw new NotImplementedException();
    }

    // Gets a list of all possible tower. Needed for what-tower-decision.
    private List<TowerStructure> GetTowerStructureList()
    {
        List<TowerStructure> mylist = new List<TowerStructure>();

        List<GameObject> turrets = PrefabContainer.Instance.turrets;

        foreach (GameObject bt in turrets)
        {
            mylist.Add(bt.GetComponent<BaseTurret>().GetTowerStructure());
        }

        return mylist;
    }
}