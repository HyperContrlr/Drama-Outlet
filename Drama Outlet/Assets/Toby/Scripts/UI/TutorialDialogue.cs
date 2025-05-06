using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    [TextArea(3, 10)]
    public List<string> tutorialDialogues;
    public List<GameObject> gameObjectsToDeactivate;
    public List<GameObject> arrows;
    public List<Hover> hovers;
    public License maskLicense;
    public Image tutorialImage;
    public bool startedDialogue;
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject currentArrow;
    public TimeOfDay time;
    void Start()
    {
        
    }

    public void OnEnable()
    {
        if (SaveDataController.Instance.CurrentData.isNewGame == true)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            NoTutorial();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startedDialogue == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Next();
            }
        }
    }

    public void StartDialogue()
    {
        SaveDataController.Instance.CurrentData.isNewGame = false;
        time.enabled = false;
        textMeshProUGUI.fontSize = 30;
        startedDialogue = true;
        tutorialImage.enabled = false;
        maskLicense.isBought = false;
        foreach (GameObject go in gameObjectsToDeactivate)
        {
            go.SetActive(false);
        }
        foreach (var hov in hovers)
        {
            hov.enabled = false;
        }
        Statics.ReadStatement(tutorialDialogues[0]);
        tutorialDialogues.Remove(tutorialDialogues[0]);
    }

    public void NoTutorial()
    {
        foreach (GameObject go in arrows)
        {
            Destroy(go);
        }
        foreach (GameObject go in gameObjectsToDeactivate)
        {
            go.SetActive(true);
        }
        SaveDataController.Instance.CurrentData.isNewGame = false;
        this.gameObject.SetActive(false);
    }

    public void Next()
    {
        Destroy(currentArrow);
        if (tutorialDialogues.Count == 0)
        {
            foreach (GameObject go in gameObjectsToDeactivate)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in arrows)
            {
                Destroy(go);
            }
            foreach (var hov in hovers)
            {
                hov.enabled = true;
            }
            time.enabled = true;
            textMeshProUGUI.fontSize = 35;
            Destroy(this.gameObject);
        }
        else
        {
            Statics.ReadStatement(tutorialDialogues[0]);
            tutorialDialogues.Remove(tutorialDialogues[0]);
        }
        if (arrows.Count > 0)
        {
            arrows[0].SetActive(true);
            currentArrow = arrows[0];
            arrows.Remove(arrows[0]);
        }
    }
}
