﻿using UnityEngine;
using System.Collections;

public class PressurePlate : Slowable
{

    public GameObject mesh;
    public float pressTime = 0.5f;
    public float releaseTime = 2f;

    private float pressedRatio;
    private float baseMeshY;
    private float meshHeight;

    protected override void Awake()
    {
        base.Awake();
        baseMeshY = mesh.transform.localPosition.y;
        meshHeight = mesh.transform.localScale.y;
    }

    void Update()
    {
        if (pressedRatio > 0)
        {
            pressedRatio -= Time.deltaTime * worldSpeedFactor / releaseTime;
            pressedRatio = Mathf.Max(0, pressedRatio);

            Vector3 localPosition = mesh.transform.localPosition;
            localPosition.y = baseMeshY - pressedRatio * meshHeight;
            mesh.transform.localPosition = localPosition;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            pressedRatio += Time.deltaTime * worldSpeedFactor / pressTime;
            pressedRatio = Mathf.Min(1, pressedRatio);
        }
    }

}