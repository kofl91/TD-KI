using UnityEngine;
using System.Collections;

public class EndzoneDespawn : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Destroy");
        SpawnManager.Instance.Despawn(col.gameObject);
        if (col.tag == "Enemy")
        {
            LevelManger.Instance.EnemyCrossed();
        }
    }
}
