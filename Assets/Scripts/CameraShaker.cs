using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{

    public static CameraShaker shaker;

    public Camera cam;
    public float shake = 0;
    public float shakeAmount = 0.4f;
    public float decreaseFactor = 1.0f;

    void Awake()
    {
        shaker = this;
    }

    void OnDestroy()
    {
        shaker = null;
    }

    void Update()
    {
        if (shake > 0)
        {
            cam.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            cam.transform.localPosition = Vector3.zero;
            shake = 0;
        }
    }

}
