using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public int stepforward = 1;
    public int stepback = -1;
    public int borderXHigh = 200;
    public int borderXLow = -200;
    public int borderYHigh = 200;
    public int borderYLow = -200;
    public int borderZHigh = 200;
    public int borderZLow = -200;
    public int ScrollUp = 200;
    public int ScrollDwon = 1;

	// Update is called once per frame
	void Update () {

        if (Input.GetKey("s") || Input.GetKey("down"))
            {
            if (transform.position.z < borderZLow)
                transform.position += new Vector3(0, 0, stepforward);
            }      
        if (Input.GetKey("w") || Input.GetKey("up"))
            {
                if (transform.position.z > borderZHigh)
                    transform.position += new Vector3(0, 0, stepback);
            }
       
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
            if (transform.position.x > borderXHigh)
                transform.position += new Vector3(stepback, 0, 0);
            }
      
            if (Input.GetKey("a") || Input.GetKey("left"))
            {
            if (transform.position.x < borderXLow)
                transform.position += new Vector3(stepforward, 0, 0);
            }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
            if (transform.position.y > ScrollDwon)
                transform.position += new Vector3(0, stepback, 0);         
            }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
            if (transform.position.y < ScrollUp)
                transform.position += new Vector3(0, stepforward, 0);
            }
    }
}
