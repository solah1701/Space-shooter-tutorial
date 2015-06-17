using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float tumble;

    // Use this for initialization
    void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
