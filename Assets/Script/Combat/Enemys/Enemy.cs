using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    protected GameObject pathstart;
    protected Transform targetPathNote;
    protected int pathNodeIndex = 0;
    public float speed = 5f;
    public float life = 1f;
    public int Score = 1;

    //protected DamageResistance resistance;

    protected DamageInfo dmgfactor = new DamageInfo();

    public float debuffDuration = 0.0f;

    public float speedmodifier = 1.0f;


    public Enemy()
    {
        dmgfactor.setNeutralResistance();
    }

    // Use this for initialization
    void Start()
    {
        pathstart = GameObject.Find("Waypoints");
    }

    protected void GetNextPath()
    {       
            targetPathNote = pathstart.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
           if (pathNodeIndex == 10)
        {
            Destroy(gameObject);
            GameManager.Instance.firstPlayer.EnemyCrossed();

        }
    }

    // Update is called once per frame
    void Update()
    {

        debuffDuration -= Time.deltaTime;
        if (debuffDuration < 0.0f)
        {
            speedmodifier = 1.0f;
        }

        if (targetPathNote == null)
        {
            GetNextPath();
            if (targetPathNote == null)
            {
                Destroy(gameObject);
            }
        }
        Vector3 dir = targetPathNote.position - this.transform.localPosition;
        float distThisFrame = speed * speedmodifier * 10 * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            targetPathNote = null;

        }
        else
            transform.Translate(dir.normalized * distThisFrame, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime);
    }

    protected void ReachedGoal()
    {
      
        Destroy(gameObject);
    }

    public void OnDamage(DamageInfo damage)
    {
        life -= damage.calcAbsoluteDmg(dmgfactor);
        if (life <= 0)
        {
            GameManager.Instance.firstPlayer.enemyKilled++;
            Destroy(this.gameObject);
        }
    }
}
