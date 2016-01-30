using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

    public Transform player = null;

    protected Vector3 offset;

    public void Awake()
    {
        offset = transform.position - player.position;
    }

    public void FixedUpdate()
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Lerp(transform.position.x, player.position.x + offset.x, Time.deltaTime * 10f);
        position.y = Mathf.Lerp(transform.position.y, player.position.y + offset.y, Time.deltaTime * 0.5f);
        position.z = Mathf.Lerp(transform.position.z, player.position.z + offset.z, Time.deltaTime * 10f);
        transform.position = position;
    }
}



