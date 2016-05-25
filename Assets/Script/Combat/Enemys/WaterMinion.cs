using UnityEngine;
using System.Collections;

public class WaterMinion : Enemy {

    WaterMinion()
    {
        dmgfactor.fire = 0.5f;
        dmgfactor.nature = 1.5f;
        speed = 8f;
        life = 6f;
        Score = 2;
    }
}