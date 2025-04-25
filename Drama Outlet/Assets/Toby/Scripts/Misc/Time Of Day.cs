using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class TimeOfDay : MonoBehaviour
{
    [SerializeField] public float timer;
    [SerializeField] private bool startedDay;
    [SerializeField] private bool closeUpShop;
    [SerializeField] private float morningWindow;
    [SerializeField] private float afternoonWindow;
    [SerializeField] private float eveningWindow;
    [SerializeField] private NPCSpawner npcSpawner;
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] private float rotation;
    [SerializeField] private List<BuildingManager> buildingsPlaced;
    [SerializeField] private bool pressedDown;
    void Start()
    {
        Statics.timeOfDay = Statics.Time.EarlyMorning;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true && startedDay == true)
        {
            pressedDown = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true && Statics.timeOfDay == Statics.Time.EarlyMorning)
        {
            startedDay = true;
            npcSpawner.enabled = true;
            npcSpawner.SpawnCustomer();
        }
      
        if (Input.GetKeyDown(KeyCode.Space) == true && Statics.timeOfDay == Statics.Time.Night)
        {
            closeUpShop = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            pressedDown = false;
        }


        if (startedDay == true)
        {
            if (pressedDown == true)
            {
                timer += Time.deltaTime * 2;
                rotation -= Time.deltaTime * 2;
                foreach (var npc in npcSpawner.spawnedNPCs)
                {
                    npc.thisNPC.speed = npc.thisNPC.speed + 2;
                }
            }
            else
            {
                timer += Time.deltaTime;
                rotation -= Time.deltaTime;
                foreach (var npc in npcSpawner.spawnedNPCs)
                {
                    npc.thisNPC.speed = npc.thisNPC.storedSpeed;
                }
            }
            rotationPoint.transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
        }
       
        if (timer <= morningWindow && startedDay == true)
        {
            Statics.timeOfDay = Statics.Time.Morning;
        }
        
        else if (timer <= afternoonWindow && timer > morningWindow)
        {
            Statics.timeOfDay = Statics.Time.Afternoon;
        }
       
        else if (timer <= eveningWindow && timer > afternoonWindow)
        {
            Statics.timeOfDay = Statics.Time.Evening;
        }
        
        else if (timer > eveningWindow)
        {
            npcSpawner.enabled = false;
            Statics.timeOfDay = Statics.Time.Night;
            if (startedDay == true)
            {
                buildingsPlaced.Clear();
                buildingsPlaced = FindObjectsByType<BuildingManager>(FindObjectsSortMode.None).ToList();
                foreach (var build in buildingsPlaced)
                {
                    Statics.approvalValue += build.thisBuilding.ratingBonus;
                }
            }
            startedDay = false;
        }
    }
}

