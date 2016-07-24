using UnityEngine;
using System.Collections;
using System;

// WORKS ONLY IF GAMEOBJECT IS A PLANE!!!
public class CreateTiles : MonoBehaviour,IBelongsToPlayer {

    public GameObject tile;
    private PlayerController owner;

    bool[,] canPlaceHere;
    public PlayerController GetPlayer()
    {
        return owner;
    }

    public void SetPlayer(PlayerController player)
    {
        owner = player;
    }


    // Use this for initialization
    void Start () {
        Vector3 runPosi;
        runPosi = transform.position;
        // Find a corner of the plane.
        runPosi.x -= transform.lossyScale.x * 5;
        runPosi.z -= transform.lossyScale.z * 5;
        // Adjust for the size of a Tile
        runPosi.x += 0.5f;
        runPosi.z += 0.5f;
        int sizeX = (int)Math.Floor(10 * transform.lossyScale.x);
        int sizeY = (int)Math.Floor(10 * transform.lossyScale.z);
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
                    GameObject newTile = Instantiate(tile, spawnPosi, tile.transform.rotation) as GameObject;
                    ClickableTile clickTile = newTile.GetComponent<ClickableTile>();
                    if (clickTile)
                    {
                        clickTile.tileX = x;
                        clickTile.tileY = y;
                        clickTile.owner = owner;
                        
                    }
                    newTile.transform.parent = transform;
                    // here i can place
                    canPlaceHere[x, y] = true;
                }
                else
                    canPlaceHere[x, y] = false;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
