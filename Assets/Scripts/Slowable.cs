using UnityEngine;
using System.Collections;

public abstract class Slowable : MonoBehaviour
{

    public delegate void WorldSpeedHandler(float newSpeed);
    public event WorldSpeedHandler OnWorldSpeedChanged;

    public float speed;

    protected float worldSpeedFactor = 1;

    private GameController gameController;

    public void SetWorldSpeed(float newSpeed)
    {
        worldSpeedFactor = newSpeed;
        if (OnWorldSpeedChanged != null) OnWorldSpeedChanged(newSpeed);
    }

    protected virtual void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        gameController.RegisterSlowable(this);
    }

    void OnDestroy()
    {
        gameController.RemoveSlowable(this);
    }

}
