using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class SpawnPoints
{
    public Transform self;
    public Transform destination;
}

public class SpawnManager : MonoSingleton<SpawnManager>
{

    public List<SpawnPoint> spawnPoint = new List<SpawnPoint>();
    public List<GameObject> spawnPrefabs = new List<GameObject>();

    private List<GameObject> activeEnemys = new List<GameObject>();

    public void Spawn(int spawnPrefabIndex)
    {
        Spawn(spawnPrefabIndex, 0);
    }

    public void Spawn(int spawnPrefabIndex,int spawnPointIndex)
    {
        GameObject go = Instantiate(spawnPrefabs[spawnPrefabIndex]
            , spawnPoint[spawnPointIndex].self.position
            , spawnPoint[spawnPointIndex].self.rotation) as GameObject;
        go.SendMessage("SetDestination", spawnPoint[spawnPointIndex].destination);
        activeEnemys.Add(go);
    }

    public void Despawn(GameObject go)
    {
        activeEnemys.Remove(go);
        Destroy(go);
    }


    public void ClearEnemys()
    {
        foreach(GameObject go in activeEnemys)
        {
            Destroy(go);
        }
        activeEnemys.Clear();
    }


    public int getEnemysLeft()
    {
        return activeEnemys.Count;
    }

    // Temporary

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Spawn(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Spawn(1);
    }
}
