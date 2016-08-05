using UnityEngine;
using System.Collections;

public class GameOverSpwan : MonoBehaviour {

    public GameObject Minion;


    int nextWave  = 0;
    int count;
    float rate;

    float cD = 5f;
    float waveCountdown;

    void Start()
    {
        StartCoroutine(Wave());
    }
    IEnumerator Wave()
    {
        while(true)
        { 
        yield return new WaitForSeconds(1);
        Instantiate(Minion, this.transform.position, this.transform.rotation);
        }

    }
   
   
}