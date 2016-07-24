using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController2 : MonoBehaviour, IPlayer
{
    // Game Logic
    public int Life = 20;
    public int Gold = 100;

    // Gold Generation
    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Increases the golf every second
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            Gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }
    }

    #region GameLogicFunctions
    public bool SpendMoney(int cost)
    {
        bool enoughMoney = (Gold >= cost);
        if (enoughMoney)
        {
            Gold -= cost;
        }
        return enoughMoney;
    }

    public void BuildTower(TowerStructure tower, TileStructure tile)
    {
        GameObject go = Instantiate(tower.prefab);
        go.transform.position = tile.obj.transform.position;
    }

    public bool LooseLife()
    {
        Life--;
        return Life >= 0;
    }

    public int GetMoney()
    {
        return Gold;
    }

    #endregion
}
