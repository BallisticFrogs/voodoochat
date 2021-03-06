﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private readonly int hashRunning = Animator.StringToHash("running");

    private int attackRaycastMask = 1 << Layers.default_layer | 1 << Layers.obstacles | 1 << Layers.obstacles_noghost;

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

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Material matNormal;
    public Material matTrance;
    public Material matGhost;
    public Material matExorcism;

    private Rigidbody rb;
    private PlayerState playerState;
    private GhostLife ghostLife;
    private Text playerStatus;
    private Text instructionsLabel;
    private FadeAway fadeAwayComponent;
    private GameController gameController;
    private float lastAttackTime = -1000;

    private AudioSource audioSource;

    private string tranceInstructions = "Bullet time!!! \nControls are reversed!";
    private string exorcismInstructions = "Press SPACE to attack!";
    private string ghostInstructions = "Lights are the only danger! \nWatch your grey health bar! \n(top-left of your screen)";

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
                    // raycast to prevent killing enemies through doors/walls/trees
                    if (!Physics.Linecast(transform.position + Vector3.up * 0.5f, enemy.transform.position + Vector3.up * 0.5f, attackRaycastMask))
                    {
                        enemy.Die();
                    }
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
        instructionsLabel = GameObject.FindGameObjectWithTag(Tags.InstructionsLabel).GetComponent<Text>();
        fadeAwayComponent = instructionsLabel.GetComponent<FadeAway>();
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
            skinnedMeshRenderer.material = matNormal;
        }
        if (newState == PlayerState.TRANCE)
        {
            vfxToIce.PlayVfx();
            gameController.SetWorldSpeed(0.5f);
            playerStatus.text = "TRANCE";
            skinnedMeshRenderer.material = matTrance;
            displaySpecialInstructions(tranceInstructions);
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
            skinnedMeshRenderer.material = matGhost;

            displaySpecialInstructions(ghostInstructions);
            audioSource.clip = audioGhost;
            audioSource.Play();
        }
        if (newState == PlayerState.EXORCISM)
        {
            vfxToMetal.PlayVfx();
            gameObject.layer = Layers.player_exorcism;
            playerStatus.text = "EXORCISM";
            skinnedMeshRenderer.material = matExorcism;
            displaySpecialInstructions(exorcismInstructions);
            audioSource.clip = audioExorcism;
            audioSource.Play();
        }
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    private void displaySpecialInstructions(string instructions)
    {
        fadeAwayComponent.fadeStarted = false;
        //reset timer before fade out
        fadeAwayComponent.timerUntilfade = 0;
        Color color = instructionsLabel.color;
        color.a = 1;
        instructionsLabel.color = color;
        instructionsLabel.text = instructions;
    }

}