using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public int stock;
    private void Update()
    {
        if (stock > 0)
        {
            this.gameObject.GetComponentInChildren<Image>().color = Color.white;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
        else if (stock <= 0) 
        {
            this.gameObject.GetComponentInChildren<Image>().color = Color.gray;
            this.gameObject.GetComponent<Button>().enabled = false;
        }
    }
    public void AddToStock()
    {
        stock++;
    }
    public void SubtractFromStock()
    {
        stock--;
    }
}
