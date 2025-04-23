using UnityEngine;

[CreateAssetMenu(fileName = "products", menuName = "Scriptable Objects/products")]
public class products : ScriptableObject
{

    [SerializeField] public NPCAI.NPC.Preference type;

    [SerializeField] public string name;

    [SerializeField] public string description;

    [SerializeField] public float price;

    [SerializeField] public float objectSellPrice;

    [SerializeField] public float restockPricePerStockMissing;

    [SerializeField] public float sellPricePerStock;

    [SerializeField] public float stockBought;

    [SerializeField] public float maxStock;

    [SerializeField] public bool isLocked;

    [SerializeField] public GameObject objectPrefab;
}
