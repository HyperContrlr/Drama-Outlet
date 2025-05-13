using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 20f;//gee golly I wonder what this could do :p
    public RectTransform creditsPanel;
    public GameObject creditsPanelObject;
    public Vector2 initialPos;
    public bool isScrolling;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets the component.. idk why I started doing comments for this script.
        creditsPanel = GetComponent<RectTransform>();
    }

    public void Update()
    {
       if (isScrolling == true)
       {
           creditsPanel.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
       }
       if (isScrolling == false)
       {
           creditsPanel.anchoredPosition = initialPos;
       }
    }
    public void StartDaScrolling()
    {
        isScrolling = true;
        //does the entire thing that this script was created to do.
        creditsPanelObject.SetActive(true);
    }
    public void StopDaScrolling()
    {
        isScrolling = false;
        //stops the scrolling
        creditsPanel.anchoredPosition = initialPos;
        creditsPanelObject.SetActive(false);
    }
}
