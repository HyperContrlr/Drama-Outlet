using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour
{
    [SerializeField] private buildings thisItem;
    [SerializeField] private InventoryItem unlockedItem;
    [SerializeField] private string name;
    [SerializeField] private float cost;
    [SerializeField] private GameObject lockedOverlay;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Image thisImage;
    public void ItemUnlock()
    {
        if (thisItem.hasProduct == false && SaveDataController.Instance.CurrentData.approvalValue >= thisItem.ratingUnlock)
        {
            thisItem.isLocked = false;
        }
        else if (thisItem.hasProduct == true && thisItem.soldProduct.isLocked == false)
        {
            thisItem.isLocked = false;
        }
        else
        {
            thisItem.isLocked = true;
        }

        if (thisItem.isLocked == true)
        {
            thisImage.color = Color.black;
            lockedOverlay.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            thisImage.color = Color.white;
            lockedOverlay.SetActive(false);
            buyButton.SetActive(true);
        }
    }
    public void SetValues()
    {
        name = thisItem.name;
        cost = thisItem.cost;
    }

    public void BuyItem()
    {
        if (thisItem.isLocked == false && SaveDataController.Instance.CurrentData.money >= cost)
        {
            SaveDataController.Instance.CurrentData.money -= cost;
            unlockedItem.AddToStock();
        }
        else if (SaveDataController.Instance.CurrentData.money < cost)
        {
            Statics.ReadRejection1();
        }
    }
    void Start()
    {
        SetValues();
        ItemUnlock();
    }

    public void AddToSecurity()
    {
        if (thisItem.isSecurity == true)
        {
            SaveDataController.Instance.CurrentData.securityValue += thisItem.securityBonus;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ItemUnlock();
        GenericDisplayText<string>.DisplayText(nameText, name);
        GenericDisplayText<float>.DisplayTextWithExtra(moneyText, cost, 0);
    }
}
