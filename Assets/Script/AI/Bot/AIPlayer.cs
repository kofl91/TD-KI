using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AIPlayer : MonoBehaviour
{

    // A Player-Reference to build tower and manage ressources
    public PlayerController player;
    public PlayerController enemy;

    protected List<TowerStructure> towers;

    public bool isPlaying = false;

    protected bool isInitialized = false;
    private float lastAction=0.0f;
    private float cooldown = 1.0f;

    public abstract void MakeMove();

    void Update()
    {

        if (Time.time - lastAction > cooldown)
        {
            lastAction = Time.time;
            if (isPlaying)
            {
                MakeMove();
            }
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
    protected List<EnemyStructure> GetEnemyStructureList()
    {
        List<EnemyStructure> mylist = new List<EnemyStructure>();

        List<GameObject> allEnemys = PrefabContainer.Instance.enemys;

        int index = 0;
        foreach (GameObject bt in allEnemys)
        {
            EnemyStructure es = new EnemyStructure();
            es.enemy = bt.GetComponent<BaseEnemy>();
            es.id = index;
            index++;
            mylist.Add(es);
        }
        return mylist;
    }


    internal abstract void Reset();
}