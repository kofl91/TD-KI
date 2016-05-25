using UnityEngine;
using System.Collections;

public class RedTower : BaseTurret
{

	// Use this for initialization
	void Start () {
        goldCost = 40;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Use this for initialization
    RedTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.fire = 3;
    }

}
