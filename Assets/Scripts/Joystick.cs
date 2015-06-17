using UnityEngine;

[RequireComponent(typeof(TouchManager))]
public class Joystick : MonoBehaviour
{
    public float tilt;
    public float moveHorizontal;
    public float moveVertical;
    private TouchManager touchManager;

    void Start()
    {
        if (touchManager == null) touchManager = GetComponent<TouchManager>();
        if (touchManager == null) throw new MissingComponentException("TouchManager component is missing");
    }

    void FixedUpdate()
    {
        var body = GetComponent<Rigidbody>();
        var thisCollider = GetComponent<Collider>();
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (touchManager.pickedCollider == thisCollider)
        {
            Debug.Log("Touched Joystick");
        }
        body.rotation = Quaternion.Euler(moveVertical * tilt, 0.0f, moveHorizontal * -tilt);
    }
}
