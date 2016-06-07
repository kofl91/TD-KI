using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int count;

    private List<GameObject> enemys = new List<GameObject>();

    private bool isSending = false;

    internal bool isOver()
    {
        return enemys.Count == 0;
    }

    internal void send()
    {
        isSending = true;
    }
    void Update()
    {
        if (count > 0)
        {
            enemys.Add(Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject);
            count--;
        }
    }
}