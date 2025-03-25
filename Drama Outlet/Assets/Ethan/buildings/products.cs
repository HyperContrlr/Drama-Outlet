using UnityEngine;

[CreateAssetMenu(fileName = "products", menuName = "Scriptable Objects/products")]
public class products : ScriptableObject
{
    [SerializeField] private string name;

    [SerializeField] private string description;

    [SerializeField] private float price;

    [SerializeField] private float restockPrice;

    [SerializeField] private float sellPriceperStock;

    [SerializeField] private float maxStock;

    [SerializeField] private bool isLocked = true;
}
