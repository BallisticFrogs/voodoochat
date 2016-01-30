using UnityEngine;
using System.Collections;

public class TrapTranslation : Slowable
{

    public Transform start;
    public Transform finish;
    public GameObject pole;

    private Transform activeTarget;

    // Use this for initialization
    void Start()
    {
        activeTarget = finish;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 gap = activeTarget.position - pole.transform.position;
        Vector3 movement = gap.normalized * speed * worldSpeedFactor * Time.deltaTime;
        if (movement.magnitude >= gap.magnitude)
        {
            movement = gap;
        }

        pole.transform.position += movement;

        if ((activeTarget.position - pole.transform.position).magnitude < 0.1f)
        {
            if (activeTarget == finish)
            {
                activeTarget = start;
            }
            else
            {
                activeTarget = finish;
            }
        }
    }

}
