using UnityEngine;
using System.Collections;

public class PressurePlate : Slowable
{

    public GameObject mesh;
    public float pressTime = 0.5f;
    public float releaseTime = 2f;
    public ActivableObject activatedObject;

    public AudioClip pressurePlateDown;
    public AudioClip pressurePlateUp;
    private AudioSource audioSource;

    private float pressedRatio;
    private float baseMeshY;
    private float meshHeight;
    private bool active;

    protected override void Awake()
    {
        base.Awake();
        baseMeshY = mesh.transform.localPosition.y;
        meshHeight = mesh.transform.localScale.y;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (pressedRatio > 0)
        {
            if (active == true && pressedRatio <= 0.9f && !audioSource.isPlaying)
            {
                audioSource.clip = pressurePlateUp;
                audioSource.Play();
            }
            pressedRatio -= Time.deltaTime * worldSpeedFactor / releaseTime;
            pressedRatio = Mathf.Max(0, pressedRatio);

            Vector3 localPosition = mesh.transform.localPosition;
            localPosition.y = baseMeshY - pressedRatio * meshHeight;
            mesh.transform.localPosition = localPosition;
        }
        if (active && pressedRatio == 0)
        {
            active = false;
        }

        if (activatedObject != null) activatedObject.SetActive(active);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (pressedRatio == 0)
            {
                audioSource.clip = pressurePlateDown;
                audioSource.Play();
            }
            pressedRatio += Time.deltaTime * worldSpeedFactor / pressTime;
            pressedRatio = Mathf.Min(1, pressedRatio);

            if (pressedRatio == 1)
            {
                active = true;
            }
        }
    }

}
