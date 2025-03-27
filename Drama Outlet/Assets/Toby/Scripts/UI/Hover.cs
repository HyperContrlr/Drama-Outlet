using Unity.VisualScripting;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private ComedyDialogue dialogue;
    
    [SerializeField] private GameObject dialogueBox;

    public void OnHoverLicense(License license)
    {
        if (license.isLocked == true || license.starLock == true)
        {
            dialogueBox.SetActive(true);
            Statics.ReadRejection3();
        }
        dialogueBox.SetActive(true);
        dialogue.ReadDescription(license.description);
    }
    public void OnHoverProduct(products product)
    {
        dialogueBox.SetActive(true);
        dialogue.ReadDescription(product.description);
    }

    public void OnHoverExit()
    {
        dialogueBox.SetActive(false);
    }

}

