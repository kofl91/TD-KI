using UnityEngine;
using System.Collections;

public class FireEnemy : Enemy {


    FireEnemy()
    {
        dmgfactor.fire = 0.5f;
        dmgfactor.water = 1.5f;
        speed = 8f;
        life = 6f;
        Score = 2;
    }
}

