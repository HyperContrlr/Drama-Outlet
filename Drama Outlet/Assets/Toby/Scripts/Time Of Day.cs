using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    [SerializeField] public float timer;
    [SerializeField] private bool startedDay;
    [SerializeField] private bool closeUpShop;
    [SerializeField] private float morningWindow;
    [SerializeField] private float afternoonWindow;
    [SerializeField] private float eveningWindow;
    [SerializeField] private NPCSpawner npcSpawner;
    void Start()
    {
        Statics.timeOfDay = Statics.Time.EarlyMorning;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && Statics.timeOfDay == Statics.Time.EarlyMorning)
        {
            startedDay = true;
            npcSpawner.SpawnCustomer();
        }
      
        if (Input.GetKeyDown(KeyCode.Space) == true && Statics.timeOfDay == Statics.Time.Night)
        {
            closeUpShop = true;
        }
       
        if (startedDay == true)
        {
            timer += Time.deltaTime;
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
            Statics.timeOfDay = Statics.Time.Night;
            startedDay = false;
        }
    }
}
