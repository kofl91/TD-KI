using UnityEngine;
using System.Collections;
using System;

public class EndzoneDespawn : MonoBehaviour {

    private PlayerController owner;

    public GameObject particleEffect;

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
            GameObject pE = Instantiate(particleEffect, transform.position, Quaternion.identity) as GameObject;
            pE.transform.SetParent(transform);
            Destroy(pE, 1f);
        }   
    }
}
