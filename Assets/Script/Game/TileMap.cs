using UnityEngine;
using System.Collections;

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
                tiles[x, y] = 0;
            }
        }
        tiles[3, 1] = 2;
        tiles[3, 2] = 1;
        tiles[3, 3] = 2;
        tiles[3, 4] = 2;


        GenerateMapVisual();
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, y), Quaternion.identity);
                if(tiles[x,y] == 1)
                {
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                    ct.tilemap = this;
                }
              
            }
        }
    }

    public void CreateTurretUnit(int x,int y)
    {
        Instantiate(turretPrefab, new Vector3(x, 1, y), Quaternion.identity);
    }
}