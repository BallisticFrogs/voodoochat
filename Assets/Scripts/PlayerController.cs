using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private readonly int hashRunning = Animator.StringToHash("running");

    public float speed;

    public Animator puppetAnimator;

    public PlayerTransitionVfx vfxToNormal;
    public PlayerTransitionVfx vfxToIce;
    public PlayerTransitionVfx vfxToGhost;
    public PlayerTransitionVfx vfxToMetal;

    private Rigidbody rb;
    private PlayerState playerState;
    private GhostLife ghostLife;
    private GameController gameController;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        ghostLife = GetComponent<GhostLife>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed * Time.deltaTime;
        bool running = false;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            running = true;
        }

        puppetAnimator.SetBool(hashRunning, running);
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

        // add effects of new state
        if (newState == PlayerState.NORMAL)
        {
            vfxToNormal.PlayVfx();
        }
        if (newState == PlayerState.TRANCE)
        {
            vfxToIce.PlayVfx();
        }
        if (newState == PlayerState.GHOST)
        {
            gameObject.layer = Layers.player_ghost;
            gameController.ShowAllTraversableObjects(true);
            ghostLife.enabled = true;
            vfxToGhost.PlayVfx();
        }
        if (newState == PlayerState.EXORCISM)
        {
            vfxToMetal.PlayVfx();
        }
    }

}