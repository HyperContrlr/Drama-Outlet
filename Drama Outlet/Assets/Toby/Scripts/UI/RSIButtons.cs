using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RSIButtons : MonoBehaviour
{
    [SerializeField] public MouseControls currentSelectedObject;
    [SerializeField] public bool managerSelected;
    [SerializeField] private TextMeshProUGUI restockPrice;
    [SerializeField] private TextMeshProUGUI sellPrice;
    [SerializeField] private List<MouseControls> managers;
    [SerializeField] public float restockPrices;
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
            if (currentSelectedObject == null)
            {
                return;
            }
            if (currentSelectedObject.isProduct == true)
            {
                restockPrice.text = string.Format($"[{currentSelectedObject.restockPrice}]");
            }
            sellPrice.text = string.Format($"[${currentSelectedObject.sellPrice}]");
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
        managers = FindObjectsByType<MouseControls>(FindObjectsSortMode.None).ToList();
        foreach (var mang in managers)
        {
            restockPrices += mang.restockPrice;
            sellPrices += mang.sellPrice;
        }
    }
    public void RestockAll()
    {
        if (restockPrices > SaveDataController.Instance.CurrentData.money)
        {
            Statics.ReadStatement("Sorry we can't Restock All now.");
            Invoke("CloseAnimator", 6);
        }
        else if (restockPrices <= SaveDataController.Instance.CurrentData.money)
        {
            foreach (var mang in managers)
            {
                if (mang.isProduct == true)
                {
                    mang.Restock();
                }
                else
                {
                    Debug.Log("We can't restock this you silly.");
                }
            }
        }
    }
    public void CloseAnimator()
    {
        FindFirstObjectByType<ComedyDialogue>().EndDialogue();
    }
    public void SellAll()
    {
        foreach (var mang in managers)
        {
            mang.Sell();
        }
    }
    public void Restock()
    {
        if (managerSelected == true && currentSelectedObject.isProduct == true)
        {
            currentSelectedObject.Restock();
            managerSelected = false;
        }
        else if (managerSelected == false)
        {
            RestockAll();
        }
    }
    public void Sell()
    {
        if (managerSelected == true)
        {
            currentSelectedObject.Sell();
            managerSelected = false;
        }
        else
        {
            return;
        }
    }
}
