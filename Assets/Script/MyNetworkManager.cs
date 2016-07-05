using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

public class MyNetworkManager : NetworkManager {

    int i = 0;
    public List<Transform> startPoints = new List<Transform>();
    public GameObject spawnManager;

    public List<PlayerController> players = new List<PlayerController>();

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (i < 2)
        {
            GameObject player = (GameObject)Instantiate(playerPrefab, startPoints[i].position, Quaternion.identity);
            players.Add(player.GetComponent<PlayerController>());
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            player.GetComponent<PlayerController>().RpcSetID (i + 1);
        }

        i++;
        if (i == 2)
        {
            GameObject spawni = (GameObject)Instantiate(spawnManager, Vector3.zero, Quaternion.identity);
            Spawner mySpawner = spawni.GetComponent<Spawner>();
            mySpawner.player1 = players[0];
            mySpawner.player2 = players[1];
            mySpawner.neutralSpawnPoint = GameObject.FindGameObjectWithTag("NeutralSpawnPoint"); 
            mySpawner.hiredSpawnPoint = GameObject.FindGameObjectWithTag("HiredSpawnPoint");
            foreach (PlayerController player in players)
            {
                player.RpcInitPlayerController();
            }
        }
    }
}
