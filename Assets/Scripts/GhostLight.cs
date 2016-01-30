using UnityEngine;
using System.Collections;

public class GhostLight : MonoBehaviour
{

    public float baseDamage;

    private float radius;

    void Awake()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            radius = sphereCollider.radius;
        }
    }


    void OnTriggerStay(Collider other)
    {
        GhostLife ghostLife = other.gameObject.GetComponent<GhostLife>();
        if (ghostLife != null)
        {
            Vector3 v = other.gameObject.transform.position - transform.position;
            float dstToLight = v.magnitude;
            if (dstToLight < radius)
            {
                // check the light can reach the ghost
                bool hit = Physics.Linecast(transform.position, other.gameObject.transform.position);

                if (hit)
                {
                    // compute and apply dmg
                    float dmg = baseDamage * (1 - dstToLight / radius);
                    ghostLife.ReceiveDamage(dmg);
                }
            }
        }
    }

}
