using UnityEngine;

public class CameraIgnoreGround : MonoBehaviour
{
    [SerializeField] private LayerMask inputLayerMask;

    private void Start()
    {
        Camera.main.eventMask = inputLayerMask;
    }
}
