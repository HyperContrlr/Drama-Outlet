using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Midnight : MonoBehaviour
{

    public float moneyLost;
    public GameObject stolenNote;
    public TextMeshProUGUI noteText;
    [TextArea(3, 10)]
    public List<string> possibleWarningNotes;
    [TextArea(3, 10)]
    public List<string> possibleStolenNotes;
    //public void OnEnable()
    //{
    //    if (Statics.approvalValue <= 60)
    //    {
    //        List<string> shuffledNotes = possibleWarningNotes.Shuffle().ToList();
    //        GenericDisplayText<string>.DisplayText(noteText, shuffledNotes[0]);
    //    }
    //    if (Statics.approvalValue > 60)
    //    {
    //        List<string> shuffledNotes = possibleStolenNotes.Shuffle().ToList();
    //        GenericDisplayText<string>.DisplayText(noteText, shuffledNotes[1]);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        this.gameObject.SetActive(false);
        //stolenNote.SetActive(true);
    }
}
