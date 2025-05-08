using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 20f;//gee golly I wonder what this could do :p
    public RectTransform creditsPanel;
    public GameObject creditsPanelObject;
    public Vector2 initialPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets the component.. idk why I started doing comments for this script.
        creditsPanel = GetComponent<RectTransform>();
    }
    public void StartDaScrolling()
    {
        //does the entire thing that this script was created to do.
        creditsPanelObject.SetActive(true);
        creditsPanel.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
    public void StopDaScrolling()
    {
        //stops the scrolling
        creditsPanel.anchoredPosition = initialPos;
        creditsPanelObject.SetActive(false);
    }
}
