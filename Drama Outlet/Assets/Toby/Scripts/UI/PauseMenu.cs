using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public KeyCode pause;
    public bool isOpen;
    public Animator comedyAnimator;
    public ComedyDialogue dialogue;
    public bool turnedOff;
    public Image check;
    public TutorialDialogue tutorial;
    // Update is called once per frame

    public void Start()
    {
        if (SaveDataController.Instance.CurrentData.isNewGame == true)
        {
            tutorial.gameObject.SetActive(true);
        }
        else
        {
            tutorial.gameObject.SetActive(true);
            tutorial.NoTutorial();
        }
    }


    void Update()
    {
        if ((Input.GetKeyDown(pause) || Input.GetKeyDown(KeyCode.Escape)) && isOpen == true)
        {
            Continue();
        }
        else if ((Input.GetKeyDown(pause) || Input.GetKeyDown(KeyCode.Escape)) && isOpen == false)
        {
            Pause();
        }
    }
    public void Pause()
    {
        isOpen = true;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void Continue()
    {
        isOpen = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Screen");
    }

    public void TurnOffComedy()
    {
        if (turnedOff == false)
        {
            comedyAnimator.gameObject.SetActive(false);
            comedyAnimator.enabled = false;
            dialogue.enabled = false;
            turnedOff = true;
            check.enabled = true;
            return;
        }
        else 
        {
            comedyAnimator.gameObject.SetActive(true);
            comedyAnimator.enabled = true;
            dialogue.enabled = true;
            turnedOff = false;
            check.enabled = false;
        }
    }
}
