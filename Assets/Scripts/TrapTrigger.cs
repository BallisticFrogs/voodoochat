using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.CompareTag(Tags.Player))
        {
            GameObject player = collider.gameObject;
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (!playerHealth.invicibility)
            {
                playerHealth.ReceiveDamage(1);
            }
        }
    }

}
