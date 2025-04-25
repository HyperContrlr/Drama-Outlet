using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Hover : MonoBehaviour
{
    [SerializeField] private ComedyDialogue dialogue;
    
    [SerializeField] private Animator dialogueAnimator;

    [SerializeField] private bool limitedTimes;

    [SerializeField] private int showThrice;

    [SerializeField] private bool hasBeenShown;

    [SerializeField] private string description;
    public void OnPointerEnter(BaseEventData eventData)
    {
        OnHoverGeneric(description);
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        OnHoverExit();
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
        dialogueAnimator.SetBool("IsOpen", true);
        dialogue.ReadDescription(product.description);
    }

    public void OnHoverItem(buildings item)
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
    public void OnHoverGeneric(string description)
    {
        if (limitedTimes == true)
        {
            if (hasBeenShown == false && showThrice <= 3)
            {
                dialogueAnimator.SetBool("IsOpen", true);
                dialogue.ReadDescription(description);
                showThrice++;
            }
            if (showThrice == 3)
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

