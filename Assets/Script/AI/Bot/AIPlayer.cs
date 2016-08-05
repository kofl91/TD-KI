using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AIPlayer : MonoBehaviour
{

    // A Player-Reference to build tower and manage ressources
    public PlayerController player;

    protected List<TowerStructure> towers;

    public bool isPlaying = false;

    protected bool isInitialized = false;

    public abstract void MakeMove();

    void Update()
    {
        if (isPlaying)
        {
            MakeMove();
        }
    }

    public abstract void Init();

    protected abstract void AIBuild();

    protected abstract void AIDestroy();

    protected abstract void AISend();

    protected abstract void AIUpgrade();

    // Gets a list of all possible tower. Needed for what-tower-decision.
    protected List<TowerStructure> GetTowerStructureList()
    {
        List<TowerStructure> mylist = new List<TowerStructure>();

        List<GameObject> turrets = PrefabContainer.Instance.turrets;

        foreach (GameObject bt in turrets)
        {
            mylist.Add(bt.GetComponent<BaseTower>().GetTowerStructure());
        }

        return mylist;
    }

    internal abstract void Reset();
}