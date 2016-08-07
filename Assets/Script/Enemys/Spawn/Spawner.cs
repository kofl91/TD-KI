using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


// Singleton welcher das Spawnen der Minions übernimmt
public class Spawner : NetworkBehaviour {

    #region Attribute
    // Referenzen zu den beiden Spielerns
    public PlayerController[] player;
    public PlayerController player1;
    public PlayerController player2;

    // Standorte der  Spawn/Despawn Punkte
    public GameObject neutralSpawnPoint;
    public GameObject hiredSpawnPoint;
    public GameObject basePlayer1;
    public GameObject basePlayer2;

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

    #endregion

    #region SpawnFunktionen
    // Heuert einen Minion für Spieler 1 an.
    public void HireMinionForPlayer1(int enemyID)
    {
        HireMinion(player1, enemyID);
    }

    // Heuert einen Minion für Spieler 2 an.
    public void HireMinionForPlayer2(int enemyID)
    {
        HireMinion(player2, enemyID);
    }

    // Heuert einen Minion an
    public void HireMinion(PlayerController sendingPlayer,int enemyID)
    {
        int cost = PrefabContainer.Instance.enemys[enemyID].GetComponent<BaseEnemy>().bounty;
        // Kann sich der Spieler die Einheit leisten?
        if (sendingPlayer.Gold > cost) {
            sendingPlayer.Gold -= cost;
            GameObject towards;
            PlayerController playerToSendTowards;
            // Entscheide wer der Gegner ist.
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


    bool init = false;
    // Eine Spawn Iteration
    public void spawnRegular()
    {
        if (!init)
        {
            player = FindObjectsOfType<PlayerController>();
            player1 = player[0];
            player2 = player[1];
            init = true;
            basePlayer1 = player1.GetComponentInChildren<EndzoneDespawn>().gameObject;
            basePlayer2 = player2.GetComponentInChildren<EndzoneDespawn>().gameObject;
        }

        int enemyID = waves[0].enemyID;
        waves[0].Decr();
        if (GetComponent<NetworkIdentity>())
        {
            CmdspawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform.position, basePlayer1, player1.gameObject);
            CmdspawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform.position, basePlayer2, player2.gameObject);
        }
        else
        {
            spawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform, basePlayer1, player1);
            spawnMinionAtTowardsVersus(enemyID, neutralSpawnPoint.transform, basePlayer2, player2);
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
