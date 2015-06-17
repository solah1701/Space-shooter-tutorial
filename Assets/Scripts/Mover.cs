using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;
    }
}
