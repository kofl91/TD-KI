using UnityEngine;
using System.Collections;
using System;

public class TileMap : MonoBehaviour
{




    public TileType[] tileTypes;

    public GameObject turretPrefab;

    int[,] tiles;

    int mapSizeX = 10;
    int mapSizeY = 10;

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
        setWayFromTo(3, 0, 3, 9);

        setWayFromTo(3, 4, 6, 4);

        setWayFromTo(6, 4, 6, 9);

        GenerateMapVisual();
    }

    private void setWayFromTo(int x1, int y1, int x2, int y2)
    {
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
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 3f, y), Quaternion.identity);
                if (tiles[x, y] == 1)
                {
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                    ct.tilemap = this;
                }

            }
        }
    }

    public void CreateTurretUnit(int x, int y)
    {
        GameObject go = (GameObject)Instantiate(turretPrefab, new Vector3(x, 1 , y), Quaternion.identity);
    }
}