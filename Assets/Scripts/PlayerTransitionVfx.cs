using UnityEngine;
using System.Collections;

public class PlayerTransitionVfx : MonoBehaviour
{

    private readonly int hashPlay = Animator.StringToHash("play");

    public Animator animator;

    void Awake()
    {
        animator.gameObject.SetActive(false);
    }

    //    void Update()
    //    {
    //        if (Input.GetKeyDown(KeyCode.M))
    //        {
    //            PlayVfx();
    //        }
    //    }

    public void PlayVfx()
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger(hashPlay);
    }

    public void StopVfx()
    {
        animator.gameObject.SetActive(false);
    }

}
