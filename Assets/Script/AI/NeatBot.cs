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


    // This initializes the Bot.
    // Finds the spawner, player and grid. Also creates the
    // evaluators
    protected override void Init()
    {
        player = GetComponentInParent<PlayerController>();

        positionNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "positionNet");
        towerNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "towerNet");
        actionNetFileSavePath = Application.persistentDataPath + string.Format("/{0}.champ.xml", "actionNet");

        NeatGenome genome = null;
        // Try to load the genome from the XML document.
        try
        {
            NeatGenomeFactory ngf = new NeatGenomeFactory(20 * 20, 2);

            // Position
            using (XmlReader xr = XmlReader.Create(positionNetFileSavePath))
                genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false, ngf)[0];
            positionNet = experiment.CreateGenomeDecoder().Decode(genome);

            // Tower
            using (XmlReader xr = XmlReader.Create(towerNetFileSavePath))
                genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false, ngf)[0];
            towerNet = experiment.CreateGenomeDecoder().Decode(genome);

            // Action
            using (XmlReader xr = XmlReader.Create(actionNetFileSavePath))
                genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false, ngf)[0];
            actionNet = experiment.CreateGenomeDecoder().Decode(genome);
        }
        catch
        {
            Debug.Log("Failed to load brain.");
            Debug.Log(positionNetFileSavePath);
            Debug.Log(actionNetFileSavePath);
            Debug.Log(towerNetFileSavePath);
            Debug.Log(positionNetFileSavePath);
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
        towerNet.Activate();
        int towerID = (int)(towerNet.OutputSignalArray[0] * towers.Count());
        TowerStructure bestTower = towers[towerID];
        if (player.SpendMoney(bestTower.cost))
        {
            int x = (int)(positionNet.OutputSignalArray[0] * player.grid.sizeX);
            int y = (int)(positionNet.OutputSignalArray[1] * player.grid.sizeY);

            TileStructure nextPosition = player.grid.tiles[x,y];

            if (nextPosition.type == eTile.Free)
            {
                nextPosition.obj.GetComponent<MeshRenderer>().enabled = true;
                player.BuildTower(bestTower, nextPosition);
            }
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