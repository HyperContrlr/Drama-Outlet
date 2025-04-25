using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using JetBrains.Annotations;
using TMPro;

public class LicenseShop : MonoBehaviour
{

    [SerializeField] private License thisLicense;

    [SerializeField] private string name;

    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private float cost;

    [SerializeField] private Image thisImage;

    [SerializeField] private GameObject thisObject;

    [SerializeField] private GameObject nextImage;

    [SerializeField] private GameObject previousImage;

    [SerializeField] private List<GameObject> imagesToDelete;

    [SerializeField] private GameObject lockedOverlay;

    [SerializeField] private GameObject buyButton;

    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private bool numeroUno;

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

    public void LicenseUnlock()
    {
        if (thisLicense.isLocked == true || thisLicense.starLock == true)
        {
            lockedOverlay.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            lockedOverlay.SetActive(false);
            buyButton.SetActive(true);
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
            if (thisLicense.hasStarLock == true && thisLicense.starLock == true)
            {
                lockedOverlay.SetActive(true);
                buyButton.SetActive(false);
            }
            else
            {
                lockedOverlay.SetActive(false);
                buyButton.SetActive(true);
            }
        }
        name = thisLicense.name;
        cost = thisLicense.cost;
        thisLicense.thisImage = thisImage;
        SpriteCheck();
    }
    void Start()
    {
        SetValues();
        LicenseUnlock();
    }
    private void Update()
    {
        LicenseUnlock();
        GenericDisplayText<string>.DisplayText(nameText, name);
        GenericDisplayText<float>.DisplayTextWithExtra(moneyText, cost, 0);
    }

    public void Next()
    {
        foreach (GameObject item in imagesToDelete)
        {
            item.SetActive(false);
        }
        nextImage.SetActive(true);
        nextImage.GetComponent<LicenseShop>().SetValues();
        thisObject.SetActive(false);
    }

    public void Previous()
    {
        foreach (GameObject item in imagesToDelete)
        {
            item.SetActive(false);
        }
        previousImage.SetActive(true);
        previousImage.GetComponent<LicenseShop>().SetValues();
        thisObject.SetActive(false);
    }
    public void BuyLicense()
    {
        if (thisLicense.isBought == true)
        {
            dialogueBox.SetActive(true);
            Statics.ReadRejection2();
        }
        else if (thisLicense.isLocked == true)
        {
            dialogueBox.SetActive(true);
            Statics.ReadRejection3();
        }
        else if (Statics.money >= cost && thisLicense.isBought == false && thisLicense.isLocked == false)
        {
            //play bought sound effect
            thisLicense.Unlock();
            Statics.money -= cost;
            thisLicense.isBought = true;
        }
        else if (Statics.money < cost)
        {
            dialogueBox.SetActive(true);
            Statics.ReadRejection1();
        }
    }
}
