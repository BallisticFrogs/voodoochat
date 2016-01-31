using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour
{
    private AudioSource audio;

	// Use this for initialization
	void Awake ()
	{
	    audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void PlayWalkingSound ()
	{
	    if (!audio.isPlaying)
	    {
            audio.Play();
        }
	}
}
