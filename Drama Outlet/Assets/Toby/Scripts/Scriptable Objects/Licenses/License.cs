using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "License", menuName = "Scriptable Objects/License")]
public class License : ScriptableObject
{
    [SerializeField] public string name;

    [TextArea(3, 10)]
    [SerializeField] public string description;

    [SerializeField] public float cost;

    [SerializeField] public Image thisImage;

    [SerializeField] public List<products> productsUnlocked;

    [SerializeField] public bool isBought;

    [SerializeField] public bool isLocked = true;

    [SerializeField] public bool starLock;

    [SerializeField] public bool hasStarLock;

    [SerializeField] public float StarsForUnlock;

    [SerializeField] public List<License> licensesUnlocked;

    public bool mask;
    public void Unlock()
    {
        foreach (var product in productsUnlocked)
        {
            product.isLocked = false;
        }
        if (licensesUnlocked.Count != 0)
        {
            foreach (License lice in licensesUnlocked)
            {
                lice.isLocked = false;
            }
        }
    }

    public void ResetValues()
    {
        isLocked = true;
        if (mask == true)
        {
            isBought = true;
            isLocked = false;
        }
        else
        isBought = false;
        if (hasStarLock == true)
        {
            starLock = true;
        }
        else
        {
            starLock = false;
        }
    }
}
