using UnityEngine;
using System.Collections;

public class RotatesTowardsTarget : MonoBehaviour {

    public Transform target;
	
	// Update is called once per frame
	void Update () {
        
	    if (target)
        {
            transform.LookAt(target);
        }
	}
}
