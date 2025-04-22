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
    [SerializeField] private float restockPrices;
    [SerializeField] private float sellPrices;
    private float timer;

    public void OnEnable()
    {
        Defaults();
    }
    public void Update()
    {
        if (managerSelected == true)
        {
            restockPrice.text = string.Format($"Restock [${currentProductManager.costToRestock}] R");
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
        foreach (ProductManager pro in productManagers)
        {
            restockPrices += pro.costToRestock;
            sellPrices += pro.thisProduct.objectSellPrice;
        }
    }
    public void RestockAll()
    {
        if (restockPrices > Statics.money)
        {
            Statics.ReadStatement("Sorry we can't Restock All now.");
            Invoke("CloseAnimator", 3);
        }
        else
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
        currentProductManager.Restock();
        managerSelected = false;
    }
    public void Sell()
    {
        currentProductManager.Sell();
        managerSelected = false;
    }
}
