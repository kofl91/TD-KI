using UnityEngine;
using System.Collections;

public class CanDisplayDetails : MonoBehaviour {

    void OnMouseUp()
    {
        BaseTower tower = GetComponent<BaseTower>();
        if (tower)
        {
            FindObjectOfType<PlayerUI>().DisplayTowerDetails(tower);
        }
        
    }
}
