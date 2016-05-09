using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BasePlayer : MonoBehaviour
{
    public int Gold { set; get; }
    public int LivesLeft { set; get; }

    internal void EarnBounty(int bounty)
    {
        throw new NotImplementedException();
    }

    // List of Turrets placed or Map of player needed.
}
