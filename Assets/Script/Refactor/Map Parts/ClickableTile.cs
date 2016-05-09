using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;

    void OnMouseUp()
    {
         Debug.Log("Click!");
        //tilemap2.CreateTurretUnit(tileX, tileY);
    }
}
