using UnityEngine;
using System.Collections;

public class SampleAgentScript : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    private Rigidbody rb;

	// Use this for initialization
	void Awake () {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector3.zero;
        agent.SetDestination(target.position);
	}
}
