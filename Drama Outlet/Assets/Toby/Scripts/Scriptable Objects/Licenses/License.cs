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

    [SerializeField] public Sprite defaultSprite;

    [SerializeField] public Sprite collectedSprite;

    [SerializeField] public List<products> productsUnlocked;

    [SerializeField] public bool isBought;

    [SerializeField] public bool isLocked = true;

    [SerializeField] public bool starLock;

    [SerializeField] public bool hasStarLock;

    [SerializeField] public float StarsForUnlock;

    [SerializeField] public List<License> licensesUnlocked;
    public void Unlock()
    {
        foreach (products product in productsUnlocked)
        {
            isBought = true;
            thisImage.sprite = collectedSprite;
            product.isLocked = true;
        }
        if (licensesUnlocked.Count != 0)
        {
            foreach (License lice in licensesUnlocked)
            {
                lice.isLocked = false;
            }
        }
    }
}
