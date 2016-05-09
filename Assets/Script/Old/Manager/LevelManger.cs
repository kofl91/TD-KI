using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManger : MonoSingleton<LevelManger> {

    private int lifePoint = 10;

    private bool spawnActive = false;

    private bool waveActive = false;

    private List<Wave> waves = new List<Wave>();

    private int currentWave = 1;

    private int gold = 100;

    private int goldIncrement = 1;
    private float lastGoldIncrement;
    private float goldIncrementDelay = 1.0f;

    public int waveSpawnDelay = 15;
    private float lastWaveSpawn;

    public override void Init()
    {
        foreach (Wave w in GetComponents<Wave>())
        {
            waves.Add(w);
        }
    }

    public int GetCurrentWave()
    {
        return 1;
    }

    public int GetEnemysLeft()
    {
        return SpawnManager.Instance.getEnemysLeft();
    }

    public int GetCurrentGold()
    {
        return gold;
    }

    internal void IncreaseGold(int goldBounty)
    {
        gold += goldBounty;
    }

    public int GetLivesLeft()
    {
        return lifePoint;
    }

    public void StartWave()
    {
        waves[0].StartWave();
        spawnActive = true;
        waveActive = true;
    }

    public void EndWave()
    {
        Destroy(waves[0]);
        waves.RemoveAt(0);
        spawnActive = false;
        currentWave++;
    }


    public void EnemyCrossed()
    {
        lifePoint--;
        UIManager.Instance.DrawWaveInfo();
        if (lifePoint == 0)
        {
            Defeat();
        }
    }

    private void Victory()
    {
        Debug.Log("Victory!");
        UIManager.Instance.DrawMessage("Victory");

    }

    private void Defeat () {

        // Stop waves from Spawning
        spawnActive = false;
        waveActive = false;
        waves[0].isPlaying = false;
        SpawnManager.Instance.ClearEnemys();
        Debug.Log("Defeat");
        UIManager.Instance.DrawMessage("Defeat");
    }

    private void Update()
    {
        increaseGold();
        UIManager.Instance.DrawResourcesInfo();
        UIManager.Instance.DrawWaveInfo();
        if (!waveActive)
        {
            if (Time.time - lastWaveSpawn > waveSpawnDelay && waveSpawnDelay != 0.0f)
            {
                StartWave();
                lastWaveSpawn = Time.time;
            }
        }else
        {
            if(!spawnActive && !GameObject.FindGameObjectWithTag("Enemy"))
            {
                Debug.Log("Wave cleared!");
                waveActive = false;
                if (waves.Count == 0)
                    Victory();
            }
        }
        
    }

    private void increaseGold()
    {
        if (Time.time - lastGoldIncrement > goldIncrementDelay && goldIncrementDelay != 0.0f)
        {
            gold += goldIncrement;
            lastGoldIncrement = Time.time;
        }
    }


    public void CreateTurretUnit(int x, int y, GameObject turretPrefab)
    {
        BaseTurret turret = turretPrefab.GetComponent<BaseTurret>();
        if (turret.cost < gold)
        {
            gold -= turret.cost;
            GameObject go = (GameObject)Instantiate(turretPrefab, new Vector3(x, 1, y), Quaternion.identity);
        }
    }

}