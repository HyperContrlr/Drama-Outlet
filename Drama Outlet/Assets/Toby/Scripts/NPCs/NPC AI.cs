using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class NPCAI : MonoBehaviour
{
    [SerializeField] private List<GameObject> productSpots;
    [SerializeField] private GameObject checkOut;
    public NPC thisNPC;
    
    public void RandomizeNPCValues(NPC npc)
    {
        npc.speed = Statics.randyTheRandom.Next((int)npc.speedMax, (int)npc.speedMin);
        npc.amountToBuy = Statics.randyTheRandom.Next((int)npc.maxAmountToBuy, (int)npc.minAmountToBuy);
        npc.personality = (NPCAI.NPC.Personality)Random.Range(0, 2);
        int i = (int)Random.Range(0, 7);
        npc.preference = (NPCAI.NPC.Preference)Mathf.Pow(i, 2);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
