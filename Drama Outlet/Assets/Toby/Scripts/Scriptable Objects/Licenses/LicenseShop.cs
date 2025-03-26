using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using JetBrains.Annotations;

public class LicenseShop : MonoBehaviour
{

    [SerializeField] private License thisLicense;

    [SerializeField] private string name;

    [SerializeField] private string description;

    [SerializeField] private float cost;

    [SerializeField] private Image thisImage;

    [SerializeField] private GameObject thisObject;

    [SerializeField] private GameObject nextImage;

    [SerializeField] private GameObject previousImage;

    [SerializeField] private GameObject lockedOverlay;

    [SerializeField] private GameObject buyButton;

    public void SpriteCheck()
    {
        if (thisLicense.isBought == true)
        {
            thisImage.sprite = thisLicense.collectedSprite;
        }
        else
        {
            thisImage.sprite = thisLicense.defaultSprite;
        }
    }
    public void SetValues()
    {
        if (thisLicense.isLocked == true)
        {
            lockedOverlay.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            lockedOverlay.SetActive(false);
            buyButton.SetActive(true);
        }
        name = thisLicense.name;
        description = thisLicense.description;
        cost = thisLicense.cost;
        thisLicense.thisImage = thisImage;
        SpriteCheck();
    }
    void Start()
    {
        SetValues();
    }

    public void Next()
    {
        nextImage.SetActive(true);
        nextImage.GetComponent<LicenseShop>().SetValues();
        thisObject.SetActive(false);
    }

    public void Previous()
    {
        previousImage.SetActive(true);
        previousImage.GetComponent<LicenseShop>().SetValues();
        thisObject.SetActive(false);
    }
    public void BuyLicense()
    {
        if (thisLicense.isBought == true)
        {
            Statics.ReadRejection2();
        }
        else if (thisLicense.isLocked == true)
        {
            Statics.ReadRejection3();
        }
        if (Statics.money >= cost && thisLicense.isBought == false && thisLicense.isLocked == false)
        {
            //play bought sound effect
            thisLicense.Unlock();
            Statics.money -= cost;
        }
        else
        {
            Statics.ReadRejection1();
        }
    }
}
