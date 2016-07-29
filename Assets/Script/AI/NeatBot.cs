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
    public override void Init()
    {
        player = GetComponentInParent<PlayerController>();

        gridEvaluator = new GridEvaluator(player.grid);
        towerEvaluator = new TowerEvaluator();
        towerEvaluator.SetTowerList(GetTowerStructureList());

        positionNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "positionNet");
        towerNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "towerNet");
        actionNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "actionNet");

        NeatGenome genome = null;
        // Try to load the genome from the XML document.
        try
        {
            NeatGenomeFactory ngf = new NeatGenomeFactory(13, 1);
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

    public void SetActionNet(IBlackBox box)
    {
        actionNet = box;
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
        DamageInfo enemyResi = player.GetNextEnemyResistance();
        DamageInfo playerDmg = player.GetCurrentTowerDmg();
        DamageInfo opponentDmg = player.GetOpponentTowerDmg();
        actionNet.InputSignalArray[0] = player.Gold;
        actionNet.InputSignalArray[1] = playerDmg.normal;
        actionNet.InputSignalArray[2] = playerDmg.fire;
        actionNet.InputSignalArray[3] = playerDmg.water;
        actionNet.InputSignalArray[4] = playerDmg.nature;

        actionNet.InputSignalArray[5] = opponentDmg.normal;
        actionNet.InputSignalArray[6] = opponentDmg.fire;
        actionNet.InputSignalArray[7] = opponentDmg.water;
        actionNet.InputSignalArray[8] = opponentDmg.nature;

        actionNet.InputSignalArray[9] =  enemyResi.normal;
        actionNet.InputSignalArray[10] = enemyResi.fire;
        actionNet.InputSignalArray[11] = enemyResi.water;
        actionNet.InputSignalArray[12] = enemyResi.nature;


        actionNet.Activate();

        Action action = (Action)(actionNet.OutputSignalArray[0] * 2);
            //Enum.GetNames(typeof(Action)).Length);
        //Debug.Log("Action is: "+action);
        switch (action)
        {
            case Action.Build:
                AIBuild();
                break;
            case Action.Destroy:
                AIDestroy();
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
        List<EnemyStructure> enemys = PrefabContainer.Instance.GetAllEnemys();
        EnemyEvaluator enemyEvaluator = new EnemyEvaluator(enemys);
        DamageInfo enemyDmg = player.GetOpponentTowerDmg();
        // TODO: Calculate enemy damage
        player.SendEnemys(enemyEvaluator.GetBestEnemy(enemyDmg));
    }

    protected override void AIUpgrade()
    {
        throw new NotImplementedException();
    }

    internal override void Reset()
    {
        gridEvaluator = new GridEvaluator(player.grid);
    }
}