﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health = 3;
    public bool invicibility = false;
    public float timeElapsedSinceLastHit;

    private GameController gameController;
    private HealthUI healthWidget;

    // Use this for initialization
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.OnUiLoaded += OnUiLoaded;

    }

    // Update is called once per frame
    void Update()
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

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        invicibility = true;
        healthWidget.updateHeartUI(health);
        timeElapsedSinceLastHit = 0.0f;
        CameraShaker.shaker.shake = 0.3f;
    }

    private void OnUiLoaded()
    {
        healthWidget = GameObject.FindGameObjectWithTag(Tags.HealthWidget).GetComponent<HealthUI>();
        healthWidget.gameObject.SetActive(enabled);
    }

    void Die()
    {
        // TODO do dying thinks

        // game over
        gameController.GameOver();
    }

}
