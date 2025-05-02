using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private Animator popUpAnimator;

    [SerializeField] private GameObject inventory;

    [SerializeField] private GameObject licensesMenu;

    [SerializeField] private GameObject shopMenu;

    [SerializeField] private bool isOn;

    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private Sprite defaultStar;

    [SerializeField] private Sprite gainedStar;

    [SerializeField] private Image openButton;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    [SerializeField] private Color defaultColor;

    [SerializeField] private Color gainedColor;

    [SerializeField] private List<GameObject> stars;

    [SerializeField] private List<Sprite> approvalMaskSprites;
    [SerializeField] private Image approvalMask;

    [SerializeField] private List<Sprite> securityMaskSprites;
    [SerializeField] private Image securityMask;

    [SerializeField] private List<License> starLockedLicense;

    public void OpenOrCloseMenu(GameObject menu)
    {
        if (isOn == true)
        {
            menu.SetActive(false);
            isOn = false;
        }
        else if (isOn == false)
        {
            menu.SetActive(true);
            isOn = true;
        }
    }
    public void PlayAnimation()
    {
        if (popUpAnimator.GetBool("IsOpen") == true)
        {
            popUpAnimator.SetBool("IsOpen", false);
            openButton.sprite = openSprite;
        }
        else if (popUpAnimator.GetBool("IsOpen") == false)
        {
            popUpAnimator.SetBool("IsOpen", true);
            openButton.sprite = closeSprite;
        }
    }

    public void UnlockLicense()
    {
        foreach (License license in starLockedLicense)
        {
            if (SaveDataController.Instance.CurrentData.starsGained >= license.StarsForUnlock)
            {
                license.starLock = false;
            }
            else
            {
                if (license.starLock == false)
                {
                    license.starLock = false;
                }
                else
                {
                    license.starLock = true;
                }
            }
        }
    }
    public void MaskAndStarCheck()
    {
        //Approval 
        if (SaveDataController.Instance.CurrentData.approvalValue <= 0)
        {
            //Big Sad
            if (SaveDataController.Instance.CurrentData.approvalValue <= -20)
            {
                SaveDataController.Instance.CurrentData.starsGained = 0;
                approvalMask.sprite = approvalMaskSprites[0];
            }
            //Worried
            if (SaveDataController.Instance.CurrentData.approvalValue > -20 && SaveDataController.Instance.CurrentData.approvalValue < 0)
            {
                SaveDataController.Instance.CurrentData.starsGained = 0;
                approvalMask.sprite = approvalMaskSprites[1];
            }
            //Neutral
            if (SaveDataController.Instance.CurrentData.approvalValue == 0)
            {
                SaveDataController.Instance.CurrentData.starsGained = 0;
                approvalMask.sprite = approvalMaskSprites[2];
                stars[0].GetComponent<Image>().sprite = defaultStar;
                stars[0].GetComponent<Image>().color = defaultColor;
            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Image>().sprite = defaultStar;
                star.GetComponent<Image>().color = defaultColor;
            }
        }
        else if (SaveDataController.Instance.CurrentData.approvalValue > 0)
        {
            {
                int i = 0;
                //Small Smirk
                if (SaveDataController.Instance.CurrentData.approvalValue >= 20 && SaveDataController.Instance.CurrentData.approvalValue < 40)
                {
                    SaveDataController.Instance.CurrentData.starsGained = 1;
                    UnlockLicense();
                    i = 0;
                    foreach (GameObject star in stars)
                    {
                        if (i == 0)
                        {
                            star.GetComponent<Image>().sprite = gainedStar;
                            star.GetComponent<Image>().color = gainedColor;
                            i++;
                        }
                        else
                        {
                            star.GetComponent<Image>().sprite = defaultStar;
                            star.GetComponent<Image>().color = defaultColor;
                        }
                    }
                    approvalMask.sprite = approvalMaskSprites[3];
                }
                //Grin
                if (SaveDataController.Instance.CurrentData.approvalValue >= 40 && SaveDataController.Instance.CurrentData.approvalValue < 60)
                {
                    SaveDataController.Instance.CurrentData.starsGained = 2;
                    UnlockLicense();
                    i = 0;
                    foreach (GameObject star in stars)
                    {
                        if (i <= 1)
                        {
                            star.GetComponent<Image>().sprite = gainedStar;
                            star.GetComponent<Image>().color = gainedColor;
                            i++;
                        }
                        else
                        {
                            star.GetComponent<Image>().sprite = defaultStar;
                            star.GetComponent<Image>().color = defaultColor;
                        }
                    }
                    approvalMask.sprite = approvalMaskSprites[4];
                }
                //Smile
                if (SaveDataController.Instance.CurrentData.approvalValue >= 60 && SaveDataController.Instance.CurrentData.approvalValue < 80)
                {
                    SaveDataController.Instance.CurrentData.starsGained = 3;
                    UnlockLicense();
                    i = 0;
                    foreach (GameObject star in stars)
                    {
                        if (i <= 2)
                        {
                            star.GetComponent<Image>().sprite = gainedStar;
                            star.GetComponent<Image>().color = gainedColor;
                            i++;
                        }
                        else
                        {
                            star.GetComponent<Image>().sprite = defaultStar;
                            star.GetComponent<Image>().color = defaultColor;

                        }
                    }
                    approvalMask.sprite = approvalMaskSprites[5];
                }
                //Smile w/ Teeth
                if (SaveDataController.Instance.CurrentData.approvalValue >= 80 && SaveDataController.Instance.CurrentData.approvalValue < 100)
                {
                    SaveDataController.Instance.CurrentData.starsGained = 4;
                    UnlockLicense();
                    i = 0;
                    foreach (GameObject star in stars)
                    {
                        if (i <= 3)
                        {
                            star.GetComponent<Image>().sprite = gainedStar;
                            star.GetComponent<Image>().color = gainedColor;
                            i++;
                        }
                        else
                        {
                            star.GetComponent<Image>().sprite = defaultStar;
                            star.GetComponent<Image>().color = defaultColor;
                        }
                    }
                    approvalMask.sprite = approvalMaskSprites[6];
                }
                //Extreme Happy Face
                if (SaveDataController.Instance.CurrentData.approvalValue >= 100)
                {
                    SaveDataController.Instance.CurrentData.starsGained = 5;
                    UnlockLicense();
                    stars[4].GetComponent<Image>().sprite = gainedStar;
                    stars[4].GetComponent<Image>().color = gainedColor;
                    approvalMask.sprite = approvalMaskSprites[7];
                }
            }
        }
        //Security
        if (SaveDataController.Instance.CurrentData.securityValue <= 0)
        {
            //Big Sad
            if (SaveDataController.Instance.CurrentData.securityValue < -50)
            {
                securityMask.sprite = securityMaskSprites[0];
            }
            //Worried
            if (SaveDataController.Instance.CurrentData.securityValue > -20 && SaveDataController.Instance.CurrentData.securityValue < 0)
            {
                securityMask.sprite = securityMaskSprites[1];
            }
            //Neutral
            if (SaveDataController.Instance.CurrentData.securityValue == 0)
            {
                securityMask.sprite = securityMaskSprites[2];
            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Image>().sprite = defaultStar;
            }
        }
        else if (SaveDataController.Instance.CurrentData.securityValue > 0)
        {
            {
                //Small Smirk
                if (SaveDataController.Instance.CurrentData.securityValue >= 20)
                {
                    securityMask.sprite = securityMaskSprites[3];
                }
                //Grin
                if (SaveDataController.Instance.CurrentData.securityValue >= 40)
                {
                    securityMask.sprite = securityMaskSprites[4];
                }
                //Smile
                if (SaveDataController.Instance.CurrentData.securityValue >= 60)
                {
                    securityMask.sprite = securityMaskSprites[5];
                }
                //Smile w/ Teeth
                if (SaveDataController.Instance.CurrentData.securityValue >= 80)
                {
                    securityMask.sprite = securityMaskSprites[6];
                }
                //Extreme Happy Face
                if (SaveDataController.Instance.CurrentData.securityValue >= 100)
                {
                    securityMask.sprite = securityMaskSprites[7];
                }
            }
        }
    }
    public void Update()
    {
        GenericDisplayText<float>.DisplayTextWithExtra(moneyText, SaveDataController.Instance.CurrentData.money, 0);
        GenericDisplayText<float>.DisplayTextWithExtra(dayText, SaveDataController.Instance.CurrentData.day, 1);
        MaskAndStarCheck();
        UnlockLicense();
    }
    
    [ContextMenu("Change Approval")]
    public void ChangeApproval()
    {
        SaveDataController.Instance.CurrentData.approvalValue += 20;
        Debug.Log(SaveDataController.Instance.CurrentData.approvalValue);
    }

    [ContextMenu("Change Approval Negative")]
    public void ChangeApprovalNegative()
    {
        SaveDataController.Instance.CurrentData.approvalValue -= 10;
        Debug.Log(SaveDataController.Instance.CurrentData.approvalValue);
    }
}