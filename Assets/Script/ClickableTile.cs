using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap tilemap;

    void OnMouseUp()
    {
        Debug.Log("Click!");
        tilemap.CreateTurretUnit(tileX, tileY);
    }
}
