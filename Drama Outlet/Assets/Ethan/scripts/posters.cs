using UnityEngine;

public class posters : MonoBehaviour
{
    public bool poster1 = false;
    public bool poster2 = false;
    public bool poster3 = false;
    public bool securityCamera = false;

    public GameObject Poster1;
    public GameObject Poster2;
    public GameObject Poster3;
    public GameObject SecurityCamera;

    void Update()
    {
        if (poster1)
        {
            Poster1.SetActive(true);
        }
        if (poster2)
        {
            Poster2.SetActive(true);
        }
        if (poster3)
        {
            Poster3.SetActive(true);
        }

        if (securityCamera)
        {
            SecurityCamera.SetActive(true);
        }
    }
        
    public void PosterOne()
    {
        poster1 = true;
    }
    public void PosterTwo()
    {
        poster2 = true;
    }
    public void PosterThree()
    {
        poster3 = true;
    }
    public void SecurityCam()
    {
        securityCamera = true;
    }
}
