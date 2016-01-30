using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.SendMessage("Kill");
        }
    }
}
