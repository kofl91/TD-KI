using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


// Singleton welcher das Spawnen der Minions übernimmt
public class Spawner : NetworkBehaviour {

    #region Attribute
    // Referenzen zu den beiden Spielerns
    private PlayerController[] player;

    // Standorte der  Spawn/Despawn Punkte
    public List<GameObject> neutralSpawnPoints;
    public List<GameObject> hiredSpawnPoints;
    private List<GameObject> playerBases;

    // Parent Container für alle Enemy Objekte
    public GameObject container;
    
    // Liste an Wellen. Werden aus den Componenten gezogen
    private List<Wave> waves;
    // Index der aktuellen Welle
    private int waveNumber = 0;

    // Timing Variablen 
    // Abstand zwischen zwei Wellen
    public float delayBetweenWaves = 20.0f;
    // Übrige Zeit bis zur nächsten Welle
    private float delayLeft = 0.0f;
    // Übrige Zeit bis nächste Einheit gespawnt wird
    private float intervalLeft = 0.0f;

    // Status Variablen
    public bool isSpawning = true;
    private bool isWaitingForNextWave = false;

    public bool preDefinedWaves = false;

    #endregion

    #region SpawnFunktionen
    // Heuert einen Minion für Spieler 1 an.
    public void HireMinionForPlayer1(int enemyID)
    {
        HireMinion(player[0], enemyID);
    }

    // Heuert einen Minion für Spieler 2 an.
    public void HireMinionForPlayer2(int enemyID)
    {
        HireMinion(player[1], enemyID);
    }

    // Heuert einen Minion an
    public void HireMinion(PlayerController sendingPlayer,int enemyID)
    {
        Debug.Log("Should send stuff!");
        int cost = waves[0].enemyBounty;
        // Kann sich der Spieler die Einheit leisten?
        if (sendingPlayer.Gold > cost) {
            sendingPlayer.Gold -= cost;
            GameObject towards;
            PlayerController playerToSendTowards;
            // Entscheide wer der Gegner ist.
            if (sendingPlayer == player[0])
            {
                towards = playerBases[1];
                playerToSendTowards = player[1];
            }
            else
            {
                towards = playerBases[0];
                playerToSendTowards = player[0];
            }   
            foreach ( GameObject spawnPoint in hiredSpawnPoints)
            {
                spawnMinionAtTowardsVersus(enemyID, spawnPoint.transform, towards, playerToSendTowards);
            }
        }
    }

    // Spawnt einen Minion, an einer Position, das zu einem Punkt läuft und gegen eine Spieler ist.
    public GameObject spawnMinionAtTowardsVersus(int enemyID, Transform at, GameObject towards, PlayerController player)
    {
        GameObject spawnedMinion = Instantiate(PrefabContainer.Instance.enemys[enemyID], at.position, at.rotation) as GameObject;
        BaseEnemy buffer = spawnedMinion.GetComponent<BaseEnemy>();
        buffer.target = towards;
        buffer.enemy = player;
        buffer.bounty = waves[0].enemyBounty;
        buffer.SetMaxLife(waves[0].enemyHP);

        spawnedMinion.transform.parent = container.transform;
        return spawnedMinion;
    }

    
    [Command]
    public void CmdspawnMinionAtTowardsVersus(int enemyID, Vector3 at, GameObject towards, GameObject player)
    {
        GameObject spawnedMinion = Instantiate(PrefabContainer.Instance.enemys[enemyID], at, Quaternion.identity) as GameObject;
        BaseEnemy buffer = spawnedMinion.GetComponent<BaseEnemy>();
        buffer.target = towards;
        buffer.enemy = player.GetComponent<PlayerController>();
        buffer.bounty = waves[0].enemyBounty;
        buffer.SetMaxLife(waves[0].enemyHP);
        spawnedMinion.transform.parent = container.transform;
        NetworkServer.Spawn(spawnedMinion);
        spawnedMinion.GetComponent<NavMeshAgent>().enabled = true;
    }


    public bool init = false;
    // Eine Spawn Iteration
    public void spawnRegular()
    {
        if (!init)
        {
            player = FindObjectsOfType<PlayerController>();
            if (player.Length > 1)
            {
                init = true;
                playerBases = new List<GameObject>();
                playerBases.Add(player[0].GetComponentInChildren<EndzoneDespawn>().gameObject);
                playerBases.Add(player[1].GetComponentInChildren<EndzoneDespawn>().gameObject);
            }
            return;
        }

        int enemyID = waves[0].enemyID;
        waves[0].Decr();
        if (GetComponent<NetworkIdentity>())
        {
            foreach (GameObject spawnPoint in neutralSpawnPoints)
            {
                CmdspawnMinionAtTowardsVersus(enemyID, spawnPoint.transform.position, playerBases[0], player[0].gameObject);
                CmdspawnMinionAtTowardsVersus(enemyID, spawnPoint.transform.position, playerBases[1], player[1].gameObject);
            }
            
        }
        else
        {
            foreach (GameObject spawnPoint in neutralSpawnPoints)
            {
                Debug.Log("offline send!");
                spawnMinionAtTowardsVersus(enemyID, spawnPoint.transform, playerBases[0], player[0]);
                spawnMinionAtTowardsVersus(enemyID, spawnPoint.transform, playerBases[1], player[1]);
            }
        }
    }

    [Command]
    public void CmdSpawnOnNetwork(GameObject go)
    {
        NetworkServer.Spawn(go);
    }

    #endregion

    #region Unity
    // Use this for initialization
    void Start()
    {
        if (preDefinedWaves)
        {
            getWavesFromComponent();
        }else
        {
            generateWaves();
        }  
    }
	
    void generateWaves()
    {
        float HP = 5;
        float Bounty = 1;
        int numberEnemyTypes = 5;
        waves = new List<Wave>();
        for (int i = 0; i < 99; i++)
        {
            HP *= 1.1f;
            Bounty *= 1.1f;
            if (i % 10 == 9)
            {
                waves.Add(new Wave((int)HP * 3, (int)Bounty, i % numberEnemyTypes, 5, 2));
            }
            else
            {
                waves.Add(new Wave((int)HP, (int)Bounty, i % numberEnemyTypes, 10, 2));
            }
        }
        foreach (Wave w in waves)
        {
            w.Reset();
        }
    }

    void getWavesFromComponent()
    {
        // Get Waves from components
        waves = new List<Wave>();
        waves.AddRange(GetComponents<Wave>());
        foreach (Wave w in waves)
        {
            w.Reset();
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
                if (waves[0].hasEnded())
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
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    #endregion

    #region Hilfsfunktionen
    // Gibt den Index der aktuellen Welle zurück
    public int GetWave()
    {
        return waveNumber;
    }

    // Gibt zurück ob alle Gegner einer Welle tot sind
    private bool waveIsOver()
    {
        return container.GetComponents<BaseEnemy>().Length == 0;
    }

    // Setzt den Spawnmanager zurück
    internal void Reset()
    {
        foreach (Wave w in waves)
        {
            w.Reset();
        }
        isSpawning = false;
        waveNumber = 0;
        BaseEnemy[] enemys = FindObjectsOfType<BaseEnemy>();
        foreach (BaseEnemy be in enemys)
        {
            Destroy(be.gameObject);
        }
        waves = new List<Wave>();
        waves.AddRange(GetComponents<Wave>());
        foreach (Wave w in waves)
        {
            w.Reset();
        }
    }

    // Gibt den nächsten Gegner zurück
    public BaseEnemy GetNextEnemy()
    {
        if (waves.Count > 1)
        {
            return PrefabContainer.Instance.enemys[waves[1].enemyID].GetComponent<BaseEnemy>();
        }
        else
            return PrefabContainer.Instance.enemys[0].GetComponent<BaseEnemy>();
    }
    #endregion
}
