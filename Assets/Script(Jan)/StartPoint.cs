﻿using UnityEngine;
using System.Collections;


public class StartPoint : MonoBehaviour {

    float spawnCD = 1f;
    float spwanCDremaining = 0;

    public WaveComponent[] waveComps;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
       
        spwanCDremaining -= Time.deltaTime;
        if (spwanCDremaining < 0)
        {
            spwanCDremaining = spawnCD;

            foreach (WaveComponent wc in waveComps)
            {
                if (wc.spwaned < wc.num)
                {
                    wc.spwaned++;
                    Instantiate(wc.enemyPref, this.transform.position, this.transform.rotation);                 
                    break;
                }
            }
          
        }

    }
}


