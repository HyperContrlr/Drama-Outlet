using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RSIButtons : MonoBehaviour
{
    [SerializeField] public ProductManager currentProductManager;
    [SerializeField] public bool managerSelected;
    [SerializeField] private TextMeshProUGUI restockPrice;
    [SerializeField] private TextMeshProUGUI sellPrice;
    [SerializeField] private List<ProductManager> productManagers;
    [SerializeField] private List<Store> buildings;
    [SerializeField] private float restockPrices;
    [SerializeField] private float sellPrices;
    [SerializeField] private ButtonedPressed button;
    private float timer;

    public void OnEnable()
    {
        Defaults();
    }
    public void Update()
    {
        if (managerSelected == true)
        {
            restockPrice.text = string.Format($"[{currentProductManager.costToRestock}]");
            sellPrice.text = string.Format($"[${currentProductManager.thisProduct.objectSellPrice}]");
        }
        if (managerSelected == false)
        {
            Defaults();
            restockPrice.text = string.Format($"[{restockPrices}]");
            sellPrice.text = string.Format($"[${sellPrices}]");
        }
    }

    public void Defaults()
    {
        restockPrices = 0;
        sellPrices = 0;
        productManagers = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        buildings = FindObjectsByType<Store>(FindObjectsSortMode.None).ToList();
        foreach (ProductManager pro in productManagers)
        {
            restockPrices += pro.costToRestock;
            sellPrices += pro.thisProduct.objectSellPrice;
        }
    }
    public void RestockAll()
    {
        if (restockPrices > Statics.money && button.timer < 3)
        {
            Statics.ReadStatement("Sorry we can't Restock All now.");
            Invoke("CloseAnimator", 3);
        }
        else if (restockPrices <= Statics.money && button.timer >= 3)
        {
            foreach (ProductManager pro in productManagers)
            {
                pro.Restock();
            }
        }
    }
    public void CloseAnimator()
    {
        FindFirstObjectByType<ComedyDialogue>().EndDialogue();
    }
    public void SellAll()
    {
        foreach (ProductManager pro in productManagers)
        {
            pro.Sell();
        }
    }
    public void Restock()
    {
        if (managerSelected == true)
        {
            currentProductManager.Restock();
            managerSelected = false;
        }
        else
        {
            return;
        }
    }
    public void Sell()
    {
        if (managerSelected == true)
        {
            currentProductManager.Sell();
            managerSelected = false;
        }
        else
        {
            return;
        }
    }
}
