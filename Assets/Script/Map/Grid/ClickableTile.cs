using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public PlayerController owner;

    void OnMouseUp()
    {
        if (!owner)
        {
            owner = GetComponentInParent<PlayerController>();
        }
        owner.CreateTurretUnit(tileX, tileY);
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
