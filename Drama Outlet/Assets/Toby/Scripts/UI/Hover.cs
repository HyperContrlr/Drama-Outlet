using Unity.VisualScripting;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private ComedyDialogue dialogue;
    
    [SerializeField] private Animator dialogueAnimator;

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

    public void OnHoverExit()
    {
        dialogueAnimator.SetBool("IsOpen", false);
    }

}

