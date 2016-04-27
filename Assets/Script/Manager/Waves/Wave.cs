using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {
    public bool isPlaying = false;

    public List<WaveEvent> waveEvents = new List<WaveEvent>();

    public void StartWave()
    {
        isPlaying = true;
        if (waveEvents.Count != 0)
        {
            waveEvents[0].StartEvent();
        }
        else
        {
            LevelManger.Instance.EndWave();
        }
    }
    private void Update()
    {
        if (!isPlaying)
            return;
        if (!waveEvents[0].RunEvent())
        {
            //Debug.Log("End Event");
            waveEvents.RemoveAt(0);
            if(waveEvents.Count == 0)
            {
                LevelManger.Instance.EndWave();
            }
            else
            {
                waveEvents[0].StartEvent();
            }
        }
    }
    [System.Serializable]
    public class WaveEvent
    {
        public float duration = 15.0f;
        public List<SpawnInfo> spawnInfos = new List<SpawnInfo>();
        private float startTime;

        public void StartEvent()
        {
            startTime = Time.time;
        }

        public bool RunEvent()
        {
            if ((duration == 0.0f) && (spawnInfos.Count == 0))
                return false;
            else if( Time.time - startTime > duration && duration != 0.0f)
                return false;

            for (int i = 0; i< spawnInfos.Count; i++)
            {
                spawnInfos[i].ReadyToSpawn();
                if (spawnInfos[i].ammount == 0)
                {
                    spawnInfos.RemoveAt(i);
                }
            }

            return true;
        }

        [System.Serializable]
        public class SpawnInfo
        {
            public int spawnPointIndex = 0;
            public int spawnPrefabIndex = 0;
            public int ammount = 10;
            public float interval = 1.0f;

            private float lastTime;

            public void ReadyToSpawn()
            {
                if((Time.time - lastTime) >= interval)
                {
                    SpawnManager.Instance.Spawn(spawnPrefabIndex, spawnPointIndex);
                    lastTime = Time.time;
                }
            }

        }
    }

    

}


