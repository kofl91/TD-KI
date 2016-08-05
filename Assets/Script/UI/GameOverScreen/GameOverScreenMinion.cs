using UnityEngine;
using System.Collections;

public class GameOverScreenMinion : MonoBehaviour {

    public Transform endPoint;
    NavMeshAgent agent;
    void Start() {

        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.SetDestination(endPoint.position);
        if(agent.remainingDistance <= 3 && agent.remainingDistance != 0)
        {
            Destroy(gameObject);
        }
    }

}
