using UnityEngine;
using System.Collections;

public class ActivableObject : MonoBehaviour
{

    private readonly int hashActive = Animator.StringToHash("active");

    public Animator animator;

    private bool activatedThisFrame;

    public void SetActive(bool active)
    {
        activatedThisFrame |= active;
    }

    void Update()
    {
        animator.SetBool(hashActive, activatedThisFrame);
        activatedThisFrame = false;
    }

}
