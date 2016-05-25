using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StartPoint : MonoBehaviour {

    float spawnCD = 1f;
    float spwanCDremaining = 0;

    public WaveComponent[] waveComps;
    int waveComponentIndex = 0;
    float timePassed = 0f;
    bool waiting = true;

    // Use this for initialization
    void Start()
    {


    }
    WaveComponent waveComponent;

    // Update is called once per frame
    void Update()
    {
        foreach (WaveComponent w in waveComps)
        {
            if (w.Update(this))
            {
                Instantiate(w.enemyPref, transform.position, transform.rotation);
            }

        }
    }
}



