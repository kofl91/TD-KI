using UnityEngine;
using System.Collections;

public class EndzoneDespawn : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Destroy");
        Destroy(col.gameObject);
    }
}
