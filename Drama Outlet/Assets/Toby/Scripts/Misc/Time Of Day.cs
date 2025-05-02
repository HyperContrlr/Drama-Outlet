using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Analytics;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
public class TimeOfDay : MonoBehaviour
{
    [SerializeField] public float timer;
    [SerializeField] public bool startedDay;
    [SerializeField] public bool closeUpShop;
    [SerializeField] public float morningWindow;
    [SerializeField] public float afternoonWindow;
    [SerializeField] public float eveningWindow;
    [SerializeField] private NPCSpawner npcSpawner;
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] public float rotation;
    [SerializeField] private List<BuildingManager> buildingsPlaced;
    [SerializeField] public bool pressedDown;
    [SerializeField] private Image spaceBarMorning;
    [SerializeField] private Image spaceBarNight;
    [SerializeField] private bool checkOut;
    [SerializeField] private Image midnight;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private bool isntLost;
    [SerializeField] private bool changedOnce;
    public Light2D mainLight;

    public Events eventSystem;
    void Start()
    {
        SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.EarlyMorning;
    }

    public void CloseAnimator()
    {
        FindFirstObjectByType<ComedyDialogue>().EndDialogue();
    }
    void Update()
    {
        Debug.Log(SaveDataController.Instance.CurrentData.timeOfDay);
        checkOut = IsCheckOut();
        if (SaveDataController.Instance.CurrentData.timeOfDay == SaveData.Time.EarlyMorning)
        {
            spaceBarMorning.gameObject.SetActive(true);
            spaceBarNight.gameObject.SetActive(false);
            mainLight.intensity = 0.5f;
        }
        
        if (Input.GetKey(KeyCode.Space) == true && startedDay == true)
        {
            pressedDown = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && SaveDataController.Instance.CurrentData.timeOfDay == SaveData.Time.EarlyMorning && checkOut == true)
        {
            eventSystem.SetEvents();
            isntLost = false;
            spaceBarMorning.gameObject.SetActive(false);
            startedDay = true;
            npcSpawner.enabled = true;
            npcSpawner.SpawnCustomer();
        }

        else if (checkOut == false && SaveDataController.Instance.CurrentData.timeOfDay == SaveData.Time.EarlyMorning && Input.GetKeyDown(KeyCode.Space))
        {
            Statics.ReadStatement("You need a checkout to open the store!! Use the one in your inventory");
            Invoke("CloseAnimator", 3); 
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && SaveDataController.Instance.CurrentData.timeOfDay == SaveData.Time.Night)
        {
            closeUpShop = true;
            spaceBarNight.gameObject.SetActive(false);
            midnight.gameObject.SetActive(true);
            SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.Midnight;
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
            SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.Morning;
            if (eventSystem.lightAffected == false)
            mainLight.intensity = 0.6f;
        }
        
        else if (timer <= afternoonWindow && timer > morningWindow)
        {
            SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.Afternoon;
            if (eventSystem.lightAffected == false)
                mainLight.intensity = 0.8f;
        }
       
        else if (timer <= eveningWindow && timer > afternoonWindow)
        {
            SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.Evening;
            if (eventSystem.lightAffected == false)
                mainLight.intensity = 0.6f;
        }
        
        else if (timer > eveningWindow)
        {
            spaceBarNight.gameObject.SetActive(true);
            npcSpawner.enabled = false;
            SaveDataController.Instance.CurrentData.timeOfDay = SaveData.Time.Night;
            mainLight.intensity = 0.4f;
            if (startedDay == true)
            {
                buildingsPlaced.Clear();
                buildingsPlaced = FindObjectsByType<BuildingManager>(FindObjectsSortMode.None).ToList();
                foreach (var build in buildingsPlaced)
                {
                    SaveDataController.Instance.CurrentData.approvalValue += build.thisBuilding.ratingBonus;
                }
                List<NPCAI> npcs = FindObjectsByType<NPCAI>(FindObjectsSortMode.None).ToList();
                foreach (var npc in npcs)
                {
                    SaveDataController.Instance.CurrentData.money += npc.thisNPC.money;
                    npc.state = NPCAI.States.Leaving;
                    npc.target = npc.leave;
                }
            }
            startedDay = false;
            if (SaveDataController.Instance.CurrentData.approvalValue <= -50 && SaveDataController.Instance.CurrentData.money == 0)
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
            npc.gameObject.GetComponent<Animator>().speed = 2f;
            npc.waitTimeBase = 2f;
        }
    }

    public void SpeedDown()
    {
        timer += Time.deltaTime;
        rotation -= Time.deltaTime;
        foreach (var npc in npcSpawner.spawnedNPCs)
        {
            npc.thisNPC.speed = npc.thisNPC.storedSpeed;
            npc.gameObject.GetComponent<Animator>().speed = 1f;
            npc.ResetWaitTime();
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

