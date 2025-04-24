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
    [SerializeField] private List<Sprite> stockSprites;
    [SerializeField] private SpriteRenderer currentSprite;
    [SerializeField] private string identifier_;
    [SerializeField] private List<InventoryItem> items;
    [SerializeField] public InventoryItem item;
    [SerializeField] private List<Store> stores;
    public float stock;
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
        foreach (var store in stores)
        {
            store.isSelected = false;
        }   
    }
    public void Start()
    {
        items = FindObjectsByType<InventoryItem>(FindObjectsSortMode.None).ToList();
        foreach (var item_ in items)
        {
            if (item_.identifier == identifier_)
            {
                item = item_;
            }
        }
        stock = thisProduct.maxStock;
        buttons = FindFirstObjectByType<RSIButtons>();
        productManagers = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        productManagers.Remove(this);
        stores = FindObjectsByType<Store>(FindObjectsSortMode.None).ToList();
    }
    public void SpriteSet()
    {
        float threeFourths = thisProduct.maxStock * 0.75f;
        float oneHalf = thisProduct.maxStock * 0.5f;
        float oneFourth = thisProduct.maxStock * 0.25f;
        if (stock <= thisProduct.maxStock && stock > threeFourths)
        {
            currentSprite.sprite = stockSprites[0];
        }
        else if (stock <= threeFourths && stock > oneHalf)
        {
            currentSprite.sprite = stockSprites[1];
        }
        else if (stock <= oneHalf && stock > oneFourth)
        {
            currentSprite.sprite = stockSprites[2];
        }
        else if (stock <= oneFourth && stock > 0)
        {
            currentSprite.sprite = stockSprites[3];
        }
        else if (stock <= 0)
        {
            currentSprite.sprite = stockSprites[4];
        }
    }
    public void Update()
    {
        SpriteSet();
        if (isSelected == true)
        {
            currentSprite.color = Color.yellow;
            buttons.currentProductManager = this;
            buttons.managerSelected = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Store();
            }
        }
        else
        {
            currentSprite.color = Color.white;
        }
        productManagers = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        productManagers.Remove(this);
        stores = FindObjectsByType<Store>(FindObjectsSortMode.None).ToList();
        costToRestock = thisProduct.restockPricePerStockMissing * (thisProduct.maxStock - stock);
    }
    public void Buy(float itemsBought)
    {
        if (itemsBought > stock)
        {
            for (float i = stock; i > 0; i--)
            {
                thisProduct.stockBought += 1;
            }
            stock = 0;
        }
        else
        {
            thisProduct.stockBought = itemsBought;
            stock -= itemsBought;
        }
        if (stock == 0)
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
            stock = thisProduct.maxStock;
        }
    }

    public void Store()
    {
        item.AddToStock();
        Destroy(this.gameObject);
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