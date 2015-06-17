using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

[RequireComponent(typeof(Joystick))]
[RequireComponent(typeof(Button))]
public class PlayerController : MonoBehaviour
{
    public float speed, tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    private Joystick joystick;
    private Button button;

    void Start()
    {
        if (joystick == null) joystick = GetComponent<Joystick>();
        if (joystick == null) throw new MissingComponentException("Joystick component is missing");
        if (button == null) button = GetComponent<Button>();
        if (button == null) throw new MissingComponentException("Button component is missing");

    }

    void Update()
    {
        if (!Input.GetButton("Fire1") || !(Time.time > nextFire)) return;
        nextFire = Time.time + fireRate;
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        var sound = GetComponent<AudioSource>();
        sound.Play();
    }

    void FixedUpdate()
    {
        var body = GetComponent<Rigidbody>();
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        body.velocity = movement * speed;
        body.position = new Vector3(
            Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax));
        body.rotation = Quaternion.Euler(0.0f, 0.0f, body.velocity.x * -tilt);
    }
}
