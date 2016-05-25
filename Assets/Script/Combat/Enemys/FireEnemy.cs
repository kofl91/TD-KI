using UnityEngine;
using System.Collections;

public class FireEnemy : MonoBehaviour {


    GameObject pathstart;
    Transform targetPathNote;
    int pathNodeIndex = 0;
    float speed = 8f;
    float life = 6f;
    int Score = 2;
    int Plfanzenrez = 3;
    int WaterDmg = -2;

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
            GameManager.Instance.GetPlayerLife();
            GameManager.Instance.firstPlayer.EnemyCrossed();

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
        float distThisFrame = speed * 10 * Time.deltaTime;

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

    public void OnDamage(DamageInfo damage)
    {
        life -= damage.amount;
        if (life <= 0)
        {
            GameManager.Instance.firstPlayer.enemyKilled++;
            Destroy(this.gameObject);
        }
    }
}

