using UnityEngine;
using System.Collections;

public class PflanzEnemy : BaseEnemy
{

    PflanzEnemy()
    {
        life = 6f;
        bounty = 2;
        dmgfactor.nature = 0.5f;
        dmgfactor.fire = 1.5f;
    } 
}


