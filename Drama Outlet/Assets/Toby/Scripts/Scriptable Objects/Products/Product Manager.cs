using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ProductManager : MonoBehaviour
{
    [SerializeField] public products thisProduct;
    [SerializeField] public float costToRestock;
    [SerializeField] public TextMeshProUGUI restockText;
    public void Update()
    {
        costToRestock = thisProduct.restockPricePerStockMissing * (thisProduct.maxStock - thisProduct.currentStock);
        restockText.text = costToRestock.ToString();
    }
    public void Buy(float itemsBought)
    {
        if (itemsBought > thisProduct.currentStock)
        {
            for (float i = thisProduct.currentStock; i > 0; i--)
            {
                thisProduct.stockBought += 1;
            }
            thisProduct.currentStock = 0;
        }
        else
        {
            thisProduct.stockBought = itemsBought;
            thisProduct.currentStock -= itemsBought;
        }
        if (thisProduct.currentStock == 0)
        {
            //alert player to lack of stock
        }
    }

    public void Restock()
    {
        if (Statics.money < costToRestock)
        {
            Statics.ReadStatement("Sorry we don't have enough money to restock that.");
        }
        else
        {
            Statics.money -= costToRestock;
            thisProduct.currentStock = thisProduct.maxStock;
        }
    }

    public void Store()
    {
        //something with inventory
    }

    public void Sell()
    {
        Statics.money += thisProduct.objectSellPrice;
        Destroy(this.gameObject);
    }
}