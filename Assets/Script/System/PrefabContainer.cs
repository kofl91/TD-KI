using UnityEngine;
using System.Collections.Generic;


// Eine Singleton Klasse die alle Prefabs zur verfügung stellt.
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
            creep.id = i;
            creep.enemy = enemys[i].GetComponent<BaseEnemy>();
            retList.Add(creep);
        }
        return retList;
    }
}
