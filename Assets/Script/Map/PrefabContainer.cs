using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabContainer : MonoSingleton<PrefabContainer> {

    public GameObject endZonePrefab;
    public List<GameObject> enemys;
    public List<GameObject> turrets;
}
