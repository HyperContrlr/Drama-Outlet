using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class comicMove : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public Vector3 offsetComic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            if(Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                transform.position = transform.position + Vector3.Lerp(transform.position, mousePosition + offsetComic, moveSpeed);
            }
        }
    }
}
