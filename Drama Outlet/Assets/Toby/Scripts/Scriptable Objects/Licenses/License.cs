using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "License", menuName = "Scriptable Objects/License")]
public class License : ScriptableObject
{
    [SerializeField] public string name;

    [SerializeField] public string description;

    [SerializeField] public float cost;

    [SerializeField] public Image thisImage;

    [SerializeField] public Sprite collectedSprite;

    [SerializeField] public List<products> productsUnlocked;

    public void Unlock()
    {
        foreach (products product in productsUnlocked)
        {
            thisImage.sprite = collectedSprite;
            product.isLocked = true;
        }
    }
}
