using SharpNeat.Genomes.Neat;
using SharpNeat.Phenomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;
using System.IO;

public class NeatBot : AIPlayer
{

    IBlackBox towerNet;
    IBlackBox positionNet;
    IBlackBox actionNet;
    private SimpleExperiment experiment;
    private string positionNetFileSavePath;
    private string towerNetFileSavePath;
    private string actionNetFileSavePath;


    // The part of the Bot that decides where to place a tower
    GridEvaluator gridEvaluator;
    // The part of the Bot that decides what tower to place
    TowerEvaluator towerEvaluator;

    // This initializes the Bot.
    // Finds the spawner, player and grid. Also creates the
    // evaluators
    protected override void Init()
    {
        player = GetComponentInParent<PlayerController>();

        gridEvaluator = new GridEvaluator(player.grid);
        towerEvaluator = new TowerEvaluator();

        positionNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "positionNet");
        towerNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "towerNet");
        actionNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "actionNet");

        NeatGenome genome = null;
        // Try to load the genome from the XML document.
        try
        {
            NeatGenomeFactory ngf = new NeatGenomeFactory(4, 1);
            // Action
            using (XmlReader xr = XmlReader.Create(actionNetFileSavePath))
                genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false, ngf)[0];
            actionNet = experiment.CreateGenomeDecoder().Decode(genome);
        }
        catch
        {
            Debug.Log("Failed to load brain.");
            return;
        }
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
        actionNet.Activate();

        Action action = (Action) (actionNet.OutputSignalArray[0] * Enum.GetNames(typeof(Action)).Length);
        Debug.Log("Action is: "+action);
        switch (action)
        {
            case Action.Build:
                AIBuild();
                break;
            case Action.Destroy:
                AIDestory();
                break;
            case Action.BuildGoldTower:
                // Build Gold Tower
                Debug.Log("Build Gold Tower");
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

    // Builds a tower
    // The position and tower are chosen by a neuronal network
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
        List<EnemyStructure> enemys = PrefabContainer.Instance.GetAllEnemys();
        EnemyEvaluator enemyEvaluator = new EnemyEvaluator(enemys);
        DamageInfo enemyDmg = new DamageInfo();
        // TODO: Calculate enemy damage
        player.SendEnemys(enemyEvaluator.GetBestEnemy(enemyDmg));
    }

    protected override void AIUpgrade()
    {
        throw new NotImplementedException();
    }
}