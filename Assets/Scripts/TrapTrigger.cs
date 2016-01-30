using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameObject player = collider.gameObject;
            PlayerHealth  playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.health -=1;
        }
    }
}
