using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private Animator popUpAnimator;
    [SerializeField] private TMPro.TMP_Text dialogueText;
  
    public void PlayAnimation()
    {
        if (popUpAnimator.GetBool("IsOpen") == true)
        {
            popUpAnimator.SetBool("IsOpen", false);
            dialogueText.gameObject.SetActive(false);
        }
        else if (popUpAnimator.GetBool("IsOpen") == false)
        {
            popUpAnimator.SetBool("IsOpen", true);
            dialogueText.gameObject.SetActive(true);
        }
    }
}
