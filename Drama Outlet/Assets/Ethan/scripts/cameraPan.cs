using UnityEngine;
using UnityEngine.InputSystem;

public class cameraPan : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public Camera cam;
    public Vector3 offset;
    private void Start()
    {
        cam = Camera.main;
    }
    private Vector3 lastMousePosition;

    void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
        //if (Input.GetMouseButtonDown(2))
        //{
        //    lastMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //}

        if (Input.GetMouseButton(2))
        {
            Vector3 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = lastMousePosition - currentMousePosition;
            cam.transform.position += delta;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, offset.z);
            lastMousePosition = currentMousePosition;
        }
    }
     
    
}
