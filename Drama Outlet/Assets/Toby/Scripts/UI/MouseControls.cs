using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    [SerializeField] public InventoryItem item;
    [SerializeField] private List<InventoryItem> items;
    [SerializeField] private string identifier;
    [SerializeField] private RSIButtons buttons;
    [SerializeField] public bool isSelected;
    [SerializeField] private List<MouseControls> objectsInScene;
    [SerializeField] public bool isCheckOut;
    [SerializeField] public bool isProduct;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public float restockPrice;
    [SerializeField] public float sellPrice;
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
            buttons.managerSelected = true;
        }
        foreach (var mang in objectsInScene)
        {
            mang.isSelected = false;
        }
    }
    void Start()
    {
        items = FindObjectsByType<InventoryItem>(FindObjectsSortMode.None).ToList();
        foreach (var item in items)
        {
            if (item.identifier == identifier)
            {
                this.item = item;
            }
        }
        objectsInScene = FindObjectsByType<MouseControls>(FindObjectsSortMode.None).ToList();
        objectsInScene.Remove(this);
        buttons = FindFirstObjectByType<RSIButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        objectsInScene = FindObjectsByType<MouseControls>(FindObjectsSortMode.None).ToList();
        objectsInScene.Remove(this);
        if (isProduct == true)
        {
            restockPrice = this.gameObject.GetComponent<ProductManager>().costToRestock;
            sellPrice = this.gameObject.GetComponent<BuildingManager>().thisBuilding.objectSellPrice;
        }
        else
        {
            restockPrice = 0;
            sellPrice = this.gameObject.GetComponent<BuildingManager>().thisBuilding.objectSellPrice;
        }
        if (isSelected == true)
        {
            spriteRenderer.color = Color.yellow;
            buttons.currentSelectedObject = this;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Store();
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void Sell()
    {
            if (identifier == "Check-Out")
            {
                Store();
            }
            else
            {
                SaveDataController.Instance.CurrentData.money += this.gameObject.GetComponent<BuildingManager>().thisBuilding.objectSellPrice;
                DataSet();
                Destroy(this.gameObject);
            }
    }

    public void Restock()
    {
        if (isProduct == true)
        {
            this.gameObject.GetComponent<ProductManager>().Restock();
        }
        else
        {
            Statics.ReadStatement("We can't restock this you silly.");
            Invoke("CloseAnimator", 3);
        }
    }

    public void Store()
    {
        DataSet();
        item.AddToStock();
        Destroy(this.gameObject);
    }

    public void CloseAnimator()
    {
        FindFirstObjectByType<ComedyDialogue>().EndDialogue();
    }

    public void DataSet()
    {
        FurnitureData data = new()
        {
            position = this.transform.position,
            rotation = this.transform.rotation,
            thisBuilding = this.gameObject.GetComponent<BuildingManager>().thisBuilding,
        };
        SaveDataController.Instance.CurrentData.furniturePositions.Remove(data);
    }

    public void OnDisable()
    {
        Statics.UpdateGraph(this.gameObject);
    }
}
