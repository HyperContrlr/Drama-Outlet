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
            int result = Statics.RollADice(4);
            if (Statics.approvalValue >= 40)
            {
                if (result >= 12)
                {
                    SpawnCustomer();
                }
            }
            else if (Statics.approvalValue >= 80)
            {
                if (result >= 8)
                {
                    SpawnCustomer();
                }
            }
            else if (Statics.approvalValue >= 100)
            {
                if (result >= 5)
                {
                    SpawnCustomer();
                }
            }
            else if (Statics.approvalValue < 40)
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
        if (Statics.approvalValue < 80)
        {
            amountToSpawn = Statics.RollADice(0);
        }
        else if (Statics.approvalValue >= 80)
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
