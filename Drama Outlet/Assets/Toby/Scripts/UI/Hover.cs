using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Hover : MonoBehaviour
{
    [SerializeField] private ComedyDialogue dialogue;
    
    [SerializeField] private Animator dialogueAnimator;

    [SerializeField] private bool limitedTimes;

    [SerializeField] private int showThrice;

    [SerializeField] public int timesToShow = 3;

    [SerializeField] private bool hasBeenShown;

    [SerializeField] private string description;
    public float timer;
    public bool isOver;
    public void OnMouseEnter()
    {
        isOver = true;
    }

    public void OnMouseExit()
    {
        isOver = false;
        timer = 0;
    }
    public void OnHoverLicense(License license)
    {
        if (license.isLocked == true || license.starLock == true)
        {
            dialogueAnimator.SetBool("IsOpen", true);
            Statics.ReadRejection3();
        }
        else
        {
            dialogueAnimator.SetBool("IsOpen", true);
            dialogue.ReadDescription(license.description);
        }
    }
    public void OnHoverProduct(products product)
    {
        if (limitedTimes == true)
        {
            if (hasBeenShown == false && showThrice <= timesToShow)
            {
                dialogueAnimator.SetBool("IsOpen", true);
                dialogue.ReadDescription(product.description);
                showThrice++;
            }
            if (showThrice == timesToShow)
            {
                hasBeenShown = true;
            }
        }
        else
        {
            dialogueAnimator.SetBool("IsOpen", true);
            dialogue.ReadDescription(product.description);
        }
    }

    public void OnHoverItem(buildings item)
    {
        if (limitedTimes == true)
        {
            if (hasBeenShown == false && showThrice <= timesToShow)
            {
                dialogueAnimator.SetBool("IsOpen", true);
                dialogue.ReadDescription(item.description);
                showThrice++;
            }
            if (showThrice == timesToShow)
            {
                hasBeenShown = true;
            }
        }
        else
        {
            if (item.isLocked == true)
            {
                dialogueAnimator.SetBool("IsOpen", true);
                Statics.ReadRejection3();
            }
            else
            {
                dialogueAnimator.SetBool("IsOpen", true);
                dialogue.ReadDescription(item.description);
            }
        }
    }
    public void OnHoverGeneric(string description)
    {
        if (limitedTimes == true)
        {
            if (hasBeenShown == false && showThrice <= timesToShow)
            {
                dialogueAnimator.SetBool("IsOpen", true);
                dialogue.ReadDescription(description);
                showThrice++;
            }
            if (showThrice == timesToShow)
            {
                hasBeenShown = true;
            }
        }
        else
        {
            dialogueAnimator.SetBool("IsOpen", true);
            dialogue.ReadDescription(description);
        }
    }

    public void Update()
    {
        if (isOver == true)
        {
            timer += Time.deltaTime;
        }
        else if (isOver == false)
        {
            timer = 0;
        }
    }

    public void SetDescription(string descrption_)
    {
        description = descrption_;
    }

    public void OnHoverInventory(InventoryItem item)
    {
        dialogue.ReadDescription(item.mysteriousDescription);
    }

    public void OnHoverExit()
    {
        dialogueAnimator.SetBool("IsOpen", false);
    }

}

