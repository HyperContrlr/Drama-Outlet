using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuickTimeEvent : MonoBehaviour
{
    public Events eventSystem;
    public List<TextMeshProUGUI> textList;
    public List<KeyCode> keyList;
    public bool isCountingDown;
    public float countdownTime;
    public float maxCountdownTime;
    public UnityEvent badResult;
    public UnityEvent goodResult;
    public bool firstButton;
    public bool secondButton;
    public bool thirdButton;
    public bool fourthButton;

    public void SetupQuickTime()
    {
        keyList = keyList.Shuffle().ToList();
        for (int i = 0; i > 3; ++i)
        {
            textList[i].text = keyList[i].ToString();
        }
        isCountingDown = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountingDown == true)
        {
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0)
            {
                isCountingDown = false;
                countdownTime = maxCountdownTime;
                badResult.Invoke();
                eventSystem.currentEvent.eventOver.Invoke();
            }
            else if (Input.GetKeyDown(keyList[0]) && firstButton == false)
            {
                firstButton = true;
            }
            else if (Input.GetKeyDown(keyList[1]) && secondButton == false && firstButton == true)
            {
                secondButton = true;
            }
            else if (Input.GetKeyDown(keyList[2]) && thirdButton == false && firstButton == true && secondButton == true)
            {
                thirdButton = true;
            }
            else if (Input.GetKeyDown(keyList[3]) && fourthButton == false && firstButton == true && secondButton == true && thirdButton == true)
            {
                fourthButton = true;
            }
            else if (fourthButton == true)
            {
                isCountingDown = false;
                countdownTime = maxCountdownTime;
                goodResult.Invoke();
                eventSystem.currentEvent.eventOver.Invoke();
            }
            else
            {
                firstButton = false;
                secondButton = false;
                thirdButton = false;
                fourthButton = false;
            }
        }
    }
}
