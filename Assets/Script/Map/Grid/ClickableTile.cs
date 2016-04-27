using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap tilemap;
    public TileMap2 tilemap2;

    void OnMouseUp()
    {
        // Debug.Log("Click!");
        tilemap2.CreateTurretUnit(tileX, tileY);
    }
}
