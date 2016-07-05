using UnityEngine;
using System.Collections;
using System;

// WORKS ONLY IF GAMEOBJECT IS A PLANE!!!
public class CreateTiles : MonoBehaviour, IBelongsToPlayer
{

    public GameObject tile;
    private PlayerController owner;
    public int player;

    bool[,] canPlaceHere;
    public void SetPlayer(int id)
    {
        player = id;
    }

    public PlayerController GetPlayer()
    {
        return GameObject.FindObjectsOfType<PlayerController>()[player - 1];
    }


    // Use this for initialization
    public void CreateGrid()
    {
        Vector3 runPosi;
        runPosi = transform.position;
        // Find a corner of the plane.
        runPosi.x -= transform.lossyScale.x * 5;
        runPosi.z -= transform.lossyScale.z * 5;
        // Adjust for the size of a Tile
        runPosi.x += 0.5f;
        runPosi.z += 0.5f;
        int gridSize = 2;
        int sizeX = (int)Math.Floor(10.0f * gridSize);
        int sizeY = (int)Math.Floor(10.0f * gridSize);
        canPlaceHere = new bool[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector3 deltaPosi;

                deltaPosi.x = x;
                deltaPosi.z = y;
                deltaPosi.y = 2.0f;
                Vector3 spawnPosi = runPosi + deltaPosi;
                spawnPosi.y = 2.0f;
                if (Terrain.activeTerrain.SampleHeight(spawnPosi) > 1.5f)
                {
                    GameObject newTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    newTile.transform.position = spawnPosi;
                    newTile.AddComponent<ClickableTile>();
                    newTile.GetComponent<MeshRenderer>().enabled = false;

                    ClickableTile clickTile = newTile.GetComponent<ClickableTile>();
                    if (clickTile)
                    {
                        clickTile.tileX = x;
                        clickTile.tileY = y;
                        clickTile.SetPlayer(player);
                    }
                    newTile.transform.parent = transform;
                    // here i can place
                    canPlaceHere[x, y] = true;
                }
                else
                    canPlaceHere[x, y] = false;
            }
        }
        GetPlayer().setPlaceAbleArea(canPlaceHere, sizeX, sizeY);
    }
}
