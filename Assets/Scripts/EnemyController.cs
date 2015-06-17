using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    public float speed, tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float fireZone;
    private float nextFire;

    void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.rotation = Quaternion.Euler(0f, 180f, 0f);
        body.velocity = transform.forward * speed;
        nextFire = GetFireRate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Time.time > nextFire) || IsEnemyInTheWay()) return;
        nextFire = GetFireRate();
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        var sound = GetComponent<AudioSource>();
        sound.Play();
    }

    float GetFireRate()
    {
        return Time.time + Random.Range(fireRate, fireRate + 5.0f);
    }

    bool IsEnemyInTheWay()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit)) return false;
        if (transform.position.z > fireZone) return true;
        return hit.transform.tag == "Red Enemy" || hit.transform.tag == "Purple Enemy" || transform.position.z > fireZone;
    }
}
