using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;

    void OnMouseUp()
    {
        GameManager.Instance.SendMessage("TileClicked",this);
    }
}
