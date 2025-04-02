using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Pathfinding;
using System.Linq;

public partial class NPCAI : MonoBehaviour
{
    [SerializeField] private List<GameObject> productSpots;

    [SerializeField] private GameObject target;
    
    [SerializeField] private GameObject checkOut;

    public enum States {Moving, Looking, Buying, Leaving, Stopped}

    public States state = States.Moving;
    
    public NPC thisNPC;

    public bool isSpecial;

    private Pathfinding.Path customerPath;

    private float nextWaypointDistance = 3f;

    public int currentWaypoint = 0;

    private bool reachedEndOfPath = false;

    [SerializeField] private Rigidbody rb;

    [SerializeField] Seeker seeker;

    [SerializeField] public float waitTimeBase;

    [SerializeField] private float waitTime;

    public void RandomizeNPCValues()
    {
        thisNPC.speed = Statics.randyTheRandom.Next((int)thisNPC.speedMax, (int)thisNPC.speedMin);
        thisNPC.amountToBuy = Statics.randyTheRandom.Next((int)thisNPC.maxAmountToBuy, (int)thisNPC.minAmountToBuy);
        if (isSpecial == false)
        {
            thisNPC.personality = (NPCAI.NPC.Personality)Random.Range(0, 3);
        }
        int x = (int)Random.Range(1, 3);
        for (int y = 0; y < x; y++)
        {
            int i = (int)Random.Range(0, 4);
            thisNPC.preference |= (NPC.Preference)Mathf.Pow(i, 2);
        }
        if (thisNPC.personality == NPC.Personality.Window_Shopper)
        {
            waitTimeBase = 10;
            if (Statics.approvalValue >= 20)
            {
                int chance = Statics.RollADice(1);
                if (chance == 6)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }

            if (Statics.approvalValue >= 40)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 5)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }

            if (Statics.approvalValue >= 60)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 4)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }

            if (Statics.approvalValue >= 80)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 3)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }

            if (Statics.approvalValue >= 100)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 2)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }
        }
        
        if (thisNPC.personality == NPC.Personality.Average_Shopper)
        {
            waitTimeBase = 15;
            if (Statics.approvalValue >= 50)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 5)
                {
                    thisNPC.personality = NPC.Personality.Big_Spender;
                }
            }

            if (Statics.approvalValue > 0)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 4)
                {
                    thisNPC.personality = NPC.Personality.Window_Shopper;
                }
            }

            if (Statics.approvalValue <= -20)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 5)
                {
                    thisNPC.personality = NPC.Personality.Window_Shopper;
                }
            }    
        }

        if (thisNPC.personality == NPC.Personality.Big_Spender)
        {
            waitTime = 5;
            if (Statics.approvalValue > 0)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 4)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }

            if (Statics.approvalValue <= -20)
            {
                int chance = Statics.RollADice(1);
                if (chance >= 5)
                {
                    thisNPC.personality = NPC.Personality.Average_Shopper;
                }
            }
        }
    }
    public void FindProductSpots()
    {
        List<ProductManager> products = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        foreach (var product in products) 
        {
            if (thisNPC.preference.HasFlag(product.thisProduct.type))
            {
                productSpots.Add(product.gameObject);
            }
            
        }
        if (productSpots.Count <= 0)
        {
            thisNPC.personality = NPC.Personality.Window_Shopper;
        }
        if (thisNPC.personality == NPC.Personality.Window_Shopper)
        {
            productSpots.Clear();
            foreach (var product in products)
            {
                productSpots.Add(product.gameObject);
            }
        }
    }
    void Start()
    {
        RandomizeNPCValues();
        FindProductSpots();
    }

    public void WindowShopper()
    {

    }
    public void AverageShopper()
    {

    }
    public void BigSpender()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (thisNPC.personality == NPC.Personality.Window_Shopper)
        {
            WindowShopper();
        }
        else if (thisNPC.personality == NPC.Personality.Average_Shopper)
        {
            AverageShopper();
        }

    }
}
