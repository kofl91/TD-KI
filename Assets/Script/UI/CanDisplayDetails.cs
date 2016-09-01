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
        Debug.Log("the size is: " + size);
        range.transform.localScale = new Vector3(size, size, size);
        Debug.Log("the scale is: " + range.transform.lossyScale.ToString());
    }

    void OnMouseExit()
    {
        range.SetActive(false);
    }
}
