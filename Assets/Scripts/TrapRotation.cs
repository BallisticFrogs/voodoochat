using UnityEngine;
using System.Collections;

public class TrapRotation : Slowable
{

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed * worldSpeedFactor * Time.deltaTime);
    }

}
