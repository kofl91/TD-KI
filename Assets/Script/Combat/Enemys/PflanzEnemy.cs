using UnityEngine;
using System.Collections;

public class PflanzEnemy : Enemy
{

    PflanzEnemy()
    {
        speed = 8f;
        life = 6f;
        Score = 2;
        dmgfactor.nature = 0.5f;
        dmgfactor.fire = 1.5f;
    } 
}


