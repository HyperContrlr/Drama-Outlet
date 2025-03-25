using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "buildings", menuName = "Scriptable Objects/buildings")]
public class buildings : ScriptableObject
{
    [SerializeField] private string name;

    [SerializeField] private string description;

    [SerializeField] private float cost;

    [SerializeField] private bool isLocked = true;

    [SerializeField] private float ratingUnlock;

}
