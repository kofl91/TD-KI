using UnityEngine;
using System.Collections;

public class FireEnemy : BaseEnemy {


    FireEnemy()
    {
        dmgfactor.fire = 0.5f;
        dmgfactor.water = 1.5f;
        life = 6f;
        bounty = 2;
    }
}

