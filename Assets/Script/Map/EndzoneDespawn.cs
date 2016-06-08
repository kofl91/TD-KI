using UnityEngine;
using System.Collections;
using System;

public class EndzoneDespawn : MonoBehaviour, IBelongsToPlayer {

    private PlayerController owner;

    public PlayerController GetPlayer()
    {
        return owner;
    }

    public void SetPlayer(PlayerController player)
    {
        owner = player;
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Destroy");
        if (col.tag == "Enemy")
        {
            //GameManager.Instance.currentMap.Despawn(col.gameObject);
            owner.EnemyCrossed();
            Destroy(col.gameObject);
        }
    }
}
