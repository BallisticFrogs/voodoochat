using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private readonly int hashRunning = Animator.StringToHash("running");

    public float speed;
    public float attackCooldown = 0.5f;

    public Animator puppetAnimator;

    public CollisionTracker attackCollider;

    public PlayerTransitionVfx vfxToNormal;
    public PlayerTransitionVfx vfxToIce;
    public PlayerTransitionVfx vfxToGhost;
    public PlayerTransitionVfx vfxToMetal;
    public ParticleSystem attackParticleSystem;

    public AudioClip audioGhost;
    public AudioClip audioTrance;
    public AudioClip audioExorcism;

    private Rigidbody rb;
    private PlayerState playerState;
    private GhostLife ghostLife;
    private Text playerStatus;
    private GameController gameController;
    private float lastAttackTime = -1000;

    private AudioSource audioSource;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
        ghostLife = GetComponent<GhostLife>();
        gameController.OnUiLoaded += OnUiLoaded;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //bool attack = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftControl);
        bool attack = Input.GetButtonDown("Fire1");
        if (attack && Time.time > lastAttackTime + attackCooldown && playerState == PlayerState.EXORCISM)
        {
            lastAttackTime = Time.time;

            // vfx
            attackParticleSystem.Play();

            // effect
            foreach (GameObject obj in attackCollider.colliders)
            {
                Enemy enemy = obj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Die();
                }
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (playerState == PlayerState.TRANCE)
        {
            moveHorizontal *= -1;
            moveVertical *= -1;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 velocity = rb.velocity;
        rb.velocity = movement.normalized * speed * Time.deltaTime;

        // Check if fly
        if (!Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.1f))
        {
            if (velocity.y < 0f)
                rb.velocity += new Vector3(0f, velocity.y, 0f);
        }


        bool running = false;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            running = true;
        }

        puppetAnimator.SetBool(hashRunning, running);
    }

    private void OnUiLoaded()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.PlayerStatus).GetComponent<Text>();
    }

    public void SetPlayerState(PlayerState newState)
    {
        if (newState == playerState) return;

        PlayerState oldState = playerState;
        playerState = newState;

        // remove effects of previous state
        if (oldState == PlayerState.GHOST)
        {
            gameObject.layer = Layers.player_normal;
            gameController.ShowAllTraversableObjects(false);
            ghostLife.enabled = false;
        }
        if (oldState == PlayerState.TRANCE)
        {
            gameController.SetWorldSpeed(1);
        }
        if (oldState == PlayerState.EXORCISM)
        {
            gameObject.layer = Layers.player_normal;
        }

        // add effects of new state
        if (newState == PlayerState.NORMAL)
        {
            vfxToNormal.PlayVfx();
            playerStatus.text = "-----";
        }
        if (newState == PlayerState.TRANCE)
        {
            vfxToIce.PlayVfx();
            gameController.SetWorldSpeed(0.5f);
            playerStatus.text = "TRANCE";
            audioSource.clip = audioTrance;
            audioSource.Play();
        }
        if (newState == PlayerState.GHOST)
        {
            gameObject.layer = Layers.player_ghost;
            gameController.ShowAllTraversableObjects(true);
            ghostLife.enabled = true;
            vfxToGhost.PlayVfx();
            playerStatus.text = "GHOST";
            audioSource.clip = audioGhost;
            audioSource.Play();
        }
        if (newState == PlayerState.EXORCISM)
        {
            vfxToMetal.PlayVfx();
            gameObject.layer = Layers.player_exorcism;
            playerStatus.text = "EXORCISM";
            audioSource.clip = audioExorcism;
            audioSource.Play();
        }
    }

}