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
    public float stock;
    public void Start()
    {
        stock = thisProduct.maxStock;
        buttons = FindFirstObjectByType<RSIButtons>();
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