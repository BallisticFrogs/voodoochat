using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

    public Transform player = null;
    public float cameraHeight = 30.0f;
    public float zOffset = -1.0f;

    private Transform cam = null;


    public void Start()
    {
        cam = transform;
    }
    public void Update()
    { 
        Vector3 pos = player.position;
        pos.y = cameraHeight;
        pos.z += zOffset;
        cam.position = pos;
    }
}



