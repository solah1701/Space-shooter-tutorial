using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{

    private static class MouseManager
    {
        public enum MousePhase
        {
            Began,
            Moved,
            Ended
        }

        public static bool ButtonDown { get; private set; }
        public static MousePhase Phase { get; set; }

        public static void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Phase = ButtonDown ? MousePhase.Moved : MousePhase.Began;
                ButtonDown = true;
            }
            if (!Input.GetMouseButtonUp(0)) return;
            ButtonDown = false;
            Phase = MousePhase.Ended;
        }
    }

    public float maxPickingDistance = 10000;// increase if needed, depending on your scene size
    private Vector3 startPos;
    public Vector3 touchPos;
    public Transform pickedObject = null;
    public Collider pickedCollider = null;
    public bool dragObject;
    private bool touched;

    // Update is called once per frame
    void Update()
    {
        //If running game in editor
#if UNITY_EDITOR
        MouseManager.CheckInput();
        touched = RaycastMouse();
        //if (touched) Debug.Log("Mouse Touched");
#endif
        //If a touch is detected
        if (Input.touchCount <= 0) return;
        foreach (var touch in Input.touches)
            RaycastTouch(touch);
        touched = true;
    }

    private void CheckRayBegin(Ray ray, Vector3 position)
    {
        RaycastHit hit;
        pickedCollider = null;
        pickedObject = null;
        if (!Physics.Raycast(ray, out hit, maxPickingDistance)) return;
        pickedObject = hit.transform;
        pickedCollider = hit.collider;
        startPos = position;
        //Debug.Log(string.Format("StartPos {0}", startPos));
    }

    private void CheckRayMoved(Ray ray, Plane horPlane)
    {
        float distance1;
        if (!horPlane.Raycast(ray, out distance1)) return;
        touchPos = ray.GetPoint(distance1);
        if (dragObject && pickedObject != null) pickedObject.transform.position = touchPos;
        if (dragObject && pickedCollider != null) pickedCollider.transform.position = touchPos;
        //Debug.Log(string.Format("RayPos {0}", touchPos));
    }

    private bool RaycastMouse()
    {
        //if (!Input.GetMouseButton(0)) return;
        //Create horizontal plane
        var horPlane = new Plane(Vector3.up, Vector3.zero);

        //Gets the ray at position where the screen is touched
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        switch (MouseManager.Phase)
        {
            case MouseManager.MousePhase.Began:
                CheckRayBegin(ray, Input.mousePosition);
                return true;
            case MouseManager.MousePhase.Moved:
                CheckRayMoved(ray, horPlane);
                return true;
            case MouseManager.MousePhase.Ended:
                pickedObject = null;
                pickedCollider = null;
                return false;
        }
        return false;
    }

    private void RaycastTouch(Touch touch)
    {
        //Create horizontal plane
        var horPlane = new Plane(Vector3.up, Vector3.zero);

        //Gets the ray at position where the screen is touched
        var ray = Camera.main.ScreenPointToRay(touch.position);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                CheckRayBegin(ray, touch.position);
                break;
            case TouchPhase.Moved:
                CheckRayMoved(ray, horPlane);
                break;
            case TouchPhase.Ended:
                pickedObject = null;
                pickedCollider = null;
                break;
        }
    }
}
