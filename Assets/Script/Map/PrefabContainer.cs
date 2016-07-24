using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabContainer : MonoSingleton<PrefabContainer> {

    public GameObject wayPointPrefab;
    public GameObject spawnPointPrefab;
    public GameObject endZonePrefab;
    public List<GameObject> enemys;
    public List<GameObject> turrets;
    public GameObject tile;
}
