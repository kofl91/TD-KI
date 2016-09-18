using UnityEngine;
using System.Collections;

public class IncreaseValueOnTriggerEnter : MonoBehaviour {

    public TileStructure tile;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseEnemy>())
        {
            tile.minionsPassed += 1;
            tile.obj.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
