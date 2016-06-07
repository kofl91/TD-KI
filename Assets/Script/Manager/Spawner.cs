using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public List<Wave> waves;
    public Wave currentWave;
    // Use this for initialization
    void Start()
    {
        waves = new List<Wave>();
        Wave[] buffWaves = GetComponents<Wave>();
        foreach (Wave w in buffWaves)
        {
            waves.Add(w);
        }
        if (waves.Count > 0)
        {
            currentWave = waves[0];
            waves.RemoveAt(0);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (currentWave.isOver())
        {
            if (waves.Count > 0)
            {
                currentWave = waves[0];
                waves.RemoveAt(0);
            }
            else
            {
                Debug.Log("Level Over!");
            }
        }
        if (shouldSendWave())
        {
            currentWave.send();
        }
	}

    private bool shouldSendWave()
    {
        return false;
    }
}
