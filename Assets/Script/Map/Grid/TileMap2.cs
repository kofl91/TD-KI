using UnityEngine;
using System.Collections;
using System;

public class TileMap2 : MonoBehaviour
{
    public TileType[] tileTypes;

    public GameObject turretPrefab;

    int[,] tiles;

    int mapSizeX = 20;
    int mapSizeY = 20;

    void Start()
    {
        tiles = new int[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 1;
            }
        }
        setWayFromTo(3, 0, 3, 3);
        setWayFromTo(3, 3, 17, 2);
        setWayFromTo(4, 0, 4, 2);
            
        setWayFromTo(4, 2, 17, 2);
        setWayFromTo(17, 2, 17, 7);
        setWayFromTo(16, 2, 16, 6);
        setWayFromTo(16, 6, 3, 6);
        setWayFromTo(17, 7, 4, 7);
        setWayFromTo(4, 7, 4, 10);
        setWayFromTo(4, 10, 17, 10);
        setWayFromTo(17, 10, 17, 15);
        setWayFromTo(17, 15, 4, 15);
        setWayFromTo(4, 15, 4, 19);

        setWayFromTo(3, 7, 3, 11);
        setWayFromTo(3, 11, 16, 11);
        setWayFromTo(16, 11, 16, 14);
        setWayFromTo(16, 14, 3, 14);
        setWayFromTo(3, 14, 3, 19);
       
        GenerateMapVisual();
    }

    internal void CreateTurretUnit(int tileX, int tileY)
    {
        LevelManger.Instance.CreateTurretUnit(tileX, tileY, turretPrefab);
    }

    private void setWayFromTo(int x1, int y1, int x2, int y2)
    {
        if (x1 > x2)
        {
            int buf = x1;
            x1 = x2;
            x2 = buf;
        }
        if (y1 > y2)
        {
            int buf = y1;
            y1 = y2;
            y2 = buf;
        }

        if ((x1 <= x2) && (y1 <= y2))
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    tiles[x, y] = 0;
                }
            }
        }
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 1.0f, y), Quaternion.identity);
                if (tiles[x, y] == 1)
                {
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                    ct.tilemap2 = this;
                }

            }
        }
    }

  
}