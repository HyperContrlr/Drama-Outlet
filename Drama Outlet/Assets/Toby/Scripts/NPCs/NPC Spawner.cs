using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject regularCustomer;
    [SerializeField] private GameObject spawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Statics.timeOfDay == Statics.Time.Evening)
        {
            
        }
    }
}
