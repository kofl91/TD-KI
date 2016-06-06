using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSwitch : MonoBehaviour {

    public List<Camera> cameras;

	// Use this for initialization
	void Start () {
        Camera[] cams = GetComponentsInChildren<Camera>();
        foreach (Camera c in cams)
        {
            cameras.Add(c);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            setAllCamerasOff();
            cameras[0].enabled =true ;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            setAllCamerasOff();
            cameras[1].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            setAllCamerasOff();
            cameras[2].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            setAllCamerasOff();
            cameras[3].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            setAllCamerasOff();
            cameras[4].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            setAllCamerasOff();
            cameras[5].enabled = true;
        }
    }

    void setAllCamerasOff ()
    {
        foreach (Camera c in cameras)
        {
            c.enabled = false;
        }
    }
}
