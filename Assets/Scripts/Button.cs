using UnityEngine;

[RequireComponent(typeof(TouchManager))]
public class Button : MonoBehaviour
{
    public float tilt = 5;
    private float rest = 90;
    private TouchManager touchManager;
    public bool pressed;

    void Start()
    {
        if (touchManager == null) touchManager = GetComponent<TouchManager>();
        if (touchManager == null) throw new MissingComponentException("TouchManager component is missing");
    }

    void FixedUpdate()
    {
        var body = GetComponent<Rigidbody>();
        var move = rest + tilt;
        pressed = false;
        body.rotation = Quaternion.Euler(rest, 0.0f, 0.0f);
        //if (!Input.GetButton("Fire1") && !touchManager.pickedObject == body) return;
        if (!touchManager.pickedObject == body) return;
        pressed = true;
        body.rotation = Quaternion.Euler(move, 0.0f, 0.0f);
    }
}
