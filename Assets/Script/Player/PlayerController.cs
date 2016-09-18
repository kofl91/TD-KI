using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerController : NetworkBehaviour, IPlayer
{


    // Game Logic
    [SyncVar]
    public int Life = 20;
    [SyncVar]
    public int Gold = 100;

    // Gold Generation
    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    private int sendMinionCount = 0;

    private PlayerController enemy;
    // For Tower Placement
    [SyncVar]
    public int chosenTower = 0;

    public GridStructure grid;

    public PlayerUI UI;

    private bool isNetworkPlayer = false;

    public void Init()
    {
        // Only make grid enabled when you are the local player
        if (GetComponentInChildren<NetworkIdentity>())
        {
            isNetworkPlayer = true;
            GetComponentInChildren<GridMaker>().MakeGrid(isLocalPlayer);
            if (isLocalPlayer)
            {
                UI = FindObjectOfType<PlayerUI>();
                UI.player = this;
                //UI.SetActive(true);
            }
        }
        else
        {
            GetComponentInChildren<GridMaker>().MakeGrid(true);
        }
        grid = GetComponentInChildren<GridMaker>().GetGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            Gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }

        if (isLocalPlayer)
        {
            ScoreManager sm = FindObjectOfType<ScoreManager>();
            sm.ownLife = Life;
            sm.remainingGold = Gold;
            sm.sendMinions = sendMinionCount;
            if (Life <= 0)
            {
                RpcLoadGameOverScreen();

            }
            if (!enemy)
            {
                foreach (PlayerController p in FindObjectsOfType<PlayerController>())
                {
                    if (p != this)
                    {
                        enemy = p;
                    }
                }

            }
            else if (enemy.Life <= 0)
            {
                RpcLoadVictoryScene();

            }

        }
    }

    #region GameLogicFunctions
    public bool ChangeBalance(int goldDifference)
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

    public void SpawnTower(int x, int y)
    {
        Debug.Log("offline");
        GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
        Gold -= turretPrefab.GetComponent<BaseTower>().buildCost;
        GameObject go = (GameObject)Instantiate(turretPrefab);
        go.transform.position = grid.tiles[x, y].obj.transform.position;
        go.transform.parent = transform;
    }

    [Command]
    public void CmdSpawnTower(int x, int y)
    {
        Debug.Log("online");
        GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
        Gold -= turretPrefab.GetComponent<BaseTower>().buildCost;
        GameObject go = (GameObject)Instantiate(turretPrefab);
        go.transform.position = grid.tiles[x, y].obj.transform.position;
        go.transform.SetParent(transform);
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

    private GridEvaluator gridEvaluator;
    public void DisplayBestMoves()
    {
        if (gridEvaluator == null)
        {
            gridEvaluator = new GridEvaluator(grid);
        }
        gridEvaluator.DisplayTopTenPositions();
    }

    public void ChooseTower(int ID)
    {
        if (isNetworkPlayer)
        {
            CmdChooseTower(ID);
        }
        else
        {
            chosenTower = ID;
        }
    }

    [Command]
    public void CmdChooseTower(int ID)
    {
        chosenTower = ID;
    }

    public void removeAllTower()
    {
        BaseTower[] towerList = GetComponentsInChildren<BaseTower>();
        foreach (BaseTower t in towerList)
        {
            Destroy(t.gameObject);
        }
    }

    public void SellTower(BaseTower tower)
    {
        Gold += (int)(tower.buildCost * 0.7);
        grid.tiles[tower.posX, tower.posY].type = eTile.Free;
        Destroy(tower.gameObject);
    }

    public void SellTower(int x, int y)
    {
        Debug.Log("Selling at: " + x + " / " + y);
        BaseTower tower = grid.tiles[x, y].tower.GetComponent<BaseTower>();
        Gold += (int)(tower.buildCost * 0.7);
        grid.tiles[tower.posX, tower.posY].type = eTile.Free;
        Destroy(tower.gameObject);
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
        go.transform.SetParent(transform);
        tile.type = eTile.Tower;

        if (grid == null)
        {
            grid = GetComponentInChildren<GridMaker>().GetGrid();
        }
        grid.tiles[tile.xPos, tile.yPos].tower = go;
        go.GetComponent<BaseTower>().posX = tile.xPos;
        go.GetComponent<BaseTower>().posY = tile.yPos;
        Debug.Log("Build at: " + tile.xPos + " / " + tile.yPos);
    }

    public void CreateTurretUnit(int x, int y)
    {
        if (grid == null)
        {
            grid = GetComponentInChildren<GridMaker>().GetGrid();
        }
        if (grid.tiles[x, y].type == eTile.Free)
        {
            GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
            BaseTower tower = turretPrefab.GetComponent<BaseTower>();
            if (tower.buildCost < Gold)
            {
                grid.tiles[x, y].type = eTile.Tower;
                if (GetComponent<NetworkIdentity>())
                    CmdSpawnTower(x, y);
                else
                    SpawnTower(x, y);
            }
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

    public void SendEnemys(EnemyStructure es)
    {
        sendMinionCount++;
        CmdSendMinion(es.id);
    }

    public void SendEnemys(int id)
    {
        sendMinionCount++;
        CmdSendMinion(id);
    }

    [Command]
    public void CmdSendMinion(int id)
    {
        int cost = 50;
        // Kann sich der Spieler die Einheit leisten?
        if (Gold > cost)
        {
            Gold -= cost;
            FindObjectOfType<Spawner>().CmdHireMinion(gameObject, id);
        }
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
        foreach (TileStructure ts in grid.tiles)
        {
            if (ts.type == eTile.Tower)
                ts.type = eTile.Free;
        }
    }

    [ClientRpc]
    void RpcLoadVictoryScene()
    {
        if (isLocalPlayer)
        {
            SceneManager.LoadScene("victory");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    [ClientRpc]
    void RpcLoadGameOverScreen()
    {
        if (isLocalPlayer)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene("victory");
        }
    }




    #endregion
}
