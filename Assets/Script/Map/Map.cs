using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Map : MonoSingleton<Map>
{

    public enum eTileType
    {
        Free=0,
        Turret,
        Way
    };

    public GameObject container;

    public eTileType[,] grid;

    public int mapSizeX;
    public int mapSizeY;

    public int tileSize;
    public int xOffset = 0;
    public int yOffset = 0;

    public bool isLoaded = true;

    void Start()
    {
        createMap();
    }

    #region MapCreation
    public abstract void createMap();

    public void clearMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                grid[x, y] = eTileType.Free;
            }
        }
    }


    protected void setWayFromTo(int x1, int y1, int x2, int y2)
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
                    grid[x, y] = eTileType.Way;
                }
            }
        }
    }

    protected void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = PrefabContainer.Instance.tileTypes[(int)grid[x, y]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x* tileSize+ xOffset, 1.0f, y* tileSize+ yOffset), Quaternion.identity);
                go.transform.localScale += new Vector3(tileSize, 0, tileSize);
                go.transform.parent = container.transform;
                if (grid[x, y] != eTileType.Way)
                {
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.tileX = x;
                    ct.tileY = y;
                }

            }
        }
    }
    #endregion
    
}
