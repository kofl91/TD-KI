using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public PlayerController owner;

    void OnMouseUp()
    {
        Debug.Log("X:"+tileX+";Y:"+tileY);
        if (owner)
        {
            owner.CreateTurretUnit(tileX, tileY);
        }
    }

    void OnMouseOver()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
