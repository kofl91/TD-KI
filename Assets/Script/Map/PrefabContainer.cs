using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PrefabContainer : MonoSingleton<PrefabContainer> {

    public GameObject endZonePrefab;
    public List<GameObject> enemys;
    public List<GameObject> turrets;

    public List<EnemyStructure> GetAllEnemys()
    {
        List<EnemyStructure> retList = new List<EnemyStructure>();
        for (int i=0;i<enemys.Count;i++)
        {
            EnemyStructure creep = new EnemyStructure();
            creep.Id = i;
            creep.enemy = enemys[i].GetComponent<BaseEnemy>();
            retList.Add(creep);
        }
        return retList;
    }
}
