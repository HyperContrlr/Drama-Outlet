using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    [SerializeField] private Color defaultStar;
    
    [SerializeField] private Color gainedStar;

    [SerializeField] private List<GameObject> stars;

    [SerializeField] private List<Sprite> approvalMaskSprites;
    [SerializeField] private GameObject approvalMask;

    [SerializeField] private List<Sprite> securityMaskSprites;
    [SerializeField] private GameObject securityMask;

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

    public void MaskAndStarCheck()
    {
        if (Statics.approvalValue <= 0)
        {
            if (Statics.approvalValue == 0)
            {
                approvalMask.GetComponent<SpriteRenderer>().sprite = 
            }
            foreach (GameObject star in stars)
            {
                star.GetComponent<Image>().color = defaultStar;
            }
        }
        else if (Statics.approvalValue > 0)
        {
            {
                if (Statics.approvalValue >= 20)
                {
                    stars[0].GetComponent<Image>().color = gainedStar;
                }
                if (Statics.approvalValue >= 40)
                {
                    stars[1].GetComponent<Image>().color = gainedStar;
                }
                if (Statics.approvalValue >= 60)
                {
                    stars[2].GetComponent<Image>().color = gainedStar;
                }
                if (Statics.approvalValue >= 80)
                {
                    stars[3].GetComponent<Image>().color = gainedStar;
                }
                if (Statics.approvalValue >= 100)
                {
                    stars[4].GetComponent<Image>().color = gainedStar;
                }
            }
        }
    }
    public void Update()
    {
        moneyText.text = string.Format($"${Statics.money}");
        dayText.text = string.Format($"Day #{Statics.day}");
        MaskAndStarCheck();
    }
    
    [ContextMenu("Change Approval")]
    public void ChangeApproval()
    {
        Statics.approvalValue += 20;
    }
}
