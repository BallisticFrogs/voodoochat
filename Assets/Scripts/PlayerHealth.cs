using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int health = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        Application.LoadLevel(1);
        //do dying thinks
    }


}
