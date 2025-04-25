using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComedyDialogue : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text dialogueText;

    [SerializeField] public Animator comedyAnimator;

    [SerializeField] private GameObject dialogueBox;

    private Queue<string> sentences;

    [SerializeField] public float textSpeed;

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
        comedyAnimator.SetBool("IsOpen", true);
        if (sentences != null)
        {
            sentences.Clear();
        }
        else
        {
            sentences = new Queue<string>();
        }

        sentences.Enqueue(description);

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        //if (sentences.Count == 0)
        //{
        //    //EndDialogue();
        //    //return;
        //}

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = sentence;
        dialogueText.ForceMeshUpdate();
        yield return null;
        //yield return new WaitForSeconds(0.5f);
        //foreach (char letter in sentence.ToCharArray())
        //{
        //    dialogueText.text += letter;
        //    yield return new WaitForSeconds(textSpeed);
        //    yield return null;
        //}
    }
    public void EndDialogue()
    {
        comedyAnimator.SetBool("IsOpen", false);
    }
}
