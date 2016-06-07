using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    // Game Logic
    public int Life = 20;
    public int Gold = 100;

    // Gold Generation
    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    // For Tower Placement
    public int chosenTower = 0;

    public GameObject grid;
    float offsetX = 0;
    float offsetY = 0;
    float tilesize = 1;
    float terrainHeight = 2.0f;
 

    // Use this for initialization
    void Start () {

        // Hopefully sets all Components that require to be
        IBelongsToPlayer[] components = (IBelongsToPlayer[])GetComponentsInChildren<IBelongsToPlayer>();

        foreach (IBelongsToPlayer c in components)
        {
            c.SetPlayer(this);
        }

        // Initialize where tower should be placed
        if (grid)
        {
            offsetX = grid.transform.position.x - grid.transform.lossyScale.x * 5 + tilesize / 2;
            offsetY = grid.transform.position.z - grid.transform.lossyScale.z * 5 + tilesize / 2;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            Gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }
    }

    #region GameLogicFunctions
    internal bool ChangeBalance(int goldDifference)
    {
        Gold += goldDifference;
        if (Gold < 0)
        {
            Gold -= goldDifference;
            return false;
        }
        return true;
    }

    public void EnemyCrossed()
    {
        Life--;
        if (Life == 0)
        {
            Debug.Log("GameOver");
        }
    }

    public void CreateTurretUnit(int x, int y)
    {
        GameObject turretPrefab = PrefabContainer.Instance.turrets[chosenTower];
        BaseTurret turret = turretPrefab.GetComponent<BaseTurret>();
        if (turret.getCost() < Gold)
        {
            Gold -= turret.getCost();
            GameObject go = (GameObject)Instantiate(turretPrefab, new Vector3(x * tilesize + offsetX, terrainHeight, y * tilesize + offsetY), turretPrefab.transform.rotation);
            go.transform.parent = transform;
            BaseTurret tower = go.GetComponent<BaseTurret>();
            tower.SetPlayer(this);
        }
    }

    public void ChooseTower(int ID)
    {
        chosenTower = ID;
    }

    #endregion
}
