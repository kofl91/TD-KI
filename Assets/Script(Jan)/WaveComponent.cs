using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveComponent
{
    public GameObject enemyPref;
    public int num;
    [System.NonSerialized]
    public int spwaned = 0;
    public float startTime = 0;

    float spawnCD = 1f;
    float spwanCDremaining = 0;
    float timePassed = 0f;


    
    public bool Update(MonoBehaviour m){
        timePassed += Time.deltaTime;
        if (timePassed > startTime) { 
            spwanCDremaining -= Time.deltaTime;
            if (spwanCDremaining < 0)
            {
                spwanCDremaining = spawnCD;

                if (spwaned < num)
                {
                    spwaned++;
                    return true;
                }
            }
        }
        return false;
    }
}