using UnityEngine;

[CreateAssetMenu(fileName = "products", menuName = "Scriptable Objects/products")]
public class products : ScriptableObject
{
    [SerializeField] public enum Type {Mask, Makeup, Costume, Prop, MaskPlus, MakeupPlus, CostumePlus, PropPlus}

    [SerializeField] public Type typeOfProduct;
    
    [SerializeField] public string name;

    [SerializeField] public string description;

    [SerializeField] public float price;

    [SerializeField] public float restockPrice;

    [SerializeField] public float sellPricePerStock;

    [SerializeField] public float stockBought;

    [SerializeField] public float maxStock;

    [SerializeField] public float currentStock;

    [SerializeField] public bool isLocked = true;

    [SerializeField] public GameObject objectPrefab;
}
