using UnityEngine;
using System.Collections;

public class MoveLegs : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    public Quaternion start;
    public Quaternion end;

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = Quaternion.Slerp(start, end, Time.time * 0.3f);
    }
}
