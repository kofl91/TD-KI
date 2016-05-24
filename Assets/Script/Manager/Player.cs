using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public int Life = 20;
    public int Gold = 100;

    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;
    internal int enemyKilled = 0;

    public int turretsplaced = 0;

    public int failedActions = 0;

    public bool[,] freeTiles;

    List<GameObject> turrets = new List<GameObject>();


    public void Start(){
        freeTiles = new bool[21, 22];
        ClearTiles();
    }


    internal void IncreaseGold(int goldBounty)
    {
        Gold += goldBounty;
    }

    public void EnemyCrossed()
    {
        Life--;
        UIManager.Instance.DrawWaveInfo();
        if (Life == 0)
        {
            Debug.Log("Defeat");
        }
    }


    private void increaseGold()
    {
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            Gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }
    }


    public void Update()
    {
        increaseGold();
    }


    public void CreateTurretUnit(int x, int y, GameObject turretPrefab)
    {
        BaseTurret turret = turretPrefab.GetComponent<BaseTurret>();
        if (turret.goldCost < Gold)
        {
            if (freeTiles[x, y])
            {
                Gold -= turret.goldCost;
                int offsetX = Map.Instance.xOffset;
                int offsetY = Map.Instance.yOffset;
                int tilesize = Map.Instance.tileSize;
                Debug.Log("Turret at:" + x + "/" + y);
                GameObject go = (GameObject)Instantiate(turretPrefab, new Vector3(x * tilesize + offsetX, 2.0f, y * tilesize + offsetY), turretPrefab.transform.rotation);
                turrets.Add(go);
                turretsplaced++;
                freeTiles[x, y] = false;
            }
            else
                failedActions++;
        }
        else
            failedActions++;
    }

    public void RemoveTurrets()
    {
        foreach(GameObject t in turrets)
        {
            Destroy(t.gameObject);
        }
        turrets.Clear();
        ClearTiles();
    }

    public void ClearTiles()
    {
        for (int x = 0; x < Map.Instance.mapSizeX; x++)
        {
            for (int y = 0; y < Map.Instance.mapSizeY; y++)
            {
                if (Map.Instance.grid[x, y] != Map.eTileType.Way)
                    freeTiles[x, y] = true;
                else
                    freeTiles[x, y] = false;
            }
        }
    }

}