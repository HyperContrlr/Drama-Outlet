using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ProductManager : MonoBehaviour
{
    [SerializeField] private RSIButtons buttons;
    [SerializeField] public products thisProduct;
    [SerializeField] public float costToRestock;
    [SerializeField] public bool isSelected;
    [SerializeField] private List<ProductManager> productManagers;
    public void OnMouseDown()
    {
        if (isSelected == true)
        {
            isSelected = false;
            buttons.managerSelected = false;
        }
        else
        {
            isSelected = true;
        }
        foreach (var pro in productManagers)
        {
            pro.isSelected = false;
        }
    }
    public void OnEnable()
    {
        buttons = FindFirstObjectByType<RSIButtons>();
        productManagers = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        productManagers.Remove(this);
    }
    public void Update()
    {
        if (isSelected == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            buttons.currentProductManager = this;
            buttons.managerSelected = true;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        productManagers = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        productManagers.Remove(this);
        costToRestock = thisProduct.restockPricePerStockMissing * (thisProduct.maxStock - thisProduct.currentStock);
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
            Statics.ReadStatement("Sorry we can't Restock that.");
            Invoke("CloseAnimator", 3);
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
    public void CloseAnimator()
    {
        FindFirstObjectByType<ComedyDialogue>().EndDialogue();
    }
}