using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] public InventoryItem item;
    [SerializeField] private string identifier;
    [SerializeField] private RSIButtons buttons;
    [SerializeField] public bool isSelected;
    [SerializeField] private List<ProductManager> productManagers;
    [SerializeField] private List<Store> stores;
    [SerializeField] public bool isCheckOut;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    void Start()
    {
        stores = FindObjectsByType<Store>(FindObjectsSortMode.None).ToList();
        stores.Remove(this);
        buttons = FindFirstObjectByType<RSIButtons>();
        if (isCheckOut == false)
        {
            List<InventoryItem> items = FindObjectsByType<InventoryItem>(FindObjectsSortMode.None).ToList();
            foreach (var item_ in items)
            {
                if (item_.identifier == identifier)
                {
                    item = item_;
                }
            }
        }
        if (isCheckOut == true)
        {
            return;
        }
    }

    public void Stored()
    {
        item.AddToStock();
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        stores = FindObjectsByType<Store>(FindObjectsSortMode.None).ToList();
        stores.Remove(this);
        productManagers = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        if (isSelected == true)
        {
            spriteRenderer.color = Color.yellow;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isCheckOut == true)
                {
                    Destroy(this.gameObject);
                }
                Stored();
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
