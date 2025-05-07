using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public int stock;
    public TextMeshProUGUI stockText;
    public string identifier;
    public string mysteriousDescription = "I don't know anything about this item yet.";

    public void Start()
    {
        if (identifier == "Check-Out" && SaveDataController.Instance.CurrentData.isNewGame == false)
        {
            stock = 0;
        }
    }
    private void Update()
    {
        GenericDisplayText<int>.DisplayText(stockText, stock);
        if (stock > 0)
        {
            mysteriousDescription = "I don't know anything about this item yet.";
            this.gameObject.GetComponentInChildren<Image>().color = Color.white;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
        else if (stock <= 0) 
        {
            mysteriousDescription = "Press this object to select it, then place it in the store.";
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
