using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionTracker : MonoBehaviour
{

    public readonly HashSet<GameObject> colliders = new HashSet<GameObject>();

    void FixedUpdate()
    {
        colliders.RemoveWhere(obj => obj == null);
    }

    void OnTriggerEnter(Collider other)
    {
        colliders.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        colliders.Add(other.gameObject);
    }

}
