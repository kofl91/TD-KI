using UnityEngine;
using System.Collections;

public class WaterMinion : BaseEnemy {

    WaterMinion()
    {
        resistance.fire = 0.5f;
        resistance.nature = 1.5f;
        life = 6f;
        bounty = 2;
    }
}