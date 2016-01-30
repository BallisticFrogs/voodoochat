using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        // TODO do dying thinks

        // reset lvl
        var gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.ResetLevel();
    }

}
