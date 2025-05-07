using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TheifCatch : MonoBehaviour
{
    public NPCAI thisThief;
    public AudioSource audioSource;
    public Color lightColor;
    public Color alertColor;
    public Light2D light2D;
    public RSIButtons buttons;
    public void Start()
    {
        buttons = FindFirstObjectByType<RSIButtons>();  
        light2D = GameObject.Find("Light 2D").GetComponent<Light2D>();
        lightColor = light2D.color;
        light2D.color = alertColor;
    }

    public void OnMouseDown()
    {
        thisThief.state = NPCAI.States.Leaving;
        thisThief.target = thisThief.leave;
        thisThief.stole = false;
        SaveDataController.Instance.CurrentData.money += buttons.restockPrices;
        buttons.RestockAll();
        light2D.color = lightColor;
    }
}
