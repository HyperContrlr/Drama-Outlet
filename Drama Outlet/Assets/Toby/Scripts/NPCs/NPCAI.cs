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

    [SerializeField] private float targetDistance;

    [SerializeField] private float stopDistance;
    
    [SerializeField] private GameObject checkOut;

    [SerializeField] private GameObject leave;

    [SerializeField] private float slerp;
    public enum States {Moving, Looking, Buying, Leaving, Stopped}

    public States state = States.Moving;
    
    [SerializeField] public NPC thisNPC;

    public bool isSpecial;

    private Pathfinding.Path customerPath;

    private float nextWaypointDistance = 1f;

    public int currentWaypoint = 0;

    private bool reachedEndOfPath = false;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] Seeker seeker;

    [SerializeField] public float waitTimeBase;

    [SerializeField] private float waitTime;

    private bool noProduct;
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(customerPath.vectorPath[currentWaypoint + 1], 1);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }
    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            customerPath = p;
            currentWaypoint = 0;
        }
    }
    public void RandomizeNPCValues()
    {
        thisNPC.speed = Statics.randyTheRandom.Next((int)thisNPC.speedMin, (int)thisNPC.speedMax);
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
            waitTimeBase = 5;
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
            waitTimeBase = 8;
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
            waitTimeBase = 8;
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
        productSpots = productSpots.Shuffle().ToList();
    }
    public void Buy()
    {
        thisNPC.amountToBuy = Statics.randyTheRandom.Next((int)thisNPC.minAmountToBuy, (int)thisNPC.maxAmountToBuy);
        if (thisNPC.personality == NPC.Personality.Big_Spender)
        {
            thisNPC.amountToBuy = thisNPC.maxAmountToBuy;
        }
        target.GetComponent<ProductManager>().Buy(thisNPC.amountToBuy);
        for (float i = 0; i <= target.GetComponent<ProductManager>().thisProduct.stockBought; i++)
        {
            thisNPC.money += target.GetComponent<ProductManager>().thisProduct.sellPricePerStock;
        }
        if (target.GetComponent<ProductManager>().thisProduct.currentStock < thisNPC.amountToBuy)
        {
            //Play an angry graphic
            Statics.approvalValue -= 5;
            thisNPC.unhappy = true;
        }
        thisNPC.hasBoughtSomething = true;
    }
    public void SetTarget()
    {
        waitTime = waitTimeBase;
        if (productSpots.Count == 0 && thisNPC.hasBoughtSomething == true)
        {
            target = checkOut;
            waitTime = 5;
            state = States.Buying;
        }
        else if (productSpots.Count == 0 && thisNPC.hasBoughtSomething == false)
        {
            target = leave;
            state = States.Leaving;
        }
        else
        {
            state = States.Moving;
            target = productSpots[0];
            productSpots.Remove(target);
        }
    }
    void Start()
    {
        checkOut = GameObject.FindGameObjectWithTag("Check Out");
        leave = GameObject.FindGameObjectWithTag("Exit");
        RandomizeNPCValues();
        FindProductSpots();
        SetTarget();
        state = States.Moving;
    }

    public void Moving()
    {
        if (customerPath == null)
        {
            return;
        }

        if (currentWaypoint >= customerPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Debug.Log($"Direction: {customerPath.vectorPath[currentWaypoint + 1]}");
        Vector3 direction = (customerPath.vectorPath[currentWaypoint + 1] - transform.position).normalized;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * thisNPC.speed, slerp);
        float distance = Vector3.Distance(rb.position, customerPath.vectorPath[currentWaypoint + 1]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (targetDistance <= stopDistance)
        {
            waitTime = waitTimeBase;
            state = States.Looking;
        }
    }    
    public void Buying()
    {
        if (customerPath == null)
        {
            return;
        }

        if (currentWaypoint >= customerPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Debug.Log($"Direction: {customerPath.vectorPath[currentWaypoint + 1]}");
        Vector3 direction = (customerPath.vectorPath[currentWaypoint + 1] - transform.position).normalized;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * thisNPC.speed, slerp);
        float distance = Vector3.Distance(rb.position, customerPath.vectorPath[currentWaypoint + 1]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (targetDistance <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero;
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                Statics.money += thisNPC.money;
                target = leave;
                state = States.Leaving;
                //Maybe play a nice gaining money animation
            }
        }
    }
    public void Leaving()
    {
        if (customerPath == null)
        {
            return;
        }

        if (currentWaypoint >= customerPath.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Debug.Log($"Direction: {customerPath.vectorPath[currentWaypoint + 1]}");
        Vector3 direction = (customerPath.vectorPath[currentWaypoint + 1] - transform.position).normalized;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * thisNPC.speed, slerp);
        float distance = Vector3.Distance(rb.position, customerPath.vectorPath[currentWaypoint + 1]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (targetDistance <= stopDistance)
        {
            if (thisNPC.unhappy == false)
            {
                Statics.approvalValue += 5;
            }
            Destroy(this.gameObject);
        }
    }
    public void WindowShopper()
    {
        if (state == States.Moving)
        {
            Moving();
        }

        if (state == States.Looking)
        {
            rb.linearVelocity = Vector2.zero;
            //Play an idle animation
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                int chance = Statics.RollADice(4);
                if (chance >= 18)
                {
                    Buy();
                }
                target.GetComponent<ProductManager>().thisProduct.stockBought = 0;
                SetTarget();
            }
        }

        if (state == States.Buying)
        {
            Buying();
        }

        if (state == States.Leaving)     
        {
            Leaving();
        }
    }
    public void AverageShopper()
    {
        if (state == States.Moving)
        {
            Moving();
        }

        if (state == States.Looking)
        {
            rb.linearVelocity = Vector2.zero;
            //Play an idle animation
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                int chance = Statics.RollADice(4);
                if (chance >= 15)
                {
                    SetTarget();
                }
                else
                {
                    Buy();
                    target.GetComponent<ProductManager>().thisProduct.stockBought = 0;
                    SetTarget();
                }
            }
        }

        if (state == States.Buying)
        {
            Buying();
        }

        if (state == States.Leaving)
        {
            Leaving();
        }
    }
    public void BigSpender()
    {
        if (state == States.Moving)
        {
            Moving();
        }

        if (state == States.Looking)
        {
            rb.linearVelocity = Vector2.zero;
            //Play an idle animation
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                Buy();
                target.GetComponent<ProductManager>().thisProduct.stockBought = 0;
                SetTarget();
            }
        }

        if (state == States.Buying)
        {
            Buying();
        }

        if (state == States.Leaving)
        {
            Leaving();
        }
    }
    // Update is called once per frame
    void Update()
    {
        noProduct = IsThereProduct();
        if (noProduct == false)
        {
            target = leave;
            state = States.Leaving;
        }
        targetDistance = Vector2.Distance(this.transform.position, target.transform.position);
        UpdatePath();
        if (thisNPC.personality == NPC.Personality.Window_Shopper)
        {
            WindowShopper();
        }
        else if (thisNPC.personality == NPC.Personality.Average_Shopper)
        {
            AverageShopper();
        }
        else if (thisNPC.personality == NPC.Personality.Big_Spender)
        {
            BigSpender();   
        }
        if (rb.linearVelocity.x > 0 || rb.linearVelocity.x < 0)
        {
            thisNPC.animator.SetBool("IsMoving", true);
        }
        else if (rb.linearVelocity == Vector2.zero)
        {
            thisNPC.animator.SetBool("IsMoving", false);
        }
        if (rb.linearVelocity.x > 0)
        {
            thisNPC.spriteRenderer.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            thisNPC.spriteRenderer.flipX = true;
        }
    }
    
    [ContextMenu("Shuffle")]
    public void Shuffle()
    {
        productSpots = productSpots.Shuffle().ToList();
    }

    public bool IsThereProduct()
    {
        List<ProductManager> products = FindObjectsByType<ProductManager>(FindObjectsSortMode.None).ToList();
        if (products.Count == 0)
        {
            return false;
        }
        if (products.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
