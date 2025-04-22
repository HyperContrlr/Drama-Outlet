using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int stock;
    public GameObject thisPrefab;
    private void Update()
    {
        if (stock <= 0)
        {
            ;
        }
    }
}
