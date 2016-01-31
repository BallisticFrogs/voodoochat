using UnityEngine;
using System.Collections;

public class Enemy : Slowable
{

    private readonly int hashDie = Animator.StringToHash("die");
    private readonly int hashWalking = Animator.StringToHash("walking");

    private readonly WaitForSeconds repathWait = new WaitForSeconds(0.2f);

    public float timeToAwake = 1f;
    public float maxPursuitDistance = 15;

    public Animator animator;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private Vector3 spawnPoint;

    private PlayerController player;
    private Vector3 currentTarget;
    private float timeToNextPatrolPoint;

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

        // memorize spawn point
        spawnPoint = transform.position;

        // find player
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();

        // start
        StartCoroutine(Repath());
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
        if (animator != null) animator.SetBool(hashWalking, agent.hasPath);
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
    }

    private IEnumerator Repath()
    {
        // initial wait
        yield return new WaitForSeconds(timeToAwake);

        // repath 5 times per sec
        while (true)
        {
            bool playerIsGhost = player.GetPlayerState() == PlayerState.GHOST;
            Vector3 vectorToSpawnPoint = spawnPoint - transform.position;
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            bool chasePlayer = !playerIsGhost && vectorToPlayer.magnitude < maxPursuitDistance;

            Vector3 target = currentTarget;
            if (!chasePlayer)
            {
                timeToNextPatrolPoint -= Time.deltaTime;

                Vector3 vectorToCurrentTarget = currentTarget - transform.position;
                Vector3 vectorCurrentTargetToSpawn = currentTarget - transform.position;
                if (timeToNextPatrolPoint <= 0f || vectorCurrentTargetToSpawn.magnitude > 5)
                {
                    timeToNextPatrolPoint = 1f;

                    // change dest only if necessary
                    // ie not inside patrol radius or arrived to target
                    target = spawnPoint + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
                }
            }
            else
            {
                // follow player
                target = player.transform.position;
            }

            if (target != currentTarget)
            {
                currentTarget = target;
                if (agent.isOnNavMesh) agent.SetDestination(target);
            }

            // wait
            yield return repathWait;
        }
    }

    public void Die()
    {
        // disable capacity to attack
        GetComponent<DamageDealer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // play animation
        if (animator != null) animator.SetTrigger(hashDie);

        // remove after a delay
        gameObject.DestroyRecursive(1.2f);
    }

    private void UpdateAgentSpeed(float newSpeed)
    {
        agent.speed = speed * newSpeed;
    }

}
