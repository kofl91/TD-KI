using UnityEngine;
using System.Collections;

public class PflanzEnemy : BaseEnemy
{

    PflanzEnemy()
    {
        life = 6f;
        bounty = 2;
        resistance.nature = 0.5f;
        resistance.fire = 1.5f;
    } 
}


