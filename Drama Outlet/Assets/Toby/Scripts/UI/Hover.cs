using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Hover : MonoBehaviour
{
    [SerializeField] private ComedyDialogue dialogue;
    
    [SerializeField] private Animator dialogueAnimator;

    [SerializeField] private bool onlyShowOnce;

    [SerializeField] private bool hasBeenShown;
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
        dialogueAnimator.SetBool("IsOpen", true);
        dialogue.ReadDescription(item.description);
    }

    public void OnHoverGeneric(string description)
    {
        if (onlyShowOnce == true)
        {
            if (hasBeenShown == false)
            {
                dialogueAnimator.SetBool("IsOpen", true);
                dialogue.ReadDescription(description);
                hasBeenShown = true;
            }
        }
        else
        {
            dialogueAnimator.SetBool("IsOpen", true);
            dialogue.ReadDescription(description);
        }
    }

    public void OnHoverExit()
    {
        dialogueAnimator.SetBool("IsOpen", false);
    }

}

