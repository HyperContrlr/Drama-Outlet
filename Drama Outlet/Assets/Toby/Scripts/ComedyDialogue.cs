using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComedyDialogue : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text dialogueText;

    [SerializeField] private string defaultSentence;

    [SerializeField] private Animator comedyAnimator;

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
        Default(defaultSentence);
    }
    public void ReadDescription(string description)
    {
        sentences.Clear();

        sentences.Enqueue(description);

        DisplayNextSentence();
    }
    public void Default(string defaultSentence)
    {
        sentences.Clear();

        sentences.Enqueue(defaultSentence);

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
        Default(defaultSentence);
    }

}
