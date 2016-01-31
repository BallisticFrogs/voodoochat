using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour
{

    private readonly int hashAttack = Animator.StringToHash("attack");

    public Animator animator;

    public float attackCooldown = 1f;

    private float lastAttackTime = -1000;

    void OnCollisionEnter(Collision collision)
    {
        HandleContact(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        HandleContact(collision);
    }

    void HandleContact(Collision collision)
    {
        if (Time.time > lastAttackTime + attackCooldown && collision.collider.CompareTag(Tags.Player))
        {
            Attack(collision.collider.gameObject);
        }
    }

    void Attack(GameObject player)
    {
        lastAttackTime = Time.time;
        if (animator != null) animator.SetTrigger(hashAttack);

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (!playerHealth.invicibility)
        {
            playerHealth.ReceiveDamage(1);
        }
    }

}
