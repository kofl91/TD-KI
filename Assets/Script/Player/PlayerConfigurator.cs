﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerConfigurator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        NetworkStartPosition[] allPos = FindObjectsOfType<NetworkStartPosition>();
        NetworkStartPosition myPos = allPos[0];
        foreach( NetworkStartPosition p in allPos)
        {
            if(p.gameObject.transform.position == transform.position)
            {
                myPos = p;
            }
        }
        // Set Despawnpoint
        myPos.GetComponentInChildren<EndzoneDespawn>().gameObject.transform.SetParent(this.transform);

        // Set Grid
        GridMaker gm = myPos.GetComponentInChildren<GridMaker>();
        gm.gameObject.transform.SetParent(this.transform);
        gm.transform.position.Set(transform.position.x, 0.0f, transform.position.z);
        GetComponent<PlayerController>().Init();
        GetComponentInChildren<AlgorithmBot>(true).gridMaker = gm.gameObject;
    }
}
