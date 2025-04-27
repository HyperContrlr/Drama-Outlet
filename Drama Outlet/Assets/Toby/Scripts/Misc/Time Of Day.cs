using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Analytics;
using UnityEngine.UI;
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
    [SerializeField] private Image spaceBarMorning;
    [SerializeField] private Image spaceBarNight;
    [SerializeField] private bool checkOut;
    void Start()
    {
        Statics.timeOfDay = Statics.Time.EarlyMorning;
    }

    public void CloseAnimator()
    {
        FindFirstObjectByType<ComedyDialogue>().EndDialogue();
    }
    void Update()
    {
        checkOut = IsCheckOut();
        if (Statics.timeOfDay == Statics.Time.EarlyMorning)
        {
            spaceBarMorning.gameObject.SetActive(true);
            spaceBarNight.gameObject.SetActive(false);
        }
            if (Input.GetKey(KeyCode.Space) == true && startedDay == true)
        {
            pressedDown = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true && Statics.timeOfDay == Statics.Time.EarlyMorning && checkOut == true)
        {
            spaceBarMorning.gameObject.SetActive(false);
            startedDay = true;
            npcSpawner.enabled = true;
            npcSpawner.SpawnCustomer();
        }
        else if (checkOut == false && Statics.timeOfDay == Statics.Time.EarlyMorning && Input.GetKeyDown(KeyCode.Space))
        {
            Statics.ReadStatement("You need a checkout to open the store!! Use the one in your inventory");
            Invoke("CloseAnimator", 3); 
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
            if (IsCheckOut() == false)
            {
                Statics.ReadStatement("We don't have a checkout so I must close the store until we get one :(");
                npcSpawner.enabled = false;
            }
            else if (IsCheckOut() == true)
            {
                npcSpawner.enabled = true;
            }
            if (pressedDown == true)
            {
                timer += Time.deltaTime * 2;
                rotation -= Time.deltaTime * 2;
                foreach (var npc in npcSpawner.spawnedNPCs)
                {
                    npc.thisNPC.speed = npc.thisNPC.quickSpeed;
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
            spaceBarNight.gameObject.SetActive(true);
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

    public bool IsCheckOut()
    {
        if (GameObject.FindGameObjectsWithTag("Check Out").Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

