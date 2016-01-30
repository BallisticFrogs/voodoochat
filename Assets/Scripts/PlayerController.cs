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

        if (puppetAnimator != null) puppetAnimator.SetBool(hashRunning, running);
    }

    public void SetPlayerState(PlayerState newState)
    {
        if (newState == playerState) return;

        playerState = newState;

        if (newState == PlayerState.NORMAL)
        {
            vfxToNormal.PlayVfx();
        }
        if (newState == PlayerState.ICE)
        {
            vfxToIce.PlayVfx();
        }
        if (newState == PlayerState.GHOST)
        {
            vfxToGhost.PlayVfx();
        }
        if (newState == PlayerState.METAL)
        {
            vfxToMetal.PlayVfx();
        }
    }

}