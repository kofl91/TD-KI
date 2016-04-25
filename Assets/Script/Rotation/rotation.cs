using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(Time.deltaTime * 50, 0, 0));
	}
}
