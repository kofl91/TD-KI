using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour,IPlayer {


    // Game Logic
    public int Life = 20;
    public int Gold = 100;

    // Gold Generation
    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    // For Tower Placement
    public int chosenTower = 0;


    public GridStructure grid;

    // AI Score Data
    private int towerPlaced = 0;

    // Use this for initialization
    void Awake () {

        // Hopefully sets all Components that require to be
        IBelongsToPlayer[] components = (IBelongsToPlayer[])GetComponentsInChildren<IBelongsToPlayer>();

        foreach (IBelongsToPlayer c in components)
        {
            c.SetPlayer(this);
        }  
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
            GameObject go = (GameObject)Instantiate(turretPrefab);
            go.transform.position = grid.tiles[x, y].obj.transform.position;
            BaseTurret turret = go.GetComponent<BaseTurret>();
            if (turret.getCost() < Gold)
            {
                Gold -= turret.getCost();
                go.transform.parent = transform;
                BaseTurret tower = go.GetComponent<BaseTurret>();
                tower.SetPlayer(this);
                grid.tiles[x, y].type = eTile.Tower;
                towerPlaced++;
            }
            else
            {
                Destroy(go);
            }
        }
    }

    public void ChooseTower(int ID)
    {
        chosenTower = ID;
    }

    public void removeAllTower()
    {
        BaseTurret[] towerList = GetComponentsInChildren<BaseTurret>();
        foreach(BaseTurret t in towerList)
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
        GameObject go = (GameObject)Instantiate(tower.prefab);
        go.transform.position = tile.obj.transform.position;

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
    #endregion
}
