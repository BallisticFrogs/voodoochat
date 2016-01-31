using UnityEngine;
using System.Collections;

public class GhostLight : MonoBehaviour
{

    public float baseDamage;

    private float radius;
    private int rayCastMask = 1 << Layers.default_layer | 1 << Layers.obstacles | 1 << Layers.obstacles_noghost | 1 << Layers.player_ghost;

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
            Vector3 v = other.gameObject.transform.position - transform.position; //+ Vector3.up * 0.5f;
            float dstToLight = v.magnitude;
            if (dstToLight < radius)
            {
                // check the light can reach the ghost
                RaycastHit raycastHit;
                bool hit = Physics.Linecast(transform.position, other.gameObject.transform.position, out raycastHit, rayCastMask);

                // Debug.DrawLine(transform.position, other.gameObject.transform.position);
                if (hit && raycastHit.collider.gameObject == other.gameObject)
                {
                    // compute and apply dmg
                    float dmg = baseDamage * (1 - dstToLight / radius);
                    ghostLife.ReceiveDamage(dmg);
                }
            }
        }
    }

}
