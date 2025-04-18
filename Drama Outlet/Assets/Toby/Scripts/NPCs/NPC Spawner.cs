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
    [SerializeField] private float spawnStop;
    void Start()
    {
        NPCsToSpawn = NPCsToSpawn.Shuffle().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Instantiate(selectedNPC, spawner.transform);
        }
    }
}
