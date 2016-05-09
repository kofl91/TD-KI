using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Level : MonoBehaviour {

    enum eDifficulty
    {
        Easy,
        Medium,
        Hard,
        VeryHard,
        Insane
    };

    public Map map;

    protected List<Wave> waves;

    private eDifficulty Difficulty { set; get; }

    internal static int GetCurrentWave()
    {
        throw new NotImplementedException();
    }

    public abstract void StartLevel();
}
