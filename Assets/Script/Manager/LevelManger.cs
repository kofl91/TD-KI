using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManger : MonoSingleton<LevelManger> {

    private int lifePoint = 10;

    private bool spawnActive = false;

    private bool waveActive = false;

    private List<Wave> waves = new List<Wave>();

    public override void Init()
    {
        UIManager.Instance.DrawWaveInfo();
        foreach (Wave w in GetComponents<Wave>())
        {
            waves.Add(w);
        }
    }

    public void StartWave()
    {
        waves[0].StartWave();
        spawnActive = true;
        waveActive = true;
        UIManager.Instance.DrawWaveInfo();
    }

    public void EndWave()
    {
        Destroy(waves[0]);
        waves.RemoveAt(0);
        spawnActive = false;
        UIManager.Instance.DrawWaveInfo();
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
    }

    private void Defeat () {

        // Stop waves from Spawning
        spawnActive = false;
        waveActive = false;
        waves[0].isPlaying = false;
        SpawnManager.Instance.ClearEnemys();
        Debug.Log("Defeat");
	}

    private void Update()
    {
        if (!waveActive)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                StartWave();
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
}