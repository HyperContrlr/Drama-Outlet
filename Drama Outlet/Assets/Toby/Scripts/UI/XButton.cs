using UnityEngine;

public class XButton : MonoBehaviour
{
    public GameObject objectToDeactivate;
    
    public void X()
    {
        objectToDeactivate.SetActive(false);
    }
}
