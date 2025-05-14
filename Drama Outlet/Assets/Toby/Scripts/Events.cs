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
        public UnityEvent goodResult;
        public UnityEvent badResult;
        public GameObject spawnSpot;
    }

    public List<EventPeice> allEvents;

    public EventPeice currentEvent;

    public TimeOfDay timeOfDay;

    public float timeToEvent;

    public float offset;

    public int timeBetweenEvents;

    public int eventsInTheDay;

    [SerializeField]
    public Dictionary<int, EventPeice> eventDictionary;

    public bool eventGoingOn;

    public Button button1;
    
    public Button button2;

    public Button moneyButton;

    public TextMeshProUGUI moneyText;

    public bool lightAffected;

    public UnityEvent turnOffButtons;

    public UnityEvent turnOnButtons;
    public void Bool1()
    {
        lightAffected = true;
    }
    public void Bool2()
    {
        lightAffected = false;
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
        eventGoingOn = false;
        currentEvent.eventOver.Invoke();
        turnOffButtons.Invoke();
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
            eventGoingOn = false;
            currentEvent.eventOver.Invoke();
            turnOffButtons.Invoke();
        }
        if (choice == 1)
        {
            eventGoingOn = false;
            currentEvent.eventOver2.Invoke();
            turnOffButtons.Invoke();
        }
        if (choice == 2)
        {
            eventGoingOn = false;
        }
    }
    public void SetEvents()
    {
        offset = 0;
        if (SaveDataController.Instance.CurrentData.day <= 5)
        {
            timeBetweenEvents = 30;
            eventsInTheDay = Statics.randyTheRandom.Next(2, 6);
        }
        else if (SaveDataController.Instance.CurrentData.day > 5 && SaveDataController.Instance.CurrentData.day < 11)
        {
            timeBetweenEvents = 20;
            eventsInTheDay = Statics.randyTheRandom.Next(3, 8);
        }
        else if (SaveDataController.Instance.CurrentData.day >= 11)
        {
            timeBetweenEvents = 10;
            eventsInTheDay = Statics.randyTheRandom.Next(4, 11);
        }
        eventDictionary = new();
        for (int i = timeBetweenEvents; i < timeOfDay.eveningWindow; i += timeBetweenEvents)
        {
            eventDictionary.Add(i, default);
        }
        List<int> keys = new();
        for (int i = 1; i <= eventDictionary.Count; ++i)
        {
            keys.Add(i * timeBetweenEvents);
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
                turnOnButtons.Invoke();
            }
            else if (currentEvent.isPaywall == true)
            {
                UnityEventOn();
                moneyText.text = string.Format($"Pay: ${currentEvent.cost}");
                moneyButton.onClick.AddListener(() => MoneyButton(currentEvent.cost));
                turnOnButtons.Invoke();
            }
            else if (currentEvent.isQuickTime == true)
            {
                UnityEventOn();
            }
            else if (currentEvent.isSpawnedThing == true)
            {
                Instantiate(currentEvent.spawnedPrefab, currentEvent.spawnSpot.transform);
                currentEvent.eventOver.Invoke();
            }
        }
    }
    public void MoneyButton(float cost)
    {
        if (SaveDataController.Instance.CurrentData.money >= cost)
        {
            SaveDataController.Instance.CurrentData.money -= cost;
            UnityEventOver();
        }
        else
        {
            Statics.ReadRejection5();
        }
    }
    public void AffectMoneyNegative(float amount)
    {
        if (SaveDataController.Instance.CurrentData.money < amount)
        {
            SaveDataController.Instance.CurrentData.money = 0;
        }
        else
        {
            SaveDataController.Instance.CurrentData.money -= amount;
        }
    }
    public void AffectMoneyPositive(float amount)
    {
        SaveDataController.Instance.CurrentData.money += amount;
    }
    public void AffectApprovalNegative(float amount)
    {
        SaveDataController.Instance.CurrentData.approvalValue -= amount;
    }
    public void AffectApprovalPositive(float amount)
    {
        SaveDataController.Instance.CurrentData.approvalValue += amount;
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
            if (npcs.Count == 0)
            {
                return;
            }
            npcs = npcs.Shuffle().ToList();
            npcs[0].thisNPC.unhappy = true;
        }
        else if (choice == 1)
        {
            List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
            if (npcs.Count == 0)
            {
                return;
            }
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
            if (npcs.Count == 0)
            {
                return;
            }
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
                npc.SetTarget();
            }
        }
    }

    [ContextMenu("Spawn Event")]
    public void SpawnEvent()
    {
        currentEvent = allEvents[25];
        EventHappens();
    }

}
