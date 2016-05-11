using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public int Life = 20;
    public int Gold = 100;

    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;
    internal int enemyKilled = 0;

    public bool[,] freeTiles;

    List<GameObject> turrets = new List<GameObject>();


    public void Start(){
        freeTiles = new bool[6, 6];
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                freeTiles[x, y] = true;
            }
        }
    }


    internal void IncreaseGold(int goldBounty)
    {
        Gold += goldBounty;
    }

    public void EnemyCrossed()
    {
        Life--;
        UIManager.Instance.DrawWaveInfo();
        if (Life == 0)
        {
            Debug.Log("Defeat");
        }
    }


    private void increaseGold()
    {
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            Gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }
    }


    public void Update()
    {
        increaseGold();
    }


    public void CreateTurretUnit(int x, int y, GameObject turretPrefab)
    {
        //Debug.Log("TurretBUILDING!!!!");
        BaseTurret turret = turretPrefab.GetComponent<BaseTurret>();
        if (turret.goldCost < Gold)
        {
            if (freeTiles[x, y])
            {
                Gold -= turret.goldCost;
                turrets.Add((GameObject)Instantiate(turretPrefab, new Vector3(x, 1, y), Quaternion.identity));
                freeTiles[x, y] = false;
            }  
        }
    }

    public void RemoveTurrets()
    {
        foreach(GameObject t in turrets)
        {
            Destroy(t.gameObject);
        }
        turrets.Clear();
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                freeTiles[x, y] = true;
            }
        }
    }

}