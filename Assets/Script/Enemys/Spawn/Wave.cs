using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int enemyHP;

    public int enemyBounty;

    public int enemyID;

    private int count;

    public int maxCount;

    public float interval;

    internal void Reset()
    {
        count = maxCount;
    }

    internal void Decr()
    {
        count--;
    }

    internal bool hasEnded()
    {
        return count <= 0;
    }
}