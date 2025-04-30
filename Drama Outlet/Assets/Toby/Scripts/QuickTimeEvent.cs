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
        for (int i = 0; i > 4; ++i)
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
            //else if () 
        }
    }
}
