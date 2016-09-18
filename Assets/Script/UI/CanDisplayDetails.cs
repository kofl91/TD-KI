using UnityEngine;
using System.Collections;

public class CanDisplayDetails : MonoBehaviour {

    public enum ObjectType { EnemyField,TowerField,EnemyIcon,TowerIcon};

    public ObjectType objectType;

    public GameObject range;

    void OnMouseUp()
    {
        BaseTower tower = GetComponent<BaseTower>();
        if (tower)
        {
            FindObjectOfType<PlayerUI>().DisplayTowerDetails(tower);
        }
        
    }

    void OnMouseEnter()
    {
        range.SetActive(true);
        float size = GetComponent<BaseTower>().range * 2;
        range.transform.localScale = new Vector3(size, size, size);
    }

    void OnMouseExit()
    {
        range.SetActive(false);
    }
}
