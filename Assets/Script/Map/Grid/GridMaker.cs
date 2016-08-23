using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

/*
This Class creates a Grid of a specified size.
*/
public class GridMaker : MonoBehaviour
{
    #region ATTRIBUTES 
    public const int planeSize = 10;

    // A GameObject that is spawned at each point on the Grid.
    public GameObject tile;

    // Configurable attributes
    public float terrainHeight = 2.0f;
    public float terrainHeightThreshold = 0.5f;
    public float tileSize = 1.0f;


    private int sizeX;
    private int sizeY;
    private TileStructure[,] grid;

    private GridStructure myGrid;

    bool enableTiles;

    #endregion

    #region METHODS

    // Use this for initialization
    public void MakeGrid(bool enabled)
    {
        enableTiles = enabled;
        // Find a corner of the plane to start from.
        Vector3 cornerPosition = FindCorner();
        // Calculate gridsize.
        sizeX = (int)Math.Floor(planeSize * transform.lossyScale.x / tileSize);
        sizeY = (int)Math.Floor(planeSize * transform.lossyScale.z / tileSize);
        // Create the grid.
        grid = new TileStructure[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                TileStructure newTile;
                // Transform loop index to Position
                Vector3 deltaPosition = new Vector3(x, terrainHeight, y);
                // Calculate world position
                Vector3 spawnPosition = cornerPosition + tileSize * deltaPosition;
                spawnPosition.y = terrainHeight;
                // If the terrain under the spawnPosition is high enough, it is free for building.
                if (Terrain.activeTerrain.SampleHeight(spawnPosition) > (terrainHeight - terrainHeightThreshold))
                {
                    newTile = new TileStructure(x, y, eTile.Free, SpawnTile(spawnPosition, x, y, true));
                }
                // If it is smaller, it is part of the path
                else
                {
                    newTile = new TileStructure(x, y, eTile.Path, SpawnTile(spawnPosition, x, y, false));
                }
                grid[x, y] = newTile;
            }
        }
        myGrid = new GridStructure(sizeX, sizeY, grid);
    }

    GameObject SpawnTile(Vector3 spawnPosition, int x, int y, Boolean active)
    {
        if (tile)
        {
            GameObject newTile = Instantiate(tile, spawnPosition, Quaternion.identity) as GameObject;
            newTile.transform.localScale = newTile.transform.localScale * tileSize;
            ClickableTile clickTile = newTile.GetComponent<ClickableTile>();
            if (clickTile)
            {
                clickTile.tileX = x;
                clickTile.tileY = y;
            }

            newTile.transform.parent = transform;
            newTile.SetActive(active);
            if (!enableTiles)
            {
                newTile.SetActive(false);
            }
            return newTile;
        }
        return null;
    }

    Vector3 FindCorner()
    {
        Vector3 cornerPosition = transform.position;
        cornerPosition.x -= transform.lossyScale.x * planeSize / 2;
        cornerPosition.z -= transform.lossyScale.z * planeSize / 2;
        // Adjust corner position by the size of the tile.
        cornerPosition.x += tileSize / 2;
        cornerPosition.z += tileSize / 2;
        return cornerPosition;
    }

    #region GETTER
    public GridStructure GetGrid()
    {
        return myGrid;
    }
    #endregion
    #endregion
}
