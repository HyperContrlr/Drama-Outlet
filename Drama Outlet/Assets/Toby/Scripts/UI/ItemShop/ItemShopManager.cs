using System.Collections.Generic;
using UnityEngine;

public class ItemShopManager : MonoBehaviour
{
    [SerializeField] private GameObject nextImage;

    [SerializeField] private GameObject previousImage;

    [SerializeField] private List<GameObject> imagesToDelete;

    [SerializeField] private GameObject thisObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next()
    {
        foreach (GameObject item in imagesToDelete)
        {
            item.SetActive(false);
        }
        nextImage.SetActive(true);
        previousImage.SetActive(false);
        thisObject.SetActive(false);
    }

    public void Previous()
    {
        foreach (GameObject item in imagesToDelete)
        {
            item.SetActive(false);
        }
        previousImage.SetActive(true);
        nextImage.SetActive(false);
        thisObject.SetActive(false);
    }
}
