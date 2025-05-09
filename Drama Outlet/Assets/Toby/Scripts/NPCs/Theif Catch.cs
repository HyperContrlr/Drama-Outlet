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
    }

    public void OnMouseDown()
    {
        thisThief.state = NPCAI.States.Leaving;
        thisThief.target = thisThief.leave;
        thisThief.stole = false;
        SaveDataController.Instance.CurrentData.money += buttons.restockPrices;
        buttons.RestockAll();
    }
}
