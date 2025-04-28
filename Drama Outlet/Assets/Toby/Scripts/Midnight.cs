using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Midnight : MonoBehaviour
{

    public float moneyLost;
    public bool hasStartedStealing;
    public GameObject stolenNote;
    public TextMeshProUGUI noteText;
    public GameObject moneyLossText;
    public GameObject productLostText;
    public GameObject approvalLostText;
    public GameObject safeText;
    public TextMeshProUGUI warningText;
    public GameObject happyText;
    [TextArea(3, 10)]
    public List<string> warningTexts;
    [TextArea(3, 10)]
    public List<string> possibleWarningNotes;
    [TextArea(3, 10)]
    public List<string> possibleStolenNotes;

    public void OnEnable()
    {
        if (Statics.approvalValue <= 60 && hasStartedStealing == false)
        {
            List<string> shuffledNotes = possibleWarningNotes.Shuffle().ToList();
            GenericDisplayText<string>.DisplayText(noteText, shuffledNotes[0]);
            List<string> shuffledWarnings = warningTexts.Shuffle().ToList();
            GenericDisplayText<string>.DisplayText(warningText, shuffledWarnings[0]);
        }
        if (Statics.approvalValue > 60)
        {
            hasStartedStealing = true;
            warningText.gameObject.SetActive(false);
            List<string> shuffledNotes = possibleStolenNotes.Shuffle().ToList();
            GenericDisplayText<string>.DisplayText(noteText, shuffledNotes[1]);
            int chance = Statics.RollADice(4);
            for (float i = Statics.securityValue; i <= 0; i = i - 10)
            {
                chance += (int)0.5;
            }
            //Safe
            if (chance <= 20 && chance > 15)
            {
                approvalLostText.SetActive(false);
                moneyLossText.SetActive(false);
                productLostText.SetActive(false);
                safeText.SetActive(true);
            }
            //Aprroval Loss
            else if (chance <= 15 && chance > 10)
            {
                approvalLostText.SetActive(true);
                moneyLossText.SetActive(false);
                productLostText.SetActive(false);
                safeText.SetActive(false);
                Statics.approvalValue -= 10;
            }
            //Money Loss
            else if (chance <= 10 && chance > 5)
            {
                approvalLostText.SetActive(false);
                moneyLossText.SetActive(true);
                productLostText.SetActive(false);
                safeText.SetActive(false);
                int chance2 = Statics.RollADice(0);
                if (chance2 == 1)
                {
                    moneyLost = Statics.money * 0.25f;
                    Statics.money -= moneyLost;
                }
                else if (chance2 == 2)
                {
                    moneyLost = Statics.money * 0.25f;
                    Statics.money -= moneyLost;
                }
                else if (chance2 == 3)
                {
                    moneyLost = Statics.money * 0.5f;
                    Statics.money -= moneyLost;
                }
                else if (chance2 == 4)
                {
                    moneyLost = Statics.money * 0.75f;
                    Statics.money -= moneyLost;
                }
            }
            //Product Loss
            else if (chance <= 5 && chance > 0)
            {
                approvalLostText.SetActive(false);
                moneyLossText.SetActive(false);
                productLostText.SetActive(true);
                safeText.SetActive(false);
                List<ProductManager> products = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
                foreach (var pro in products)
                {
                    int chance3 = Statics.FlipACoin();
                    if (chance == 0)
                    {
                        pro.stock -= pro.thisProduct.maxStock / 2;
                    }
                    else if (chance == 1)
                    {
                        Debug.Log("Failed");
                    }
                }
            }
        }
        if (Statics.approvalValue >= 100 && hasStartedStealing == true)
        {
            hasStartedStealing = false;
            happyText.SetActive(true);
            approvalLostText.SetActive(false);
            moneyLossText.SetActive(false);
            productLostText.SetActive(false);
            safeText.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        Statics.day += 1;
        this.gameObject.SetActive(false);
        stolenNote.SetActive(true);
    }
}
