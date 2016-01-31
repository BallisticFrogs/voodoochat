using UnityEngine;
using System.Collections;

public class PlayerTransitionVfx : MonoBehaviour
{

    private readonly int hashPlay = Animator.StringToHash("play");

    public Animator animator;

    public ParticleSystem[] particleSystems;

    void Awake()
    {
        //animator.gameObject.SetActive(false);
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
        Debug.LogWarning("PlayVfx");
        //animator.gameObject.SetActive(true);
        animator.SetTrigger(hashPlay);

        foreach (ParticleSystem system in particleSystems)
        {
            system.Play();
        }
    }

    public void StopVfx()
    {
        Debug.LogWarning("StopVfx");
        //animator.gameObject.SetActive(false);
        foreach (ParticleSystem system in particleSystems)
        {
            system.Stop();
        }
    }

}
