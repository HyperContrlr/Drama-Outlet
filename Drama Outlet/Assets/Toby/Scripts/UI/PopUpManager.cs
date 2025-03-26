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

    [SerializeField] private GameObject dialogueBox;
    
    [SerializeField] private GameObject inventory;
    
    [SerializeField] private GameObject licensesMenu;

    [SerializeField] private GameObject shopMenu;

    [SerializeField] private bool isOn;

    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private Sprite defaultStar;
    
    [SerializeField] private Sprite gainedStar;

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
            isOn= true;
        }
    }
    public void PlayAnimation()
    {
        if (popUpAnimator.GetBool("IsOpen") == true)
        {
            dialogueBox.SetActive(false);
            popUpAnimator.SetBool("IsOpen", false);
        }
        else if (popUpAnimator.GetBool("IsOpen") == false)
        {
            popUpAnimator.SetBool("IsOpen", true);
        }
    }
    
    public void UnlockLicense()
    {
        foreach (License license in starLockedLicense)
        {
            if (Statics.starsGained >= license.StarsForUnlock)
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
        if (Statics.approvalValue <= 0)
        {
            //Big Sad
            if (Statics.approvalValue <= -20)
            {
                Statics.starsGained = 0;
                approvalMask.sprite = approvalMaskSprites[0];
            }
            //Worried
            if (Statics.approvalValue > -20 && Statics.approvalValue < 0)
            {
                Statics.starsGained = 0;
                approvalMask.sprite = approvalMaskSprites[1];
            }
            //Neutral
            if (Statics.approvalValue == 0)
            {
                Statics.starsGained = 0;
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
        else if (Statics.approvalValue > 0)
        {
            {
                int i = 0;
                //Small Smirk
                if (Statics.approvalValue >= 20)
                {
                    Statics.starsGained = 1;
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
                if (Statics.approvalValue >= 40)
                {
                    Statics.starsGained = 2;
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
                             star.GetComponent<Image>().sprite= defaultStar;
                             star.GetComponent<Image>().color = defaultColor;
                        }
                    }
                    approvalMask.sprite = approvalMaskSprites[4];
                }
                //Smile
                if (Statics.approvalValue >= 60)
                {
                    Statics.starsGained = 3;
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
                if (Statics.approvalValue >= 80)
                {
                    Statics.starsGained = 4;
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
                if (Statics.approvalValue >= 100)
                {
                    Statics.starsGained = 5;
                    UnlockLicense();
                    stars[4].GetComponent<Image>().sprite = gainedStar;
                    stars[4].GetComponent<Image>().color = gainedColor;
                    approvalMask.sprite = approvalMaskSprites[7];
                }
            }
        }
        //Security
        if (Statics.securityValue <= 0)
        {
            //Big Sad
            if (Statics.securityValue < -20)
            {
                securityMask.sprite = securityMaskSprites[0];
            }
            //Worried
            if (Statics.securityValue > -20 && Statics.securityValue < 0)
            {
                securityMask.sprite = securityMaskSprites[1];
            }
            //Neutral
            if (Statics.securityValue == 0)
            {
                securityMask.sprite = securityMaskSprites[2];
            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Image>().sprite = defaultStar;
            }
        }
        else if (Statics.securityValue > 0)
        {
            {
                //Small Smirk
                if (Statics.securityValue >= 20)
                {
                    securityMask.sprite = securityMaskSprites[3];
                }
                //Grin
                if (Statics.securityValue >= 40)
                {
                    securityMask.sprite = securityMaskSprites[4];
                }
                //Smile
                if (Statics.securityValue >= 60)
                {
                    securityMask.sprite = securityMaskSprites[5];
                }
                //Smile w/ Teeth
                if (Statics.securityValue >= 80)
                {
                    securityMask.sprite = securityMaskSprites[6];
                }
                //Extreme Happy Face
                if (Statics.securityValue >= 100)
                {
                    securityMask.sprite = securityMaskSprites[7];
                }
            }
        }
    }
    public void Update()
    {
        GenericDisplayText<float>.DisplayTextWithExtra(moneyText, Statics.money, 0);
        GenericDisplayText<float>.DisplayTextWithExtra(dayText, Statics.day, 1);
        MaskAndStarCheck();
    }
    
    [ContextMenu("Change Approval")]
    public void ChangeApproval()
    {
        Statics.approvalValue += 20;
    }

    [ContextMenu("Change Approval Negative")]
    public void ChangeApprovalNegative()
    {
        Statics.approvalValue -= 10;
    }
}
