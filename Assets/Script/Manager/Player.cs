using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    public int Life = 20;
    public int Gold = 100;

    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;
    internal int enemyKilled = 0;

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
        BaseTurret turret = turretPrefab.GetComponent<BaseTurret>();
        if (turret.goldCost < Gold)
        {
            Gold -= turret.goldCost;
            Instantiate(turretPrefab, new Vector3(x, 1, y), Quaternion.identity);
        }
    }

}