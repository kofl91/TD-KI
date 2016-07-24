using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AIPlayer : MonoBehaviour
{

    public bool isPlaying = false;

    protected bool isInitialized = false;

    public abstract void MakeMove();

    void Update()
    {
        if (isPlaying)
        {
            MakeMove();
        }
    }

    public abstract void Init();
}