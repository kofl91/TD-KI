using UnityEngine;
using System.Collections;

public class ControlCamera : MonoBehaviour
{

   
    void Update()
    {
       
        if (Input.GetKey("w")) { 
        transform.Translate(0, 2, 0);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(0, -2, 0);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(2, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(-2, 0, 0);
        }
    }
}
