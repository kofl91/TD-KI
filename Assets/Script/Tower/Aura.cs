using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour {

    public float speedFactor;
    public float rangeFactor;
    public float dmgFactor;


    void OnTriggerEnter(Collider col)
    {

        if (col.GetComponent<BaseTower>())
        {
            col.GetComponent<BaseTower>().cooldown *= speedFactor;
            col.GetComponent<BaseTower>().range *= rangeFactor;
            col.GetComponent<BaseTower>().turretDmg.Multiply(dmgFactor);
        }
    }

    void OnDestroy()
    {
        Collider[] allCollider = Physics.OverlapSphere(transform.position, 10.0f, LayerMask.GetMask("Tower"));
        foreach(Collider col in allCollider)
        {
            if (col.GetComponent<BaseTower>())
            {
                col.GetComponent<BaseTower>().cooldown /= 0.8f;
            }
        }

    }
}
