using UnityEngine;
using UnityEngine.UIElements;

public class posterZoomThang : MonoBehaviour
{
    public GameObject thisThang;
    public GameObject uiThang;
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiThang.SetActive(true);
        }
    }
    public void OnMouseExit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiThang.SetActive(false);
        }
    }
}

