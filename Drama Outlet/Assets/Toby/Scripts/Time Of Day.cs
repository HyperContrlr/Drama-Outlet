using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    [SerializeField] public float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
