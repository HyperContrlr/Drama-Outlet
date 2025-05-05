using UnityEngine;

public class posters : MonoBehaviour
{
    public bool poster1 = false;
    public bool poster2 = false;
    public bool poster3 = false;

    public GameObject Poster1;
    public GameObject Poster2;
    public GameObject Poster3;

    void Update()
    {
        if (poster1)
        {
            Poster1.SetActive(true);
        }
        else if (poster2)
        {
            Poster2.SetActive(false);
        }
        else if (poster3)
        {
            Poster3.SetActive(true);
        }
    }
        
}
