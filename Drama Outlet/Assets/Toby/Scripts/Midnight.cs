using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Midnight : MonoBehaviour
{
    public License tragedyLicense;
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
    public TimeOfDay day;
    public void OnEnable()
    {
        List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
        if (npcs == null || npcs.Count == 0)
        {
            Debug.Log("No NPCs");
        }
        else
        {
            foreach (var npc in npcs)
            {
                Destroy(npc.gameObject);
            }
        }
        if (SaveDataController.Instance.CurrentData.approvalValue < 60 && hasStartedStealing == false)
        {
            List<string> shuffledNotes = possibleWarningNotes.Shuffle().ToList();
            GenericDisplayText<string>.DisplayText(noteText, shuffledNotes[0]);
            List<string> shuffledWarnings = warningTexts.Shuffle().ToList();
            GenericDisplayText<string>.DisplayText(warningText, shuffledWarnings[0]);
        }
        if (SaveDataController.Instance.CurrentData.approvalValue >= 60)
        {
            hasStartedStealing = true;
            warningText.gameObject.SetActive(false);
            List<string> shuffledNotes = possibleStolenNotes.Shuffle().ToList();
            GenericDisplayText<string>.DisplayText(noteText, shuffledNotes[1]);
            int chance = Statics.RollADice(4);
            for (float i = SaveDataController.Instance.CurrentData.securityValue; i <= 0; i = i - 10)
            {
                chance += (int)0.5;
            }
            if (chance > 20)
            {
                approvalLostText.SetActive(false);
                moneyLossText.SetActive(false);
                productLostText.SetActive(false);
                safeText.SetActive(true);
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
                SaveDataController.Instance.CurrentData.approvalValue -= 10;
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
                    moneyLost = SaveDataController.Instance.CurrentData.money * 0.25f;
                    SaveDataController.Instance.CurrentData.money -= moneyLost;
                }
                else if (chance2 == 2)
                {
                    moneyLost = SaveDataController.Instance.CurrentData.money * 0.25f;
                    SaveDataController.Instance.CurrentData.money -= moneyLost;
                }
                else if (chance2 == 3)
                {
                    moneyLost = SaveDataController.Instance.CurrentData.money * 0.5f;
                    SaveDataController.Instance.CurrentData.money -= moneyLost;
                }
                else if (chance2 == 4)
                {
                    moneyLost = SaveDataController.Instance.CurrentData.money * 0.75f;
                    SaveDataController.Instance.CurrentData.money -= moneyLost;
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
        if (SaveDataController.Instance.CurrentData.approvalValue >= 100 && hasStartedStealing == true)
        {
            hasStartedStealing = false;
            happyText.SetActive(true);
            approvalLostText.SetActive(false);
            moneyLossText.SetActive(false);
            productLostText.SetActive(false);
            safeText.SetActive(false);
            if (tragedyLicense.isBought == true)
            {
                SaveDataController.Instance.CurrentData.money += 4000;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        SaveDataController.Instance.CurrentData.day += 1;
        SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.EarlyMorning;
        day.spaceBarMorning.enabled = true;
        day.timer = 0;
        day.rotation = 90;
        stolenNote.SetActive(true);
    }
}
