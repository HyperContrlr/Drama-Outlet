using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> NPCsToSpawn;
    [SerializeField] private GameObject selectedNPC;
    [SerializeField] private GameObject spawner;
    [SerializeField] private int amountToSpawn;
    [SerializeField] private float spawnTimer;
    [SerializeField] public float spawnStop;
    [SerializeField] public List<NPCAI> spawnedNPCs;
    [SerializeField] public GameObject spawnedNPC;
    void Start()
    {
        NPCsToSpawn = NPCsToSpawn.Shuffle().ToList();
        spawnTimer = spawnStop;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            if (SaveDataController.Instance.CurrentData.timeOfDay == SaveData.Time.Afternoon)
            {
                spawnStop = 8f;
            }
            else if (SaveDataController.Instance.CurrentData.timeOfDay == SaveData.Time.Evening)
            {
                spawnStop = 10f;
            }
            else
            {
                spawnStop = 15f;
            }
                int result = Statics.RollADice(4);
            if (SaveDataController.Instance.CurrentData.approvalValue >= 40)
            {
                if (result >= 12)
                {
                    SpawnCustomer();
                }
            }
            else if (SaveDataController.Instance.CurrentData.approvalValue >= 80)
            {
                if (result >= 8)
                {
                    SpawnCustomer();
                }
            }
            else if (SaveDataController.Instance.CurrentData.approvalValue >= 100)
            {
                if (result >= 5)
                {
                    SpawnCustomer();
                }
            }
            else if (SaveDataController.Instance.CurrentData.approvalValue < 40)
            {
                if (result >= 15)
                {
                    SpawnCustomer();
                }
            }
            spawnTimer = spawnStop;
        }
    }
    public void SpawnCustomer()
    {
        if (SaveDataController.Instance.CurrentData.approvalValue < 80)
        {
            amountToSpawn = Statics.RollADice(0);
        }
        else if (SaveDataController.Instance.CurrentData.approvalValue >= 80)
        {
            amountToSpawn = Statics.RollADice(1);
        }
        for (int i = 0; i < amountToSpawn; i++) 
        {
            NPCsToSpawn = NPCsToSpawn.Shuffle().ToList();
            selectedNPC = NPCsToSpawn[0];
            spawnedNPC = Instantiate(selectedNPC, spawner.transform);
            spawnedNPCs.Add(spawnedNPC.GetComponent<NPCAI>());
        }
    }

}
