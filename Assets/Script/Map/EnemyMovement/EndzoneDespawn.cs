using UnityEngine;
using System.Collections;

public class EndzoneDespawn : MonoBehaviour {

    public PlayerController owner;

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
