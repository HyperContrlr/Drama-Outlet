using UnityEngine;

[CreateAssetMenu(fileName = "products", menuName = "Scriptable Objects/products")]
public class products : ScriptableObject
{
    [SerializeField] public string name;

    [SerializeField] public string description;

    [SerializeField] public float price;

    [SerializeField] public float restockPrice;

    [SerializeField] public float sellPriceperStock;

    [SerializeField] public float maxStock;

    [SerializeField] public bool isLocked = true;

    [SerializeField] public GameObject objectPrefab;
}
