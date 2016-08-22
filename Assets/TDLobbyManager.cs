using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class TDLobbyManager : LobbyManager
{

    public override void OnMatchCreate(CreateMatchResponse matchInfo)
    {
        Debug.Log("Match started with :" + numPlayers + " player.");
    }
}
