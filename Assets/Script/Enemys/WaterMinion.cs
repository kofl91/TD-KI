using UnityEngine;
using System.Collections;

public class WaterMinion : BaseEnemy {

    WaterMinion()
    {
        dmgfactor.fire = 0.5f;
        dmgfactor.nature = 1.5f;
        life = 6f;
        bounty = 2;
    }
}