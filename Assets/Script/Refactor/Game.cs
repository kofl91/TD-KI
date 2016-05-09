using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoSingleton<Game> {

    public List<Level> levels;
    Level chosenLevel;
    public List<BasePlayer> players;
    public List<SendableEnemy> sendableEnemys;

    public void Start()
    {
        chosenLevel = new TutorialLevel();
        chosenLevel.StartLevel();
    }


    public GameObject GetEnemyByID(int spawnPrefabIndex)
    {
        return PrefabContainer.Instance.enemys[spawnPrefabIndex];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            chosenLevel.map.Spawn(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            chosenLevel.map.Spawn(1);
    }
}
