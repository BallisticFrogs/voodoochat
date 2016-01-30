using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;
    public bool invicibility = false;
    public float timeElapsedSinceLastHit;

    private GameController gameController;
    private GameObject healthWidget;

    // Use this for initialization
    void Awake() {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.OnUiLoaded += OnUiLoaded;

    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (invicibility)
	    {
	        timeElapsedSinceLastHit += Time.deltaTime;
	        if (timeElapsedSinceLastHit >= 2.0f)
	        {
	            invicibility = false;
	        }
	    }
	    else
	    {
	        
	    }
	    if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void dealDamage(int damage)
    {
        health -= damage;
        invicibility = true;
        healthWidget.GetComponent<HealthUI>().updateHeartUI(health);
        timeElapsedSinceLastHit = 0.0f;
    }

    private void OnUiLoaded()
    {
        healthWidget = GameObject.FindGameObjectWithTag(Tags.HealthWidget);
        healthWidget.gameObject.SetActive(enabled);
    }

    void Die()
    {
        // TODO do dying thinks

        // reset lvl
        var gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.ResetLevel();
    }

}
