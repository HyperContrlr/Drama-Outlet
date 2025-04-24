using UnityEngine;

public class SwitchColliders : MonoBehaviour
{
    private Collider2D collider1;
    private Collider2D collider2;
    public bool hasRotated;
    public bool beenPlaced;

    public void Switch()
    {
        if (beenPlaced == false)
        {
            hasRotated = FindFirstObjectByType<buildingPlacer>().flipped;
        }
        if (hasRotated == true)
        {
            collider1.enabled = false;
            collider2.enabled = true;
        }
        else if (hasRotated == false)
        {
            collider1.enabled = true;
            collider2.enabled = false;
        }
    }
    public void Update()
    {
    
    }
}
