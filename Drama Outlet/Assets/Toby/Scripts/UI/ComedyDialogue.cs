using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComedyDialogue : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text dialogueText;

    [SerializeField] private Animator comedyAnimator;

    [SerializeField] private GameObject dialogueBox;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void XButton()
    {
        dialogueBox.SetActive(false);
    }
    public void ReadDescription(string description)
    {
        dialogueBox.SetActive(true);
        sentences.Clear();

        sentences.Enqueue(description);

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        comedyAnimator.SetBool("IsOpen", false);
    }
}
