using UnityEngine;
using System.Collections;

public class SampleAgentScript : Slowable
{

    public Transform target;

    private NavMeshAgent agent;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        if (speed == 0)
        {
            speed = agent.speed;
        }

        OnWorldSpeedChanged += UpdateAgentSpeed;
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
        agent.SetDestination(target.position);
    }

    private void UpdateAgentSpeed(float newSpeed)
    {
        agent.speed = speed * newSpeed;
    }

}
