using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostLife : MonoBehaviour
{

    private Slider ghostLifeWidget;

    public float maxLife = 100;
    public float regenDelay = 1f;
    public float regenStrength = 0.5f;
    public bool showGhostLife = true;

    private float ghostLife;

    private float timeToRegen;

    private GameController gameController;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.OnUiLoaded += OnUiLoaded;

        enabled = false;
    }

    void OnEnable()
    {
        ghostLife = maxLife;
        if (ghostLifeWidget != null)
        {
            ghostLifeWidget.gameObject.SetActive(true);
            UpdateLifeWidget();
        }
    }

    void OnDisable()
    {
        if (ghostLifeWidget != null)
        {
            ghostLifeWidget.gameObject.SetActive(false);
        }
    }

    private void OnUiLoaded()
    {
        ghostLifeWidget = GameObject.FindGameObjectWithTag(Tags.GhostLifeWidget).GetComponent<Slider>();
        ghostLifeWidget.gameObject.SetActive(enabled);
    }

    void Update()
    {
        timeToRegen -= Time.deltaTime;
        if (timeToRegen <= 0 && ghostLife < maxLife)
        {
            ghostLife += regenStrength;
            if (ghostLife > maxLife)
            {
                ghostLife = maxLife;
            }
            UpdateLifeWidget();
        }
    }

    public void ReceiveDamage(float dmg)
    {
        ghostLife -= dmg;
        UpdateLifeWidget();
        timeToRegen = regenDelay;
        if (ghostLife < 0)
        {
            // game over
            gameController.GameOver();
        }
    }

    private void UpdateLifeWidget()
    {
        if (ghostLifeWidget != null)
        {
            ghostLifeWidget.value = ghostLife/100;
        }
    }

}
