using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "buildings", menuName = "Scriptable Objects/buildings")]
public class buildings : ScriptableObject
{
    [SerializeField] public string name;

    [SerializeField] public string description;

    [SerializeField] public float cost;

    [SerializeField] public float objectSellPrice;

    [SerializeField] public bool isLocked = true;

    [SerializeField] public bool hasProduct;

    [SerializeField] public products soldProduct;

    [SerializeField] public float ratingBonus;

    [SerializeField] public float ratingUnlock;

    [SerializeField] public GameObject objectPrefab;

}
