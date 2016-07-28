using UnityEngine;
using System.Collections;
using System;

public class EndzoneDespawn : MonoBehaviour {

    private PlayerController owner;

    private void OnTriggerEnter(Collider col)
    {
        if (!owner)
        {
            owner = GetComponentInParent<PlayerController>();
        }
        if (col.tag == "Enemy")
        {
            owner.EnemyCrossed();
            Destroy(col.gameObject);
        }
    }
}
