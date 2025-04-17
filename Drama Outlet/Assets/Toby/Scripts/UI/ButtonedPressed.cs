using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class ButtonedPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RSIButtons buttons;
    public bool restock;
    public bool sell;
    public float timer;
    public bool heldDown;

    public void Update()
    {
        if (heldDown == true)
        {
            timer += Time.deltaTime;
        }
        if (restock == true && timer >= 5 && buttons.managerSelected == false)
        {
            RestockAll();
        }
        if (sell == true && timer >= 5 && buttons.managerSelected == false)
        {
            SellAll();
        }
    }
    public void RestockAll()
    {
        buttons.RestockAll();
    }

    public void SellAll()
    {
        buttons.SellAll();
    }
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        heldDown = true;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        heldDown = false;
        timer = 0;
    }
}
