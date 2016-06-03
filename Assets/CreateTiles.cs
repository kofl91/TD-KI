using UnityEngine;
using System.Collections;

// WORKS ONLY IF GAMEOBJECT IS A PLANE!!!
public class CreateTiles : MonoBehaviour {

    public GameObject tile;

	// Use this for initialization
	void Start () {
        RaycastHit hit;
        Vector3 runPosi;
        runPosi = transform.position;
        // Find a corner of the plane.
        runPosi.x -= transform.lossyScale.x * 5;
        runPosi.z -= transform.lossyScale.z * 5;
        // Adjust for the size of a Tile
        runPosi.x += 0.5f;
        runPosi.z += 0.5f;
        for (int x = 0; x < 10*transform.lossyScale.x; x++)
        {
            for (int y = 0; y < 10 * transform.lossyScale.z; y++)
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
                    newTile.transform.parent = transform;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
