using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class EndzoneDespawn : MonoBehaviour, IBelongsToPlayer {

    private PlayerController owner;
    public int player = 0;

    public void SetPlayer(int id)
    {
        player = id;

    }

    public PlayerController GetPlayer()
    {
        return GameObject.FindObjectsOfType<PlayerController>()[player - 1];
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Destroy");
        if (col.tag == "Enemy")
        {
            //GameManager.Instance.currentMap.Despawn(col.gameObject);
            GetPlayer().EnemyCrossed();
            if(NetworkServer.active)
                Destroy(col.gameObject);
        }
    }
}
