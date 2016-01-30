using UnityEngine;
using System.Collections;

public class TrapRotation : MonoBehaviour {

    public float speedRotation;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector3.up * speedRotation * Time.deltaTime);
    }
}
