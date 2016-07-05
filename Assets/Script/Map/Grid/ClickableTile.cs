using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour, IBelongsToPlayer {

    public int tileX;
    public int tileY;
    public int player;

    void OnMouseUp()
    {
        GetPlayer().CmdCreateTurretUnit(transform.position);
    }

    void OnMouseOver()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    public void SetPlayer(int id)
    {
        player = id;

    }

    public PlayerController GetPlayer()
    {
        return GameObject.FindObjectsOfType<PlayerController>()[player - 1];
    }

}
