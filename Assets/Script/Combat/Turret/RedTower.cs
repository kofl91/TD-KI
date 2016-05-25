using UnityEngine;
using System.Collections;

public class RedTower : BaseTurret
{
<<<<<<< HEAD

	// Use this for initialization
	void Start () {
        goldCost = 40;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
=======
    // Use this for initialization
    RedTower()
    {
        turretDmg = new DamageInfo();
        turretDmg.fire = 3;
    }
>>>>>>> 0c2aeff776256dbd722cafe29698893ae8c50e67
}
