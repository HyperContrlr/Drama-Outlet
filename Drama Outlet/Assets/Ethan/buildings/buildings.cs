using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "buildings", menuName = "Scriptable Objects/buildings")]
public class buildings : ScriptableObject
{
    [SerializeField] public string name;

    [SerializeField] public string description;

    [SerializeField] public float cost;

    [SerializeField] public bool isLocked = true;

    [SerializeField] public float ratingUnlock;

    [SerializeField] public GameObject objectPrefab;

}
