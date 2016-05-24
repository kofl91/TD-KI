using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveComponent
{
    public GameObject enemyPref;
    public int num;
    [System.NonSerialized]
    public int spwaned = 0;

}