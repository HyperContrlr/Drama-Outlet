using UnityEngine;
using UnityEngine.InputSystem;

public class cameraPan : MonoBehaviour
{
    public float zoomSpeed = 50f;
    public Camera cam;
    private Vector3 lastMousePosition;
    public float zOffset;
    private void Start()
    {
        cam = Camera.main;
        lastMousePosition = cam.transform.position;
    }
    void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1f, 10f);
        Vector3 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = lastMousePosition - currentMousePosition;
            cam.transform.position += delta;
            //cam.transform.position += new Vector3(delta.x, delta.y, zOffset);
            lastMousePosition = currentMousePosition;
        }
    }
}
