﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoSingleton<Spawner> {


    public PlayerController player1;
    public PlayerController player2;

    public GameObject neutralSpawnPoint;
    public GameObject hiredSpawnPoint;
    private GameObject basePlayer1;
    private GameObject basePlayer2;


    public GameObject container;

    private List<GameObject> activeWaveEnemys = new List<GameObject>();

    private List<Wave> waves;
    private int waveNumber = 0;

    public float delayBetweenWaves = 20.0f;
    private float delayLeft = 0.0f;

    // States
    public bool isSpawning = true;
    private bool isWaitingForNextWave = false;

    private float intervalLeft = 0.0f;


    public void HireMinionForPlayer1(int enemyID)
    {
        HireMinion(1, enemyID);
    }

    public void HireMinionForPlayer2(int enemyID)
    {
        HireMinion(2, enemyID);
    }


    public void HireMinion(int sendingPlayer,int enemyID)
    {
        int cost = PrefabContainer.Instance.enemys[enemyID].GetComponent<BaseEnemy>().bounty;
        PlayerController myplayer = GameObject.FindObjectsOfType<PlayerController>()[sendingPlayer - 1];
        if (myplayer.Gold> cost) {
            myplayer.Gold -= cost;
            GameObject towards;
            int playerToSendTowards;
            if (sendingPlayer == 1)
            {
                towards = basePlayer2;
                playerToSendTowards = 2;
            }
            else
            {
                towards = basePlayer1;
                playerToSendTowards = 1;
            }   
            spawnMinionAtTowardsVersus(enemyID, hiredSpawnPoint.transform, towards, playerToSendTowards);
        }
        
    }

    public GameObject spawnMinionAtTowardsVersus(int enemyID, Transform at, GameObject towards, int player)
    {
        GameObject spawnedMinion = Instantiate(PrefabContainer.Instance.enemys[enemyID], at.position, at.rotation) as GameObject;
        spawnedMinion.transform.parent = container.transform;
        spawnedMinion.GetComponent<BaseEnemy>().SetTarget(towards.transform.position);
        spawnedMinion.GetComponent<BaseEnemy>().enemyPlayerID = player;
        if (NetworkServer.active)
            NetworkServer.Spawn(spawnedMinion);
   
        return spawnedMinion;
        
    }

    public void spawnRegular()
    {
        int enemyID = waves[0].enemyID;
        waves[0].count--;
        activeWaveEnemys.Add(spawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform, basePlayer1, 1));
        activeWaveEnemys.Add(spawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform, basePlayer2, 2));
    }

    // Use this for initialization
    void Start()
    {
        // Get Waves from components
        waves = new List<Wave>();
        Wave[] buffWaves = GetComponents<Wave>();
        basePlayer1 = player1.GetComponentInChildren<EndzoneDespawn>().gameObject;
        basePlayer2 = player2.GetComponentInChildren<EndzoneDespawn>().gameObject;
        foreach (Wave w in buffWaves)
        {
            waves.Add(w);
        }
    }
	
	// Update is called once per frame
	void Update () {

        //Spawning Logic
        if (isSpawning)
        {
            intervalLeft -= Time.deltaTime;
            if (intervalLeft < 0.0f)
            {
                intervalLeft = waves[0].interval;
                spawnRegular();
                if (waves[0].count <= 0)
                {
                    isSpawning = false;
                    waves.RemoveAt(0);
                    waveNumber++;
                    if (waves.Count > 0)
                    {
                        isWaitingForNextWave = true;
                        delayLeft = delayBetweenWaves;
                    }
                }
            }
        }

        if (isWaitingForNextWave)
        {
            delayLeft -= Time.deltaTime;
            if (delayLeft < 0.0f)
            {
                isWaitingForNextWave = false;
                isSpawning = true;
            }
        }

        if (waves.Count <= 0)
        {
            if (waveIsOver())
            {
                Debug.Log("Game Over");
            }
        }
    }

    private bool waveIsOver()
    {
        return activeWaveEnemys.Count == 0;
    }
}
