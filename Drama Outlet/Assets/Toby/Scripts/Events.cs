using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public struct EventPeice
    {
        [TextArea(3, 10)]
        public List<string> eventTexts;
        public UnityEvent eventAction;
        public UnityEvent eventOver;
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

    public Dictionary<int, EventPeice> eventDictionary;

    public bool eventGoingOn;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfDay.startedDay == true)
        {
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
    public void SetText(int choice, TextMeshProUGUI text)
    {
        if (choice == 0)
        {
            GenericDisplayText<string>.DisplayText(text, currentEvent.eventTexts[0]);
        }
        if (choice == 1)
        {
            GenericDisplayText<string>.DisplayText(text, currentEvent.eventTexts[1]);
        }
        if (choice == 2)
        {
            GenericDisplayText<string>.DisplayText(text, currentEvent.eventTexts[2]);
        }
        if (choice == 3)
        {
            GenericDisplayText<string>.DisplayText(text, currentEvent.cost);
        }
    }
    public void SetEvents()
    {
        if (Statics.day <= 5)
        {
            timeBetweenEvents = 30f;
            eventsInTheDay = Statics.randyTheRandom.Next(1, 4);
        }
        else if (Statics.day > 5 && Statics.day < 11)
        {
            timeBetweenEvents = 20f;
            eventsInTheDay = Statics.randyTheRandom.Next(2, 6);
        }
        else if (Statics.day >= 20)
        {
            timeBetweenEvents = 10f;
            eventsInTheDay = Statics.randyTheRandom.Next(3, 8);
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
    public void AffectMoney(float amount, int choice)
    {
        if (choice == 0)
        {
            Statics.money += amount;
        }
        if (choice == 1)
        {
            Statics.money -= amount;
        }
    }
    public void AffectApproval(float amount, int choice)
    {
        if (choice == 0)
        {
            Statics.approvalValue += amount;
        }
        if (choice == 1)
        {
            Statics.approvalValue -= amount;
        }
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
