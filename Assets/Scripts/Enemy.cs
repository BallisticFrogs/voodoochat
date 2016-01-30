using UnityEngine;
using System.Collections;

public class Enemy : Slowable
{

    private Transform target;

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

        // find player
        GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
        target = player.transform;
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void Die()
    {
        // TODO play animation + vfx
        gameObject.DestroyRecursive();
    }

    private void UpdateAgentSpeed(float newSpeed)
    {
        agent.speed = speed * newSpeed;
    }

}
