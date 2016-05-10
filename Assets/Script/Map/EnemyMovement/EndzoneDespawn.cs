using UnityEngine;
using System.Collections;

public class EndzoneDespawn : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Destroy");
        if (col.tag == "Enemy")
        {
            GameManager.Instance.currentMap.Despawn(col.gameObject);
            GameManager.Instance.firstPlayer.EnemyCrossed();
        }
    }
}
