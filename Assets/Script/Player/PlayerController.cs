using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour{


    // Game Logic
    public int Life = 20;
    public int Gold = 100;

    // Gold Generation
    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    // For Tower Placement
    public int chosenTower = 0;

    public GameObject grid;
    bool[,] canPlaceHere;
    int mapsizeX;
    int mapsizeY;
    float offsetX = 0;
    float offsetY = 0;
    float tilesize = 1;
    float terrainHeight = 2.0f;
    [SyncVar]
    public int myId=0;

    // AI Score Data
    public int towerPlaced = 0;


    [ClientRpc]
    public void RpcSetID(int id)
    {
        myId = id;
    }
    // Use this for initialization
    [ClientRpc]
    public void RpcInitPlayerController () {

        // Hopefully sets all Components that require to be
        IBelongsToPlayer[] components = (IBelongsToPlayer[])GetComponentsInChildren<IBelongsToPlayer>();

        foreach (IBelongsToPlayer c in components)
        {
            c.SetPlayer(myId);
        }
        // Initialize where tower should be placed
        CreateTiles tilegenerator = gameObject.AddComponent<CreateTiles>();
        tilegenerator.SetPlayer(myId);
        tilegenerator.CreateGrid();
        offsetX = transform.position.x - transform.lossyScale.x * 5 + tilesize / 2;
        offsetY = transform.position.z - transform.lossyScale.z * 5 + tilesize / 2;
        GetComponentInChildren<EndzoneDespawn>().SetPlayer(myId);
        Debug.Log("player initialized "+myId);
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
    [Command]
    public void CmdCreateTurretUnit(Vector3 pos)
    {
        Debug.Log("Creating for:"+myId);
        //if (canPlaceHere[x, y])
        {
            GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
            GameObject go = (GameObject)Instantiate(turretPrefab, new Vector3(pos.x, terrainHeight, pos.z), turretPrefab.transform.rotation);
            BaseTurret turret = go.GetComponent<BaseTurret>();
            if (turret.getCost() < Gold)
            {
                Gold -= turret.getCost();
                go.transform.parent = transform;
                turret.SetPlayer(myId);
                //canPlaceHere[x, y] = false;
                towerPlaced++;
                NetworkServer.Spawn(go);
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

    public void setPlaceAbleArea(bool[,] area,int x,int y)
    {
        canPlaceHere = area;
        mapsizeX = x;
        mapsizeY = y;
    }

    public int getMapSizeY()
    {
        return mapsizeY;
    }

    public int getMapSizeX()
    {
        return mapsizeX;
    }

    public bool[,] getPlaceAbleArea()
    {
        return canPlaceHere;
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
}
