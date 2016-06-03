using UnityEngine;
using System.Collections;

public class TargetForAgent : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform target;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
	}
}
