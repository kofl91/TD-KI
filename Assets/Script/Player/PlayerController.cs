using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour,IPlayer {


    // Game Logic
    [SyncVar]
    public int Life = 20;
    [SyncVar]
    public int Gold = 100;

    // Gold Generation
    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    // For Tower Placement
    [SyncVar]
    public int chosenTower = 0;

    public GridStructure grid;
    
    void Start()
    {
        // Only make grid enabled when you are the local player
        if (GetComponentInChildren<NetworkIdentity>())
        {
            GetComponentInChildren<GridMaker>().MakeGrid(isLocalPlayer);
        }
        else
        {
            GetComponentInChildren<GridMaker>().MakeGrid(true);
        }
        grid = GetComponentInChildren<GridMaker>().GetGrid();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            Gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }
    }

    #region GameLogicFunctions
    internal bool ChangeBalance(int goldDifference)
    {
        Gold += goldDifference;
        if (Gold < 0)
        {
            Gold -= goldDifference;
            return false;
        }
        return true;
    }

    public void EnemyCrossed()
    {
        Life--;
        if (Life == 0)
        {
            Debug.Log("GameOver");
        }
    }

    public void CreateTurretUnit(int x, int y)
    {
        if (grid == null)
        {
            grid = GetComponentInChildren<GridMaker>().GetGrid();
        }
        if (grid.tiles[x,y].type == eTile.Free)
        {
            GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
            BaseTower tower = turretPrefab.GetComponent<BaseTower>();
            if (tower.buildCost < Gold)
            {
                grid.tiles[x, y].type = eTile.Tower;
                if (GetComponent<NetworkIdentity>())
                    CmdSpawnTower(x, y);
                else
                    SpawnTower(x,y);
            }
        }
    }

    public void SpawnTower(int x, int y)
    {
        GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
        Gold -= turretPrefab.GetComponent<BaseTower>().buildCost;
        GameObject go = (GameObject)Instantiate(turretPrefab);
        go.transform.position = grid.tiles[x, y].obj.transform.position;
        go.transform.parent = transform;
    }

    [Command]
    public void CmdSpawnTower(int x, int y)
    {
        
        GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
        Gold -= turretPrefab.GetComponent<BaseTower>().buildCost;
        GameObject go = (GameObject)Instantiate(turretPrefab);
        go.transform.position = grid.tiles[x, y].obj.transform.position;
        go.transform.parent = transform;
        NetworkServer.Spawn(go);
    }

    internal DamageInfo GetNextEnemyResistance()
    {
        return FindObjectOfType<Spawner>().GetNextEnemy().GetResistance();
    }

    internal DamageInfo GetOpponentTowerDmg()
    {
        PlayerController[] allPlayer = GameObject.FindObjectsOfType<PlayerController>();
        PlayerController opponent;
        foreach (PlayerController p in allPlayer)
        {
            if (p != this)
            {
                opponent = p.GetComponent<PlayerController>();
                return opponent.GetCurrentTowerDmg();
            }
        }
        Debug.Log("No other player found!");
        return new DamageInfo();
    }

    public PlayerController GetOppponent()
    {
        PlayerController[] allPlayer = GameObject.FindObjectsOfType<PlayerController>();

        foreach (PlayerController p in allPlayer)
        {
            if (p != this)
            {
                return p;
            }
        }
        Debug.Log("No other player found!");
        return null;
    }

    internal DamageInfo GetCurrentTowerDmg()
    {
        DamageInfo myDmg = new DamageInfo();
        List<BaseTower> turrets = new List<BaseTower>();
        turrets.AddRange(GetComponentsInChildren<BaseTower>());
        foreach (BaseTower t in turrets)
        {
            myDmg.Add(t.turretDmg);
        }
        return myDmg;
    }

    public void ChooseTower(int ID)
    {
        chosenTower = ID;
    }

    public void removeAllTower()
    {
        BaseTower[] towerList = GetComponentsInChildren<BaseTower>();
        foreach(BaseTower t in towerList)
        {
            Destroy(t.gameObject);
        }
    }
    #endregion

    #region InterfaceImplementation

    /** INTERFACE FUNCTIONS */
    public bool SpendMoney(int cost)
    {
        return ChangeBalance(-cost);
    }

    public void BuildTower(TowerStructure tower, TileStructure tile)
    {
        if (grid == null)
        {
            grid = GetComponentInChildren<GridMaker>().GetGrid();
        }
        GameObject go = (GameObject)Instantiate(tower.prefab);
        go.transform.position = tile.obj.transform.position;
        go.transform.parent = this.transform;
        tile.type = eTile.Tower;

        if (grid == null)
        {
            grid = GetComponentInChildren<GridMaker>().GetGrid();
        }

        if (grid.tiles[tile.xPos, tile.yPos].type != eTile.Tower)
        {
            grid.tiles[tile.xPos, tile.yPos].type = eTile.Tower;
            Debug.Log("Still had to be done!");
        }
    }

    public bool LooseLife()
    {
        EnemyCrossed();
        return Life < 0;
    }

    public int GetMoney()
    {
        return Gold;
    }

    internal void SendEnemys(EnemyStructure es)
    {
        FindObjectOfType<Spawner>().HireMinion(this, es.Id);
    }

    public int GetLife()
    {
        return Life;
    }

    internal void Reset()
    {
        removeAllTower();
        Life = 20;
        Gold = 100;
        resetGrid();
    }

    void resetGrid()
    {
        foreach(TileStructure ts in grid.tiles)
        {
            if (ts.type == eTile.Tower)
                ts.type = eTile.Free;
        }
    }

    #endregion
}
