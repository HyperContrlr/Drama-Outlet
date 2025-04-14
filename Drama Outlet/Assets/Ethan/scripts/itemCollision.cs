using UnityEngine;

public class itemCollision : MonoBehaviour
{
    public bool isOver;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("works");
        if (other.gameObject.CompareTag("furniture"))
        {
            isOver = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("furniture"))
        {
            isOver = false;
        }
    }
}
