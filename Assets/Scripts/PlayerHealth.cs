using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;

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
	    healthWidget.GetComponent<HealthUI>().updateHeartUI(health);

        if (health <= 0)
        {
            health = 0;
            Die();
        }
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
