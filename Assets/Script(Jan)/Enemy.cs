using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    GameObject pathstart;
    Transform targetPathNote;
    int pathNodeIndex = 0;
    public float speed = 5f;
    public float life = 1f;
    public int Score = 1;
    // Use this for initialization
    void Start()
    {
        pathstart = GameObject.Find("Waypoints");

    }

    void GetNextPath()
    {       
            targetPathNote = pathstart.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
           if (pathNodeIndex == 10)
        {
            Destroy(gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (targetPathNote == null)
        {
            GetNextPath();
            if (targetPathNote == null)
            {
                Destroy(gameObject);
            }
        }
        Vector3 dir = targetPathNote.position - this.transform.localPosition;
        float distThisFrame = speed* 10 * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            targetPathNote = null;

        }
        else
            transform.Translate(dir.normalized * distThisFrame, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime);
    }

    void ReachedGoal()
    {
      
        Destroy(gameObject);
    }

    public void TakeDmg(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
           
            Destroy(gameObject);
        }
    }
}
