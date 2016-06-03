using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;

    void Start()
    {
        Debug.Log("I exist");
    }


    void OnMouseUp()
    {
        //GameManager.Instance.SendMessage("TileClicked",this);
        Debug.Log("Clicked");
    }

    void OnMouseOver()
    {
        Debug.Log("Over");
        GetComponent<MeshRenderer>().enabled = true;
    }

    void OnMouseExit()
    {
        Debug.Log("Out");
        GetComponent<MeshRenderer>().enabled = false;
    }
}
