using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainingStartPoint : MonoBehaviour
{

    float spawnCD = 1f;
    float spwanCDremaining = 0;
    List<GameObject> minions = new List<GameObject>();

    public WaveComponent[] waveComps;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        bool didSpawn = false;
        spwanCDremaining -= Time.deltaTime;
        if (spwanCDremaining < 0)
        {
            spwanCDremaining = spawnCD;

            foreach (WaveComponent wc in waveComps)
            {
                if (wc.spwaned < wc.num)
                {
                    //wc.spwaned++;
                    minions.Add(Instantiate(wc.enemyPref, this.transform.position, this.transform.rotation) as GameObject);
                    didSpawn = true;
                    break;
                }
            }
            if (didSpawn = false)
            {
                Destroy(gameObject);
            }
        }

    }

    public void DestroyAllMinions()
    {
        foreach( GameObject go in minions)
        {
            Destroy(go);
        }
        minions.Clear();
    }

    public void OnMinionDeath(GameObject go)
    {
        if(minions.Contains(go))
        {
            minions.Remove(go);
            Destroy(go);
        }
    }
}
