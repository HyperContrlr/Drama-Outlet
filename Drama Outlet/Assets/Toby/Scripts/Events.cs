using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    [System.Serializable]
    public struct EventPeice
    {
        public TextMeshProUGUI eventDescription;
        public UnityEvent eventAction;
        public UnityEvent eventOver;
        public UnityEvent eventOver2;
        public bool isQuickTime;
        public bool isChoice;
        public bool isPaywall;
        public bool isSpawnedThing;
        public bool hasHappened;
        public GameObject spawnedPrefab;
        public float cost;
    }

    public List<EventPeice> allEvents;

    public EventPeice currentEvent;

    public GameObject open;

    public TimeOfDay timeOfDay;

    public float timeToEvent;

    public float offset;

    public float timeBetweenEvents;

    public int eventsInTheDay;

    [SerializeField]
    public Dictionary<int, EventPeice> eventDictionary;

    public bool eventGoingOn;

    public Button button1;
    
    public Button button2;

    public Button moneyButton;

    public TextMeshProUGUI moneyText;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfDay.startedDay == true)
        {
            if (timeOfDay.pressedDown == true)
            {
                timeToEvent += Time.deltaTime * 2;
            }
            else
                timeToEvent += Time.deltaTime;
            if (timeToEvent >= timeBetweenEvents)
            {
                timeToEvent = 0;
                offset += timeBetweenEvents;
                if (eventDictionary.ContainsKey((int)offset))
                {
                    currentEvent = eventDictionary[(int)offset];
                    EventHappens();
                }
            }
        }
    }
    public void UnityEventOn()
    {
        currentEvent.eventAction.Invoke();
    }
    public void UnityEventOver()
    {
        currentEvent.eventOver.Invoke();
        eventGoingOn = false;
    }

    public void SetButton()
    {
        button1.onClick.AddListener(() => UnityEventChoice(0));
        button2.onClick.AddListener(() => UnityEventChoice(1));
    }
    public void SetText(string statement)
    {
        currentEvent.eventDescription.text = statement;
    }

    public void UnityEventChoice(int choice)
    {
        if (choice == 0)
        {
            currentEvent.eventOver.Invoke();
            eventGoingOn = false;
        }
        if (choice == 1)
        {
            currentEvent.eventOver2.Invoke();
            eventGoingOn = false;
        }
    }
    public void SetEvents()
    {
        offset = 0;
        if (Statics.day <= 5)
        {
            timeBetweenEvents = 30f;
            eventsInTheDay = Statics.randyTheRandom.Next(2, 5);
        }
        else if (Statics.day > 5 && Statics.day < 11)
        {
            timeBetweenEvents = 20f;
            eventsInTheDay = Statics.randyTheRandom.Next(2, 7);
        }
        else if (Statics.day >= 20)
        {
            timeBetweenEvents = 10f;
            eventsInTheDay = Statics.randyTheRandom.Next(3, 9);
        }
        eventDictionary = new();
        for (int i = 0; i < timeOfDay.eveningWindow / timeBetweenEvents; ++i)
        {
            eventDictionary.Add((int)(i * timeBetweenEvents), default);
        }
        List<int> keys = new();
        for (int i = 0; i < eventsInTheDay; ++i)
        {
            keys.Add(i);
        }
        keys = keys.Shuffle().Take(eventsInTheDay).ToList();
        for (int i = 0; i < eventsInTheDay; ++i)
        {
            int randomSlot = keys[i];
            int randomEvent = Statics.randyTheRandom.Next(0, allEvents.Count);
            eventDictionary[randomSlot] = allEvents[randomEvent];
        }
    }

    public void EventHappens()
    {
        if (currentEvent.Equals(default(EventPeice)))
        {
            return;
        }
        if (eventGoingOn == true)
        {
            return;
        }
        else
        {
            eventGoingOn = true;
            if (currentEvent.isChoice == true)
            {
                UnityEventOn();
            }
            else if (currentEvent.isPaywall == true)
            {
                UnityEventOn();
                moneyText.text = string.Format($"Pay: ${currentEvent.cost}");
                moneyButton.onClick.AddListener(() => MoneyButton(currentEvent.cost));
            }
            else if (currentEvent.isQuickTime == true)
            {
                UnityEventOn();
            }
            else if (currentEvent.isSpawnedThing == true)
            {
                Instantiate(currentEvent.spawnedPrefab, open.transform);
                currentEvent.eventOver.Invoke();
            }
        }
    }
    public void MoneyButton(float cost)
    {
        if (Statics.money >= cost)
        {
            Statics.money -= cost;
            UnityEventOver();
        }
        else
        {
            Statics.ReadRejection5();
        }
    }
    public void AffectMoneyNegative(float amount)
    {
        if (Statics.money < amount)
        {
            Statics.money = 0;
        }
        else
        {
            Statics.money -= amount;
        }
    }
    public void AffectMoneyPositive(float amount)
    {
        Statics.money += amount;
    }
    public void AffectApprovalNegative(float amount)
    {
        Statics.approvalValue -= amount;
    }
    public void AffectApprovalPositive(float amount)
    {
        Statics.approvalValue += amount;
    }
    public void LoseStock()
    {
        List<ProductManager> products = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        products = products.Shuffle().ToList();
        products[0].stock = 0;
    }
    public void MakeUnhappy(int choice)
    {
        if (choice == 0)
        {
            List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
            npcs = npcs.Shuffle().ToList();
            npcs[0].thisNPC.unhappy = true;
        }
        else if (choice == 1)
        {
            List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
            foreach (var npc in npcs)
            {
                int option = Statics.FlipACoin();
                if (option == 0)
                {
                    return;
                }
                else
                {
                    npc.thisNPC.unhappy = true;
                }
            }
        }
        else if (choice == 2)
        {
            List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
            foreach (var npc in npcs)
            {
                npc.thisNPC.unhappy = true;
            }
        }
    }
    public void StopNPCs(int choice)
    {
        if (choice == 0)
        {
            List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
            foreach (var npc in npcs)
            {
                npc.isPaused = true;
                npc.previousState = npc.state;
                npc.state = NPCAI.States.Stopped;
            }
        }
        if (choice == 1)
        {
            List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
            foreach (var npc in npcs)
            {
                npc.isPaused = false;
                npc.state = npc.previousState;
            }
        }
    }
}
