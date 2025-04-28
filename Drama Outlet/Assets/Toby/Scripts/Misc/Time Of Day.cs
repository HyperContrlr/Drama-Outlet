using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Analytics;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    [SerializeField] public bool pressedDown;
    [SerializeField] private Image spaceBarMorning;
    [SerializeField] private Image spaceBarNight;
    [SerializeField] private bool checkOut;
    [SerializeField] private Image midnight;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private bool isntLost;
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
        Debug.Log(Statics.timeOfDay);
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
            isntLost = false;
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
            spaceBarNight.gameObject.SetActive(false);
            midnight.gameObject.SetActive(true);
            Statics.timeOfDay = Statics.Time.Midnight;
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
                SpeedUp();
            }
            else
            {
                SpeedDown();
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
            if (Statics.approvalValue <= -50 && Statics.money == 0)
            {
                List<ProductManager> products = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
                foreach (var product in products)
                {
                    if (product.stock > 0)
                    {
                        isntLost = true;
                    }
                }
                if (isntLost == false)
                {
                    loseScreen.SetActive(true);
                }
                else
                {
                    Statics.ReadStatement("Our business isn't over yet!! We still have stock so let's be smart with out money.");
                    Invoke("CloseAnimator", 3);
                }
            }
        }
    }
    public void SpeedUp()
    {
        timer += Time.deltaTime * 2;
        rotation -= Time.deltaTime * 2;
        foreach (var npc in npcSpawner.spawnedNPCs)
        {
            npc.thisNPC.speed = npc.thisNPC.quickSpeed;
        }
    }

    public void SpeedDown()
    {
        timer += Time.deltaTime;
        rotation -= Time.deltaTime;
        foreach (var npc in npcSpawner.spawnedNPCs)
        {
            npc.thisNPC.speed = npc.thisNPC.storedSpeed;
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

