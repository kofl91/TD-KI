﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoSingleton<Spawner> {


    public PlayerController player1;
    public PlayerController player2;

    public GameObject neutralSpawnPoint;
    public GameObject hiredSpawnPoint;
    public GameObject basePlayer1;
    public GameObject basePlayer2;


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
        HireMinion(player1, enemyID);
    }


    public void HireMinion(PlayerController sendingPlayer,int enemyID)
    {
        int cost = PrefabContainer.Instance.enemys[enemyID].GetComponent<BaseEnemy>().bounty;
        if (sendingPlayer.Gold> cost) {
            sendingPlayer.Gold -= cost;
            GameObject towards;
            PlayerController playerToSendTowards;
            if (sendingPlayer == player1)
            {
                towards = basePlayer2;
                playerToSendTowards = player2;
            }
            else
            {
                towards = basePlayer1;
                playerToSendTowards = player1;
            }   
            spawnMinionAtTowardsVersus(enemyID, hiredSpawnPoint.transform, towards, playerToSendTowards);
        }
        
    }

    public GameObject spawnMinionAtTowardsVersus(int enemyID, Transform at, GameObject towards, PlayerController player)
    {
        Debug.Log(at);
        Debug.Log(this.transform);
        GameObject spawnedMinion = Instantiate(PrefabContainer.Instance.enemys[enemyID], at.position, at.rotation) as GameObject;
        spawnedMinion.GetComponent<BaseEnemy>().target = towards;
        spawnedMinion.GetComponent<BaseEnemy>().enemy = player;
        spawnedMinion.transform.parent = container.transform;
        return spawnedMinion;
    }

    public void spawnRegular()
    {
        int enemyID = waves[0].enemyID;
        waves[0].count--;
        activeWaveEnemys.Add(spawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform, basePlayer1, player1));
        activeWaveEnemys.Add(spawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform, basePlayer2, player2));
    }

    // Use this for initialization
    void Start()
    {
        // Get Waves from components
        waves = new List<Wave>();
        Wave[] buffWaves = GetComponents<Wave>();
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