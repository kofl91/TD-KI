using UnityEngine;
using System.Collections;


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
    void updateWaveComponent(WaveComponent waveComponent)
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
                else
                {
                    waiting = true;
                    waveComponentIndex++;
                }
            }
        }
    }
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



