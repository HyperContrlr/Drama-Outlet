using UnityEngine;

public class level : MonoBehaviour
{
    public bool mouseOver = false;
    public void OnMouseOver()
    {
        mouseOver = true;
        Debug.Log("Over");
    }
    public void OnMouseExit()
    {
        mouseOver = false;
        Debug.Log("and Out");
    }
}
