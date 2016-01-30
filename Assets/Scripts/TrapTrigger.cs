using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.CompareTag("Player"))
        {
            GameObject player = collider.gameObject;
            PlayerHealth  playerHealth = player.GetComponent<PlayerHealth>();
            if (!playerHealth.invicibility)
            {
                playerHealth.dealDamage(1);
            }

        }
    }
}
