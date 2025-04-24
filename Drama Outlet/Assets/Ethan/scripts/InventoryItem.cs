using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public int stock;
    public TextMeshProUGUI stockText;
    public string identifier;
    private void Update()
    {
        GenericDisplayText<int>.DisplayText(stockText, stock);
        if (stock > 0)
        {
            this.gameObject.GetComponentInChildren<Image>().color = Color.white;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
        else if (stock <= 0) 
        {
            this.gameObject.GetComponentInChildren<Image>().color = Color.black;
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
