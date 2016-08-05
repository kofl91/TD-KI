using UnityEngine;
using System.Collections;

public class FireEnemy : BaseEnemy {


    FireEnemy()
    {
        resistance.fire = 0.5f;
        resistance.water = 1.5f;
        life = 6f;
        bounty = 2;
    }
}

